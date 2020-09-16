using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandSay : Command
    {

        public CommandSay()
        {
            commandRegex = new Regex(@"^(talk |say |t |"" ?)(.+)$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }
            bool direct = character is NPC;
            var speach = GetArgument(2);

            if (String.IsNullOrWhiteSpace(speach))
            {
                character.Message("What are you trying to say ?");
                return;
            }

            if (direct)
                room.DescribeActionNow($@"**{character.GetName()}** said: ""{speach}""", character);
            else
                room.DescribeAction($@"**{character.GetName()}** said: ""{speach}""", character);
            character.Message($@"You said: ""{speach}""");

            character.RewardExperience(1);
        }
    }
}
