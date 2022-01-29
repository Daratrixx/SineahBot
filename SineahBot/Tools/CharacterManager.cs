using Microsoft.EntityFrameworkCore;
using SineahBot.Data;
using SineahBot.Database.Entities;
using SineahBot.Database.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static SineahBot.Tools.CharacterCreator;

namespace SineahBot.Tools
{
    public static class CharacterManager
    {

        public static int ExpMultiplier = 2;
        public static int GoldMultiplier = 2;
        public static int RegenerationInterval = 10;
        public static int AlteractionInterval = 2;
        static CharacterManager()
        {
            // start the character regeneration interval
            new MudInterval(RegenerationInterval, () =>
            {
                foreach (var c in characters)
                {
                    c.Value.Regenerate();
                }
            });

            // start the character alteration expiration interval
            new MudInterval(AlteractionInterval, () =>
            {
                foreach (var c in characters)
                {
                    c.Value.TickAlterations(AlteractionInterval);
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
        public static void SavePlayerCharacters()
        {
            var playerCharacters = characters.Values.Where(x => x.agent is Player).ToArray();
            foreach (var character in playerCharacters)
            {
                SaveCharacter(character);
            }
        }
        public static Character GetCharacter(Guid idCharacter)
        {
            if (!characters.ContainsKey(idCharacter))
            {
                var character = LoadCharacter(idCharacter);
                if (character == null) throw new Exception($"Impossible to find character with id {idCharacter}");
                characters[idCharacter] = character;
                character.pronouns = character.pronouns; // update the properties
                ClassProgressionManager.ApplyClassProgressionForCharacter(character, true);
                character.faction = FactionManager.CreatePlayerRepFaction();
                return character;
            }
            return characters[idCharacter];
        }

        public static Character LoadCharacter(Guid id)
        {
            var characterEntity = Program.Database.LoadCharacter(id);
            return Program.Mapper.Map<CharacterEntity, Character>(characterEntity);
        }

        public static void SaveCharacter(Character character)
        {
            var characterEntity = Program.Mapper.Map<Character, CharacterEntity>(character);
            Program.Database.SaveCharacter(characterEntity);
        }

        public static Character CreateCharacterForPlayer(CharacterCreationState state)
        {
            var character = new Character();
            character.agent = state.player;
            character.id = Guid.NewGuid();
            character.name = state.name;
            character.gender = state.gender;
            character.pronouns = state.pronouns;
            character.characterClass = state.characterClass;
            character.level = 1;
            character.experience = 0;
            character.gold = 90;
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
            character.AddToInventory(Data.Templates.Equipments.Weapons.Dagger);
            character.Equip(Data.Templates.Equipments.Weapons.Dagger);
            characters[character.id] = character;
            SaveCharacter(character);
            character.faction = FactionManager.CreatePlayerRepFaction();
            state.player.idCharacter = character.id;
            state.player.character = character;
            return character;
        }

        public static void DeletePlayerCharacter(Player player)
        {
            if (player == null) throw new Exception("Player cannot be null.");
            if (player.character == null || player.idCharacter == null) throw new Exception("This player doesn't have a character to be deleted.");
            var character = player.character;
            // clear character context
            characters.Remove(character.id);
            character.agent = null;
            // clear character data
            RoomManager.RemoveCharacterMessages(character);
            var characterEntity = Program.Mapper.Map<Character, CharacterEntity>(character);
            Program.Database.RemoveCharacter(characterEntity);

            // clear player state and prepare for new character
            player.idCharacter = null;
            player.character = null;
            player.playerStatus = PlayerStatus.CharacterCreation;
        }

        public static Character CreateTestCharacter()
        {
            var character = new Character()
            {
                characterStatus = CharacterStatus.Normal,
                id = Guid.NewGuid(),
                name = "test character",
                //description = "You notice the test character."
            };
            characters[character.id] = character;
            return character;
        }
    }
}
