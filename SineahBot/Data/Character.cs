using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Character : Entity, IAgent, IAttackable, IAttacker, IKillable, IObservable, IObserver, IDamageable, IInventory
    {
        public IAgent agent;

        //public CharacterClass characterClass { get; set; }
        public int experience { get; set; }
        public int level { get; set; }
        public int maxHealth { get; set; }
        public int health { get; set; }
        public int gold { get; set; }
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
            health = Math.Max(0, health - damageAmount);
            if (agent != null)
            {
                agent.Message($"You took {damageAmount} damage.");
            }
            return health == 0;
        }

        public void OnKilled(IAgent agent)
        {
            RoomManager.RemoveFromCurrentRoom(this, false);
            if (this.agent != null)
            {
                if (agent != null)
                {
                    this.agent.Message($"You have been killed by {agent.name}!");
                }
                else
                {
                    this.agent.Message($"You died!");
                }
                if (this.agent is Player)
                {
                    CharacterManager.DeletePlayerCharacter(this.agent as Player);
                }
            }
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

        public int GetWeaponDamage()
        {
            return new Random().Next(5, 10);
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
