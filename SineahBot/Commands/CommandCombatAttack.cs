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

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }
            bool direct = character is NPC;
            var attacker = character as IAttacker;
            if (!attacker.ActionCooldownOver())
            {
                return;
            }

            var targetName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                character.Message("What are you trying to attack ?");
            }
            else
            {
                var target = room.FindInRoom(targetName);
                if (target != null && target is IAttackable)
                {
                    var attackableTarget = target as IAttackable;
                    if (attackableTarget is IDamageable)
                    {
                        var damageableTarget = target as IDamageable;
                        var damage = attacker.GetWeaponDamage() + new Random().Next(5, 10);
                        character.Message($"You attacked {attackableTarget.GetName()} for {damage} damages.");
                        if (direct)
                            room.DescribeActionNow($"{attacker.GetName()} attacked {attackableTarget.GetName()}.", character, attackableTarget as IAgent);
                        else
                            room.DescribeAction($"{attacker.GetName()} attacked {attackableTarget.GetName()}.", character, attackableTarget as IAgent);
                        if (damageableTarget.OnDamage(damage, attacker))
                        {
                            if (damageableTarget is IDestructible)
                            {
                                character.Message($"You destroyed {attackableTarget.GetName()}!");
                                if (direct)
                                    room.DescribeActionNow($"{attacker.GetName()} destroyed {attackableTarget.GetName()}!", character, attackableTarget as IAgent);
                                else
                                    room.DescribeAction($"{attacker.GetName()} destroyed {attackableTarget.GetName()}!", character, attackableTarget as IAgent);
                                (damageableTarget as IDestructible).OnDestroyed();
                            }
                            if (damageableTarget is IKillable)
                            {
                                character.Message($"You killed {attackableTarget.GetName()}!");
                                if (direct)
                                    room.DescribeActionNow($"{attacker.GetName()} killed {attackableTarget.GetName()}!", character, attackableTarget as IAgent);
                                else
                                    room.DescribeAction($"{attacker.GetName()} killed {attackableTarget.GetName()}!", character, attackableTarget as IAgent);
                                (damageableTarget as IKillable).OnKilled(character);
                            }
                        }
                        attacker.StartActionCooldown();
                    }
                    else
                    {
                        character.Message($"You attacked {attackableTarget.GetName()}.");
                        if (direct)
                            room.DescribeActionNow($"{attacker.GetName()} attacked {attackableTarget.GetName()}.", character, attackableTarget as IAgent);
                        else
                            room.DescribeAction($"{attacker.GetName()} attacked {attackableTarget.GetName()}.", character, attackableTarget as IAgent);
                    }

                    character.experience += 1;
                }
                else
                {
                    character.Message($@"Can't find any ""{targetName}"" to attack here !");
                }
            }
        }

        public override bool IsWorkbenchCommand(Character character = null)
        {
            return false;
        }

    }
}
