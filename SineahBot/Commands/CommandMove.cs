﻿using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMove : Command
    {

        public CommandMove()
        {
            commandRegex = new Regex(@"^(move |go |move to |go to )?(north|n|east|e|south|s|west|w|in|out|up|down)$", RegexOptions.IgnoreCase);
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
            switch (directionName.ToLower())
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
                    character.Message($@"Can't move to unknown direction ""{directionName}""");
                    throw new Exception($@"Can't move to unknown direction ""{directionName}""");
            }
            if (!room.IsValidDirection(direction))
            {
                character.Message($@"This room doesn't have a ""{direction}"" access.");
                return;
            }
            if (!RoomManager.MoveFromRoom(character, room, direction))
            {
                character.Message("This access is locked.");
            }
            else character.experience += 1;
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

    public enum MoveDirection
    {
        North,
        East,
        South,
        West,
        In,
        Out,
        Up,
        Down
    }
}
