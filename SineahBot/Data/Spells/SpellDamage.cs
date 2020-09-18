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
        public override void Cast(ICaster caster, Entity target)
        {
            if (target is IDamageable)
            {
                var damageAmount = baseDamage + caster.GetSpellPower() + new Random().Next(5, 10);
                (target as IDamageable).OnDamage(damageAmount, caster as Entity);
                if (caster is IAgent)
                {
                    (caster as IAgent).Message($"You dealt {damageAmount} damage to {target.GetName()}.");
                }
            }
        }
        public override string GetDescription(ICaster caster = null)
        {
            return base.GetDescription(caster);
        }
        public override string GetEffectDescription(ICaster caster = null)
        {
            if (caster != null)
            {
                return base.GetEffectDescription(caster) + $"Damages target\n> Damaging potential : **{baseDamage + caster.GetSpellPower()} ({baseDamage} + [spell power])**";
            }
            else
            {
                return base.GetEffectDescription(caster) + $"Damages target\n> Damaging potential : **{baseDamage} +  [spell power]**";
            }
        }
    }
}
