using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SineahBot.Tools
{
    public static class CombatManager
    {
        public static Dictionary<Guid, Combat> combats = new Dictionary<Guid, Combat>();

        public static List<Character> GetCombatForCharacter(Character character)
        {
            if (!combats.ContainsKey(character.id))
            {
                var combat = new Combat();
                combat.Add(character);
                combats[character.id] = combat;
                return combat;
            }
            return combats[character.id];
        }
        public static void RemoveFromCombat(Character character) {
            var combat = GetCombatForCharacter(character);
            if(combat != null) {
                combat.Remove(character);
                combats.Remove(character.id);
            }
        }
        public static void AddToCombat(Character character, Combat combat)
        {
            combat.Add(character);
            combats[character.id] = combat;
        }
    }
}
