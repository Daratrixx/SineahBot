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
    public class CommandMetaInventory : Command
    {

        public CommandMetaInventory()
        {
            commandRegex = new Regex(@"^(!inventory|!inv)?$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            var spellName = GetArgument(2);
            DisplayinformationForCharacter(character, spellName);
        }
        public void DisplayinformationForCharacter(Character character, string spellName)
        {
            character.Message(GetCharacterInventory(character));
        }

        public string GetCharacterInventory(Character character)
        {
            return $"**INVENTORY** (*gold*: **{character.gold}**)\n"
            + String.Join('\n', character.items.Select(x => GetItemInformation(x.Key, x.Value, character)));
        }
        public string GetItemInformation(Item item, int count, Character character)
        {
            return $"> **{item.GetName()}** (x{count}) {item.GetFullDescription(character)}";
        }
    }
}
