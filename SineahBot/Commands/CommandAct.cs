using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandAct : Command
    {

        public CommandAct()
        {
            commandRegex = new Regex(@"^(act |me |[*] ?)(.+)$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }

            bool direct = character is NPC;
            var act = GetArgument(2);

            if (String.IsNullOrWhiteSpace(act))
            {
                character.Message("What are you trying to act ?");
                return;
            }

            act = act.Replace("*", "");

            Act(character, room, act, direct);
            character.RewardExperience(1);
        }

        public static void Act(Character character, Room room, string act, bool direct = false)
        {
            if (direct)
                room.DescribeActionNow($@"***{character.GetName()}** {act}*", character);
            else
                room.DescribeAction($@"***{character.GetName()}** {act}*", character);
            character.Message($@"***{character.GetName()}** {act}*");
        }
    }
}
