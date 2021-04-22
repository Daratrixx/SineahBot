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
            var targetName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                character.Message("What are you trying to drop ?");
                return;
            }

            var target = character.FindInInventory(targetName) as Item;

            if (target == null)
            {
                character.Message($@"you don't have ""{targetName}"" in your inventory.");
                return;
            }

            var itemTarget = target as Item;
            if (itemTarget is Equipment && character.IsEquiped(itemTarget as Equipment))
                character.Unequip((itemTarget as Equipment).slot);
            character.RemoveFromInventory(itemTarget);
            character.Message($"You dropped {itemTarget.name}.");
            room.AddToRoom(itemTarget);
            if (direct)
                room.DescribeActionNow($"{character.GetName()} dropped {itemTarget.name}.", character);
            else
                room.DescribeAction($"{character.GetName()} dropped {itemTarget.name}.", character);

            character.RewardExperience(1);
        }
    }
}
