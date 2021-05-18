using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandDrop : Command
    {
        public CommandDrop()
        {
            commandRegex = new Regex(@"^(drop |d )(.+)$", RegexOptions.IgnoreCase);
            isNormalCommand = true;
            isCombatCommand = true;
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

            var targetName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                character.Message("What are you trying to drop ?");
                return;
            }

            var item = character.FindInInventory(targetName);

            if (item == null)
            {
                character.Message($@"You don't have ""{targetName}"" in your inventory.");
                return;
            }

            Drop(character, room, item);

            character.RewardExperience(1);
        }

        public static void Drop(Character character, Room room, Item item)
        {
            var itemTarget = item as Item;
            if (itemTarget is Equipment equ && character.IsEquiped(equ))
                character.Unequip((itemTarget as Equipment).slot);
            character.RemoveFromInventory(itemTarget);
            character.Message($"You dropped {itemTarget.name}.");
            room.AddToRoom(itemTarget);
            room.DescribeAction($"{character.GetName()} dropped {itemTarget.name}.", character);
        }
    }
}
