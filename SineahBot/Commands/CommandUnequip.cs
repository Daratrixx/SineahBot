using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandUnequip : Command
    {
        public CommandUnequip()
        {
            commandRegex = new Regex(@"^(unequip|uneq) (.+)$", RegexOptions.IgnoreCase);
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

            var equipmentName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(equipmentName))
            {
                character.Message("What are you trying to unequip ?");
                return;
            }

            var item = character.FindInInventory(equipmentName) as Equipment;
            if (item == null || !character.IsEquiped(item))
            {
                character.Message($@"Can't find any ""{equipmentName}"" to unequip!");
                return;
            }

            Unequip(character, room, item);

            character.RewardExperience(1);
        }

        public static void Unequip(Character character, Room room, Equipment item)
        {
            character.Message($"You unequiped {item.GetName(character)}.");
            character.Unequip(item.slot);

            room.DescribeAction($"{character.GetName()} unequiped {item.GetName()}.", character);
        }
    }
}
