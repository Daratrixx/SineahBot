using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaEquipment : Command
    {
        public CommandMetaEquipment()
        {
            commandRegex = new Regex(@"^(!equipment|!equip|!e)?$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            DisplayEquipmentForCharacter(character);
        }
        public void DisplayEquipmentForCharacter(Character character)
        {
            character.Message(GetCharacterEquipment(character));
        }

        public static string GetCharacterEquipment(Character character)
        {
            return $"**EQUIPMENT**\n"
            + GetEquipmentList(character);
        }

        public static string GetEquipmentList(Character character = null)
        {
            return String.Join('\n', character.equipments.Values.Where(x => x != null).Select(x => GetEquipmentInformation(x, character)));
        }

        public static string GetEquipmentInformation(Equipment e, Character character)
        {
            return $"> **{e.GetName()}** {e.GetFullDescription(character)}";
        }
    }
}
