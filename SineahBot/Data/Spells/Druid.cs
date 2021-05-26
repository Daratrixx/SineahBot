using SineahBot.Commands;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Spells
{
    public static class Druid
    {
        public static Spell HealingTouch = new Spell("Healing touch", new string[] { "healingtouch", "healingt", "htouch", "ht", "healing", "heal" })
        {
            description = "Heal the target for a decent amount.",
            manaCost = 10,
            needsTarget = true,
            canSelfCast = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.Heal() {
                    baseHeal = 15
                }
            },
        };
        public static Spell Wrath = new Spell("Wrath", new string[] { })
        {
            description = "Damage the target for a decent amount.",
            manaCost = 10,
            needsTarget = true,
            canSelfCast = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.SpellDamage() {
                    baseDamage = 15
                }
            },
        };
        public static Spell Metabolize = new Spell("Metabolize", new string[] { "metabol", "metab", "met", })
        {
            description = "Cure the target from poison.",
            manaCost = 5,
            needsTarget = true,
            canSelfCast = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.RemoveAlter() {
                    alteration = AlterationType.Poisoned
                }
            },
        };
        public static Spell VenomousWhip = new Spell("Venomous whip", new string[] { "venomouswhip", "venom whip", "venomwhip", "venom", "whip", "poison" })
        {
            description = "Strike the target and poison them for a little while.",
            manaCost = 10,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AttackDamage() {
                    baseDamage = 0
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Poisoned,
                    baseDuration = 10,
                    spellPowerDurationRatio = 0.5
                }
            },
        };
        public static Spell Hibernate = new Spell("Hibernate", new string[] { "sleep" })
        {
            description = "Put the target to sleep until they take damage or their health and mana is full again.",
            manaCost = 15,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.Custom(
                (caster,target) =>
                {
                    if(target is Character)
                    {
                        var character = target as Character;
                        if (character.characterStatus == CharacterStatus.Combat)
                            CombatManager.RemoveFromCombat(character);
                        CommandSleep.Sleep(character, RoomManager.GetRoomById(target.currentRoomId));
                    }
                },
                (caster) =>
                {
                    return "- Puts the target to sleep.";
                })
            },
        };
    }
}
