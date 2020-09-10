using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandUnlock : Command
    {
        public CommandUnlock()
        {
            commandRegex = new Regex(@"^(unlock) (north|n|east|e|south|s|west|w|in|out)$", RegexOptions.IgnoreCase);
        }

        public override void Run(IAgent agent, Room room)
        {
            string directionName = GetArgument(2);
            MoveDirection direction;
            switch (directionName)
            {
                case "n":
                case "north":
                    direction = MoveDirection.North;
                    break;
                case "e":
                case "east":
                    direction = MoveDirection.East;
                    break;
                case "s":
                case "south":
                    direction = MoveDirection.South;
                    break;
                case "w":
                case "west":
                    direction = MoveDirection.West;
                    break;
                case "in":
                    direction = MoveDirection.In;
                    break;
                case "out":
                    direction = MoveDirection.Out;
                    break;
                case "up":
                    direction = MoveDirection.Up;
                    break;
                case "down":
                    direction = MoveDirection.Down;
                    break;
                default:
                    agent.Message($@"Can't unlock unknown direction ""{directionName}""");
                    throw new Exception($@"Can't unlock unknown direction ""{directionName}""");
            }
            if (!room.IsValidDirection(direction))
            {
                agent.Message($@"This room doesn't have a ""{direction}"" access.");
                return;
            }
            var connection = room.GetRoomConnectionInDirection(direction);
            if (!connection.locked)
            {
                agent.Message("This access is already unlocked.");
            }
            else if (connection.keyItemName != null && agent is IInventory)
            {
                var agentInventory = agent as IInventory;
                if (agentInventory.IsItemInInventory(connection.keyItemName))
                {
                    connection.Unlock();
                    room.DescribeAction($"{agent.name} has unlocked the access ({direction})", agent);
                    connection.toRoom.DescribeAction($"Someone unlocked the access from the other side.");
                    agent.Message($"You unlocked the access ({direction})");
                }
                else
                {
                    agent.Message("You ned a key for that.");
                }
            }
            else
            {
                connection.Unlock();
                room.DescribeAction($"{agent.name} has unlocked the access ({direction})", agent);
                connection.toRoom.DescribeAction($"Someone unlocked the access from the other side.");
                agent.Message($"You unlocked the access ({direction})");
                if (agent is Character) (agent as Character).experience += 1;
            }
        }

        public override bool IsCombatCommand(IAgent agent = null)
        {
            return false;
        }
        public override bool IsWorkbenchCommand(IAgent agent = null)
        {
            return false;
        }

    }
}
