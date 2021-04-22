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
    public class CommandSearch : Command
    {

        public CommandSearch()
        {
            commandRegex = new Regex(@"^(search|loot) (.+)?$", RegexOptions.IgnoreCase);
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

        public override bool IsSearchCommand(Character character = null)
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
            var containerName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(containerName))
            {
                character.Message("What are you trying to search?");
                return;
            }

            var container = room.FindInRoom<Container>(containerName);
            if (container == null)
            {
                character.Message($"There is no \"{containerName}\" to search in this room.");
                return;
            }

            character.characterStatus = CharacterStatus.Search;
            character.currentContainer = container;
            character.Message($"You started searching **{container.GetName()}**.\n> It contains:\n{CommandMetaInventory.GetItemListInInventory(container, character)}");

            character.RewardExperience(2);
        }
    }
}
