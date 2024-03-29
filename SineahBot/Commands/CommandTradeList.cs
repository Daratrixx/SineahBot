﻿using SineahBot.Data;
using SineahBot.Data.Enums;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandTradeList : Command
    {
        public CommandTradeList()
        {
            commandRegex = new Regex(@"^(l|list)( .*)?$", RegexOptions.IgnoreCase);
            isNormalCommand = false;
            isCombatCommand = false;
            isTradeCommand = true;
            isSearchCommand = false;
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

            var entryName = GetArgument(2);

            var shop = character.currentShop;
            if (!string.IsNullOrEmpty(entryName))
            {
                var entry = shop.FindShopEntry(entryName);
                if (entry == null)
                {
                    character.Message($"This merchant doesn't trade \"{entryName}\"");
                    return;
                }

                character.Message($"**{shop.GetName().ToUpper()}**\n> **{entry.referenceItem.GetName()}**\n> {entry.referenceItem.details}");
            }
            else
            {
                character.Message($"**{shop.GetName().ToUpper()}**{GetShopListForCharacter(shop, character)}");
            }
        }

        public static string GetShopListForCharacter(Shop shop, Character character)
        {
            string output = "";
            var buyableList = String.Join('\n', shop.GetBuyableEntries()
            .Select(x => $"*{x.referenceItem.GetName(character)}* - **{x.goldCost}** gold"));
            var sellableList = String.Join('\n', shop.GetSellableEntries()
            .Select(x => $"*{x.referenceItem.GetName(character)}* - **{x.goldRefund}** gold"));

            if (!string.IsNullOrWhiteSpace(buyableList))
                output += $"\nThis merchant will sell to you: (your gold: **{character.gold}**)\n{buyableList}";
            if (!string.IsNullOrWhiteSpace(sellableList))
                output += $"\nThis merchant will buy from you:\n{sellableList}";
            return output;
        }
    }
}
