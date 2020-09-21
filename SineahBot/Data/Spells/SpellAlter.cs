using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Spells
{
    public class SpellAlter : Spell
    {
        public AlterationType[] alterations;
        public int baseDuration;
        public double spellPowerDurationRatio = 1;
        public SpellAlter(string spellName, string[] alternativeNames = null) : base(spellName, alternativeNames)
        {

        }
        public override void Cast(ICaster caster, Entity target)
        {
            if (target is Character)
            {
                var c = target as Character;
                var duration = baseDuration + (int)(caster.GetSpellPower() * spellPowerDurationRatio);
                foreach(var alt in alterations)
                {
                    c.AddAlteration(alt, duration, caster is NPC);
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
                return base.GetEffectDescription(caster) + $"Applies alterations ({String.Join(", ", alterations.Select(x => x.ToString()))})" +
                $"\n> Duration : **{baseDuration + caster.GetSpellPower()} ({baseDuration} + [spell power])**";
            }
            else
            {
                return base.GetEffectDescription(caster) + $"Applies alterations ({String.Join(", ",alterations.Select(x=>x.ToString()))})" +
                $"\n> Duration : **{baseDuration} +  [spell power]**";
            }
        }
    }
}
