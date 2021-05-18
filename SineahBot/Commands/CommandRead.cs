using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandRead : Command
    {
        public CommandRead()
        {
            commandRegex = new Regex(@"^(read|r) (.+)( (p|p |page|page )?(\d*))?$", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
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

            var targetName = GetArgument(2);
            var pageIndex = GetArgument(5);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                character.Message("What are you trying to read ?");
                return;
            }

            var target = room.FindInRoom(targetName) as Display;
            if (target == null)
            {
                character.Message($@"Can't find any ""{targetName}"" here !");
                return;
            }

            int page = 0;
            int.TryParse(pageIndex, out page);
            if (page > 0) --page;

            var display = $"**{target.GetName(character)}**";
            if (target.HasMultiplePages())
                display += $" (*page {(page + 1)}/{target.GetPageCount()}*)";
            display += $"\n{target.GetContent(character, page)}";
            if (target.HasMultiplePages() && page + 1 < target.GetPageCount())
                display += $"\n*Type `read {target.GetName(character)} {page + 2}` to read the next page.*";
            character.Message(display);

            character.RewardExperience(1);
        }
    }
}
