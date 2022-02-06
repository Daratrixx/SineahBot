using SineahBot.Data;
using SineahBot.Data.Enums;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandSearch : Command
    {
        public CommandSearch()
        {
            commandRegex = new Regex($@"^(search|loot) {targetRegex_3}?$", RegexOptions.IgnoreCase);
            isNormalCommand = true;
            isCombatCommand = false;
            isTradeCommand = false;
            isSearchCommand = false;
        }

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }

            var container = GetTarget<Container>(character, room, 2);
            if (container == null) return; // error message already given in GetTarget


            if (container.lockable && container.locked)
            {
                character.Message($"**{container.GetName()}** is locked, you cannot search it.");
                return;
            }

            character.characterStatus = CharacterStatus.Search;
            character.currentContainer = container;
            character.Message($"You started searching **{container.GetName()}**.\n> It contains:\n{CommandMetaInventory.GetItemListInInventory(container, character)}");

            character.RewardExperience(2);
        }
    }
}
