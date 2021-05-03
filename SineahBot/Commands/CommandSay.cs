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
            var speach = GetArgument(2);

            if (String.IsNullOrWhiteSpace(speach))
            {
                character.Message("What are you trying to say ?");
                return;
            }

            Say(character, room, speach);

            character.RewardExperience(1);
        }

        public static void Say(Character character, Room room, string speach)
        {
            room.DescribeAction($@"**{character.GetName()}** said: ""{speach}""", character);
            character.Message($@"You said: ""{speach}""");
        }
    }
}
