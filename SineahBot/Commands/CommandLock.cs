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
            isNormalCommand = true;
            isCombatCommand = false;
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

            Lock(character, room, direction);

            character.RewardExperience(1);
        }

        public static bool Lock(Character character, Room room, MoveDirection direction)
        {
            if (!room.IsValidDirection(direction))
            {
                character.Message($@"This room doesn't have a ""{direction}"" access.");
                return false;
            }
            var connection = room.GetRoomConnectionInDirection(direction);

            if (connection.locked)
            {
                character.Message("This access is already locked.");
                return false;
            }

            if (connection.keyItemName != null && !character.IsItemInInventory(connection.keyItemName))
            {
                character.Message("You need a key for that.");
                return false;
            }

            connection.Lock();
            // describe in current room
            room.DescribeAction($"{character.GetName()} has locked the access ({direction})", character);
            // describe in adjacent room
            connection.toRoom.DescribeAction($"Someone locked the access from the other side.");
            character.Message($"You locked the access ({direction})");

            return true;
        }
    }
}
