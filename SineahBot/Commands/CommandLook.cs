using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandLook : Command
    {
        public CommandLook()
        {
            commandRegex = new Regex(@"^(look|l)( .+)?$", RegexOptions.IgnoreCase);
            isNormalCommand = true;
            isCombatCommand = true;
            isTradeCommand = false;
            isSearchCommand = false;
        }

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }

            var targetName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                character.Message(room.GetFullDescription(character));
                return;
            }

            var target = room.FindInRoom(targetName) as IObservable;
            if (target == null) target = character.FindInInventory(targetName) as IObservable;
            if (target == null)
            {
                character.Message($@"Can't find any ""{targetName}"" here !");
                return;
            }

            var observableTarget = target;
            character.Message($"**{observableTarget.GetName(character)}**\n{observableTarget.GetFullDescription(character)}");

            character.RewardExperience(1);
        }
    }
}
