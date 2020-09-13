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
        public string GetEffectDescription()
        {
            return "";
        }

        public abstract bool Cast(ICaster caster, Entity target); // returns true if the target died

        public string GetName(IAgent agent = null)
        {
            return name;
        }

        public static Spell MinorHealing = new SpellHeal("Minor healing",
        new string[] { "minh", "minheal", "mheal", "mh", "sh" })
        { baseHeal = 5, manaCost = 5 };
        public static Spell MajorHealing = new SpellHeal("Major healing",
        new string[] { "majh", "majheal", "heal", "h", "bh" })
        { baseHeal = 15, manaCost = 10 };
        public static Spell DivineHand = new SpellHeal("Divine hand",
        new string[] { "divh", "divhand", "dh" })
        { baseHeal = 40, manaCost = 20 };


        public static Spell MagicDart = new SpellDamage("Magic dart",
        new string[] { "magicdart", "magicd", "mdart", "magd", "md" })
        { baseDamage = 15, manaCost = 10, CanSelfCast = false };
        public static Spell ArcaneBlast = new SpellDamage("Arcane blast",
        new string[] { "arcanblast", "arcanb", "ablast", "arcb", "ab" })
        { baseDamage = 30, manaCost = 15, CanSelfCast = false };
        public static Spell Overcharge = new SpellDamage("Overcharge",
        new string[] { "overc", "ocharge", "oc" })
        { baseDamage = 5, manaCost = 5, CanSelfCast = false };
    }
}
