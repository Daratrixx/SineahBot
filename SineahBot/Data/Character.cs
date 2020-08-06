using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public class Character : Entity, IAgent, IAttackable, IAttacker, IKillable, IObservable, IObserver, IDamageable
    {
        public IAgent agent;
        public string description { get; set; }

        public CharacterStatus characterStatus { get; set; }

        public string GetNormalDescription()
        {
            throw new NotImplementedException();
        }

        public string GetSearchDescription()
        {
            throw new NotImplementedException();
        }

        public bool IsHidden(int detectionValue)
        {
            throw new NotImplementedException();
        }

        public void Message(string message)
        {
            if (agent != null) agent.Message(message);
        }

        public void OnAttacked(IAgent agent)
        {
            throw new NotImplementedException();
        }

        public void OnAttacking(IAttackable attackable)
        {
            throw new NotImplementedException();
        }

        public bool OnDamage(int damageAmount)
        {
            throw new NotImplementedException();
        }

        public void OnKilled(IAgent agent)
        {
            throw new NotImplementedException();
        }

        public void OnObserved(IAgent agent)
        {
            throw new NotImplementedException();
        }

        public void OnObserving(IObservable observable)
        {
            throw new NotImplementedException();
        }

        public void OnSearched(IAgent agent)
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
