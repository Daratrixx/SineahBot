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
                if (character == null) throw new Exception($"Impossible to fine character with id {idCharacter}");
                characters[idCharacter] = character;
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
            character.maxHealth = character.health = 30;
            player.idCharacter = character.id;
            player.character = character;
            characters[character.id] = character;
            Program.database.Characters.Add(character);
            return character;
        }

        public static void DeletePlayerCharacter(Player player) {
            if (player == null) throw new Exception("Player cannot be null.");
            if (player.character == null || player.idCharacter == null) throw new Exception("This player doesn't have a character to be deleted.");
            player.idCharacter = null;
            player.playerStatus = PlayerStatus.CharacterCreation;
            player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.None;
            player.characterName = null;
            var character = player.character;
            characters.Remove(character.id);
            Program.database.Characters.Remove(character);
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
