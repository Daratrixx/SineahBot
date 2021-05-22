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
            commandRegex = new Regex($@"^(get|g|pickup|pick up|grab|take) {targetRegex_3}$", RegexOptions.IgnoreCase);
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

            var item = GetTarget<Item>(character, room, 2);
            if (item == null) return; // error message already given in GetTarget

            Pickup(character, room, item);

            character.RewardExperience(1);
        }

        public static void Pickup(Character character, Room room, Item item)
        {
            room.RemoveFromRoom(item);
            room.DescribeAction($"{character.GetName()} picked up {item.GetName()}.", character);
            character.AddToInventory(item);
            character.Message($"You picked up {item.GetName()}.");
        }
    }
}
