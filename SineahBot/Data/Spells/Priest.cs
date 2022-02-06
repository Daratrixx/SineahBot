using SineahBot.Data.Enums;
using SineahBot.Interfaces;
using System;

namespace SineahBot.Data.Spells
{
    public static class Priest
    {
        public static Spell MinorHealing = new Spell("Minor healing", new string[] { "minh", "minheal", "mheal", "mh", "sh" })
        {
            description = "Heal the targeted character for a small amount of health.",
            manaCost = 5,
            needsTarget = true,
            canSelfCast = true,
            aggressiveSpell = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.Heal() {
                    baseHeal = 5
                }
            },
        };
        public static Spell Cure = new Spell("Cure", new string[] { })
        {
            description = "Cure the target from sickness.",
            canSelfCast = true,
            needsTarget = true,
            aggressiveSpell = false,
            manaCost = 5,
            effects = new Spell.Effect[] {
                new Spell.Effect.RemoveAlter() {
                    alteration = AlterationType.Sickness
                },
            },
        };
        public static Spell MajorHealing = new Spell("Major healing", new string[] { "majh", "majheal", "heal", "h", "bh" })
        {
            description = "Heal the targeted character for a moderate amount of health.",
            manaCost = 10,
            needsTarget = true,
            canSelfCast = true,
            aggressiveSpell = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.Heal() {
                    baseHeal = 15
                }
            },
        };
        public static Spell Smite = new Spell("Smite", new string[] { })
        {
            description = "Harms foe with the power of the Goddess. Will deal additionnal damage to undead.",
            canSelfCast = false,
            needsTarget = true,
            manaCost = 10,
            effects = new Spell.Effect[] {
                new Spell.Effect.Custom(
                (caster,target) =>
                {
                    var damage = (caster.GetSpellPower() * new Random().Next(50, 100)) / 100;
                    if (target is Character && (target as Character).HasCharacterTag(CharacterTag.Undead)) damage = damage * 2;
                        (target as IDamageable).DamageHealth(damage, DamageType.Pure, caster);
                },
                (caster) =>
                {
                        return $"- Damages target\n> Damaging potential : **{caster.GetSpellPower()} ([spell power])**\n> Damage type: {DamageType.Pure}\n>Damage doubled against Undead.";
                })
            },
        };
        public static Spell DivineHand = new Spell("Divine hand", new string[] { "divinehand", "divhand", "divh", "dh" })
        {
            description = "Heal the targeted character for a high amount of health, and dispell many ailment.",
            manaCost = 20,
            needsTarget = true,
            canSelfCast = true,
            aggressiveSpell = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.Heal() {
                    baseHeal = 60
                },
                new Spell.Effect.RemoveAlter() {
                    alteration = AlterationType.Blind
                },
                new Spell.Effect.RemoveAlter() {
                    alteration = AlterationType.Burnt
                },
                new Spell.Effect.RemoveAlter() {
                    alteration = AlterationType.Deaf
                },
                new Spell.Effect.RemoveAlter() {
                    alteration = AlterationType.Weakened
                },
            },
        };

        public static Spell DivineMight = new Spell("Divine might", new string[] { "divinemight", "divmight", "divm", "dm" })
        {
            description = "Infuse the target with the might of the Goddess for a short time.",
            manaCost = 20,
            needsTarget = true,
            canSelfCast = true,
            aggressiveSpell = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Amplified,
                    baseDuration = 30,
                    spellPowerDurationRatio = 2
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Empowered,
                    baseDuration = 30,
                    spellPowerDurationRatio = 2
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Hardened,
                    baseDuration = 30,
                    spellPowerDurationRatio = 2
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Warded,
                    baseDuration = 30,
                    spellPowerDurationRatio = 2
                },
            },
        };
    }
}
