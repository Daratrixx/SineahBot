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
                var damageAmount = baseDamage + caster.GetSpellPower() + new Random().Next(5, 10);
                var returnValue = (target as IDamageable).OnDamage(damageAmount, caster);
                if (caster is IAgent && caster != target)
                {
                    (caster as IAgent).Message($"You dealt {damageAmount} damage to {target.GetName()}.");
                }
                return returnValue;
            }
            return false;
        }
        public override string GetDescription(ICaster caster = null)
        {
            return base.GetDescription(caster);
        }
        public override string GetEffectDescription(ICaster caster = null)
        {
            return base.GetEffectDescription(caster) + $"\n> Damaging potential : **{baseDamage} + ({(caster != null ? caster.GetSpellPower().ToString() : "spell power")})**";
        }
    }
}
