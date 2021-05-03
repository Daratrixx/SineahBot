using SineahBot.Commands;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            manaCost = 5,
            effects = new Spell.Effect[] {
                new Spell.Effect.RemoveAlter() {
                    alteration = AlterationType.Sickness
                }
            },
        };
        public static Spell MajorHealing = new Spell("Major healing", new string[] { "majh", "majheal", "heal", "h", "bh" })
        {
            description = "Heal the targeted character for a moderate amount of health.",
            manaCost = 10,
            needsTarget = true,
            canSelfCast = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.Heal() {
                    baseHeal = 15
                }
            },
        };
        public static Spell Smite = new Spell("Smite", new string[] {  })
        {
            description = "Harms foe with the power of the gods. Will deal additionnal damage to undead.",
            canSelfCast = false,
            needsTarget = true,
            manaCost = 10,
            effects = new Spell.Effect[] {
                new Spell.Effect.Custom(
                (caster,target) =>
                {
                    var damage = caster.GetSpellPower();
                    if (target is Character && (target as Character).HasCharacterTag(CharacterTag.Undead)) damage = damage * 2;
                        (target as IDamageable).DamageHealth(damage, DamageType.Magical, caster);
                },
                (caster) =>
                {
                    return "- Damages target (double damage against undead)";
                })
            },
        };
        public static Spell DivineHand = new Spell("Divine hand", new string[] { "divh", "divhand", "dh" })
        {
            description = "Heal the targeted character for a high amount of health.",
            manaCost = 20,
            needsTarget = true,
            canSelfCast = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.Heal() {
                    baseHeal = 60
                }
            },
        };
    }
}
