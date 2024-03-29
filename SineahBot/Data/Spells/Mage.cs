﻿using SineahBot.Data.Enums;

namespace SineahBot.Data.Spells
{
    public static class Mage
    {
        public static Spell MagicDart = new Spell("Magic dart", new string[] { "magicdart", "magicd", "mdart", "magd", "md" })
        {
            description = "Inflict a moderate amount of damage to the target.",
            manaCost = 10,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.SpellDamage() {
                    baseDamage = 15
                }
            },
        };
        public static Spell Amplify = new Spell("Amplify", new string[] { "boost" })
        {
            manaCost = 15,
            needsTarget = true,
            canSelfCast = true,
            aggressiveSpell = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Amplified,
                    baseDuration = 20,
                    spellPowerDurationRatio = 1
                }
            },
            description = "Increase the targets spell potency for a short time."
        };
        public static Spell ArcaneBlast = new Spell("Arcane blast", new string[] { "arcanblast", "arcanb", "ablast", "arcb", "ab" })
        {
            description = "Inflict a high amount of damage to the target.",
            manaCost = 20,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.SpellDamage() {
                    baseDamage = 35
                }
            },
        };
        public static Spell Incinerate = new Spell("Incinerate", new string[] { "inc", "burn" })
        {
            description = "Set damages and set the target ablaze for a short time.",
            manaCost = 10,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.PureDamage() {
                    baseDamage = 20
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Burning,
                    baseDuration = 10,
                    spellPowerDurationRatio = 0.25f
                },
            },
        };
        public static Spell Overcharge = new Spell("Overcharge", new string[] { "overc", "ocharge", "oc" })
        {
            description = "Inflict a small amount of damage to the target, and reduce the potency of the targets spells for a short time.",
            manaCost = 5,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.SpellDamage() {
                    baseDamage = 5
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Weakened,
                    baseDuration = 10,
                    spellPowerDurationRatio = 0.5f
                }
            },
        };

        public static Spell AbsoluteZero = new Spell("Absolute zero", new string[] { "absolutezero", "absolutez", "azero", "az" })
        {
            description = "Deal massive damage and reduce healing for the target.",
            manaCost = 40,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.SpellDamage() {
                    baseDamage = 95
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Burnt,
                    baseDuration = 10,
                    spellPowerDurationRatio = 0.25f
                }
            },
        };
    }
}
