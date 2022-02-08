using SineahBot.Data;
using SineahBot.Data.Enums;
using SineahBot.Tools.Progression;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBot.Tools
{
    public static class CharacterAncestryManager
    {
        public static CharacterAncestry[] Ancestries = Enum.GetValues<CharacterAncestry>();
        public static CharacterAncestry[] HumanAncestries = { CharacterAncestry.Human };
        public static CharacterAncestry[] MonsterAncestries = { CharacterAncestry.Kobold };
        public static CharacterAncestry[] SecretAncestries = { };

        static CharacterAncestryManager()
        {
            CreateProgression(CharacterAncestry.Human).SetHealth(20, 5).SetMana(10, 2).SetPhysicalPower(3, 1).SetMagicalPower(5, 1).SetHealthRegen(2, 1).SetManaRegen(2, 1);
            CreateProgression(CharacterAncestry.Kobold).SetHealth(24, 6).SetMana(10, 1).SetPhysicalPower(5, 1).SetMagicalPower(3, 1).SetHealthRegen(4, 2).SetManaRegen(1, 0.5);
        }

        public static string GetStartAncestryList()
        {
            return String.Join('/', HumanAncestries.Union(MonsterAncestries));
        }

        private static Dictionary<CharacterAncestry, AncestryProgression> AncestryProgressions = new Dictionary<CharacterAncestry, AncestryProgression>();

        public static void ApplyAncestryProgressionForCharacter(Character character, bool maximize = false)
        {
            var progression = AncestryProgressions[character.characterAncestry];
            progression.ApplyToCharacter(character);
        }

        public static AncestryProgression CreateProgression(CharacterAncestry characterClass)
        {
            return AncestryProgressions[characterClass] = new AncestryProgression(characterClass);
        }

        public class AncestryProgression : ProgressionProfile<AncestryProgression, CharacterAncestry>
        {
            public AncestryProgression(CharacterAncestry characterAncestry) : base(characterAncestry) { }
        }
    }
}
