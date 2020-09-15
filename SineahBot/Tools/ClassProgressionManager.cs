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
        public static CharacterClass[] secretClass = new CharacterClass[] { CharacterClass.Druid };
        public static CharacterClass[] physicalClass = new CharacterClass[] { CharacterClass.Militian,
        CharacterClass.Guard, CharacterClass.Footman, CharacterClass.Knight,
        CharacterClass.Ranger, CharacterClass.Archer, CharacterClass.Sharpshooter,
        CharacterClass.Druid };
        public static CharacterClass[] magicalClass = new CharacterClass[] { CharacterClass.Scholar,
        CharacterClass.Abbot, CharacterClass.Prelate, CharacterClass.Bishop,
        CharacterClass.Enchanter, CharacterClass.Mage, CharacterClass.Wizard,
        CharacterClass.Druid };

        public static string GetStartClassListString()
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
            { CharacterClass.Abbot, new ClassProgression(CharacterClass.Abbot, 20, 10, 15, 3)
            .RegisterSpell(Spell.MinorHealing, 2)
            .RegisterSubclass(CharacterClass.Prelate, 4) },
            { CharacterClass.Prelate, new ClassProgression(CharacterClass.Prelate, 25, 12, 15, 3)
            .RegisterSpell(Spell.MinorHealing, 2)
            .RegisterSpell(Spell.MajorHealing, 4)
            .RegisterSubclass(CharacterClass.Bishop, 7) },
            { CharacterClass.Bishop, new ClassProgression(CharacterClass.Bishop, 30, 14, 15, 3)
            .RegisterSpell(Spell.MinorHealing, 2)
            .RegisterSpell(Spell.MajorHealing, 4)
            .RegisterSpell(Spell.DivineHand, 7) },
            // magic path
            { CharacterClass.Enchanter, new ClassProgression(CharacterClass.Enchanter, 20, 10, 20, 4)
            .RegisterSpell(Spell.MagicDart, 2)
            .RegisterSubclass(CharacterClass.Mage, 4) },
            { CharacterClass.Mage, new ClassProgression(CharacterClass.Mage, 24, 11, 20, 4)
            .RegisterSpell(Spell.MagicDart, 2)
            .RegisterSpell(Spell.ArcaneBlast, 4)
            .RegisterSubclass(CharacterClass.Wizard, 7) },
            { CharacterClass.Wizard, new ClassProgression(CharacterClass.Wizard, 28, 12, 20, 4)
            .RegisterSpell(Spell.MagicDart, 2)
            .RegisterSpell(Spell.ArcaneBlast, 4)
            .RegisterSpell(Spell.Overcharge, 7) },

            // secret class
            { CharacterClass.Druid, new ClassProgression(CharacterClass.Druid, 20, 15, 15, 3)
            .RegisterSpell(Spell.MinorHealing, 2)
            .RegisterSpell(Spell.MagicDart, 4)
            .RegisterSpell(Spell.MajorHealing, 6) },
        };
        private static Dictionary<CharacterClass, string> classDescription = new Dictionary<CharacterClass, string>() {
            { CharacterClass.Militian, "Citizen following the path of arms, ready to take and give a beating." },
            { CharacterClass.Guard, "Experimented fighter that can hold their ground on their own." },
            { CharacterClass.Footman, "Veteran combatant that can easily dispatch monsters." },
            { CharacterClass.Knight, "Elite soldier, they will be a precious ally or a fierceful adversary." },
            { CharacterClass.Ranger, "Fighter able to use range to their advantage." },
            { CharacterClass.Archer, "Combatant well versed in ranged warfare." },
            { CharacterClass.Sharpshooter, "Elite fighter destroying their ennemies from afar." },
            { CharacterClass.Scholar, "Citizen following the path of knowledge, bound to discover their latent abilities." },
            { CharacterClass.Abbot, "Follower a religious order, capable of minor healing feats." },
            { CharacterClass.Prelate, "Influent figure of a religious order, invoking powerful miracles" },
            { CharacterClass.Bishop, "High ranking figure of a religious order, harnessing the wrath and mercy of gods." },
            { CharacterClass.Enchanter, "A low rank magic user, only capable of causing damages and troubles." },
            { CharacterClass.Mage, "A seasoned magic caster, with a large array of spells at their disposal." },
            { CharacterClass.Wizard, "Incredibly powerful being, capable of wiping armies in on spell." },
            { CharacterClass.Druid, "Mysterious protector of nature, both a fierce warrior and a spiritual guide." },
        };

        public static string GetClassDescription(CharacterClass characterClass)
        {
            return classDescription[characterClass];
        }
        public static void ApplyClassProgressionForCharacter(Character character, bool maximize = false)
        {
            var progression = classProgressions[character.characterClass];
            character.maxHealth = progression.baseHealth + character.level * progression.levelHealth;
            character.maxMana = progression.baseMana + character.level * progression.levelMana;
            character.spells = progression.availableSpells.Where(x => x.Value <= character.level).Select(x => x.Key).ToArray();
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
