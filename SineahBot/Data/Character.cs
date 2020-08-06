using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public class Character : Entity, IAgent, IAttackable, IAttacker, IKillable, IObservable, IObserver, IDamageable
    {
        public string description { get; set; }

        public CharacterStatus characterStatus { get; set; }

        public void OnObserved(IAgent agent)
        {
            throw new NotImplementedException();
        }
    }

    public enum CharacterStatus
    {
        Normal,
        Combat,
        Workbench
    }
}
