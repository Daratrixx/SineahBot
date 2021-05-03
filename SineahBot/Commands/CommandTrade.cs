using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandTrade : Command
    {
        public CommandTrade()
        {
            commandRegex = new Regex(@"^(trade|shop)( .+)?$", RegexOptions.IgnoreCase);
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
            var shopName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(shopName))
            {
                var shop = room.FindShopInRoom(shopName);
                if (shop == null)
                {
                    character.Message("There is no way to trade in this room.");
                    return;
                }

                shop.AddClient(character);
                character.Message($"You started trading with **{shop.GetName()}**.{CommandTradeList.GetShopListForCharacter(shop, character)}");
            }
            else
            {
                var shop = room.FindShopInRoom(shopName);
                //if (shop == null && character is IInventory) target = (character as IInventory).FindInInventory(shopName); // inventory magic shop ?
                if (shop == null)
                {
                    character.Message($@"No ""{shopName}"" wants to trade in this room.");
                    return;
                }

                shop.AddClient(character);
                character.Message($"You started trading with **{shop.GetName()}**.{CommandTradeList.GetShopListForCharacter(shop, character)}");
            }
        }
    }
}
