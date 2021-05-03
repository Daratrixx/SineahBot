using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandEquip : Command
    {
        public CommandEquip()
        {
            commandRegex = new Regex(@"^(equip|eq) (.+)$", RegexOptions.IgnoreCase);
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
                character.Message("What are you trying to equip ?");
                return;
            }

            var item = character.FindInInventory(equipmentName) as Equipment;
            if (item == null)
            {
                character.Message($@"Can't find any ""{equipmentName}"" to equip!");
                return;
            }

            Equip(character, room, item);

            character.RewardExperience(1);
        }

        public static void Equip(Character character, Room room, Equipment item)
        {
            character.Message($"You equiped {item.GetName(character)}.");
            character.Equip(item);

            room.DescribeAction($"{character.GetName()} equiped {item.GetName()}.", character);
        }
    }
}
