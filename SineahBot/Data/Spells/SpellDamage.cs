using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Spells
{
    public class SpellDamage : Spell
    {
        public int baseDamage = 0; 
        public SpellDamage(string spellName, string[] alternativeNames = null) : base(spellName, alternativeNames)
        {

        }
        public override bool Cast(ICaster caster, Entity target)
        {
            if (target is IDamageable)
            {
                var damageAmount = baseDamage + caster.GetSpellPower();
                var returnValue = (target as IDamageable).OnDamage(damageAmount, caster);
                if (caster is IAgent)
                {
                    (caster as IAgent).Message($"You dealt {damageAmount} damage to {target.GetName()}.");
                }
                return returnValue;
            }
            return false;
        }
    }
}
