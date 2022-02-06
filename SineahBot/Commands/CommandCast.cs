using SineahBot.Data;
using SineahBot.Extensions;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandCast : Command
    {
        public CommandCast()
        {
            commandRegex = new Regex(@"^cast (.+?)( on (.+))?$", RegexOptions.IgnoreCase);
            isNormalCommand = true;
            isCombatCommand = true;
            isTradeCommand = false;
            isSearchCommand = false;
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
                character.Message("You action cooldown is still active, please wait and retry.");
                return;
            }

            bool direct = character is NPC;
            var spellName = GetArgument(1);
            var hasTarget = HasArgument(3);
            var targetName = GetArgument(3);

            if (String.IsNullOrWhiteSpace(spellName))
            {
                character.Message("What are you trying to cast?");
                return;
            }
            var spell = character.GetSpell(spellName);
            if (spell == null)
            {
                character.Message($@"Can't cast unknown spell ""{spellName}"". Type `!spells` to get a list of spells you can cast.");
                return;
            }
            if (!spell.needsTarget && hasTarget)
            {
                character.Message($"This spell doesn't need a target! Type `cast {spellName}` instead.");
                return;
            }
            if (spell.needsTarget && !hasTarget && !spell.canSelfCast)
            {
                character.Message("You need a target to cast that spell.");
                return;
            }
            Entity target = null;
            if (spell.canSelfCast && (!spell.needsTarget || targetName.Is(null, "", "self", "me", "myself", character.GetName())))
            {
                target = character;
            }
            else
            {
                target = room.FindInRoom(targetName);
                if (target == null && spell.canSelfCast)
                {
                    target = character;
                }
                if (target == null && !spell.canSelfCast)
                {
                    character.Message($@"Can't find any ""{targetName}"" to cast on here !");
                    return;
                }
            }

            if (!character.CanCastSpell(spell))
            {
                character.Message($@"Not enough mana to cast *{spell.GetName()}*.");
                return;
            }

            CastOn(character, room, spell, target);

            character.RewardExperience(1);
        }

        public static void CastOn(Character character, Room room, Spell spell, Entity target)
        {
            if(character == target as Character)
            {
                character.Message($"You cast {spell.GetName()} on yourself!");
                room.DescribeAction($"{character.GetName()} cast {spell.GetName()} on {character.themselves}!", character);
            } else
            {
                character.Message($"You cast {spell.GetName()} on {target.name}!");
                room.DescribeAction($"{character.GetName()} cast {spell.GetName()} on {target.name}!", character);
            }

            character.CastSpellOn(spell, target);
            character.StartActionCooldown();

            room.RaiseRoomEvent(new RoomEvent(room, RoomEventType.CharacterCasts) { source = character, spell = spell, target = target as Character }, character);

            if (target is IKillable)
            {
                if ((target as IKillable).IsDead())
                {
                    (target as IKillable).OnKilled(character);
                    if (character == target as Character)
                    {
                        character.Message($"You killed yourself!");
                        room.DescribeAction($"{character.GetName()} killed {character.themselves}!", character, target as IAgent);
                    }
                    else
                    {
                        character.Message($"You killed {target.GetName()}!");
                        room.DescribeAction($"{character.GetName()} killed {target.GetName()}!", character, target as IAgent);
                    }

                    if (target is Character)
                        room.RaiseRoomEvent(new RoomEvent(room, RoomEventType.CharacterKills) { source = character, target = target as Character }, character);
                }
            }
        }
    }
}
