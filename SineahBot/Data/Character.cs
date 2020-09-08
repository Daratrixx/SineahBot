using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Character : Entity, IAgent, IAttackable, IAttacker, IKillable, IObservable, IObserver, IDamageable, IInventory
    {
        public IAgent agent;
        //public string description { get; set; }
        public int experience { get; set; }

        public CharacterStatus characterStatus { get; set; }

        public string GetShortDescription(IAgent agent = null)
        {
            return $"Here is {name}.";
        }

        public string GetFullDescription(IAgent agent = null)
        {
            return $"Here is a longer description of {name}.";
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

        public void OnObserving(IObservable observable)
        {
            throw new NotImplementedException();
        }

        private List<Item> inventory = new List<Item>();
        public void AddToInventory(Item item)
        {
            inventory.Add(item);
        }

        public Item FindInInventory(string name)
        {
            name = name.ToLower();
            return inventory.FirstOrDefault(x => x.name.ToLower() == name || x.alternativeNames.Contains(name));
        }

        public bool IsItemInInventory(string name)
        {
            name = name.ToLower();
            return inventory.FirstOrDefault(x => x.name.ToLower() == name || x.alternativeNames.Contains(name)) != null;
        }

        public void RemoveFromInventory(Item item)
        {
            inventory.Remove(item);
        }

        public string GetName(IAgent agent = null)
        {
            return name;
        }
    }

    public enum CharacterStatus
    {
        Normal,
        Combat,
        Workbench,
        Unknown
    }
}
