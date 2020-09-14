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
            }
            else
            {
                var inventory = character as IInventory;
                var target = room.FindInRoom(targetName);
                if (target != null && target is Item)
                {
                    var itemTarget = target as Item;
                    room.RemoveFromRoom(itemTarget);
                    character.Message($"You picked up {itemTarget.name}.");
                    if (direct)
                        room.DescribeActionNow($"{character.GetName()} picked up {itemTarget.name}.", character);
                    else
                        room.DescribeAction($"{character.GetName()} picked up {itemTarget.name}.", character);
                    inventory.AddToInventory(itemTarget);
                    character.experience += 1;
                }
                else
                {
                    character.Message($@"Can't find any ""{targetName}"" here !");
                }
            }
        }

        public override bool IsCombatCommand(Character character = null)
        {
            return false;
        }
        public override bool IsWorkbenchCommand(Character character = null)
        {
            return false;
        }

    }
}
