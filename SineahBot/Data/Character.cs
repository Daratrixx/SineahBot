using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Character : Entity, IAgent, IAttackable, IAttacker, IKillable, IObservable, IObserver, IDamageable, IInventory, ICaster, IHealable
    {
        public IAgent agent;
        public CharacterStatus characterStatus;
        public int maxHealth;
        public int maxMana;
        public Spell[] spells = new Spell[] { Spell.MinorHealing, Spell.MagicDart };

        public CharacterClass characterClass { get; set; }
        public int level { get; set; } = 1;
        public int experience { get; set; } = 0;
        public int health { get; set; } = 20;
        public int mana { get; set; } = 10;
        public int gold { get; set; } = 0;

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

        public bool OnDamage(int damageAmount, INamed source = null)
        {
            health = Math.Max(0, health - damageAmount);
            if (agent != null)
            {
                if (source != null)
                    agent.Message($"You took {damageAmount} damage from {source.GetName()}.");
                else
                    agent.Message($"You took {damageAmount} damage.");
            }
            return health == 0;
        }

        public void OnHeal(int healAmount, INamed source = null)
        {
            health = Math.Min(maxHealth, health + healAmount);
            if (agent != null)
            {
                if (source != null)
                    agent.Message($"{source.GetName()} healed you for {healAmount} health points.");
                else
                    agent.Message($"You were healed for {healAmount} health points.");
            }
        }

        public virtual void OnKilled(IAgent agent = null)
        {
            RoomManager.RemoveFromCurrentRoom(this, false);
            if (this.agent != null)
            {
                if (agent != null)
                {
                    this.agent.Message($"You have been killed by {agent.GetName()}!");
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

        public override string GetName(IAgent agent = null)
        {
            return name;
        }

        public int GetWeaponDamage()
        {
            var bonusDamage = ClassProgressionManager.IsPhysicalClass(characterClass) ? level * 2 : level;
            return bonusDamage + new Random().Next(5, 10);
        }

        public Spell GetSpell(string spellName)
        {
            spellName = spellName.ToLower();
            var output = spells.FirstOrDefault(x => x.name.ToLower() == spellName);
            if (output == null)
                output = spells.FirstOrDefault(x => x.alternativeNames.Contains(spellName));
            return output;
        }

        public bool CastSpell(Spell spell)
        {
            return CastSpellOn(spell, this);
        }

        public bool CastSpellOn(Spell spell, Entity target)
        {
            return spell.Cast(this, target);
        }

        public int GetSpellPower()
        {
            var bonusDamage = ClassProgressionManager.IsMagicalClass(characterClass) ? level * 2 : level;
            return bonusDamage + new Random().Next(5, 10);
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

        // secret class
        Druid,
    }
}
