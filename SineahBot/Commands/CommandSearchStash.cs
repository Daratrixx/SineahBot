using SineahBot.Data;
using SineahBot.Data.Enums;
using System;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandSearchStash : Command
    {
        public CommandSearchStash()
        {
            commandRegex = new Regex(@"^(stash|store|s|drop|d)( \d+| all| \*)? (.+)$", RegexOptions.IgnoreCase);
            isNormalCommand = false;
            isCombatCommand = false;
            isTradeCommand = false;
            isSearchCommand = true;
        }

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }

            if (character.currentContainer == null)
            {
                character.Message("You are not currently searching.");
                character.characterStatus = CharacterStatus.Normal;
                return;
            }

            bool direct = character is NPC;
            var itemQuantity = GetArgument(2);
            var itemName = GetArgument(3);

            if (String.IsNullOrWhiteSpace(itemName))
            {
                character.Message("What are you trying to stash ?");
                return;
            }

            var item = character.FindInInventory(itemName);
            if (item == null)
            {
                character.Message($@"You don't have any ""{itemName}"" in your inventory.");
                return;
            }

            var quantityOwned = character.CountInInventory(item);
            var quantity = 1;
            if (itemQuantity == "*" || itemQuantity.ToLower() == "all")
            {
                quantity = quantityOwned;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(itemQuantity)) int.TryParse(itemQuantity, out quantity);
            }

            if (quantity > quantityOwned)
            {
                character.Message($"You cannot stash {quantity} **{item.GetName()}** because you only own {quantityOwned}.");
                return;
            }

            if (item is Equipment && character.IsEquiped(item as Equipment))
                character.Unequip((item as Equipment).slot);
            character.RemoveFromInventory(item, quantity);
            character.Message($"You stashed **{item.name}** x{quantity} in **{character.currentContainer.GetName()}**.");
            character.currentContainer.AddToInventory(item, quantity);

            character.RewardExperience(1);
        }
    }
}
