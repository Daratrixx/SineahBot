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

        public override bool IsCombatCommand(Character character = null)
        {
            return false;
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
                    character.Message($@"Can't unlock unknown direction ""{directionName}""");
                    throw new Exception($@"Can't unlock unknown direction ""{directionName}""");
            }

            if (!room.IsValidDirection(direction))
            {
                character.Message($@"This room doesn't have a ""{direction}"" access.");
                return;
            }

            var connection = room.GetRoomConnectionInDirection(direction);

            if (!connection.locked)
            {
                character.Message("This access is already unlocked.");
                return;
            }

            if (connection.keyItemName != null)
            {
                if (!character.IsItemInInventory(connection.keyItemName))
                {
                    character.Message("You ned a key for that.");
                    return;
                }

                connection.Unlock();
                if (direct)
                    room.DescribeActionNow($"{character.GetName()} has unlocked the access ({direction})", character);
                else
                    room.DescribeAction($"{character.GetName()} has unlocked the access ({direction})", character);
                if (direct)
                    connection.toRoom.DescribeActionNow($"Someone unlocked the access from the other side.");
                else
                    connection.toRoom.DescribeAction($"Someone unlocked the access from the other side.");
                character.Message($"You unlocked the access ({direction})");
            }
            else
            {
                connection.Unlock();
                if (direct)
                    room.DescribeActionNow($"{character.GetName()} has unlocked the access ({direction})", character);
                else
                    room.DescribeAction($"{character.GetName()} has unlocked the access ({direction})", character);
                if (direct)
                    connection.toRoom.DescribeActionNow($"Someone unlocked the access from the other side.");
                else
                    connection.toRoom.DescribeAction($"Someone unlocked the access from the other side.");
                character.Message($"You unlocked the access ({direction})");
            }

            character.RewardExperience(1);
        }
    }
}
