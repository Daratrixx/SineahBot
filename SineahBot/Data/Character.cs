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
        public int maxHealth;
        public int maxMana;
        public Spell[] spells = new Spell[] { Spell.MinorHealing, Spell.MagicDart };

        public CharacterClass characterClass { get; set; }
        public int level { get; set; } = 1;
        public int experience { get; set; } = 0;
        public int health { get; set; } = 20;
        public int mana { get; set; } = 10;
        public int gold { get; set; } = 0;

        public bool sleeping;

        public MudTimer actionCooldown = null;

        public virtual string GetShortDescription(IAgent agent = null)
        {
            return $"Here is {name}.";
        }

        public virtual string GetFullDescription(IAgent agent = null)
        {
            List<string> output = new List<string>();
            output.Add($"**{GetName(agent).ToUpper()}**");
            if (ClassProgressionManager.IsPhysicalClass(characterClass))
                output.Add("> They seem to have a powerful body.");
            if (ClassProgressionManager.IsMagicalClass(characterClass))
                output.Add("> Their eyes glow with knowledge and power.");
            if (ClassProgressionManager.IsSecretClass(characterClass))
                output.Add("> They are shrouded by an aura of mistery.");
            output.Add(GetStateDescription(agent));
            return String.Join("\n", output);
        }

        public string GetStateDescription(IAgent agent = null)
        {
            if (health <= (maxHealth * 1) / 4)
                return $"> {GetName(agent)} is at the doors gate.";
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

        public virtual bool OnDamage(int damageAmount, INamed source = null)
        {
            health = Math.Max(0, health - damageAmount);
            if (source == this) return health == 0;
            if (agent != null)
            {
                if (source != null)
                    agent.Message($"You took {damageAmount} damage from {source.GetName()}.");
                else
                    agent.Message($"You took {damageAmount} damage.");
            }
            if (sleeping)
            {
                CommandSleep.Awake(this, RoomManager.GetRoom(currentRoomId), this is NPC);
            }
            return health == 0;
        }

        public bool Sleep()
        {
            if (sleeping) return false;
            sleeping = true;
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
            if (health == maxHealth)
            {
                if (sleeping)
                {
                    CommandSleep.Awake(this, RoomManager.GetRoom(currentRoomId), true);
                }
                return;
            }
            if (sleeping)
            {
                health = Math.Min(maxHealth, health + 10);
                Message("You recovered 10 health points while sleeping.", true);
            }
            else
            {
                health = Math.Min(maxHealth, health + 2);
            }
        }

        public void OnHeal(int healAmount, INamed source = null)
        {
            health = Math.Min(maxHealth, health + healAmount);
            if (source == this) return;
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
            if (agent != null)
            {
                if (agent != this.agent)
                {
                    Message($"You have been killed by {agent.GetName()}!");
                    int rewardExp = ClassProgressionManager.ExperienceForNextLevel(this.level) / 10;
                    if (this.agent is Player)
                        rewardExp += rewardExp / 10;
                    else
                        rewardExp += experience;
                    if (agent is Player)
                    {
                        var player = agent as Player;
                        player.character.experience += (rewardExp * Math.Max(this.level, 1)) / Math.Max(player.character.level, 1);
                        player.character.gold += this.gold;
                    }
                    if (agent is Character)
                    {
                        var character = agent as Character;
                        character.experience += (rewardExp * Math.Max(this.level, 1)) / Math.Max(character.level, 1);
                        character.gold += this.gold;
                    }
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
            if (this.agent is Player)
            {
                CharacterManager.DeletePlayerCharacter(this.agent as Player);
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
