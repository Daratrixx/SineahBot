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
            }
            else
            {
                var inventory = character as IInventory;
                var target = room.FindInRoom(itemName);
                if (target == null && (character is IInventory)) target = (character as IInventory).FindInInventory(itemName);
                if (target != null && target is Consumable)
                {
                    var itemTarget = target as Consumable;
                    inventory.RemoveFromInventory(itemTarget);
                    character.Message($"You consumed {itemTarget.GetName(character)}.");
                    if (direct)
                        room.DescribeActionNow($"{character.GetName()} consumed {itemTarget.GetName()}.", character);
                    else
                        room.DescribeAction($"{character.GetName()} consumed {itemTarget.GetName()}.", character);
                    itemTarget.OnConsumed(character);
                    character.experience += 1;
                }
                else
                {
                    character.Message($@"Can't find any ""{itemName}"" to consume!");
                }
            }
        }

        public override bool IsWorkbenchCommand(Character character = null)
        {
            return false;
        }

    }
}
