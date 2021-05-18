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
    public class CommandDirection : Command
    {
        public CommandDirection()
        {
            commandRegex = new Regex(@"^(dir|directions?)$", RegexOptions.IgnoreCase);
            isNormalCommand = true;
            isCombatCommand = true;
            isTradeCommand = false;
            isSearchCommand = false;
        }

        public override void Run(Character character, Room room)
        {
            character.Message(String.Join(", ", room.GetDirections().Select(x => x.ToString())) + '.');
        }
    }
}
