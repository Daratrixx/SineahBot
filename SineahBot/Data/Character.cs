using SineahBot.Commands;
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
        public Shop currentShop = null;
        public int maxHealth;
        public int maxMana;
        public Dictionary<Item, int> items = new Dictionary<Item, int>();
        public Spell[] spells = new Spell[] { Spell.MinorHealing, Spell.MagicDart };

        public CharacterClass characterClass { get; set; }
        public int level { get; set; } = 1;
        public int experience { get; set; } = 0;
        public int health { get; set; } = 20;
        public int mana { get; set; } = 10;
        public int gold { get; set; } = 0;

        public bool sleeping;

        public MudTimer actionCooldown = null;

        public int RewardExperience(int amount)
        {
            experience += amount * CharacterManager.ExpMultiplier;
            return amount * CharacterManager.ExpMultiplier;
        }
        public int RewardGold(int amount)
        {
            gold += amount * CharacterManager.GoldMultiplier;
            return amount * CharacterManager.GoldMultiplier;
        }

        public virtual string GetShortDescription(IAgent agent = null)
        {
            return $"Here is {name}.";
        }

        public virtual string GetFullDescription(IAgent agent = null)
        {
            List<string> output = new List<string>();
            if (ClassProgressionManager.IsPhysicalClass(characterClass))
                output.Add("They seem to have a powerful body.");
            if (ClassProgressionManager.IsMagicalClass(characterClass))
                output.Add("Their eyes glow with knowledge and power.");
            if (ClassProgressionManager.IsSecretClass(characterClass))
                output.Add("They are shrouded by an aura of mistery.");
            output.Add(GetStateDescription(agent));
            return String.Join("\n> ", output);
        }

        public string GetStateDescription(IAgent agent = null)
        {
            if (health <= (maxHealth * 1) / 4)
                return $"> {GetName(agent)} is at death's door.";
            if (health <= (maxHealth * 2) / 4)
                return $"> {GetName(agent)} is badly injured.";
            if (health <= (maxHealth * 3) / 4)
                return $"> {GetName(agent)} is lightly bruised.";
            return $"> {GetName(agent)} seems in good shape.";
        }

        public void Message(string message, bool direct = false)
        {
            if (agent != null) agent.Message(message, direct);
        }

        public void OnAttacked(IAgent agent)
        {
            throw new NotImplementedException();
        }

        public virtual void OnDamage(int damageAmount, Entity source = null)
        {
            health = Math.Max(0, health - damageAmount);
            if (source == this) return;
            if (agent != null)
            {
                if (source != null)
                    agent.Message($"You took {damageAmount} damage from {source.GetName()}.", source is NPC);
                else
                    agent.Message($"You took {damageAmount} damage.");
            }
            if (sleeping)
            {
                CommandSleep.Awake(this, RoomManager.GetRoom(currentRoomId), this is NPC);
            }
            if (source is Character)
            {
                CombatManager.OnDamagingCharacter(this, source as Character);
            }
        }

        public bool IsDead()
        {
            return health == 0;
        }

        public bool Sleep()
        {
            if (sleeping) return false;
            sleeping = true;
            if (currentShop != null) currentShop.RemoveClient(this);
            if (characterStatus != CharacterStatus.Normal) characterStatus = CharacterStatus.Normal;
            return true;
        }

        public bool Awake()
        {
            if (!sleeping) return false;
            sleeping = false;
            return true;
        }

        public void Regenerate()
        {
            if (health == maxHealth && mana == maxMana)
            {
                if (sleeping)
                {
                    CommandSleep.Awake(this, RoomManager.GetRoom(currentRoomId), true);
                }
                return;
            }
            if (sleeping)
            {
                health = Math.Min(maxHealth, health + 8);
                mana = Math.Min(maxMana, mana + 4);
                Message("You recovered 8 health and 4 mana while sleeping.", true);
            }
            else
            {
                mana = Math.Min(maxMana, mana + 1);
                health = Math.Min(maxHealth, health + 2);
            }
        }

        public void RestoreHealth(int healthAmount, INamed source = null)
        {
            health = Math.Min(maxHealth, health + healthAmount);
            if (source == this) return;
            if (agent != null)
            {
                if (source != null)
                    agent.Message($"You recovered {healthAmount} health from {source.GetName(this)}.", source is NPC);
                else
                    agent.Message($"You recovered {healthAmount} health.");
            }
        }
        public void RestoreMana(int manaAmount, INamed source = null)
        {
            mana = Math.Min(maxMana, mana + manaAmount);
            if (source == this) return;
            if (agent != null)
            {
                if (source != null)
                    agent.Message($"You recovered {manaAmount} mana from {source.GetName(this)}.", source is NPC);
                else
                    agent.Message($"You recovered {manaAmount} mana.");
            }
        }
        public virtual void OnKilled(Entity killer = null)
        {
            RoomManager.RemoveFromCurrentRoom(this, false);
            if (killer != null)
            {
                if (killer != this)
                {
                    Message($"You have been killed by {killer.GetName()}!", agent is NPC);
                }
                else
                {
                    Message($"You killed yourself!");
                }
            }
            else
            {
                Message($"You died!");
            }
            CombatManager.OnCharacterKilled(this, killer is NPC);
            if (this.agent is Player)
            {
                CharacterManager.DeletePlayerCharacter(this.agent as Player);
            }
        }

        public virtual int GetExperienceReward()
        {
            int rewardExp = ClassProgressionManager.ExperienceForNextLevel(level) / 10;
            if (this.agent is Player)
                rewardExp += rewardExp / 10;
            else
                rewardExp += experience;
            return rewardExp * level;
        }
        public virtual int GetGoldReward()
        {
            return this.gold;
        }

        public void OnObserving(IObservable observable)
        {
            throw new NotImplementedException();
        }

        public void AddToInventory(Item item, int count = 1)
        {
            if (IsItemInInventory(item.GetName()))
            {
                items[item] += count;
            }
            else
            {
                items[item] = count;
            }
        }

        public Item FindInInventory(string name)
        {
            name = name.ToLower();
            return items.Keys.FirstOrDefault(x => x.name.ToLower() == name || x.alternativeNames.Contains(name));
        }

        public bool IsItemInInventory(string name)
        {
            name = name.ToLower();
            var existing = items.Keys.FirstOrDefault(x => x.name.ToLower() == name || x.alternativeNames.Contains(name));
            return existing != null;

        }

        public int CountInInventory(string name)
        {
            var existing = FindInInventory(name);
            return existing != null ? items[existing] : 0;
        }

        public int CountInInventory(Item item)
        {
            return item != null ? items[item] : 0;
        }

        public void RemoveFromInventory(Item item, int count = 1)
        {
            if (IsItemInInventory(item.GetName()))
            {
                items[item] -= count;
                if (items[item] <= 0)
                    items.Remove(item);
            }
        }

        public override string GetName(IAgent agent = null)
        {
            return name;
        }

        public virtual int GetWeaponDamage()
        {
            var bonusDamage = ClassProgressionManager.IsPhysicalClass(characterClass) ? level * 2 : level;
            return bonusDamage;
        }

        public Spell GetSpell(string spellName)
        {
            spellName = spellName.ToLower();
            var output = spells.FirstOrDefault(x => x.name.ToLower() == spellName);
            if (output == null)
                output = spells.FirstOrDefault(x => x.alternativeNames.Contains(spellName));
            return output;
        }

        public bool CanCastSpell(Spell spell)
        {
            return mana > spell.manaCost;
        }

        public void CastSpell(Spell spell)
        {
            CastSpellOn(spell, this);
        }

        public void CastSpellOn(Spell spell, Entity target)
        {
            mana -= spell.manaCost;
            spell.Cast(this, target);
        }

        public virtual int GetSpellPower()
        {
            var bonusDamage = ClassProgressionManager.IsMagicalClass(characterClass) ? level * 2 : level;
            return bonusDamage;
        }

        public void OnAttacking()
        {

        }

        public bool ActionCooldownOver()
        {
            return actionCooldown == null;
        }
        public void StartActionCooldown()
        {
            actionCooldown = new MudTimer(5, () => { actionCooldown = null; });
            if (sleeping)
            {
                CommandSleep.Awake(this, RoomManager.GetRoom(currentRoomId), this is NPC);
            }
        }
    }

    public class CharacterItem
    {
        public Guid id { get; set; }
        public Guid idCharacter { get; set; }
        public string ItemName { get; set; }
        public int StackSize { get; set; }
    }

    public enum CharacterStatus
    {
        Normal,
        Combat,
        Trade,
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
