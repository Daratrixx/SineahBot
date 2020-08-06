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

        }
        public static Character GetCharacter(Guid idCharacter)
        {
            return characters[idCharacter];
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
