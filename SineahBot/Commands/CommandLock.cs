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

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }
            bool direct = character is NPC;
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
                    character.Message($@"Can't lock unknown direction ""{directionName}""");
                    throw new Exception($@"Can't lock unknown direction ""{directionName}""");
            }
            if (!room.IsValidDirection(direction))
            {
                character.Message($@"This room doesn't have a ""{direction}"" access.");
                return;
            }
            var connection = room.GetRoomConnectionInDirection(direction);
            if (connection.locked)
            {
                character.Message("This access is already locked.");
            }
            else if (connection.keyItemName != null)
            {
                var agentInventory = character as IInventory;
                if (agentInventory.IsItemInInventory(connection.keyItemName))
                {
                    connection.Lock();
                    // describe in current room
                    if (direct)
                        room.DescribeActionNow($"{character.GetName()} has locked the access ({direction})", character);
                    else
                        room.DescribeAction($"{character.GetName()} has locked the access ({direction})", character);
                    // describe in adjacent room
                    if (direct)
                        connection.toRoom.DescribeActionNow($"Someone locked the access from the other side.");
                    else
                        connection.toRoom.DescribeAction($"Someone locked the access from the other side.");
                    character.Message($"You locked the access ({direction})");
                    character.experience += 1;
                }
                else
                {
                    character.Message("You ned a key for that.");
                }
            }
            else
            {
                connection.Lock();
                // describe in current room
                if (direct)
                    room.DescribeActionNow($"{character.GetName()} has locked the access ({direction})", character);
                else
                    room.DescribeAction($"{character.GetName()} has locked the access ({direction})", character);
                // describe in adjacent room
                if (direct)
                    connection.toRoom.DescribeActionNow($"Someone locked the access from the other side.");
                else
                    connection.toRoom.DescribeAction($"Someone locked the access from the other side.");
                character.Message($"You locked the access ({direction})");
                character.experience += 1;
            }
        }

        public override bool IsCombatCommand(Character character = null)
        {
            return false;
        }
        public override bool IsWorkbenchCommand(Character character = null)
        {
            return false;
        }

    }
}
