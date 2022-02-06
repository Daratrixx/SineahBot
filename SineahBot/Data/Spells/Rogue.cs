using SineahBot.Data.Enums;

namespace SineahBot.Data.Spells
{
    public static class Rogue
    {
        public static Spell PoisonDart = new Spell("Poison dart", new string[] { "poisondart", "poisond", "pdart", "poisd", "pd", "poison" })
        {
            description = "Poison the target, reducing their mana regen for a while.",
            manaCost = 5,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Poisoned,
                    baseDuration = 20,
                    spellPowerDurationRatio = 2
                }
            },
        };
        public static Spell Bleedout = new Spell("Bleedout", new string[] { "bleed" })
        {
            description = "Cut the target so deep they bleed for a while",
            manaCost = 10,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AttackDamage() {
                    baseDamage = 5
                },
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Bleeding,
                    baseDuration = 20,
                    spellPowerDurationRatio = 2
                },
            },
        };
        public static Spell Disapear = new Spell("Disapear", new string[] { "stealth" })
        {
            description = "Make you invisible for a short time.",
            manaCost = 20,
            needsTarget = false,
            canSelfCast = true,
            aggressiveSpell = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Invisible,
                    baseDuration = 0,
                    spellPowerDurationRatio = 2
                }
            },
        };
    }
}
