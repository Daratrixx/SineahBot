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

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }
            bool direct = character is NPC;
            var caster = character as ICaster;
            if (!caster.ActionCooldownOver())
            {
                return;
            }

            var spellName = GetArgument(1);

            if (String.IsNullOrWhiteSpace(spellName))
            {
                character.Message("What are you trying to cast ?");
                return;
            }
            var spell = caster.GetSpell(spellName);
            if (spell == null)
            {
                character.Message($@"Can't cast unknown spell ""{spellName}"". Type **!spells** to get a list of spells you can cast.");
                return;
            }
            if (spell.NeedsTarget)
            {
                var targetName = GetArgument(2);
                if (String.IsNullOrWhiteSpace(targetName) && !spell.CanSelfCast)
                {
                    character.Message("What are you trying to cast on ?");
                    return;
                }

                Entity target = null;
                if (targetName == "self" && spell.CanSelfCast)
                {
                    target = caster as Entity;

                }
                else
                {
                    target = room.FindInRoom(targetName);
                    if (target == null && spell.CanSelfCast)
                    {
                        target = caster as Entity;
                    }
                    if (target == null && !spell.CanSelfCast)
                    {
                        character.Message($@"Can't find any ""{targetName}"" to cast on here !");
                        return;
                    }
                }

                character.Message($"You casted {spell.GetName()} on {target.name}!");
                if (direct)
                    room.DescribeActionNow($"{caster.GetName()} casted {spell.GetName()} on {target.name}!", character);
                else
                    room.DescribeAction($"{caster.GetName()} casted {spell.GetName()} on {target.name}!", character);
                if (caster.CastSpellOn(spell, target)) // true if target died
                {
                    if (target is IKillable)
                    {
                        (target as IKillable).OnKilled(character);
                        character.Message($"You killed {target.GetName()}!");
                        if (direct)
                            room.DescribeActionNow($"{caster.GetName()} killed {target.GetName()}!", character, target as IAgent);
                        else
                            room.DescribeAction($"{caster.GetName()} killed {target.GetName()}!", character, target as IAgent);
                    }
                }
                caster.StartActionCooldown();
                character.experience += 1;
            }
            else
            {
                character.Message($"This spell doesn't need a target! Type **cast {spellName}** instead.");
                return;
            }

        }

        public override bool IsWorkbenchCommand(Character character = null)
        {
            return false;
        }

    }
}
