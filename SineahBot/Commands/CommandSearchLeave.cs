using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandSearchLeave : Command
    {
        public CommandSearchLeave()
        {
            commandRegex = new Regex(@"^(leave|exit|out|quit|off|done|away|escape)$", RegexOptions.IgnoreCase);
        }

        public override bool IsNormalCommand(Character character = null)
        {
            return false;
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
            if (character.currentContainer == null)
            {
                character.Message("You are not currently searching.");
                character.characterStatus = CharacterStatus.Normal;
                return;
            }

            character.currentContainer = null;
            if (character.characterStatus == CharacterStatus.Search) character.characterStatus = CharacterStatus.Normal;
            character.Message("You stopped searching.");
        }
    }
}
