using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Combat : List<Character>
    {
        private Dictionary<Character, List<Character>> allies = new Dictionary<Character, List<Character>>();
        private Dictionary<Character, List<Character>> foes = new Dictionary<Character, List<Character>>();

        public void CharacterAttack(Character attacking, Character target)
        {

        }
        public void CharacterProtect(Character protecting, Character target)
        {

        }
        public void CharacterHeal(Character healing, Character target)
        {

        }
    }
}
