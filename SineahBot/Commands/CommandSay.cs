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
            isNormalCommand = true;
            isCombatCommand = true;
            isTradeCommand = true;
            isSearchCommand = true;
        }

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }
            var speech = GetArgument(2);

            if (String.IsNullOrWhiteSpace(speech))
            {
                character.Message("What are you trying to say ?");
                return;
            }

            Say(character, room, speech);

            character.RewardExperience(1);
        }

        public static void Say(Character character, Room room, string speech)
        {
            speech = CensorManager.FilterMessage(speech);
            room.DescribeAction($@"**{character.GetName()}** said: ""{speech}""", character);
            room.RaiseRoomEvent(new RoomEvent(room, RoomEventType.CharacterSpeaks) { source = character, speakingContent = speech }, character);
            character.Message($@"You said: ""{speech}""");
        }
    }
}
