using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandUnlockContainer : Command
    {
        public CommandUnlockContainer()
        {
            commandRegex = new Regex(@"^(unlock) (.+)$", RegexOptions.IgnoreCase);
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
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }

            string containerName = GetArgument(2);
            Container container = room.FindInRoom<Container>(containerName);

            if (container == null)
            {
                character.Message($@"This room doesn't have a ""{containerName}"" container to lock.");
                return;
            }

            if (!container.lockable)
            {
                character.Message($@"The container ""{container.GetName()}"" cannot be locked");
                return;
            }

            if (!container.locked)
            {
                character.Message("This container is already unlocked.");
                return;
            }

            if (container.keyItemName != null && !character.IsItemInInventory(container.keyItemName))
            {
                character.Message("You need a key for that.");
                return;
            }

            container.Unlock();
            room.DescribeAction($"{character.GetName()} has unlocked **{container.GetName()}**", character);
            character.Message($"You unlocked **{container.GetName()}**");

            character.RewardExperience(1);
        }
    }
}
