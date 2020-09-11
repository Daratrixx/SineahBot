﻿using SineahBot.Interfaces;
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
        public override bool Cast(ICaster caster, Entity target)
        {
            if (target is IHealable)
            {
                var healingAmount = baseHeal + caster.GetSpellPower();
                (target as IHealable).OnHeal(healingAmount, caster);
                if (caster is IAgent)
                {
                    (caster as IAgent).Message($"You healed {target.GetName()} for {healingAmount} health points.");
                }
            }
            return false;
        }
    }
}