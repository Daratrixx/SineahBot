using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class Equipments
    {
        // weapons
        public static Equipment Dagger = new Equipment("Dagger", EquipmentSlot.Weapon, new string[] { })
        {
            description = "A dagger lies around.",
            details = "A sharp, quick dagger. Gives a small damage and mana boost",
            bonusDamage = 5,
            bonusMana = 10
        };
        public static Equipment Sword = new Equipment("Sword", EquipmentSlot.Weapon, new string[] { })
        {
            description = "A sword lies around.",
            details = "A standard, well-made sword. Gives a large damage boost.",
            bonusDamage = 20,
        };
        public static Equipment Axe = new Equipment("Axe", EquipmentSlot.Weapon, new string[] { })
        {
            description = "A axe lies around.",
            details = "A dangerous looknig axe. Gives a decent damage and health boost.",
            bonusDamage = 10,
            bonusHealth = 20,
        };
        public static Equipment Mace = new Equipment("Mace", EquipmentSlot.Weapon, new string[] { })
        {
            description = "A mace lies around.",
            details = "A reliable mace. Gives a decent damage and a small armor boost.",
            bonusDamage = 10,
            bonusArmor = 5,
        };
        public static Equipment Staff = new Equipment("Staff", EquipmentSlot.Weapon, new string[] { })
        {
            description = "A staff lies around.",
            details = "A beautiful sculpted staff. Gives a small damage and spell power boost.",
            bonusDamage = 5,
            bonusSpellPower = 5,
        };
        public static Equipment Spear = new Equipment("Spear", EquipmentSlot.Weapon, new string[] { })
        {
            description = "A spear lies around.",
            details = "A long spear. Gives a small damage and armor boost and a decent health boost.",
            bonusDamage = 5,
            bonusArmor = 5,
            bonusHealth = 20,
        };
        public static Equipment SearingBlade = new Equipment("Searing blade", EquipmentSlot.Weapon, new string[] { "searingblade", "blade" })
        {
            description = "A holy symbols thrones here.",
            details = "Blade infused with fire. Gives a small damage bonus, and the ability to cast Ignite.",
            bonusDamage = 10,
            bonusSpells = new List<Spell>() { Data.Spells.Equipment.Ignite },
        };

        // trinkets
        public static Equipment HolySymbol = new Equipment("Holy symbol", EquipmentSlot.Trinket, new string[] { "holysymbol", "hsymbol", "symbol","trinket" })
        {
            description = "A holy symbols thrones here.",
            details = "Symbols of the gods. Gives a small armor and spell power boost.",
            bonusArmor = 5,
            bonusSpellPower = 5,
        };
        public static Equipment LichBones = new Equipment("Lich bones", EquipmentSlot.Trinket, new string[] { "lichbones","lichbone", "lbones", "lbone", "lichb","lich","bones","bone","lb", "trinket" })
        {
            description = "Some lich bones throne here.",
            details = "Bones from a defeated lich. Gives a small mana and spell power boost.",
            bonusMana = 10,
            bonusSpellPower = 5,
        };
        public static Equipment SineahEmblem = new Equipment("Emblem of Sineah", EquipmentSlot.Trinket, new string[] { "sineahemblem", "sineah", "emblem", "trinket" })
        {
            description = "An emblem of Sineah throne here.",
            details = "Emblem bestowed upon heros of Sineah. Gives a large health boost.",
            bonusHealth = 40,
        };

        //
        public static Equipment BladeRing = new Equipment("Blade ring", EquipmentSlot.Ring, new string[] { "bladering", "blader", "bring", "br", "ring" })
        {
            description = "A blade ring thrones here.",
            details = "Ring engraved with a dagger. Gives a small damage boost.",
            bonusDamage = 5,
        };
        public static Equipment HealthRing = new Equipment("Health ring", EquipmentSlot.Ring, new string[] { "healthring", "healthr", "hring", "hr", "ring" })
        {
            description = "A blade ring thrones here.",
            details = "Ring engraved with a dagger. Gives a decent health boost.",
            bonusHealth = 20,
        };
        public static Equipment IronRing = new Equipment("Iron ring", EquipmentSlot.Ring, new string[] { "ironring", "ironr", "iring", "ir", "ring" })
        {
            description = "An iron ring thrones here.",
            details = "Simple iron ring. Gives a small armor boost.",
            bonusArmor = 5,
        };

        // armor
        public static Equipment MilitianArmor = new Equipment("Militian armor", EquipmentSlot.Armor, new string[] { "militianarmor", "marmor", "armor" })
        {
            description = "A militian armor is stored here.",
            details = "Simple protective gear. Gives a little bonus to armor.",
            bonusArmor = 5,
        };
        public static Equipment GuardArmor = new Equipment("Guard armor", EquipmentSlot.Armor, new string[] { "guardarmor", "garmor", "armor" })
        {
            description = "A guard armor is stored here.",
            details = "Fairly good protective gear. Gives a decent bonus to armor.",
            bonusArmor = 10,
        };
        public static Equipment KnightArmor = new Equipment("Knight armor", EquipmentSlot.Armor, new string[] { "knightarmor", "karmor", "armor" })
        {
            description = "A guard armor is stored here.",
            details = "Elite protective gear. Gives a large bonus to armor.",
            bonusArmor = 20,
        };
        public static Equipment PriestRobes = new Equipment("Priest robes", EquipmentSlot.Armor, new string[] { "priestrobes", "robes", "robe" })
        {
            description = "Some priest robes are stashed here.",
            details = "Low-level god servant gear. Gives a decent health bonus and a small mana bonus.",
            bonusMana = 10,
            bonusHealth = 20,
        };
        public static Equipment TemplarRobes = new Equipment("Templar robes", EquipmentSlot.Armor, new string[] { "templarrobes", "robes", "robe" })
        {
            description = "Some templar robes are stashed here.",
            details = "High-level god servant gear. Gives a decent health bonus and a small spell power bonus.",
            bonusSpellPower = 5,
            bonusHealth = 20,
        };
        public static Equipment FanaticRobes = new Equipment("Fanatic robes", EquipmentSlot.Armor, new string[] { "fanaticrobes", "robes", "robe" })
        {
            description = "Some fanatic robes are stashed here.",
            details = "High-level god servant gear. Gives a decent health bonus and a small damage bonus.",
            bonusDamage = 5,
            bonusHealth = 20,
        };
        public static Equipment EnchanterCloak = new Equipment("Enchanter cloak", EquipmentSlot.Armor, new string[] { "enchantercloak", "ecloak", "cloak" })
        {
            description = "An enchanter cloak is stashed here.",
            details = "Low-level magic user gear. Gives a small mana bonus.",
            bonusMana = 10,
        };
        public static Equipment MageCloak = new Equipment("Mage cloak", EquipmentSlot.Armor, new string[] { "magecloak", "mcloak", "cloak" })
        {
            description = "A mage cloak is stashed here.",
            details = "High-level magic user gear. Gives a decent mana bonus and a small armor bonus.",
            bonusArmor = 5,
            bonusMana = 20,
        };
        public static Equipment WizardCloak = new Equipment("Wizard cloak", EquipmentSlot.Armor, new string[] { "wizardcloak", "wcloak", "cloak" })
        {
            description = "A wizard cloak is stashed here.",
            details = "Elite magic user gear. Gives a large mana bonus, and a small armor and spell power bonus.",
            bonusArmor = 5,
            bonusSpellPower = 5,
            bonusMana = 40,
        };
        public static Equipment ShadowCloak = new Equipment("Shadow cloak", EquipmentSlot.Armor, new string[] { "shadowcloak", "scloak", "cloak" })
        {
            description = "A shadow cloak is stashed here.",
            details = "Gives a decent damage bonus, and hides your identity to other characters.",
            bonusDamage = 10,
            OnEquipped = (x) => { x.AddAlteration(AlterationType.Shrouded, int.MaxValue); },
            OnUnequipped = (x) => { x.RemoveAlteration(AlterationType.Shrouded); }
        };
    }
}
