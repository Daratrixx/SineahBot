using SineahBot.Data;
using SineahBot.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBot.Tools
{
    public static class CharacterClassManager
    {
        public static CharacterClass[] starterClass = new CharacterClass[] { CharacterClass.Militian, CharacterClass.Scholar };
        public static CharacterClass[] secretClass = new CharacterClass[] { CharacterClass.Druid, CharacterClass.Necromancer, CharacterClass.Rogue, CharacterClass.Barbarian };
        public static CharacterClass[] physicalClass = new CharacterClass[] {CharacterClass.Militian,
        CharacterClass.Guard, CharacterClass.Footman,
        CharacterClass.Knight, CharacterClass.Paladin,
        /*CharacterClass.Ranger, CharacterClass.Archer, CharacterClass.Sharpshooter,*/
        CharacterClass.Druid, CharacterClass.Rogue, CharacterClass.Assassin, CharacterClass.Barbarian };
        public static CharacterClass[] magicalClass = new CharacterClass[] { CharacterClass.Scholar,
        CharacterClass.Abbot, CharacterClass.Prelate, CharacterClass.Bishop,
        CharacterClass.Enchanter, CharacterClass.Mage, CharacterClass.Wizard,
        CharacterClass.Paladin,
        CharacterClass.Druid, CharacterClass.Necromancer, CharacterClass.Lich };

        public static string GetStartClassList()
        {
            return String.Join('/', starterClass.Select(x => x.ToString()));
        }
        public static bool IsStartingClass(CharacterClass characterClass)
        {
            return starterClass.Contains(characterClass) || secretClass.Contains(characterClass);
        }
        public static bool IsSecretClass(CharacterClass characterClass)
        {
            return secretClass.Contains(characterClass);
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
            .RegisterSpell(Data.Spells.Militian.Protect, 1)
            .RegisterSpell(Data.Spells.Militian.Taunt, 2)
            .RegisterSubclass(CharacterClass.Guard, 3)
            /*.RegisterSubclass(CharacterClass.Ranger, 3)*/ },
            // melee path
            { CharacterClass.Guard, new ClassProgression(CharacterClass.Guard, 30, 13)
            .RegisterSpell(Data.Spells.Militian.Protect, 1)
            .RegisterSpell(Data.Spells.Militian.Taunt, 2)
            .RegisterSubclass(CharacterClass.Footman, 5) },
            { CharacterClass.Footman, new ClassProgression(CharacterClass.Footman, 40, 16)
            .RegisterSpell(Data.Spells.Militian.Protect, 1)
            .RegisterSpell(Data.Spells.Militian.Taunt, 2)
            .RegisterSubclass(CharacterClass.Knight, 8).RegisterSubclass(CharacterClass.Paladin, 8) },
            { CharacterClass.Knight, new ClassProgression(CharacterClass.Knight, 50, 19)
            .RegisterSpell(Data.Spells.Militian.Protect, 1)
            .RegisterSpell(Data.Spells.Militian.Taunt, 2) },
            { CharacterClass.Paladin, new ClassProgression(CharacterClass.Paladin, 40, 16)
            .RegisterSpell(Data.Spells.Militian.Protect, 1)
            .RegisterSpell(Data.Spells.Militian.Taunt, 2)
            .RegisterSpell(Data.Spells.Priest.MinorHealing, 10)
            .RegisterSpell(Data.Spells.Priest.Smite, 12) },
            // range path
            { CharacterClass.Ranger, new ClassProgression(CharacterClass.Ranger, 25, 12).RegisterSubclass(CharacterClass.Archer, 5) },
            { CharacterClass.Archer, new ClassProgression(CharacterClass.Archer, 30, 14).RegisterSubclass(CharacterClass.Sharpshooter, 8) },
            { CharacterClass.Sharpshooter, new ClassProgression(CharacterClass.Sharpshooter, 35, 16) },

            // mental path
            { CharacterClass.Scholar, new ClassProgression(CharacterClass.Scholar, 16, 8, 15, 3)
            .RegisterSpell(Data.Spells.Scholar.FirstAid, 1)
            .RegisterSubclass(CharacterClass.Abbot, 2)
            .RegisterSubclass(CharacterClass.Enchanter, 2) },
            // faith path
            { CharacterClass.Abbot, new ClassProgression(CharacterClass.Abbot, 20, 10, 15, 3)
            .RegisterSpell(Data.Spells.Scholar.FirstAid, 1)
            .RegisterSpell(Data.Spells.Priest.MinorHealing, 2)
            .RegisterSpell(Data.Spells.Priest.Cure, 3)
            .RegisterSubclass(CharacterClass.Prelate, 4) },
            { CharacterClass.Prelate, new ClassProgression(CharacterClass.Prelate, 25, 12, 15, 3)
            .RegisterSpell(Data.Spells.Scholar.FirstAid, 1)
            .RegisterSpell(Data.Spells.Priest.MinorHealing, 2)
            .RegisterSpell(Data.Spells.Priest.Cure, 3)
            .RegisterSpell(Data.Spells.Priest.MajorHealing, 4)
            .RegisterSpell(Data.Spells.Priest.Smite, 5)
            .RegisterSubclass(CharacterClass.Bishop, 7) },
            { CharacterClass.Bishop, new ClassProgression(CharacterClass.Bishop, 30, 14, 15, 3)
            .RegisterSpell(Data.Spells.Scholar.FirstAid, 1)
            .RegisterSpell(Data.Spells.Priest.MinorHealing, 2)
            .RegisterSpell(Data.Spells.Priest.Cure, 3)
            .RegisterSpell(Data.Spells.Priest.MajorHealing, 4)
            .RegisterSpell(Data.Spells.Priest.Smite, 5)
            .RegisterSpell(Data.Spells.Priest.DivineHand, 6)
            .RegisterSpell(Data.Spells.Priest.DivineMight, 10) },
            // magic path
            { CharacterClass.Enchanter, new ClassProgression(CharacterClass.Enchanter, 20, 10, 20, 4)
            .RegisterSpell(Data.Spells.Scholar.FirstAid, 1)
            .RegisterSpell(Data.Spells.Mage.MagicDart, 2)
            .RegisterSpell(Data.Spells.Mage.Amplify, 3)
            .RegisterSubclass(CharacterClass.Mage, 4) },
            { CharacterClass.Mage, new ClassProgression(CharacterClass.Mage, 24, 11, 20, 4)
            .RegisterSpell(Data.Spells.Scholar.FirstAid, 1)
            .RegisterSpell(Data.Spells.Mage.MagicDart, 2)
            .RegisterSpell(Data.Spells.Mage.Amplify, 3)
            .RegisterSpell(Data.Spells.Mage.ArcaneBlast, 4)
            .RegisterSpell(Data.Spells.Mage.Incinerate, 5)
            .RegisterSubclass(CharacterClass.Wizard, 7) },
            { CharacterClass.Wizard, new ClassProgression(CharacterClass.Wizard, 28, 12, 20, 4)
            .RegisterSpell(Data.Spells.Scholar.FirstAid, 1)
            .RegisterSpell(Data.Spells.Mage.MagicDart, 2)
            .RegisterSpell(Data.Spells.Mage.Amplify, 3)
            .RegisterSpell(Data.Spells.Mage.ArcaneBlast, 4)
            .RegisterSpell(Data.Spells.Mage.Incinerate, 5)
            .RegisterSpell(Data.Spells.Mage.Overcharge, 7)
            .RegisterSpell(Data.Spells.Mage.AbsoluteZero, 10) },

            // secret class
            { CharacterClass.Druid, new ClassProgression(CharacterClass.Druid, 20, 15, 15, 3)
            .RegisterSpell(Data.Spells.Druid.HealingTouch, 2)
            .RegisterSpell(Data.Spells.Druid.Hibernate, 3)
            .RegisterSpell(Data.Spells.Druid.Wrath, 3)
            .RegisterSpell(Data.Spells.Druid.Metabolize, 4)
            .RegisterSpell(Data.Spells.Druid.VenomousWhip, 5)
            .RegisterSpell(Data.Spells.Druid.Animate, 6) },
            { CharacterClass.Shaman, new ClassProgression(CharacterClass.Shaman, 20, 17, 10, 3)},
            { CharacterClass.Necromancer, new ClassProgression(CharacterClass.Necromancer, 24, 13, 15, 4)
            .RegisterSpell(Data.Spells.Necromancer.WitheringTouch, 2)
            .RegisterSpell(Data.Spells.Necromancer.BoneArmor, 3)
            .RegisterSpell(Data.Spells.Necromancer.BoneSlash, 5)
            .RegisterSpell(Data.Spells.Necromancer.Frenzy, 6)
            .RegisterSubclass(CharacterClass.Lich,10)},
            { CharacterClass.Lich, new ClassProgression(CharacterClass.Lich, 24, 14, 15, 5)
            .RegisterSpell(Data.Spells.Necromancer.WitheringTouch, 2)
            .RegisterSpell(Data.Spells.Necromancer.BoneArmor, 3)
            .RegisterSpell(Data.Spells.Necromancer.BoneSlash, 5)
            .RegisterSpell(Data.Spells.Necromancer.Frenzy, 6)},
            { CharacterClass.Rogue, new ClassProgression(CharacterClass.Rogue, 20, 11, 10, 3)
            .RegisterSpell(Data.Spells.Rogue.PoisonDart, 2)
            .RegisterSpell(Data.Spells.Rogue.Bleedout, 4)
            .RegisterSubclass(CharacterClass.Assassin, 10)},
            { CharacterClass.Assassin, new ClassProgression(CharacterClass.Assassin, 30, 14, 15, 3)
            .RegisterSpell(Data.Spells.Rogue.PoisonDart, 2)
            .RegisterSpell(Data.Spells.Rogue.Bleedout, 4)
            .RegisterSpell(Data.Spells.Rogue.Disapear, 10)},
            { CharacterClass.Barbarian, new ClassProgression(CharacterClass.Barbarian, 20, 16, 10, 2)
            .RegisterSpell(Data.Spells.Barbarian.Anger, 1)
            .RegisterSpell(Data.Spells.Barbarian.Fury, 2)},
        };

        private static Dictionary<CharacterClass, string> characterClassDescription = new Dictionary<CharacterClass, string>()
        {
            { CharacterClass.Militian, "" },
            { CharacterClass.Guard, "" },
            { CharacterClass.Footman, "" },
            { CharacterClass.Knight, "" },
            { CharacterClass.Ranger, "" },
            { CharacterClass.Archer, "" },
            { CharacterClass.Sharpshooter, "" },
            { CharacterClass.Scholar, "" },
            { CharacterClass.Abbot, "" },
            { CharacterClass.Prelate, "" },
            { CharacterClass.Bishop, "" },
            { CharacterClass.Enchanter, "" },
            { CharacterClass.Mage, "" },
            { CharacterClass.Wizard, "" },
            { CharacterClass.Druid, "Strange tattoos cover their body." },
            { CharacterClass.Shaman, "Mysterious protector of nature, both a fierce warrior and a spiritual guide." },
            { CharacterClass.Necromancer, "They are pale and smell of death." },
        };
        public static void ApplyClassProgressionForCharacter(Character character, bool maximize = false)
        {
            var progression = classProgressions[character.characterClass];
            character.baseHealth = progression.baseHealth + character.level * progression.levelHealth;
            character.baseMana = progression.baseMana + character.level * progression.levelMana;
            character.baseSpells.Clear();
            character.baseSpells.AddRange(progression.availableSpells.Where(x => x.Value <= character.level).Select(x => x.Key));
            if (maximize)
            {
                character.health = character.MaxHealth;
                character.mana = character.MaxMana;
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
            public Dictionary<Spell, int> availableSpells = new Dictionary<Spell, int>();

            public ClassProgression RegisterSubclass(CharacterClass subclass, int level)
            {
                subclassAtLevel[subclass] = level;
                return this;
            }
            public ClassProgression RegisterSpell(Spell spell, int level)
            {
                availableSpells[spell] = level;
                return this;
            }
            public KeyValuePair<CharacterClass, int>[] GetSubClass()
            {
                return subclassAtLevel.ToArray();
            }
        }
    }
}
