using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Spells
{
    public class SpellHeal : Spell
    {
        public int baseHeal = 0;
        public SpellHeal(string spellName, string[] alternativeNames = null) : base(spellName, alternativeNames)
        {

        }
        public override void Cast(ICaster caster, Entity target)
        {
            if (target is IHealable)
            {
                var healingAmount = baseHeal + caster.GetSpellPower() + new Random().Next(5, 10);
                (target as IHealable).RestoreHealth(healingAmount, caster);
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
                return base.GetEffectDescription(caster) + $"Heals target\n> Healing potential : **{baseHeal + caster.GetSpellPower()} ({baseHeal} + [spell power])**";
            }
            else
            {
                return base.GetEffectDescription(caster) + $"Heals target\n> Healing potential : **{baseHeal} +  [spell power]**";
            }
        }
    }
}
