using SineahBot.Data;
using SineahBot.Data.Enums;
using System.Collections.Generic;

namespace SineahBot.Templates
{
    public static class Equipments
    {
        public static class Weapons
        {
            public static Equipment Dagger = new Equipment("Dagger", EquipmentSlot.Weapon, new string[] { })
            {
                description = "A dagger lies around.",
                details = "A sharp, quick dagger. Gives a small damage and mana boost",
                weaponDamageOverwrite = DamageType.Piercing,
                bonusDamage = 5,
                bonusMana = 10,
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.DeepCut, Data.Spells.Equipment.QuickSlash },
            };
            public static Equipment Sword = new Equipment("Sword", EquipmentSlot.Weapon, new string[] { })
            {
                description = "A sword lies around.",
                details = "A standard, well-made sword. Gives a decent damage and deflect boost.",
                weaponDamageOverwrite = DamageType.Slashing,
                bonusDamage = 10,
                bonusDeflection = 5,
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.DeepCut, Data.Spells.Equipment.Thrust, Data.Spells.Equipment.Counterweigt },
            };
            public static Equipment CurvedSword = new Equipment("Curved sword", EquipmentSlot.Weapon, new string[] { })
            {
                description = "A curved sword lies around.",
                details = "An exotic curved sword. Gives a large damage boost.",
                weaponDamageOverwrite = DamageType.Slashing,
                bonusDamage = 20,
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.DeepCut },
            };
            public static Equipment Axe = new Equipment("Axe", EquipmentSlot.Weapon, new string[] { })
            {
                description = "A axe lies around.",
                details = "A dangerous looknig axe. Gives a decent damage and health boost.",
                weaponDamageOverwrite = DamageType.Slashing,
                bonusDamage = 10,
                bonusHealth = 20,
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.DeepCut, Data.Spells.Equipment.CrushingBlow },
            };
            public static Equipment Mace = new Equipment("Mace", EquipmentSlot.Weapon, new string[] { })
            {
                description = "A mace lies around.",
                details = "A reliable mace. Gives a decent damage and a small armor boost.",
                weaponDamageOverwrite = DamageType.Bludgeoning,
                bonusDamage = 10,
                bonusArmor = 5,
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.CrushingBlow },
            };
            public static Equipment Staff = new Equipment("Staff", EquipmentSlot.Weapon, new string[] { })
            {
                description = "A staff lies around.",
                details = "A beautiful sculpted staff. Gives a small damage and spell power boost.",
                weaponDamageOverwrite = DamageType.Bludgeoning,
                bonusDamage = 5,
                bonusSpellPower = 5,
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.CrushingBlow, Data.Spells.Equipment.Counterweigt },
            };
            public static Equipment Spear = new Equipment("Spear", EquipmentSlot.Weapon, new string[] { })
            {
                description = "A spear lies around.",
                details = "A long spear. Gives a small damage and armor boost and a decent health boost.",
                weaponDamageOverwrite = DamageType.Piercing,
                bonusDamage = 5,
                bonusArmor = 5,
                bonusHealth = 20,
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.Thrust, Data.Spells.Equipment.Counterweigt },
            };
            public static Equipment Torch = new Equipment("Torch", EquipmentSlot.Weapon, new string[] { })
            {
                description = "An unlit torch lies there.",
                details = "A simple common torch, that can be lit up and used to attack with Fire. Gives a small damage bonus, and the ability to cast Ignite.",
                weaponDamageOverwrite = DamageType.Fire,
                bonusDamage = 5,
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.Ignite },
            };
            public static Equipment SearingBlade = new Equipment("Searing blade", EquipmentSlot.Weapon, new string[] { "searingblade", "blade" })
            {
                description = "A searing blade lies there.",
                details = "Blade infused with fire. Gives a decent damage bonus, and the ability to cast Ignite.",
                weaponDamageOverwrite = DamageType.Slashing,
                bonusDamage = 5,
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.Ignite, Data.Spells.Equipment.DeepCut, Data.Spells.Equipment.Thrust },
            };
        }

        public static class Shields
        {
            public static Equipment Shield = new Equipment("Shield", EquipmentSlot.Shield, new string[] { })
            {
                description = "A shield lies around.",
                details = "A sharp, quick dagger. Gives a decent deflection boost.",
                bonusDeflection = 10,
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.ShieldBash },
            };
        }
        public static class Trinkets
        {
            public static Equipment HolySymbol = new Equipment("Holy symbol", EquipmentSlot.Trinket, new string[] { "holysymbol", "hsymbol", "symbol", "trinket" })
            {
                description = "A holy symbols thrones here.",
                details = "Symbols of the gods. Gives a small armor and spell power boost.",
                bonusArmor = 5,
                bonusSpellPower = 5,
            };
            public static Equipment LichBones = new Equipment("Lich bones", EquipmentSlot.Trinket, new string[] { "lichbones", "lichbone", "lbones", "lbone", "lichb", "lich", "bones", "bone", "lb", "trinket" })
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
            public static Equipment SewerTag = new Equipment("Sewer tag", EquipmentSlot.Trinket, new string[] { "sewertag", "sewer", "tag", "trinket" })
            {
                description = "A rare trinket lays here!",
                details = "The strongest trinket in the game!",
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.SummonGuardian },
            };
        }

        public static class Rings
        {
            public static Equipment BladeRing = new Equipment("Blade ring", EquipmentSlot.Ring, new string[] { "bladering", "blader", "bring", "br", "ring" })
            {
                description = "A blade ring thrones here.",
                details = "A simple ring engraved with a dagger. Gives a small damage boost.",
                bonusDamage = 5,
            };
            public static Equipment ClawRing = new Equipment("Claw ring", EquipmentSlot.Ring, new string[] { "clawring", "clawr", "cring", "cr", "ring" })
            {
                description = "A claw ring thrones here.",
                details = "A simple ring engraved with a claw. Gives a small damage and armor boost.",
                bonusDamage = 5,
                bonusArmor = 5,
            };
            public static Equipment HealthRing = new Equipment("Health ring", EquipmentSlot.Ring, new string[] { "healthring", "healthr", "hring", "hr", "ring" })
            {
                description = "A health ring thrones here.",
                details = "A silver ring with a small ruby. Gives a decent health boost.",
                bonusHealth = 20,
            };
            public static Equipment ManaRing = new Equipment("Mana ring", EquipmentSlot.Ring, new string[] { "manaring", "manar", "mring", "mr", "ring" })
            {
                description = "A mana ring thrones here.",
                details = "A silver ring with a small sapphire. Gives a decent mana boost.",
                bonusMana = 20,
            };
            public static Equipment IronRing = new Equipment("Iron ring", EquipmentSlot.Ring, new string[] { "ironring", "ironr", "iring", "ir", "ring" })
            {
                description = "An iron ring thrones here.",
                details = "A heavy iron ring. Gives a decent armor boost.",
                bonusArmor = 10,
            };
            public static Equipment AmethystRing = new Equipment("Amethyst ring", EquipmentSlot.Ring, new string[] { "amethystring", "amethystr", "aring", "ar", "ring" })
            {
                description = "An amethyst ring thrones here.",
                details = "A silver ring with a small amethyst. Gives a decent health regen and mana regen boost.",
                bonusHealthRegen = 4,
                bonusManaRegen = 2,
            };
            public static Equipment DarkRing = new Equipment("Dark ring", EquipmentSlot.Ring, new string[] { "darkring", "darkr", "dring", "dr", "ring" })
            {
                description = "An dark ring thrones here.",
                details = "A dark silver ring with a black gem. Gives the user a small damage boost and the ability to Shroud.",
                bonusDamage = 5,
                bonusSpells = new List<Spell>() { Data.Spells.Equipment.Shrouding },
            };
        }

        public static class Armor
        {
            public static Equipment KoboldTunic = new Equipment("Kobold tunic", EquipmentSlot.Armor, new string[] { "koboldtunic", "ktunic", "koboldt", "tunic", "armor" })
            {
                description = "A kobold tunic is stored here.",
                details = "Simple protective gear. Gives a small health regen boost.",
                bonusHealthRegen = 2,
            };
            public static Equipment KoboldArmor = new Equipment("Kobold armor", EquipmentSlot.Armor, new string[] { "koboldarmor", "karmor", "armor" })
            {
                description = "A kobold armor is stored here.",
                details = "Simple protective gear. Gives a small armor, health and health regen bonus.",
                bonusArmor = 5,
                bonusHealth = 10,
                bonusHealthRegen = 2,
            };

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
            public static Equipment TravellingCloak = new Equipment("Travelling cloak", EquipmentSlot.Armor, new string[] { "travellingcloak", "tcloak", "cloak" })
            {
                description = "A travelling cloak is stashed here.",
                details = "Gives a small health regen and mana regen boost.",
                bonusHealthRegen = 2,
                bonusManaRegen = 1,
                OnEquipped = (x) => { },
                OnUnequipped = (x) => { }
            };
        }
    }
}
