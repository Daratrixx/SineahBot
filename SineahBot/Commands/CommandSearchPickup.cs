using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandSearchPickup : Command
    {
        public CommandSearchPickup()
        {
            commandRegex = new Regex(@"^(get|g|pickup|pick up|grab|take)( \d+| all| \*)? (.+)?$", RegexOptions.IgnoreCase);
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

            if (character.currentContainer == null)
            {
                character.Message("You are not currently searching.");
                character.characterStatus = CharacterStatus.Normal;
                return;
            }

            bool direct = character is NPC;
            var itemQuantity = GetArgument(2);
            var itemName = GetArgument(3);

            if (String.IsNullOrWhiteSpace(itemName))
            {
                character.Message("What are you trying to get ?");
                return;
            }

            var item = character.currentContainer.FindInInventory(itemName);

            if (item == null)
            {
                character.Message($@"Can't find any ""{itemName}"" in {character.currentContainer.GetName()}!");
                return;
            }

            var quantityOwned = character.currentContainer.CountInInventory(item);
            var quantity = 1;
            if (string.IsNullOrWhiteSpace(itemQuantity) || itemQuantity == "*" || itemQuantity.ToLower() == "all")
            {
                quantity = quantityOwned;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(itemQuantity)) int.TryParse(itemQuantity, out quantity);
            }

            character.currentContainer.RemoveFromInventory(item, quantity);
            character.Message($"You picked up **{item.GetName()}** x{quantity} from **{character.currentContainer.GetName()}**.");
            //if (direct)
            //    room.DescribeActionNow($"{character.GetName()} picked up {item.GetName()} from {character.currentContainer.GetName()}.", character);
            //else
            //    room.DescribeAction($"{character.GetName()} picked up {item.GetName()} from {character.currentContainer.GetName()}.", character);
            character.AddToInventory(item, quantity);

            character.RewardExperience(1);
        }
    }
}
