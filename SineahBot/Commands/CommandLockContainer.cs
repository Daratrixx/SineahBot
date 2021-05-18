using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandLockContainer : Command
    {
        public CommandLockContainer()
        {
            commandRegex = new Regex(@"^(lock) (.+)$", RegexOptions.IgnoreCase);
            isNormalCommand = true;
            isCombatCommand = false;
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

            string containerName = GetArgument(2);
            Container container = room.FindInRoom<Container>(containerName);

            if (container == null)
            {
                character.Message($@"This room doesn't have a ""{containerName}"" container to lock.");
                return;
            }

            Lock(character, room, container);

            character.RewardExperience(1);
        }

        public static bool Lock(Character character, Room room, Container container)
        {
            if (!container.lockable)
            {
                character.Message($@"The container ""{container.GetName()}"" cannot be locked");
                return false;
            }

            if (container.locked)
            {
                character.Message("This container is already locked.");
                return false;
            }

            if (container.keyItemName != null && !character.IsItemInInventory(container.keyItemName))
            {
                character.Message("You need a key for that.");
                return false;
            }

            container.Lock();
            // describe in current room
            room.DescribeAction($"{character.GetName()} has locked **{container.GetName()}**.", character);
            character.Message($"You locked **{container.GetName()}**.");
            return true;
        }
    }
}
