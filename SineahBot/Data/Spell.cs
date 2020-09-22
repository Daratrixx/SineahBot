using SineahBot.Data.Spells;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public abstract class Spell : INamed
    {
        public Guid id;
        public string name;
        public int manaCost = 0;
        public bool NeedsTarget { get; protected set; } = true;
        public bool CanSelfCast { get; protected set; } = true;
        public Spell(string spellName, string[] alternativeNames = null)
        {
            id = Guid.NewGuid();
            name = spellName;
            if (alternativeNames != null) this.alternativeNames = alternativeNames.Select(x => x.ToLower()).ToArray();
        }
        public string[] alternativeNames = new string[] { };
        public string description { get; set; }
        public virtual string GetDescription(ICaster caster = null)
        {
            return description;
        }
        public virtual string GetEffectDescription(ICaster caster = null)
        {
            return "";
        }

        public abstract void Cast(ICaster caster, Entity target);

        public string GetName(IAgent agent = null)
        {
            return name;
        }

        public static Spell MinorHealing = new SpellHeal("Minor healing",
        new string[] { "minh", "minheal", "mheal", "mh", "sh" })
        { description = "Heal the targeted character for a small amount of health.", baseHeal = 5, manaCost = 5 };
        public static Spell MajorHealing = new SpellHeal("Major healing",
        new string[] { "majh", "majheal", "heal", "h", "bh" })
        { description = "Heal the targeted character for a moderate amount of health.", baseHeal = 15, manaCost = 10 };
        public static Spell DivineHand = new SpellHeal("Divine hand",
        new string[] { "divh", "divhand", "dh" })
        { description = "Heal the targeted character for a high amount of health.", baseHeal = 40, manaCost = 20 };

        public static Spell Incinerate = new SpellAlter("Incinerate",
        new string[] { "inc", "burn" })
        {
            description = "Sets the target ablaze.",
            CanSelfCast = false,
            NeedsTarget = true,
            manaCost = 10,
            baseDuration = 10,
            spellPowerDurationRatio = 0.25,
            alterations = new AlterationType[] { AlterationType.Burning },
        };
        public static Spell Harden = new SpellAlter("Harden",
        new string[] { "hard" })
        {
            description = "Harden the target.",
            CanSelfCast = false,
            NeedsTarget = true,
            manaCost = 15,
            baseDuration = 20,
            spellPowerDurationRatio = 0.5,
            alterations = new AlterationType[] { AlterationType.Hardened },
        };

        /*
        public static Spell Incinerate = new SpellFreeForm("Incinerate",
        new string[] { "inc", "burn" })
        {
            description = "Sets the target ablaze.",
            CanSelfCast = false,
            NeedsTarget = true,
            manaCost = 5,
            handler = (caster, target) =>
            {
            }
        };
        */

        public static Spell MagicDart = new SpellDamage("Magic dart",
        new string[] { "magicdart", "magicd", "mdart", "magd", "md" })
        { description = "Inflict a moderate amount of damage to the target.", baseDamage = 15, manaCost = 10, CanSelfCast = false };
        public static Spell ArcaneBlast = new SpellDamage("Arcane blast",
        new string[] { "arcanblast", "arcanb", "ablast", "arcb", "ab" })
        { description = "Inflict a high amount of damage to the target.", baseDamage = 40, manaCost = 20, CanSelfCast = false };
        public static Spell Overcharge = new SpellDamage("Overcharge",
        new string[] { "overc", "ocharge", "oc" })
        { description = "Inflict a small amount of damage to the target.", baseDamage = 5, manaCost = 5, CanSelfCast = false };
    }
}
