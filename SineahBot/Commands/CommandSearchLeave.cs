using SineahBot.Data;
using SineahBot.Data.Enums;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandSearchLeave : Command
    {
        public CommandSearchLeave()
        {
            commandRegex = new Regex(@"^(leave|exit|out|quit|off|done|away|escape)$", RegexOptions.IgnoreCase);
            isNormalCommand = false;
            isCombatCommand = false;
            isTradeCommand = false;
            isSearchCommand = true;
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
