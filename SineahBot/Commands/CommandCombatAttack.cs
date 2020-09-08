﻿using SineahBot.Data;
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

        public override void Run(IAgent agent, Room room)
        {
            if (!(agent is IAttacker)) throw new Exception($@"Impossible to attack as non-attacker agent");
            var targetName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                agent.Message("What are you trying to attack ?");
            }
            else
            {
                var attacker = agent as IAttacker;
                var target = room.FindInRoom(targetName);
                if (target != null && target is IAttackable)
                {
                    var attackableTarget = target as IAttackable;
                    agent.Message($"You attacked up {attackableTarget.GetName()}.");
                    if (agent is Entity)
                        room.DescribeAction($"{attacker.GetName()} picked up {attackableTarget.GetName()}.", agent);

                    if (agent is Character) (agent as Character).experience += 1;
                }
                else
                {
                    agent.Message($@"Can't find any ""{targetName}"" to attack here !");
                }
            }
        }

        public override bool IsWorkbenchCommand(IAgent agent = null)
        {
            return false;
        }

    }
}