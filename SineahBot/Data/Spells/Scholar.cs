using SineahBot.Commands;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Spells
{
    public static class Scholar
    {
        public static Spell FirstAid = new Spell("First aid", new string[] { "aid" })
        {
            description = "Heals the target for a very small amount of health.",
            manaCost = 5,
            needsTarget = true,
            canSelfCast = true,
            aggressiveSpell = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.Heal() {
                    baseHeal = 5,
                }
            },
        };
    }
}
