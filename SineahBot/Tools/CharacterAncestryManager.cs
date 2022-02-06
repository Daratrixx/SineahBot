using SineahBot.Data;
using SineahBot.Data.Enums;
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
            CreateProgression(CharacterAncestry.Human).SetHealth(20, 5).SetMana(10, 2).SetPhysicalPower(3, 1).SetMagicalPower(4, 1.5).SetHealthRegen(2, 1).SetManaRegen(2, 1);
            CreateProgression(CharacterAncestry.Kobold).SetHealth(24, 6).SetMana(10, 1).SetPhysicalPower(4, 1.5).SetMagicalPower(3, 1).SetHealthRegen(4, 2).SetManaRegen(1, 0.5);
        }

        public static string GetStartAncestryList()
        {
            return String.Join('/', HumanAncestries.Union(MonsterAncestries));
        }

        private static Dictionary<CharacterAncestry, AncestryProgression> AncestryProgressions = new Dictionary<CharacterAncestry, AncestryProgression>();

        public static void ApplyAncestryProgressionForCharacter(Character character, bool maximize = false)
        {
            var statLevel = character.level - 1;
            var progression = AncestryProgressions[character.characterAncestry];
            character.baseHealth = progression.BaseHealth + (int)Math.Round(statLevel * progression.IncrementHealth);
            character.baseMana = progression.BaseMana + (int)Math.Round(statLevel * progression.IncrementMana);
            character.basePhysicalPower = progression.BasePhysicalPower + (int)Math.Round(statLevel * progression.IncrementPhysicalPower);
            character.baseMagicalPower = progression.BaseMagicalPower + (int)Math.Round(statLevel * progression.IncrementMagicalPower);
            character.baseHealthRegen = progression.BaseHealthRegen + (int)Math.Round(statLevel * progression.IncrementHealthRegen);
            character.baseManaRegen = progression.BaseManaRegen + (int)Math.Round(statLevel * progression.IncrementManaRegen);
            character.baseSpells.AddRange(progression.AvailableSpells.Where(x => x.Value <= character.level).Select(x => x.Key));
            if (maximize)
            {
                character.health = character.MaxHealth;
                character.mana = character.MaxMana;
            }
        }

        public static AncestryProgression CreateProgression(CharacterAncestry characterClass)
        {
            return AncestryProgressions[characterClass] = new AncestryProgression(characterClass);
        }

        public class AncestryProgression
        {
            public AncestryProgression(CharacterAncestry characterClass)
            {
                this.CharacterAncestry = characterClass;
            }
            public CharacterAncestry CharacterAncestry;

            public int BaseHealth;
            public double IncrementHealth;

            public int BaseMana;
            public double IncrementMana;

            public int BasePhysicalPower;
            public double IncrementPhysicalPower;

            public int BaseMagicalPower;
            public double IncrementMagicalPower;

            public int BaseHealthRegen;
            public double IncrementHealthRegen;

            public int BaseManaRegen;
            public double IncrementManaRegen;

            public Dictionary<Spell, int> AvailableSpells = new Dictionary<Spell, int>();

            public AncestryProgression SetHealth(int baseValue, double levelIncrement = 0)
            {
                this.BaseHealth = baseValue;
                this.IncrementHealth = levelIncrement;
                return this;
            }

            public AncestryProgression SetMana(int baseValue, double levelIncrement = 0)
            {
                this.BaseMana = baseValue;
                this.IncrementMana = levelIncrement;
                return this;
            }

            public AncestryProgression SetPhysicalPower(int baseValue, double levelIncrement = 0)
            {
                this.BasePhysicalPower = baseValue;
                this.IncrementPhysicalPower = levelIncrement;
                return this;
            }

            public AncestryProgression SetMagicalPower(int baseValue, double levelIncrement = 0)
            {
                this.BaseMagicalPower = baseValue;
                this.IncrementMagicalPower = levelIncrement;
                return this;
            }

            public AncestryProgression SetHealthRegen(int baseValue, double levelIncrement = 0)
            {
                this.BaseHealthRegen = baseValue;
                this.IncrementHealthRegen = levelIncrement;
                return this;
            }

            public AncestryProgression SetManaRegen(int baseValue, double levelIncrement = 0)
            {
                this.BaseManaRegen = baseValue;
                this.IncrementManaRegen = levelIncrement;
                return this;
            }

            public AncestryProgression AddSpell(Spell spell, int level)
            {
                this.AvailableSpells[spell] = level;
                return this;
            }
        }
    }
}
