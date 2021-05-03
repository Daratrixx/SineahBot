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
            commandRegex = new Regex(@"^(atk|attack|hit|strike|fight|assault) (.+)$", RegexOptions.IgnoreCase);
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
            var targetName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                character.Message("What are you trying to attack ?");
                return;
            }

            var target = room.FindInRoom(targetName) as IAttackable;

            if (target == null)

            {
                character.Message($@"Can't find any ""{targetName}"" to attack here !");
                return;
            }

            Attack(character, room, target);

            character.RewardExperience(1);
        }

        public static void Attack(Character character, Room room, IAttackable target)
        {
            room.RaiseRoomEvent(new RoomEvent(room, RoomEventType.CharacterAttacks) { attackingCharacter = character, attackTarget = target }, character);

            if (target is IDamageable)
            {
                var damageableTarget = target as IDamageable;
                var damage = character.GetWeaponDamage() + new Random().Next(5, 10);
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
                        room.RaiseRoomEvent(new RoomEvent(room, RoomEventType.CharacterKills) { killingCharacter = character, killedTarget = killableTarget }, character);
                    }
                }
            }
            else
            {
                character.Message($"You attacked {target.GetName()}.");
                room.DescribeAction($"{character.GetName()} attacked {target.GetName()}.", character, target as IAgent);
            }
        }
    }
}
