using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandLock : Command
    {
        public CommandLock()
        {
            commandRegex = new Regex(@"^(lock) (north|n|east|e|south|s|west|w|in|out)$", RegexOptions.IgnoreCase);
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
                    agent.Message($@"Can't lock unknown direction ""{directionName}""");
                    throw new Exception($@"Can't lock unknown direction ""{directionName}""");
            }
            if (!room.IsValidDirection(direction))
            {
                agent.Message($@"This room doesn't have a ""{direction}"" access.");
                return;
            }
            var connection = room.GetRoomConnectionInDirection(direction);
            if (connection.locked)
            {
                agent.Message("This access is already locked.");
            }
            else if (connection.keyItemName != null && agent is IInventory)
            {
                var agentInventory = agent as IInventory;
                if (agentInventory.IsItemInInventory(connection.keyItemName))
                {
                    connection.Lock();
                    room.DescribeAction($"{agent.name} has locked the access ({direction})", agent);
                    connection.toRoom.DescribeAction($"Someone locked the access from the other side.");
                    agent.Message($"You locked the access ({direction})");
                }
                else
                {
                    agent.Message("You ned a key for that.");
                }
            }
            else
            {
                connection.Lock();
                room.DescribeAction($"{agent.name} has locked the access ({direction})", agent);
                connection.toRoom.DescribeAction($"Someone locked the access from the other side.");
                agent.Message($"You locked the access ({direction})");
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
