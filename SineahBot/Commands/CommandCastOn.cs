using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandCastOn : Command
    {

        public CommandCastOn()
        {
            commandRegex = new Regex(@"^cast (.+) on (.+)$", RegexOptions.IgnoreCase);
        }

        public override bool IsWorkbenchCommand(Character character = null)
        {
            return false;
        }

        public override bool IsTradeCommand(Character character = null)
        {
            return false;
        }

        public override bool IsSearchCommand(Character character = null)
        {
            return false;
        }

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }

            if (!character.ActionCooldownOver())
            {
                return;
            }

            bool direct = character is NPC;
            var spellName = GetArgument(1);

            if (String.IsNullOrWhiteSpace(spellName))
            {
                character.Message("What are you trying to cast ?");
                return;
            }
            var spell = character.GetSpell(spellName);
            if (spell == null)
            {
                character.Message($@"Can't cast unknown spell ""{spellName}"". Type **!spells** to get a list of spells you can cast.");
                return;
            }
            if (!spell.needsTarget)
            {
                character.Message($"This spell doesn't need a target! Type **cast {spellName}** instead.");
                return;
            }
            var targetName = GetArgument(2);
            if (String.IsNullOrWhiteSpace(targetName) && !spell.canSelfCast)
            {
                character.Message("What are you trying to cast on ?");
                return;
            }

            Entity target = null;
            if (targetName == "self" && spell.canSelfCast)
            {
                target = character as Entity;
            }
            else
            {
                target = room.FindInRoom(targetName);
                if (target == null && spell.canSelfCast)
                {
                    target = character as Entity;
                }
                if (target == null && !spell.canSelfCast)
                {
                    character.Message($@"Can't find any ""{targetName}"" to cast on here !");
                    return;
                }
            }

            if (!character.CanCastSpell(spell))
            {
                character.Message($@"Not enough mana to cast {spell.GetName()}.");
                return;
            }

            character.Message($"You cast {spell.GetName()} on {target.name}!");
            if (direct)
                room.DescribeActionNow($"{character.GetName()} cast {spell.GetName()} on {target.name}!", character);
            else
                room.DescribeAction($"{character.GetName()} cast {spell.GetName()} on {target.name}!", character);

            character.CastSpellOn(spell, target);
            character.StartActionCooldown();

            if (target is IKillable)
            {
                if ((target as IKillable).IsDead())
                {
                    (target as IKillable).OnKilled(character);
                    character.Message($"You killed {target.GetName()}!");
                    if (direct)
                        room.DescribeActionNow($"{character.GetName()} killed {target.GetName()}!", character, target as IAgent);
                    else
                        room.DescribeAction($"{character.GetName()} killed {target.GetName()}!", character, target as IAgent);
                }
            }

            character.RewardExperience(1);
        }
    }
}
