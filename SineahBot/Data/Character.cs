using SineahBot.Commands;
using SineahBot.Data.Templates;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Character : Entity, IAgent, IAttacker, IKillable, IObservable, IDamageable, IInventory<Character>, ICaster, IHealable
    {
        public IAgent agent;
        public CharacterStatus characterStatus;
        public Shop currentShop = null;
        public Container currentContainer = null;
        public int baseHealth;
        public int baseMana;
        public Dictionary<Item, int> items = new Dictionary<Item, int>();
        public List<Spell> spells = new List<Spell>() { };
        public Dictionary<AlterationType, Alteration> alterations = new Dictionary<AlterationType, Alteration>();
        public List<CharacterTag> tags = new List<CharacterTag>();
        public Dictionary<EquipmentSlot, Equipment> equipments = new Dictionary<EquipmentSlot, Equipment>();
        public Faction faction;
        public List<CharacterMessage> messages = new List<CharacterMessage>();
        public Character tauntSource = null;

        public string gender { get; set; }
        private string _pronouns = "they/them/theirs/their/themselves";
        public string pronouns
        {
            get { return _pronouns; }
            set
            {
                _pronouns = value;
                var split = value.Split('/');
                if (split.Length <= 4)
                    split = CharacterCreator.possiblePronouns[0].Split('/');
                they = split[0];
                They = they.Capitalize();
                them = split[1];
                Them = them.Capitalize();
                theirs = split[2];
                Theirs = theirs.Capitalize();
                their = split[3];
                Their = their.Capitalize();
                themselves = split[4];
                Themselves = themselves.Capitalize();
            }
        }
        public CharacterClass characterClass { get; set; }
        public int level { get; set; } = 1;
        public int experience { get; set; } = 0;
        public int health { get; set; } = 20;
        public int mana { get; set; } = 10;
        public int gold { get; set; } = 0;

        public bool sleeping;

        public MudTimer actionCooldown = null;

        public int MaxHealth { get { return baseHealth + bonusHealth; } }
        public int MaxMana { get { return baseMana + bonusMana; } }

        [NotMapped]
        public string they { get; private set; } = "they";
        [NotMapped]
        public string They { get; private set; } = "They";
        [NotMapped]
        public string them { get; private set; } = "them";
        [NotMapped]
        public string Them { get; private set; } = "Them";
        [NotMapped]
        public string theirs { get; private set; } = "theirs";
        [NotMapped]
        public string Theirs { get; private set; } = "Theirs";
        [NotMapped]
        public string their { get; private set; } = "their";
        [NotMapped]
        public string Their { get; private set; } = "Their";
        [NotMapped]
        public string themselves { get; private set; } = "themselves";
        [NotMapped]
        public string Themselves { get; private set; } = "Themselves";

        // equipment bonus
        public int bonusSpellPower = 0;
        public int bonusDamage = 0;
        public int bonusArmor = 0;
        public int bonusHealth = 0;
        public int bonusMana = 0;
        public int bonusHealthRegen = 0;
        public int bonusManaRegen = 0;
        public int bonusDeflection = 0;
        public int bonusEvasion = 0;

        public List<DamageType> resistances = new List<DamageType>();
        public List<DamageType> weaknesses = new List<DamageType>();

        public DamageType defaultAttackDamageType = DamageType.Bludgeoning;
        public DamageType? weaponDamageOverwrite = null;
        public DamageType GetDamageType()
        {
            return weaponDamageOverwrite ?? defaultAttackDamageType;
        }


        public List<Spell> bonusSpells = new List<Spell>();

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
            return $"Here is **{name}**.";
        }

        public virtual string GetFullDescription(IAgent agent = null)
        {
            List<string> output = new List<string>();
            if (ClassProgressionManager.IsPhysicalClass(characterClass))
                output.Add($"> {They} seem to have a powerful body.");
            if (ClassProgressionManager.IsMagicalClass(characterClass))
                output.Add($"> {Their} eyes glow with knowledge and power.");
            if (ClassProgressionManager.IsSecretClass(characterClass))
                output.Add($"> {They} are shrouded by an aura of mistery.");
            output.Add(GetEquipmentDescription(agent as Character));
            output.Add(GetStateDescription(agent));
            output.Add(GetPowerDescription(agent as Character));
            output.Add(GetAlterationDescription(agent as Character));
            return String.Join("\n", output.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        public string GetEquipmentDescription(Character character)
        {
            // check for visible equipment
            equipments.TryGetValue(EquipmentSlot.Weapon, out var Weapon);
            equipments.TryGetValue(EquipmentSlot.Armor, out var Armor);
            equipments.TryGetValue(EquipmentSlot.Shield, out var Shield);
            equipments.TryGetValue(EquipmentSlot.Familiar, out var Familiar);
            if (Weapon == null && Armor == null && Shield == null && Familiar == null)
                return $"> {They} don't seem to have any equipment";
            List<string> output = new List<string>();
            if (Weapon != null) output.Add($"{They} wield a {Weapon.GetName()}.");
            if (Armor != null) output.Add($"A {Armor.GetName()} covers {their} body.");
            if (Shield != null) output.Add($"{They} use a {Shield.GetName()}.");
            if (Familiar != null) output.Add($"A {Familiar.GetName()} follows {them}.");
            return $"> {String.Join(" ", output)}";
        }

        public string GetStateDescription(IAgent agent = null)
        {
            if (health <= (MaxHealth * 1) / 4)
                return $"> {GetName(agent)} is at death's door.";
            if (health <= (MaxHealth * 2) / 4)
                return $"> {GetName(agent)} is badly injured.";
            if (health <= (MaxHealth * 3) / 4)
                return $"> {GetName(agent)} is lightly bruised.";
            return $"> {GetName(agent)} seems in good shape.";
        }

        public virtual string GetPowerDescription(Character character)
        {
            if (character == null) return "";
            var ratio = (character.level * 10000) / (level * 100);
            if (ratio < 45) return $"> {They} will be an impossible fight.";
            if (ratio < 90) return "> This would be a tough fight.";
            if (ratio > 110) return "> This should be an easy fight.";
            if (ratio > 155) return $"> You're going to beat {them} to death.";
            return $"> You seem evenly matched.";
        }

        public string GetAlterationDescription(Character character)
        {
            return $"> Alterations: {String.Join(", ", alterations.Keys.Select(x => x.ToString())) }";
        }

        public void Message(string message)
        {
            if (agent != null) agent.Message(message);
        }

        public void RegisterMessageBypass(Action<Character, Room, string> handler, Action<Character, Room> cancelHandler = null)
        {
            if (agent != null) agent.RegisterMessageBypass(handler, cancelHandler);
        }

        public bool DeflectionCheck()
        {
            var checkRoll = new Random().Next(1, 100);
            return checkRoll <= bonusDeflection;
        }

        public bool EvasionCheck()
        {
            var checkRoll = new Random().Next(1, 100);
            return checkRoll <= bonusEvasion + 5;
        }

        public virtual void DamageHealth(int damageAmount, DamageType type, INamed source = null)
        {
            if (weaknesses.Contains(type))
            {
                damageAmount = damageAmount * 2;
            }
            if (resistances.Contains(type))
            {
                damageAmount = damageAmount / 2;
            }
            switch (type)
            {
                case DamageType.Bludgeoning:
                case DamageType.Slashing:
                case DamageType.Piercing:
                    if (HasAlteration(AlterationType.Hardened))
                        damageAmount /= 2;
                    damageAmount = (int)(damageAmount * (1 - GetArmorDamageReduction()));
                    if (DeflectionCheck())
                    {
                        damageAmount = damageAmount / 2;
                        Message($"You deflected an attack.");
                        (source as IAgent)?.Message($"{GetName()} deflected your attack.");
                    }
                    break;
                case DamageType.Arcane:
                case DamageType.Fire:
                case DamageType.Cold:
                case DamageType.Divine:
                    if (HasAlteration(AlterationType.Warded))
                        damageAmount /= 2;
                    break;
                case DamageType.Corrosive:
                    AddAlteration(AlterationType.Corroded, damageAmount);
                    break;
                default: break;
            }
            health = Math.Max(0, health - damageAmount);
            if (source != null && source != this && source != agent)
            {
                Message($"You took {damageAmount} {type} damage from {source.GetName()}.");
                (source as IAgent)?.Message($"You dealt {damageAmount} {type} damage to {GetName()}.");
            }
            else
            {
                Message($"You took {damageAmount} {type} damage.");
            }
            if (sleeping)
            {
                CommandSleep.Awake(this, RoomManager.GetRoomById(currentRoomId), this is NPC);
            }
            if (currentShop != null) currentShop.RemoveClient(this);
            if (currentContainer != null) currentContainer = null;
            if (source is Character)
            {
                CombatManager.OnDamagingCharacter(this, source as Character);
            }
            return;
        }

        public bool IsDead()
        {
            return health <= 0;
        }

        public bool Sleep()
        {
            if (sleeping) return false;
            sleeping = true;
            if (currentShop != null) currentShop.RemoveClient(this);
            if (currentContainer != null) currentContainer = null;
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
            if (health == MaxHealth && mana == MaxMana)
            {
                if (sleeping)
                {
                    CommandSleep.Awake(this, RoomManager.GetRoomById(currentRoomId), true);
                }
                return;
            }
            var healthRegen = GetHealthRegeneration();
            var manaRegen = GetManaRegeneration();
            health = Math.Min(MaxHealth, health + healthRegen);
            mana = Math.Min(MaxMana, mana + manaRegen);
            if (sleeping)
            {
                Message($"You recovered {healthRegen} health and {manaRegen} mana while sleeping.");
            }
        }

        public int GetHealthRegeneration()
        {
            var output = 2 + bonusHealthRegen;
            if (sleeping) output *= 4;
            if (ClassProgressionManager.IsPhysicalClass(characterClass)) output += level / 2;
            if (HasAlteration(AlterationType.Burnt) && !HasCharacterTag(CharacterTag.Mecanical)) output /= 2;
            if (characterStatus == CharacterStatus.Combat) output /= 2;
            return output;
        }

        public int GetManaRegeneration()
        {
            var output = 1 + bonusManaRegen;
            if (sleeping) output *= 4;
            if (ClassProgressionManager.IsMagicalClass(characterClass)) output += level / 4;
            if (HasAlteration(AlterationType.Poisoned) && !HasCharacterTag(CharacterTag.Undead) && !HasCharacterTag(CharacterTag.Mecanical)) output /= 2;
            if (characterStatus == CharacterStatus.Combat) output /= 2;
            return output;
        }

        public void RestoreHealth(int healthAmount, INamed source = null)
        {
            if (HasCharacterTag(CharacterTag.Mecanical))
            {
                return;
            }
            if (HasCharacterTag(CharacterTag.Undead))
            {
                DamageHealth(healthAmount, DamageType.Pure, source);
                return;
            }
            if (HasAlteration(AlterationType.Burnt))
                healthAmount = healthAmount / 2;
            health = Math.Min(MaxHealth, health + healthAmount);
            if (source != null && source != this && source != agent)
            {
                Message($"You recovered {healthAmount} health from {source.GetName(this)}.");
                (source as IAgent)?.Message($"You healed {GetName()} for {healthAmount} health.");
            }
            else
            {
                Message($"You recovered {healthAmount} health.");
            }
        }
        public void RestoreMana(int manaAmount, INamed source = null)
        {
            if (HasAlteration(AlterationType.Poisoned) && !HasCharacterTag(CharacterTag.Undead) && !HasCharacterTag(CharacterTag.Mecanical))
                manaAmount = manaAmount / 2;
            mana = Math.Min(MaxMana, mana + manaAmount);
            if (source == this) return;
            if (source != null)
                Message($"You recovered {manaAmount} mana from {source.GetName(this)}.");
            else
                Message($"You recovered {manaAmount} mana.");
        }
        public virtual void OnKilled(Entity killer = null)
        {
            var corpse = Containers.CreateContainerFromCharacter(this);
            var room = RoomManager.GetRoomById(currentRoomId);
            room.AddToRoom(corpse, false);
            new MudTimer(300, () =>
            {
                room.RemoveFromRoom(corpse, false);
            });
            RoomManager.RemoveFromCurrentRoom(this, false);
            if (killer != null)
            {
                if (killer != this)
                {
                    Message($"You have been killed by {killer.GetName()}!");
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
            CombatManager.OnCharacterKilled(this);
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

        public Character AddToInventory(Item item, int count = 1)
        {
            if (IsItemInInventory(item.GetName()))
            {
                items[item] += count;
            }
            else
            {
                items[item] = count;
            }
            return this;
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

        public IEnumerable<KeyValuePair<Item, int>> ListItems()
        {
            return items;
        }

        public override string GetName(IAgent agent = null)
        {
            return name;
        }

        public virtual int GetWeaponDamage()
        {
            var weaponDamage = bonusDamage + (ClassProgressionManager.IsPhysicalClass(characterClass) ? level * 2 : level);
            if (HasAlteration(AlterationType.Empowered))
                weaponDamage = (int)(weaponDamage * 1.5);
            if (HasAlteration(AlterationType.Weakened))
                weaponDamage = weaponDamage / 2;
            return weaponDamage;
        }

        public IEnumerable<Spell> GetSpells()
        {
            return spells.Union(bonusSpells);
        }

        public Spell GetSpell(string spellName)
        {
            var spells = GetSpells();
            spellName = spellName.ToLower();
            var output = spells.FirstOrDefault(x => x.name.ToLower() == spellName);
            if (output == null)
                output = spells.FirstOrDefault(x => x.alternativeNames.Contains(spellName));
            return output;
        }

        public bool CanCastSpell(Spell spell)
        {
            return mana >= spell.manaCost;
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
            var bonusDamage = bonusSpellPower + (ClassProgressionManager.IsMagicalClass(characterClass) ? level * 2 : level);
            if (HasAlteration(AlterationType.Amplified))
                bonusDamage = (int)(bonusDamage * 1.5);
            return bonusDamage;
        }

        public virtual int GetPhysicalPower()
        {
            return GetWeaponDamage();
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
            actionCooldown = new MudTimer(4, () => { actionCooldown = null; });
            if (sleeping)
            {
                CommandSleep.Awake(this, RoomManager.GetRoomById(currentRoomId), this is NPC);
            }
        }

        public void TickAlterations(int tickDuration)
        {
            if (alterations.Count == 0) return;
            foreach (var a in alterations.Values.ToArray())
            {
                OnAlterationTick(a);
                a.remainingTime -= sleeping ? tickDuration * 2 : tickDuration; // alteration expire faster while sleeping
            }
            var expired = alterations.Values.Where(x => x.remainingTime <= 0).ToList();
            foreach (var a in expired)
                RemoveAlteration(a.alteration);
        }
        public void OnAlterationTick(Alteration alteration)
        {
            switch (alteration.alteration)
            {
                case AlterationType.Burning:
                    DamageHealth(2, DamageType.Fire, alteration);
                    if (new Random().Next(1, 100) <= 20)
                        AddAlteration(AlterationType.Burnt, 300);
                    break;
                case AlterationType.Bleeding:
                    if (HasCharacterTag(CharacterTag.Mecanical) || HasCharacterTag(CharacterTag.Undead) || HasCharacterTag(CharacterTag.Plant)) break;
                    DamageHealth(3, DamageType.Pure, alteration);
                    break;
                case AlterationType.Sickness:
                    if (HasCharacterTag(CharacterTag.Mecanical) || HasCharacterTag(CharacterTag.Undead)) break;
                    DamageHealth(2, DamageType.Poison, alteration);
                    break;
                case AlterationType.Taunted:
                    Room tr = RoomManager.GetRoomById(currentRoomId);
                    if (tauntSource != null && tr.characters.Contains(tauntSource))
                        CommandCombatAttack.Attack(this, tr, tauntSource);
                    break;
                case AlterationType.Frenzied:
                    Room fr = RoomManager.GetRoomById(currentRoomId);
                    Entity target = fr.characters.Where(x => x != this).FirstOrDefault();
                    if (target != null)
                        CommandCombatAttack.Attack(this, fr, target);
                    break;
                default: break;
            }
        }

        public int? GetAlterationRemainingTime(AlterationType alteration)
        {
            if (alterations.ContainsKey(alteration))
                return alterations[alteration].remainingTime;
            return null;
        }

        public bool HasAlteration(AlterationType alteration)
        {
            return alterations.ContainsKey(alteration);

        }
        public void AddAlteration(AlterationType alteration, int duration)
        {
            if (!alterations.ContainsKey(alteration))
            {
                alterations[alteration] = new Alteration() { alteration = alteration, remainingTime = duration };
                Message($"You are now **{alteration}**.");
            }
            else
            {
                alterations[alteration].remainingTime = Math.Max(alterations[alteration].remainingTime, duration);
            }
        }
        public void RemoveAlteration(AlterationType alteration)
        {
            if (!alterations.ContainsKey(alteration))
                return;
            OnAlterationRemoved(alteration);
            alterations.Remove(alteration);
        }

        public void OnAlterationRemoved(AlterationType alteration)
        {
            Message($"You are no longer **{alteration}**.");
        }

        public bool HasCharacterTag(CharacterTag tag)
        {
            return tags.Contains(tag);
        }

        public void Equip(Equipment equipment)
        {
            var slot = equipment.slot;
            Unequip(slot);
            if (equipment == null) return;
            equipments[slot] = equipment;
            equipment.Equip(this);
        }

        public void Unequip(EquipmentSlot slot)
        {
            if (equipments.ContainsKey(slot))
            {
                equipments[slot]?.Unequip(this);
                equipments.Remove(slot);
            }
        }

        public bool IsEquiped(Equipment equipment)
        {
            if (equipment == null) return false;
            return equipments.ContainsKey(equipment.slot) && equipments[equipment.slot] == equipment;
        }

        public double GetArmorDamageReduction()
        {
            if (bonusArmor == 0) return 0;
            var reduction = bonusArmor / (bonusArmor + 20.0);
            if (HasAlteration(AlterationType.Corroded))
                return reduction / 2;
            return reduction;
        }

        public override string ToString()
        {
            return $"Character {GetName()}";
        }
    }

    public class CharacterMessage
    {
        public Guid id { get; set; }
        public Guid idCharacter { get; set; }
        public string idRoom { get; set; }
        public string message { get; set; }
    }

    public enum CharacterStatus
    {
        Normal,
        Combat,
        Trade,
        Search,
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
        Shaman,
        Necromancer,
        Lich,
        Rogue,
        Assassin,
        Barbarian
    }
}
