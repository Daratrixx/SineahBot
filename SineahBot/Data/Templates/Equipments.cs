using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class Equipments
    {
        // health
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
            bonusDamage = 12,
        };
        public static Equipment Axe = new Equipment("Axe", EquipmentSlot.Weapon, new string[] { })
        {
            description = "A axe lies around.",
            details = "A dangerous looknig axe. Gives a decent damage and health boost.",
            bonusDamage = 8,
            bonusHealth = 20,
        };
        public static Equipment Mace = new Equipment("Mace", EquipmentSlot.Weapon, new string[] { })
        {
            description = "A mace lies around.",
            details = "A reliable mace. Gives a decent damage and armor boost.",
            bonusDamage = 8,
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
            details = "A long spear. Gives a small damage boost and a decent armor and health boost.",
            bonusDamage = 5,
            bonusArmor = 5,
            bonusHealth = 20,
        };
        public static Equipment HolySymbol = new Equipment("Holy symbol", EquipmentSlot.Weapon, new string[] { "holysymbol", "symbol" })
        {
            description = "A holy symbols thrones here.",
            details = "Symbols of the gods. Gives a small armor and spell power boost.",
            bonusArmor = 5,
            bonusSpellPower = 5,
        };
        public static Equipment SearingBlade = new Equipment("Searing blade", EquipmentSlot.Weapon, new string[] { "searingblade" })
        {
            description = "A holy symbols thrones here.",
            details = "Blade infused with fire. Gives a small damage bonus, and the ability to cast Ignite.",
            bonusDamage = 5,
            bonusSpells = new List<Spell>() { Spell.Ignite },
        };
    }
}
