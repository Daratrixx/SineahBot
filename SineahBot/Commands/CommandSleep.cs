using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandSleep : Command
    {

        public CommandSleep()
        {
            commandRegex = new Regex(@"^sleep$", RegexOptions.IgnoreCase);
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

        public override bool IsSearchCommand(Character character = null)
        {
            return false;
        }

        public override void Run(Character character, Room room)
        {
            if (!Sleep(character, room))
            {
                character.Message($@"You are already asleep.");
                return;
            }

            character.RewardExperience(1);
        }

        public static bool Sleep(Character character, Room room)
        {
            if (character.Sleep())
            {
                room.DescribeAction($@"**{character.GetName()}** fell asleep.", character);
                character.Message($@"You fell asleep.");
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Awake(Character character, Room room, bool direct)
        {
            if (character.Awake())
            {
                room.DescribeAction($@"**{character.GetName()}** woke up.", character);
                character.Message($@"You woke up.");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
