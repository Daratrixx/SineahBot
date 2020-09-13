using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface ICaster : INamed, IActionRateLimited
    {
        int GetSpellPower();
        Spell GetSpell(string spellName);
        bool CastSpell(Spell spell); // returns true if the target died
        bool CastSpellOn(Spell spell, Entity target); // returns true if the target died
    }
}
