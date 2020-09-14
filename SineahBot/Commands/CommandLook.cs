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
            }
            else
            {
                var target = room.FindInRoom(targetName);
                if (target == null && character is IInventory) target = (character as IInventory).FindInInventory(targetName);
                if (target != null && target is IObservable)
                {
                    var observableTarget = target as IObservable;
                    character.Message(observableTarget.GetFullDescription(character));
                    character.experience += 1;
                }
                else
                {
                    character.Message($@"Can't find any ""{targetName}"" here !");
                }
            }
        }

        public override bool IsWorkbenchCommand(Character character = null)
        {
            return false;
        }

    }
}
