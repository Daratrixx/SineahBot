using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandTradeSell : Command
    {

        public CommandTradeSell()
        {
            commandRegex = new Regex(@"^(s|sell) (\d+ |all |[*] )?(.+)$", RegexOptions.IgnoreCase);
        }

        public override bool IsNormalCommand(Character character = null)
        {
            return false;
        }

        public override bool IsCombatCommand(Character character = null)
        {
            return false;
        }

        public override bool IsWorkbenchCommand(Character character = null)
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

            if (character.currentShop == null)
            {
                character.Message("You are not currently trading.");
                character.characterStatus = CharacterStatus.Normal;
                return;
            }

            var itemQuantity = GetArgument(2);
            var itemName = GetArgument(3);

            if (String.IsNullOrWhiteSpace(itemName))
            {
                character.Message("What are you trying to sell ?");
                return;
            }

            var shop = character.currentShop;
            var shopEntry = shop.FindShopEntry(itemName);

            if (shopEntry?.goldRefund == null)
            {
                character.Message($@"This merchant doesn't want to buy that.");
                return;
            }

            var item = character.FindInInventory(itemName);
            if (item == null)
            {
                character.Message("What are you trying to sell ?");
                return;
            }
            var quantityOwned = character.CountInInventory(item);
            var quantity = 1;
            if (itemQuantity == "*" || itemQuantity.ToLower() == "all")
            {
                quantity = quantityOwned;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(itemQuantity)) int.TryParse(itemQuantity, out quantity);
            }

            if (quantity > quantityOwned)
            {
                character.Message($"You cannot sell {quantity} {item.GetName()} because you only own {quantityOwned}.");
                return;
            }

            var totalGoldCost = shopEntry.goldRefund.Value * quantity;
            character.gold += totalGoldCost;
            character.RemoveFromInventory(shopEntry.referenceItem, quantity);
            character.Message($@"Sold *x{quantity}* {shopEntry.GetName()} (gold earned: **{totalGoldCost})**");

            character.RewardExperience(1);
        }
    }
}
