using SineahBot.Commands;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Spells
{
    public static class Mage
    {
        public static Spell MagicDart = new Spell("Magic dart", new string[] { "magicdart", "magicd", "mdart", "magd", "md" })
        {
            manaCost = 10,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.SpellDamage() {
                    baseDamage = 15
                }
            },
            description = "Inflict a moderate amount of damage to the target."
        };
        public static Spell Amplify = new Spell("Amplify", new string[] { "boost" })
        {
            manaCost = 15,
            needsTarget = true,
            canSelfCast = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Amplified,
                    baseDuration = 20,
                    spellPowerDurationRatio = 1
                }
            },
            description = "Inflict a moderate amount of damage to the target."
        };
        public static Spell ArcaneBlast = new Spell("Arcane blast", new string[] { "arcanblast", "arcanb", "ablast", "arcb", "ab" })
        {
            manaCost = 20,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.SpellDamage() {
                    baseDamage = 60
                }
            },
            description = "Inflict a high amount of damage to the target."
        };
        public static Spell Incinerate = new Spell("Incinerate", new string[] { "inc", "burn" })
        {
            manaCost = 10,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Burning,
                    baseDuration = 10,
                    spellPowerDurationRatio = 0.25
                }
            },
            description = "Sets the target ablaze."
        };
        public static Spell Overcharge = new Spell("Overcharge", new string[] { "overc", "ocharge", "oc" })
        {
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
                    spellPowerDurationRatio = 0.5
                }
            },
            description = "Inflict a small amount of damage to the target."
        };
    }
}
