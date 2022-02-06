using SineahBot.Data;
using SineahBot.Data.Enums;

namespace SineahBot.Templates
{
    public static class Familiars
    {
        public static Equipment Owl = new Equipment("Owl familiar", EquipmentSlot.Familiar, new string[] { "owl", "familiar" })
        {
            description = "Here's an Owl familiar.",
            details = "Owl familiar that helps his master attune with magic, giving a decent bonus to mana regen.",
            bonusManaRegen = 2
        };
        public static Equipment Raven = new Equipment("Raven familiar", EquipmentSlot.Familiar, new string[] { "raven", "familiar" })
        {
            description = "Here's a Raven familiar.",
            details = "Raven familiar that amplifies its master's magic, giving a small bonus to spell power.",
            bonusSpellPower = 5
        };
        public static Equipment Hound = new Equipment("Hound familiar", EquipmentSlot.Familiar, new string[] { "hound", "familiar" })
        {
            description = "Here's a Hound familiar.",
            details = "Hound familiar that supports its master, giving a decent bonus to health.",
            bonusHealth = 20
        };
        public static Equipment Lizard = new Equipment("Lizard familiar", EquipmentSlot.Familiar, new string[] { "lizard", "familiar" })
        {
            description = "Here's a Lizard familiar.",
            details = "Lizard familiar that supports its master, giving a decent bonus to health regen.",
            bonusHealthRegen = 4,
        };
    }
}
