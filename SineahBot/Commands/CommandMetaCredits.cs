using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaCredits : Command
    {
        public CommandMetaCredits()
        {
            commandRegex = new Regex(@"^!credits$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            character.Message(credits);
        }

        public static string credits = @"> ***SineahBot***
Creator: Daratrix
Testers: Tuchdalizard, Jakiw, Username, FiveBalesofHay, 4slugcats, AAAAAAAAAAAAAAAAAAA
Special thanks: 4slugcats, AAAAAAAAAAAAAAAAAAA for extra intensive testing, feedback, and idea.";
    }
}
