using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandCombatAttack : Command
    {
        public CommandCombatAttack()
        {
            commandRegex = new Regex($@"^(atk|attack|hit|strike|fight|assault) {targetRegex_3}$", RegexOptions.IgnoreCase);
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
                return;
            }

            var target = GetTarget<Entity>(character, room, 2);
            if (target == null) return; // error message already given in GetTarget

            Attack(character, room, target);

            character.RewardExperience(1);
        }

        public static void Attack(Character character, Room room, Entity target)
        {
            room.RaiseRoomEvent(new RoomEvent(room, RoomEventType.CharacterAttacks) { source = character, target = target as Character }, character);

            if (target is IDamageable)
            {
                var damageableTarget = target as IDamageable;
                var damage = (character.GetWeaponDamage() * new Random().Next(50, 100)) / 100;
                character.Message($"You attacked {target.GetName()} for {damage} damages.");
                room.DescribeAction($"{character.GetName()} attacked {target.GetName()}.", character, target as IAgent);

                damageableTarget.DamageHealth(damage, DamageType.Physical, character);
                character.StartActionCooldown();

                if (damageableTarget is IDestructible)
                {
                    if ((damageableTarget as IDestructible).IsDestroyed())
                    {
                        character.Message($"You destroyed {target.GetName()}!");
                        room.DescribeAction($"{character.GetName()} destroyed {target.GetName()}!", character, target as IAgent);
                        (damageableTarget as IDestructible).OnDestroyed();
                    }
                }

                if (damageableTarget is IKillable)
                {
                    var killableTarget = damageableTarget as IKillable;
                    if (killableTarget.IsDead())
                    {
                        character.Message($"You killed {target.GetName()}!");
                        room.DescribeAction($"{character.GetName()} killed {target.GetName()}!", character, target as IAgent);
                        killableTarget.OnKilled(character);
                        room.RaiseRoomEvent(new RoomEvent(room, RoomEventType.CharacterKills) { source = character, target = killableTarget as Character }, character);
                    }
                }
                return;
            }

            character.Message($"You attacked {target.GetName()}.");
            room.DescribeAction($"{character.GetName()} attacked {target.GetName()}.", character, target as IAgent);
        }
    }
}
