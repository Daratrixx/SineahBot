using SineahBot.Data;
using SineahBot.Data.Enums;
using System;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandConsume : Command
    {
        public CommandConsume()
        {
            commandRegex = new Regex(@"^(use |consume |c )(.+)$", RegexOptions.IgnoreCase);
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

            if (!character.ActionCooldownOver())
            {
                return;
            }

            var itemName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(itemName))
            {
                character.Message("What are you trying to consume ?");
                return;
            }

            var item = character.FindInInventory(itemName) as Consumable;
            if (item == null)
            {
                item = room.FindInRoom(itemName) as Consumable;
            }

            if (item == null)
            {
                character.Message($@"Can't find any ""{itemName}"" to consume!");
                return;
            }

            if (Consume(character, room, item))
                character.RewardExperience(1);
        }

        public static bool Consume(Character character, Room room, Consumable item)
        {
            if (!item.combatConsumable && character.characterStatus == CharacterStatus.Combat)
            {
                character.Message($@"Can't consume ""{item.GetName()}"" during combat!");
                return false;
            }

            character.RemoveFromInventory(item);
            item.OnConsumed(character);
            character.StartActionCooldown();

            character.Message($"You consumed {item.GetName(character)}.");
            room.DescribeAction($"{character.GetName()} consumed {item.GetName()}.", character);

            return true;
        }
    }
}
