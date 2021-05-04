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
            manaCost = 10,
            needsTarget = true,
            canSelfCast = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.Heal() {
                    baseHeal = 15
                }
            },
            description = "Heals the target for a decent amount."
        };
        public static Spell Wrath = new Spell("Wrath", new string[] { })
        {
            manaCost = 10,
            needsTarget = true,
            canSelfCast = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.SpellDamage() {
                    baseDamage = 15
                }
            },
            description = "Damages the target for a decent amount."
        };
        public static Spell Metabolize = new Spell("Metabolize", new string[] { "metabol", "metab", "met", })
        {
            manaCost = 5,
            needsTarget = true,
            canSelfCast = true,
            effects = new Spell.Effect[] {
                new Spell.Effect.RemoveAlter() {
                    alteration = AlterationType.Poisoned
                }
            },
            description = "Cures the target from poison."
        };
        public static Spell VenomousWhip = new Spell("Venomous whip", new string[] { "venomouswhip", "venom whip", "venomwhip", "venom", "whip", "poison" })
        {
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
            description = "Strikes the target and poison them for a little while."
        };
        public static Spell Hibernate = new Spell("Hibernate", new string[] { "sleep" })
        {
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
                            CombatManager.RemoveFromCombat(character, caster is NPC);
                        CommandSleep.Sleep(character, RoomManager.GetRoom(target.currentRoomId));
                    }
                },
                (caster) =>
                {
                    return "- Puts the target to sleep.";
                })
            },
            description = "Puts the target to sleep until the take damage or their health and mana is full again."
        };
    }
}
