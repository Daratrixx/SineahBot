using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SineahBot.Tools
{
    public static class ClassProgressionManager
    {
        public static CharacterClass[] starterClass = new CharacterClass[] { CharacterClass.Militian, CharacterClass.Scholar };
        public static CharacterClass[] physicalClass = new CharacterClass[] { CharacterClass.Militian,
        CharacterClass.Guard, CharacterClass.Footman, CharacterClass.Knight,
        CharacterClass.Ranger, CharacterClass.Archer, CharacterClass.Sharpshooter};
        public static CharacterClass[] magicalClass = new CharacterClass[] { CharacterClass.Scholar,
        CharacterClass.Abbot, CharacterClass.Prelate, CharacterClass.Bishop,
        CharacterClass.Enchanter, CharacterClass.Mage, CharacterClass.Wizard};
        public static string GetStartClassListString()
        {
            return String.Join('/', starterClass.Select(x => x.ToString()));
        }
        public static bool IsStartingClass(CharacterClass characterClass)
        {
            return starterClass.Contains(characterClass);
        }
        public static bool IsPhysicalClass(CharacterClass characterClass)
        {
            return physicalClass.Contains(characterClass);
        }
        public static bool IsMagicalClass(CharacterClass characterClass)
        {
            return magicalClass.Contains(characterClass);
        }
        private static Dictionary<CharacterClass, ClassProgression> classProgressions = new Dictionary<CharacterClass, ClassProgression>()
        {
            // physical path
            { CharacterClass.Militian, new ClassProgression(CharacterClass.Militian, 20, 10)
            .RegisterSubclass(CharacterClass.Guard, 3)
            .RegisterSubclass(CharacterClass.Ranger, 3) },
            // melee path
            { CharacterClass.Guard, new ClassProgression(CharacterClass.Guard, 30, 13).RegisterSubclass(CharacterClass.Footman, 5) },
            { CharacterClass.Footman, new ClassProgression(CharacterClass.Footman, 40, 16).RegisterSubclass(CharacterClass.Knight, 8) },
            { CharacterClass.Knight, new ClassProgression(CharacterClass.Knight, 50, 19) },
            // range path
            { CharacterClass.Ranger, new ClassProgression(CharacterClass.Ranger, 25, 12).RegisterSubclass(CharacterClass.Archer, 5) },
            { CharacterClass.Archer, new ClassProgression(CharacterClass.Archer, 30, 14).RegisterSubclass(CharacterClass.Sharpshooter, 8) },
            { CharacterClass.Sharpshooter, new ClassProgression(CharacterClass.Sharpshooter, 35, 16) },

            // mental path
            { CharacterClass.Scholar, new ClassProgression(CharacterClass.Scholar, 16, 8, 15, 3)
            .RegisterSubclass(CharacterClass.Abbot, 2)
            .RegisterSubclass(CharacterClass.Enchanter, 2) },
            // faith path
            { CharacterClass.Abbot, new ClassProgression(CharacterClass.Abbot, 20, 10, 15, 3).RegisterSubclass(CharacterClass.Prelate, 4) },
            { CharacterClass.Prelate, new ClassProgression(CharacterClass.Prelate, 25, 12, 15, 3).RegisterSubclass(CharacterClass.Bishop, 7) },
            { CharacterClass.Bishop, new ClassProgression(CharacterClass.Bishop, 30, 14, 15, 3) },
            // magic path
            { CharacterClass.Enchanter, new ClassProgression(CharacterClass.Enchanter, 20, 10, 20, 4).RegisterSubclass(CharacterClass.Mage, 4) },
            { CharacterClass.Mage, new ClassProgression(CharacterClass.Mage, 24, 11, 20, 4).RegisterSubclass(CharacterClass.Wizard, 7) },
            { CharacterClass.Wizard, new ClassProgression(CharacterClass.Wizard, 28, 12, 20, 4) }
        };
        public static void ApplyClassProgressionForCharacter(Character character, bool maximize = false)
        {
            var progression = classProgressions[character.characterClass];
            character.maxHealth = progression.baseHealth + character.level * progression.levelHealth;
            character.maxMana = progression.baseMana + character.level * progression.levelMana;
            if (maximize)
            {
                character.health = character.maxHealth;
                character.mana = character.maxMana;
            }
        }
        public static KeyValuePair<CharacterClass, int>[] GetAvailableClassChange(Character character)
        {
            var classProgression = classProgressions[character.characterClass];
            if (classProgression != null)
            {
                return classProgression.GetSubClass().Where(x => x.Value <= character.level).ToArray();
            }
            return new KeyValuePair<CharacterClass, int>[0];
        }
        public static int ExperienceForNextLevel(int level)
        {
            return level * 200;
        }

        public class ClassProgression
        {
            public ClassProgression(CharacterClass characterClass, int baseHealth, int levelHealth, int baseMana = 10, int levelMana = 2)
            {
                this.characterClass = characterClass;
                this.baseHealth = baseHealth;
                this.levelHealth = levelHealth;
                this.baseMana = baseMana;
                this.levelMana = levelMana;
            }
            public CharacterClass characterClass;
            public int baseHealth;
            public int levelHealth;
            public int baseMana;
            public int levelMana;
            public Dictionary<CharacterClass, int> subclassAtLevel = new Dictionary<CharacterClass, int>();

            public ClassProgression RegisterSubclass(CharacterClass subclass, int level)
            {
                subclassAtLevel[subclass] = level;
                return this;
            }
            public KeyValuePair<CharacterClass, int>[] GetSubClass()
            {
                return subclassAtLevel.ToArray();
            }
        }
    }
}
