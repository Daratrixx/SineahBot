using SineahBot.Commands;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Spells
{
    public static class Barbarian
    {
        public static Spell Anger = new Spell("Anger", new string[] { })
        {
            description = "User becomes Empowered for a short time.",
            manaCost = 5,
            needsTarget = false,
            canSelfCast = true,
            aggressiveSpell = false,
            effects = new Spell.Effect[]{
                new Spell.Effect.AddAlter(){
                    alteration = AlterationType.Empowered,
                    baseDuration = 20,
                    spellPowerDurationRatio = 1
                }
            }
        };
        public static Spell Fury = new Spell("Fury", new string[] { })
        {
            description = "Deals Physical (Weapon) damage to the target. If the user is low on health, deals Pure damage instead.",
            manaCost = 10,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.ConditionalEffect(
                    new Spell.Effect.AttackDamage() {
                        baseDamage = 10
                    },
                    new Spell.Effect.AttackDamage() {
                        baseDamage = 10,
                        damageTypeOverwrite = DamageType.Pure
                    },
                    (caster,target) => {
                        if(caster is Character character){
                            return character.health / (character.baseHealth + character.bonusHealth) <= 50;
                        }
                        return false;
                    },
                    (s1, s2, caster) => {
                    return $"{s1}\nIf your remaining health is 50% or less, deals Pure damage instead."; }
                )
            },
        };
    }
}
