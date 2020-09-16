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
                Entity target = null;
                if (spell.CanSelfCast)
                {
                    target = caster as Entity;
                }
                if (target == null && !spell.CanSelfCast)
                {
                    character.Message($@"This spell needs a target! Type **cast {spellName} on [target name]** instead.");
                    return;
                }

                if(!character.CanCastSpell(spell))
                {
                    character.Message($@"Not enough mana to cast {spell.GetName()}.");
                    return;
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
                    room.DescribeActionNow($"{caster.GetName()} casted {spell.GetName()}!", character);
                else
                    room.DescribeAction($"{caster.GetName()} casted {spell.GetName()}!", character);
                if (caster.CastSpell(spell)) // true if target died
                {
                    if (caster is IKillable)
                    {
                        (caster as IKillable).OnKilled();
                        character.Message($"You killed yourself!");
                        if (direct)
                            room.DescribeActionNow($"{caster.GetName()} killed themselves!", character);
                        else
                            room.DescribeAction($"{caster.GetName()} killed themselves!", character);
                    }
                }
                caster.StartActionCooldown();
            }

            character.RewardExperience(1);
        }
    }
}
