using SineahBot.Data;
using System;
using System.Collections.Generic;
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
            return characters[idCharacter];
        }

        public static Character CreateCharacterForPlayer(Player player)
        {
            var character = new Character();
            character.id = Guid.NewGuid();
            character.name = player.characterName;
            character.agent = player;
            player.character = character;
            characters[character.id] = character;
            return character;
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
                    description = "You notice the test character."
                };
            }
        }
    }
}
