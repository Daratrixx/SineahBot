using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SineahBot.Tools
{
    public static class CharacterManager
    {

        public static int ExpMultiplier = 5;
        public static int GoldMultiplier = 5;
        static CharacterManager()
        {
            new MudInterval(10, () =>
            {
                foreach (var c in characters)
                {
                    c.Value.Regenerate();
                }
            });
        }
        public static Dictionary<Guid, Character> characters = new Dictionary<Guid, Character>();

        public static void LoadCharacters(IEnumerable<Character> characters)
        {
            foreach (var character in characters)
            {
                CharacterManager.characters[character.id] = character;
            }
        }
        public static Character GetCharacter(Guid idCharacter)
        {
            if (!characters.ContainsKey(idCharacter))
            {
                var character = Program.database.Characters.FirstOrDefault(x => x.id == idCharacter);
                if (character == null) throw new Exception($"Impossible to find character with id {idCharacter}");
                characters[idCharacter] = character;
                ClassProgressionManager.ApplyClassProgressionForCharacter(character, true);
                if (ClassProgressionManager.IsPhysicalClass(character.characterClass))
                {
                    character.AddToInventory(Data.Templates.Consumables.Bread, 3);
                    character.AddToInventory(Data.Templates.Consumables.DriedMeat);
                }
                if (ClassProgressionManager.IsMagicalClass(character.characterClass))
                {
                    character.AddToInventory(Data.Templates.Consumables.Bread, 3);
                    character.AddToInventory(Data.Templates.Consumables.Water);
                }
                if (ClassProgressionManager.IsSecretClass(character.characterClass))
                {
                    character.AddToInventory(Data.Templates.Consumables.Candy);
                }
                return character;
            }
            return characters[idCharacter];
        }

        public static Character CreateCharacterForPlayer(Player player)
        {
            var character = new Character();
            character.id = Guid.NewGuid();
            character.name = player.characterName;
            character.agent = player;
            character.characterClass = player.characterClass;
            character.level = 1;
            character.experience = 0;
            ClassProgressionManager.ApplyClassProgressionForCharacter(character, true);
            if (ClassProgressionManager.IsPhysicalClass(character.characterClass))
            {
                character.AddToInventory(Data.Templates.Consumables.Bread);
                character.AddToInventory(Data.Templates.Consumables.Bread);
                character.AddToInventory(Data.Templates.Consumables.Bread);
                character.AddToInventory(Data.Templates.Consumables.DriedMeat);
            }
            if (ClassProgressionManager.IsMagicalClass(character.characterClass))
            {
                character.AddToInventory(Data.Templates.Consumables.Bread);
                character.AddToInventory(Data.Templates.Consumables.Bread);
                character.AddToInventory(Data.Templates.Consumables.Bread);
                character.AddToInventory(Data.Templates.Consumables.Water);
            }
            if (ClassProgressionManager.IsSecretClass(character.characterClass))
            {
                character.AddToInventory(Data.Templates.Consumables.Candy);
            }
            characters[character.id] = character;
            Program.database.Characters.Add(character);
            player.idCharacter = character.id;
            player.character = character;
            return character;
        }

        public static void DeletePlayerCharacter(Player player)
        {
            if (player == null) throw new Exception("Player cannot be null.");
            if (player.character == null || player.idCharacter == null) throw new Exception("This player doesn't have a character to be deleted.");
            var character = player.character;
            character.agent = null;
            characters.Remove(character.id);
            Program.database.Characters.Remove(character);
            player.idCharacter = null;
            player.character = null;
            player.playerStatus = PlayerStatus.CharacterCreation;
            player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.None;
            player.characterName = null;
            player.characterClass = CharacterClass.None;
        }

        public static Character TestCharacter
        {
            get
            {
                return new Character()
                {
                    characterStatus = CharacterStatus.Normal,
                    id = Guid.NewGuid(),
                    name = "test character",
                    //description = "You notice the test character."
                };
            }
        }
    }
}
