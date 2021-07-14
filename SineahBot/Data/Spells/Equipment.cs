using SineahBot.Commands;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Spells
{
    public static class Equipment
    {
        public static Spell Ignite = new Spell("Ignite", new string[] { "burn" })
        {
            manaCost = 5,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.SpellDamage() {
                    baseDamage = 1,
                    damageType = DamageType.Fire,
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Burning,
                    baseDuration = 30,
                    spellPowerDurationRatio = 0
                },
            },
            description = "Sets the target ablaze."
        };
        public static Spell Shrouding = new Spell("Shrouding", new string[] { "shroud" })
        {
            manaCost = 10,
            needsTarget = false,
            canSelfCast = true,
            aggressiveSpell = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Shrouded,
                    baseDuration = 300,
                    spellPowerDurationRatio = 0
                },
            },
            description = "Shrouds the target for 5 minutes."
        };
        public static Spell CrushingBlow = new Spell("Crushing blow", new string[] { "crushing", "crush" })
        {
            manaCost = 10,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AttackDamage() {
                    baseDamage = 10,
                    damageTypeOverwrite = DamageType.Bludgeoning,
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Stunned,
                    baseDuration = 3,
                    spellPowerDurationRatio = 0
                },
            },
            description = "Massive blow, dealing a lot of damage and stunning the target."
        };
        public static Spell DeepCut = new Spell("Deep cut", new string[] { "cut" })
        {
            manaCost = 8,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AttackDamage() {
                    baseDamage = 10,
                    damageTypeOverwrite = DamageType.Slashing,
                },
            },
            description = "Nasty cut that deals a lot of damage to the target."
        };
        public static Spell Thrust = new Spell("Thrust", new string[] { "poke" })
        {
            manaCost = 5,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AttackDamage() {
                    baseDamage = 10,
                    damageTypeOverwrite = DamageType.Piercing,
                },
            },
            description = "A powerful thrust attack."
        };
        public static Spell Counterweigt = new Spell("Counterweigt", new string[] { "counter", "cw" })
        {
            manaCost = 5,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AttackDamage() {
                    baseDamage = 5,
                    damageTypeOverwrite = DamageType.Bludgeoning,
                },
            },
            description = "A skillful strike using the blunt part of a weapon."
        };
        public static Spell QuickSlash = new Spell("Quick slash", new string[] { "qs", "slash" })
        {
            manaCost = 15,
            needsTarget = true,
            canSelfCast = false,
            aggressiveSpell = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.AttackDamage() {
                    baseDamage = 2,
                    damageTypeOverwrite = DamageType.Slashing,
                },
                new Spell.Effect.AttackDamage() {
                    baseDamage = 2,
                    damageTypeOverwrite = DamageType.Slashing,
                },
            },
            description = "Quickly slashes the target twice."
        };
        public static Spell ShieldBash = new Spell("Shield bash", new string[] { "bash" })
        {
            manaCost = 20,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AttackDamage() {
                    baseDamage = 25,
                    damageTypeOverwrite = DamageType.Bludgeoning,
                },
            },
            description = "Bashes the target for massive damage. Tiring for the user."
        };
    }
}
