using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public class CharacterTags
    {

        public static string GetTagDescription(CharacterTag tag)
        {
            switch (tag)
            {
                case CharacterTag.Undead: return $"Healing will deal damage instead. Can't be Poisoned, Sick, Weakened, Frenzied, Taunted, Stunned.";
                case CharacterTag.Beast: return $"Will be affected differently by some spells.";
                case CharacterTag.Mecanical: return $"Can't be healed. Can't be Poisoned, Sick, Weakened, Frenzied, Taunted, Stunned. Will be affected differently by some spells.";
                case CharacterTag.Summon: return $"Doesn't affect combar reward pool. Will be affected differently by some spells.";
                default: return "Unknown tag, no description.";
            }
        }
    }

    public enum CharacterTag
    {
        Undead,
        Beast,
        Mecanical,
        Summon,
        Plant,
    }
}
