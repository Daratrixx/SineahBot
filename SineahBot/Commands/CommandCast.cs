using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandCast : Command
    {

        public CommandCast()
        {
            commandRegex = new Regex(@"^cast (.+)$", RegexOptions.IgnoreCase);
        }

        public override bool IsWorkbenchCommand(Character character = null)
        {
            return false;
        }

        public override bool IsTradeCommand(Character character = null)
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

            if (spell.needsTarget)
            {
                Entity target = null;
                if (spell.canSelfCast)
                {
                    target = character as Entity;
                }
                if (target == null && !spell.canSelfCast)
                {
                    character.Message($@"This spell needs a target! Type **cast {spellName} on [target name]** instead.");
                    return;
                }

                if (!character.CanCastSpell(spell))
                {
                    character.Message($@"Not enough mana to cast {spell.GetName()}.");
                    return;
                }

                character.Message($"You casted {spell.GetName()} on {target.name}!");
                if (direct)
                    room.DescribeActionNow($"{character.GetName()} casted {spell.GetName()} on {target.name}!", character);
                else
                    room.DescribeAction($"{character.GetName()} casted {spell.GetName()} on {target.name}!", character);

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
            }
            else
            {
                if (!character.CanCastSpell(spell))
                {
                    character.Message($@"Not enough mana to cast {spell.GetName()}.");
                    return;
                }

                character.Message($"You casted {spell.GetName()}!");
                if (direct)
                    room.DescribeActionNow($"{character.GetName()} casted {spell.GetName()}!", character);
                else
                    room.DescribeAction($"{character.GetName()} casted {spell.GetName()}!", character);

                character.CastSpell(spell);
                character.StartActionCooldown();

                if (character.IsDead()) // true if target died
                {
                    character.OnKilled(character);
                    character.Message($"You killed yourself!");
                    if (direct)
                        room.DescribeActionNow($"{character.GetName()} killed themselves!", character);
                    else
                        room.DescribeAction($"{character.GetName()} killed themselves!", character);
                }
            }

            character.RewardExperience(1);
        }
    }
}
