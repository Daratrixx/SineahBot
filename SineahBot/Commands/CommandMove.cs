using SineahBot.Data;
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
            commandRegex = new Regex(@"^(move |go |move to |go to )?(north|n|east|e|south|s|west|w|in|out)$", RegexOptions.IgnoreCase);
        }

        public override void Run(IAgent agent, Room room)
        {
            if (!(agent is Entity)) throw new Exception($@"Impossible to move as non-entity agent");
            var entity = agent as Entity;
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
                    agent.Message($@"Can't move to unknown direction ""{directionName}""");
                    throw new Exception($@"Can't move to unknown direction ""{directionName}""");
            }
            if (!room.IsValidDirection(direction))
            {
                agent.Message($@"This room doesn't have a ""{direction}"" access.");
                return;
            }
            if (!RoomManager.MoveFromRoom(entity, room, direction))
            {
                agent.Message("This access is locked.");
            }
            else if (agent is Character) (agent as Character).experience += 1;
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
