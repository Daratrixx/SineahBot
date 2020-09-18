using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandTradeBuy : Command
    {

        public CommandTradeBuy()
        {
            commandRegex = new Regex(@"^(b|buy) (\d+ )?(.+)$", RegexOptions.IgnoreCase);
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
                character.Message("What are you trying to buy ?");
                return;
            }

            var shop = character.currentShop;
            var shopEntry = shop.FindShopEntry(itemName);

            if (shopEntry?.goldCost == null)
            {
                character.Message($@"Can't find any ""{shopEntry}"" to buy!");
                return;
            }

            var quantity = 1;
            if (!string.IsNullOrWhiteSpace(itemQuantity)) int.TryParse(itemQuantity, out quantity);
            var totalGoldCost = shopEntry.goldCost.Value * quantity;

            if (totalGoldCost > character.gold)
            {
                character.Message($@"Not enough gold to buy *x{quantity}* {shopEntry.GetName()} (gold cost: **{totalGoldCost}**)");
                return;
            }

            character.gold -= totalGoldCost;
            character.AddToInventory(shopEntry.referenceItem, quantity);
            character.Message($@"Bought *x{quantity}* {shopEntry.GetName()} (gold cost: **{totalGoldCost}**)");

            character.RewardExperience(1);
        }
    }
}
