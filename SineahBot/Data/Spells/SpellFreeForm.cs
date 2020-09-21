using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Spells
{
    public class SpellFreeForm : Spell
    {
        public Action<ICaster, Entity> handler = null;
        public SpellFreeForm(string spellName, string[] alternativeNames = null) : base(spellName, alternativeNames)
        {

        }
        public override void Cast(ICaster caster, Entity target)
        {
            handler(caster, target);
        }
        public override string GetDescription(ICaster caster = null)
        {
            return base.GetDescription(caster);
        }
        public override string GetEffectDescription(ICaster caster = null)
        {
            return "";
        }
    }
}
