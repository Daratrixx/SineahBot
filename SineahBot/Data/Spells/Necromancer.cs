using SineahBot.Commands;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Spells
{
    public static class Necromancer
    {
        public static Spell WitheringTouch = new Spell("Withering touch", new string[] { "Witheringtouch", "Witheringt", "wtouch", "wt", "withering" })
        {
            manaCost = 10,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.SpellDamage() {
                    baseDamage = 10
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Weakened,
                    baseDuration = 15,
                    spellPowerDurationRatio = 0.5
                }
            },
            description = "Deals damage to the target and weaken in."
        };
        public static Spell BoneSlash = new Spell("Bone Slash", new string[] { "boneslash", "bones", "bslash", "bs", "slash" })
        {
            manaCost = 15,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.SpellDamage() {
                    baseDamage = 35
                }
            },
            description = "Hurls a sharp bone to hurt the target."
        };
        public static Spell Frenzy = new Spell("Frenzy", new string[] { })
        {
            manaCost = 15,
            needsTarget = true,
            canSelfCast = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Sickness,
                    baseDuration = 60,
                    spellPowerDurationRatio = 0
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Empowered,
                    baseDuration = 30,
                    spellPowerDurationRatio = 1
                },
            },
            description = "Make the target stronger but sick.",

        };
        public static Spell BoneArmor = new Spell("Bone Armor", new string[] { "bonearmor", "bonea", "barmor", "ba", "armor" })
        {
            manaCost = 10,
            needsTarget = true,
            canSelfCast = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Hardened,
                    baseDuration = 30,
                    spellPowerDurationRatio = 1
                },
            },
            description = "Cover the target in protecting bone sheets."
        };
    }
}
