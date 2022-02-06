using SineahBot.Data.Enums;

namespace SineahBot.Extensions
{
    public static class DamageTypeExtensions
    {
        public static string GetDescription(this DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Bludgeoning: return $"Common Physical damage. Affected by armor.";
                case DamageType.Slashing: return $"Common Physical damage. Affected by armor.";
                case DamageType.Piercing: return $"Common Physical damage. Affected by armor.";

                case DamageType.Arcane: return $"Common Magical damage. Ignores armor.";
                case DamageType.Fire: return $"Common Magical damage. Ignores armor. Might leave the target Burnt.";
                case DamageType.Cold: return $"Common Magical damage. Ignores armor.";
                case DamageType.Divine: return $"Common Magical damage. Ignores armor. Effective against creatures of the dark.";

                case DamageType.Lightning: return $"Special damage. Ignores armor and magic defences.";
                case DamageType.Corrosive: return $"Special damage. Ignores armor and magic defences. Will leave the target Corroded, with a reduced armor.";
                case DamageType.Poison: return $"Special damage. Ignores armor and magic defences. Can only affect biological, living targets.";
                case DamageType.Pure: return $"Special damage. Ignores armor and magic defences.";
                default: return "Unknown damage type, no description.";
            }
        }
    }
}
