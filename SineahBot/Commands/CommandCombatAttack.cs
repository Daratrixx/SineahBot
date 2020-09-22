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

            if (target is IDamageable)
            {
                var damageableTarget = target as IDamageable;
                var damage = character.GetWeaponDamage() + new Random().Next(5, 10);
                character.Message($"You attacked {target.GetName()} for {damage} damages.");
                if (direct)
                    room.DescribeActionNow($"{character.GetName()} attacked {target.GetName()}.", character, target as IAgent);
                else
                    room.DescribeAction($"{character.GetName()} attacked {target.GetName()}.", character, target as IAgent);

                damageableTarget.DamageHealth(damage, character);
                character.StartActionCooldown();

                if (damageableTarget is IDestructible)
                {
                    if ((damageableTarget as IDestructible).IsDestroyed())
                    {
                        character.Message($"You destroyed {target.GetName()}!");
                        if (direct)
                            room.DescribeActionNow($"{character.GetName()} destroyed {target.GetName()}!", character, target as IAgent);
                        else
                            room.DescribeAction($"{character.GetName()} destroyed {target.GetName()}!", character, target as IAgent);
                        (damageableTarget as IDestructible).OnDestroyed();
                    }
                }

                if (damageableTarget is IKillable)
                {
                    if ((damageableTarget as IKillable).IsDead())
                    {
                        character.Message($"You killed {target.GetName()}!");
                        if (direct)
                            room.DescribeActionNow($"{character.GetName()} killed {target.GetName()}!", character, target as IAgent);
                        else
                            room.DescribeAction($"{character.GetName()} killed {target.GetName()}!", character, target as IAgent);
                        (damageableTarget as IKillable).OnKilled(character);
                    }
                }
            }
            else
            {
                character.Message($"You attacked {target.GetName()}.");
                if (direct)
                    room.DescribeActionNow($"{character.GetName()} attacked {target.GetName()}.", character, target as IAgent);
                else
                    room.DescribeAction($"{character.GetName()} attacked {target.GetName()}.", character, target as IAgent);
            }

            character.RewardExperience(1);
        }
    }
}
