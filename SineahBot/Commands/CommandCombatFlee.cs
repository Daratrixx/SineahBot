using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandCombatFlee : Command
    {

        public CommandCombatFlee()
        {
            commandRegex = new Regex(@"^(flee) (north|n|east|e|south|s|west|w|in|out|up|down)$", RegexOptions.IgnoreCase);
        }

        public override bool IsNormalCommand(Character character = null)
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
                    character.Message($@"Can't flee to unknown direction ""{directionName}""");
                    Logging.Log($@"Can't flee to unknown direction ""{directionName}""");
                    return;
            }

            if (Flee(character, room, direction))
                character.RewardExperience(1);
        }

        public static bool Flee(Character character, Room room, MoveDirection direction)
        {
            if (CommandMove.MoveCharacter(character, room, direction))
            {
                CombatManager.RemoveFromCombat(character, false);
                return true;
            }
            return false;
        }
    }
}
