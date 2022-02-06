using SineahBot.Data;
using SineahBot.Data.Enums;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandSearchLook : Command
    {
        public CommandSearchLook()
        {
            commandRegex = new Regex(@"^(l|look|list)( .*)?$", RegexOptions.IgnoreCase);
            isNormalCommand = false;
            isCombatCommand = false;
            isTradeCommand = false;
            isSearchCommand = true;
        }

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }

            if (character.currentContainer == null)
            {
                character.Message("You are not currently searching.");
                character.characterStatus = CharacterStatus.Normal;
                return;
            }

            var itemName = GetArgument(2);

            var container = character.currentContainer;
            if (!string.IsNullOrEmpty(itemName))
            {
                var item = container.FindInInventory(itemName);
                if (item == null)
                {
                    character.Message($"This there is no \"{itemName}\" in this.");
                    return;
                }

                character.Message($"**{container.GetName().ToUpper()}**\n{CommandMetaInventory.GetItemInformation(item, container.CountInInventory(item),character)}");
            }
            else
            {
                character.Message($"**{container.GetName().ToUpper()}**{CommandMetaInventory.GetItemListInInventory(container, character)}");
            }
        }
    }
}
