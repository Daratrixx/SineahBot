using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandTradeLeave : Command
    {
        public CommandTradeLeave()
        {
            commandRegex = new Regex(@"^(leave|exit|out)$", RegexOptions.IgnoreCase);
            isNormalCommand = false;
            isCombatCommand = false;
            isTradeCommand = true;
            isSearchCommand = false;
        }

        public override void Run(Character character, Room room)
        {
            if (character.currentShop == null)
            {
                character.Message("You are not currently trading");
                character.characterStatus = CharacterStatus.Normal;
                return;
            }

            character?.currentShop.RemoveClient(character);
            if (character.characterStatus == CharacterStatus.Trade) character.characterStatus = CharacterStatus.Normal;
            character.Message("You stopped trading.");
        }
    }
}
