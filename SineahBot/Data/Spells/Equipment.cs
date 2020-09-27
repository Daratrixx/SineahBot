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
            manaCost = 3,
            needsTarget = true,
            canSelfCast = false,
            effects = new Spell.Effect[] {
                new Spell.Effect.AddAlter() {
                    alteration = AlterationType.Burning,
                    baseDuration = 8,
                    spellPowerDurationRatio = 0
                }
            },
            description = "Sets the target ablaze."
        };
    }
}
