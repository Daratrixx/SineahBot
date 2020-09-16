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

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }

            bool direct = character is NPC;
            var targetName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                character.Message("What are you trying to get ?");
                return;
            }

            var inventory = character as IInventory;
            var item = room.FindInRoom(targetName) as Item;

            if (item == null)
            {
                character.Message($@"Can't find any ""{targetName}"" here !");
                return;
            }

            room.RemoveFromRoom(item);
            character.Message($"You picked up {item.GetName()}.");
            if (direct)
                room.DescribeActionNow($"{character.GetName()} picked up {item.GetName()}.", character);
            else
                room.DescribeAction($"{character.GetName()} picked up {item.GetName()}.", character);
            inventory.AddToInventory(item);

            character.RewardExperience(1);
        }
    }
}
