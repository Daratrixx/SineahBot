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
        public CharacterStatus characterStatus;
        public int maxHealth;
        public int maxMana;

        public CharacterClass characterClass { get; set; }
        public int level { get; set; }
        public int experience { get; set; }
        public int health { get; set; }
        public int mana { get; set; }
        public int gold { get; set; }

        public virtual string GetShortDescription(IAgent agent = null)
        {
            return $"Here is {name}.";
        }

        public virtual string GetFullDescription(IAgent agent = null)
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

        public bool OnDamage(int damageAmount, IAttacker attacker = null)
        {
            health = Math.Max(0, health - damageAmount);
            if (agent != null)
            {
                if (attacker != null)
                    agent.Message($"You took {damageAmount} damage from {attacker.GetName()}.");
                else
                    agent.Message($"You took {damageAmount} damage.");
            }
            return health == 0;
        }

        public void OnKilled(IAgent agent = null)
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

    public enum CharacterClass
    {
        None,
        // physical origin
        Militian,
        // melee progression
        Guard,
        Footman,
        Knight,
        // range progression
        Ranger,
        Archer,
        Sharpshooter,

        // mental origin
        Scholar,
        // faith progression
        Abbot,
        Prelate,
        Bishop,
        // magic progression
        Enchanter,
        Mage,
        Wizard,

        // hybrid classes
        Paladin, // Knight, faith augmentation
        Fanatic, // Bishop, melee augmentation
        Heretic, // Bishop, magic augmentation
    }
}
