using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandPickup : Command
    {

        public CommandPickup()
        {
            commandRegex = new Regex(@"^(get|g|pickup|pick up|grab|take) (.+)$", RegexOptions.IgnoreCase);
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

            var targetName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                character.Message("What are you trying to get ?");
                return;
            }

            var item = room.FindInRoom(targetName) as Item;

            if (item == null)
            {
                character.Message($@"Can't find any ""{targetName}"" here !");
                return;
            }

            room.RemoveFromRoom(item);
            room.DescribeAction($"{character.GetName()} picked up {item.GetName()}.", character);
            character.AddToInventory(item);

            character.RewardExperience(1);
        }
    }
}
