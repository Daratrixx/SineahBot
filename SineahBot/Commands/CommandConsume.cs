using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandConsume : Command
    {

        public CommandConsume()
        {
            commandRegex = new Regex(@"^(use |consume |c )(.+)$", RegexOptions.IgnoreCase);
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
            var itemName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(itemName))
            {
                character.Message("What are you trying to consume ?");
                return;
            }

            var item = character.FindInInventory(itemName) as Consumable;
            if (item == null)
            {
                item = room.FindInRoom(itemName) as Consumable;
            }
            else
            {
                character.RemoveFromInventory(item);
            }
            if (item == null)
            {
                character.Message($@"Can't find any ""{itemName}"" to consume!");
                return;
            }

            item.OnConsumed(character);
            character.Message($"You consumed {item.GetName(character)}.");
            if (direct)
                room.DescribeActionNow($"{character.GetName()} consumed {item.GetName()}.", character);
            else
                room.DescribeAction($"{character.GetName()} consumed {item.GetName()}.", character);

            character.RewardExperience(1);
        }
    }
}
