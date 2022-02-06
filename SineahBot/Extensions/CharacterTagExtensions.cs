using SineahBot.Data.Enums;

namespace SineahBot.Extensions
{
    public static class CharacterTagExtensions
    {

        public static string GetDescription(this CharacterTag tag)
        {
            switch (tag)
            {
                case CharacterTag.Undead: return $"Healing will deal damage instead. Can't be Poisoned, Sick, Weakened, Frenzied, Taunted, Stunned. Vulnerable to fire.";
                case CharacterTag.Beast: return $"Will be affected differently by some spells.";
                case CharacterTag.Mecanical: return $"Can't be healed. Can't be Poisoned, Sick, Weakened, Frenzied, Taunted, Stunned. Will be affected differently by some spells.";
                case CharacterTag.Summon: return $"Doesn't affect combar reward pool. Will be affected differently by some spells.";
                default: return "Unknown tag, no description.";
            }
        }
    }
}
