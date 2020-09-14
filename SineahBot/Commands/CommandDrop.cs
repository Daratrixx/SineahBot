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
                character.Message("What are you trying to drop ?");
            }
            else
            {
                var inventory = character as IInventory;
                var target = room.FindInRoom(targetName);
                if (target == null && (character is IInventory)) target = (character as IInventory).FindInInventory(targetName);
                if (target != null && target is Item)
                {
                    var itemTarget = target as Item;
                    inventory.RemoveFromInventory(itemTarget);
                    character.Message($"You dropped {itemTarget.name}.");
                    if (direct)
                        room.DescribeActionNow($"{character.GetName()} dropped {itemTarget.name}.", character);
                    else
                        room.DescribeAction($"{character.GetName()} dropped {itemTarget.name}.", character);
                    room.AddToRoom(itemTarget);
                    character.experience += 1;
                }
                else
                {
                    character.Message($@"Can't find any ""{targetName}""!");
                }
            }
        }

        public override bool IsWorkbenchCommand(Character character = null)
        {
            return false;
        }

    }
}
