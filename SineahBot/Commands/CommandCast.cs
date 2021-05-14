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

                CommandCastOn.CastOn(character, room, spell, target);
            }
            else
            {
                if (!character.CanCastSpell(spell))
                {
                    character.Message($@"Not enough mana to cast {spell.GetName()}.");
                    return;
                }

                Cast(character, room, spell);
            }

            character.RewardExperience(1);
        }

        public static void Cast(Character character, Room room, Spell spell)
        {
            character.Message($"You cast {spell.GetName()}!");
            room.DescribeAction($"{character.GetName()} cast {spell.GetName()}!", character);

            character.CastSpell(spell);
            character.StartActionCooldown();

            room.RaiseRoomEvent(new RoomEvent(room, RoomEventType.CharacterCasts) { source = character, spell = spell, target = character }, character);

            if (character.IsDead()) // true if target died
            {
                character.OnKilled(character);
                character.Message($"You killed yourself!");
                room.DescribeAction($"{character.GetName()} killed {character.themselves}!", character);
                room.RaiseRoomEvent(new RoomEvent(room, RoomEventType.CharacterKills) { source = character, target = character }, character);
            }
        }
    }
}
