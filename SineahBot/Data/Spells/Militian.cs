using SineahBot.Data.Enums;

namespace SineahBot.Data.Spells
{
    public static class Militian
    {
        public static Spell Protect = new Spell("Protect", new string[] { "protec" })
        {
            description = "The target becomes Hardened for a short time.",
            manaCost = 10,
            needsTarget = false,
            canSelfCast = true,
            aggressiveSpell = false,
            effects = new Spell.Effect[]{
                new Spell.Effect.AddAlter(){
                    alteration = AlterationType.Hardened,
                    baseDuration = 10,
                    spellPowerDurationRatio = 1
                }
            }
        };
        public static Spell Taunt = new Spell("Taunt", new string[] { })
        {
            description = "Taunts the target, forcing them to attack you for a short time.",
            manaCost = 10,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[]{
                new Spell.Effect.AddAlter(){
                    alteration = AlterationType.Taunted,
                    baseDuration = 6,
                    spellPowerDurationRatio = 1
                }
            }
        };
    }
}
