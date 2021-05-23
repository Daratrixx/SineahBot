using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface ICaster : INamed, IActionRateLimited
    {
        int GetSpellPower();
        int GetPhysicalPower();
        Spell GetSpell(string spellName);
        bool CanCastSpell(Spell spell);
        void CastSpell(Spell spell);
        void CastSpellOn(Spell spell, Entity target);
    }
}
