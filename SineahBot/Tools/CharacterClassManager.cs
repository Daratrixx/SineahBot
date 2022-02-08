using SineahBot.Data;
using SineahBot.Data.Enums;
using SineahBot.Tools.Progression;
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

        static CharacterClassManager()
        {
            // physical path
            CreateProgression(CharacterClass.Militian).SetHealth(10, 2).SetPhysicalPower(2, 1)
                .AddSpell(Data.Spells.Militian.Protect, 1)
                .AddSpell(Data.Spells.Militian.Taunt, 2)
                .AddSubclass(CharacterClass.Guard, 3);

            // melee path
            CreateProgression(CharacterClass.Guard).SetHealth(20, 2).SetPhysicalPower(4, 1)
                .AddSpell(Data.Spells.Militian.Protect, 1)
                .AddSpell(Data.Spells.Militian.Taunt, 2)
                .AddSubclass(CharacterClass.Footman, 5);
            CreateProgression(CharacterClass.Footman).SetHealth(30, 2).SetPhysicalPower(6, 1)
                .AddSpell(Data.Spells.Militian.Protect, 1)
                .AddSpell(Data.Spells.Militian.Taunt, 2)
                .AddSubclass(CharacterClass.Knight, 8)
                .AddSubclass(CharacterClass.Paladin, 8);
            CreateProgression(CharacterClass.Knight).SetHealth(40, 2).SetPhysicalPower(8, 1)
                .AddSpell(Data.Spells.Militian.Protect, 1)
                .AddSpell(Data.Spells.Militian.Taunt, 2);
            CreateProgression(CharacterClass.Paladin).SetHealth(30, 2).SetMana(20, 1).SetPhysicalPower(6, 1).SetMagicalPower(6, 1)
                .AddSpell(Data.Spells.Militian.Protect, 1)
                .AddSpell(Data.Spells.Militian.Taunt, 2)
                .AddSpell(Data.Spells.Priest.MinorHealing, 8)
                .AddSpell(Data.Spells.Priest.Smite, 10);

            // mental path
            CreateProgression(CharacterClass.Scholar).SetMana(5, 1).SetMagicalPower(2, 1)
                .AddSpell(Data.Spells.Scholar.FirstAid, 1)
                .AddSubclass(CharacterClass.Abbot, 2)
                .AddSubclass(CharacterClass.Enchanter, 2);

            // faith path
            CreateProgression(CharacterClass.Abbot).SetMana(10, 1).SetMagicalPower(4, 1)
                .AddSpell(Data.Spells.Scholar.FirstAid, 1)
                .AddSpell(Data.Spells.Priest.MinorHealing, 2)
                .AddSpell(Data.Spells.Priest.Cure, 3)
                .AddSubclass(CharacterClass.Prelate, 4);
            CreateProgression(CharacterClass.Prelate).SetMana(20, 1).SetMagicalPower(6, 1)
                .AddSpell(Data.Spells.Scholar.FirstAid, 1)
                .AddSpell(Data.Spells.Priest.MinorHealing, 2)
                .AddSpell(Data.Spells.Priest.Cure, 3)
                .AddSpell(Data.Spells.Priest.MajorHealing, 4)
                .AddSpell(Data.Spells.Priest.Smite, 5)
                .AddSubclass(CharacterClass.Bishop, 7);
            CreateProgression(CharacterClass.Bishop).SetMana(30, 1).SetMagicalPower(8, 1)
                .AddSpell(Data.Spells.Scholar.FirstAid, 1)
                .AddSpell(Data.Spells.Priest.MinorHealing, 2)
                .AddSpell(Data.Spells.Priest.Cure, 3)
                .AddSpell(Data.Spells.Priest.MajorHealing, 4)
                .AddSpell(Data.Spells.Priest.Smite, 5)
                .AddSpell(Data.Spells.Priest.DivineHand, 6)
                .AddSpell(Data.Spells.Priest.DivineMight, 10);

            // magic path
            CreateProgression(CharacterClass.Enchanter).SetMana(10, 1).SetMagicalPower(4, 1)
                .AddSpell(Data.Spells.Scholar.FirstAid, 1)
                .AddSpell(Data.Spells.Mage.MagicDart, 2)
                .AddSpell(Data.Spells.Mage.Amplify, 3)
                .AddSubclass(CharacterClass.Mage, 4);
            CreateProgression(CharacterClass.Mage).SetMana(20, 1).SetMagicalPower(6, 1)
                .AddSpell(Data.Spells.Scholar.FirstAid, 1)
                .AddSpell(Data.Spells.Mage.MagicDart, 2)
                .AddSpell(Data.Spells.Mage.Amplify, 3)
                .AddSpell(Data.Spells.Mage.ArcaneBlast, 4)
                .AddSpell(Data.Spells.Mage.Incinerate, 5)
                .AddSubclass(CharacterClass.Wizard, 7);
            CreateProgression(CharacterClass.Wizard).SetMana(30, 1).SetMagicalPower(8, 1)
                .AddSpell(Data.Spells.Scholar.FirstAid, 1)
                .AddSpell(Data.Spells.Mage.MagicDart, 2)
                .AddSpell(Data.Spells.Mage.Amplify, 3)
                .AddSpell(Data.Spells.Mage.ArcaneBlast, 4)
                .AddSpell(Data.Spells.Mage.Incinerate, 5)
                .AddSpell(Data.Spells.Mage.Overcharge, 7)
                .AddSpell(Data.Spells.Mage.AbsoluteZero, 10);

            // secret class
            CreateProgression(CharacterClass.Druid).SetHealth(10, 2).SetMana(5, 1).SetPhysicalPower(3, 1).SetMagicalPower(3, 1)
                .AddSpell(Data.Spells.Druid.HealingTouch, 2)
                .AddSpell(Data.Spells.Druid.Hibernate, 3)
                .AddSpell(Data.Spells.Druid.Wrath, 3)
                .AddSpell(Data.Spells.Druid.Metabolize, 4)
                .AddSpell(Data.Spells.Druid.VenomousWhip, 5)
                .AddSpell(Data.Spells.Druid.Animate, 6);
            CreateProgression(CharacterClass.Shaman).SetMana(10, 1).SetPhysicalPower(1, 1).SetMagicalPower(5, 1);
            CreateProgression(CharacterClass.Necromancer).SetMana(10, 1).SetMagicalPower(3, 1)
                .AddSpell(Data.Spells.Necromancer.WitheringTouch, 2)
                .AddSpell(Data.Spells.Necromancer.BoneArmor, 3)
                .AddSpell(Data.Spells.Necromancer.BoneSlash, 5)
                .AddSpell(Data.Spells.Necromancer.Frenzy, 6);
            CreateProgression(CharacterClass.Lich).SetMana(20, 1).SetMagicalPower(5, 1)
                .AddSpell(Data.Spells.Necromancer.WitheringTouch, 2)
                .AddSpell(Data.Spells.Necromancer.BoneArmor, 3)
                .AddSpell(Data.Spells.Necromancer.BoneSlash, 5)
                .AddSpell(Data.Spells.Necromancer.Frenzy, 6);
            CreateProgression(CharacterClass.Rogue).SetHealth(10, 2).SetMana(5, 1).SetPhysicalPower(3, 1)
                .AddSpell(Data.Spells.Rogue.PoisonDart, 2)
                .AddSpell(Data.Spells.Rogue.Bleedout, 4)
                .AddSubclass(CharacterClass.Assassin, 10);
            CreateProgression(CharacterClass.Assassin).SetHealth(20, 2).SetMana(10, 1).SetPhysicalPower(5, 1)
                .AddSpell(Data.Spells.Rogue.PoisonDart, 2)
                .AddSpell(Data.Spells.Rogue.Bleedout, 4)
                .AddSpell(Data.Spells.Rogue.Disapear, 10);
            CreateProgression(CharacterClass.Barbarian).SetHealth(20, 2).SetPhysicalPower(5, 1)
                .AddSpell(Data.Spells.Barbarian.Anger, 1)
                .AddSpell(Data.Spells.Barbarian.Fury, 2);
        }

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

        private static Dictionary<CharacterClass, ClassProgression> ClassProgressions = new Dictionary<CharacterClass, ClassProgression>();

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
            var progression = ClassProgressions[character.characterClass];
            progression.ApplyToCharacter(character, maximize);
        }
        public static KeyValuePair<CharacterClass, int>[] GetAvailableClassChange(Character character)
        {
            var classProgression = ClassProgressions[character.characterClass];
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

        public static ClassProgression CreateProgression(CharacterClass characterClass)
        {
            return ClassProgressions[characterClass] = new ClassProgression(characterClass);
        }

        public class ClassProgression : ProgressionProfile<ClassProgression, CharacterClass>
        {
            public ClassProgression(CharacterClass characterClass) : base(characterClass) {}

            public Dictionary<CharacterClass, int> SubclassAtLevel = new Dictionary<CharacterClass, int>();

            public ClassProgression AddSubclass(CharacterClass subclass, int level)
            {
                SubclassAtLevel[subclass] = level;
                return this;
            }

            public KeyValuePair<CharacterClass, int>[] GetSubClass()
            {
                return SubclassAtLevel.ToArray();
            }
        }
    }
}
