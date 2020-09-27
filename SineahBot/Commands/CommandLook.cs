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
        }

        public override bool IsWorkbenchCommand(Character character = null)
        {
            return false;
        }

        public override bool IsTradeCommand(Character character = null)
        {
            return false;
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
            if (target == null && character is IInventory) target = (character as IInventory).FindInInventory(targetName) as IObservable;
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
