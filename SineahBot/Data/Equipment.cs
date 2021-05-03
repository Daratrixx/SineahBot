using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Equipment : Item
    {
        public int bonusSpellPower;
        public int bonusDamage;
        public int bonusArmor;
        public int bonusHealth;
        public int bonusMana;
        public int bonusHealthRegen;
        public int bonusManaRegen;
        public int bonusDeflection;
        public int bonusEvasion;

        public List<Spell> bonusSpells = new List<Spell>();

        public EquipmentSlot slot { get; private set; }
        public Equipment(string itemName, EquipmentSlot slot, string[] alternativeNames = null) : base(itemName, alternativeNames)
        {
            this.slot = slot;
        }
        public override string GetFullDescription(IAgent agent = null)
        {
            return base.GetFullDescription(agent) + $"*(equipment: {slot})*";
        }
        public void Equip(Character c)
        {
            OnEquipped?.Invoke(c);
            c.bonusSpellPower += bonusSpellPower;
            c.bonusDamage += bonusDamage;
            c.bonusArmor += bonusArmor;
            c.bonusHealth += bonusHealth;
            c.bonusMana += bonusMana;
            c.bonusHealthRegen += bonusHealthRegen;
            c.bonusManaRegen += bonusManaRegen;
            c.bonusDeflection += bonusDeflection;
            c.bonusEvasion += bonusEvasion;
            c.bonusSpells.AddRange(bonusSpells);
        }
        public void Unequip(Character c)
        {
            OnUnequipped?.Invoke(c);
            c.bonusSpellPower -= bonusSpellPower;
            c.bonusDamage -= bonusDamage;
            c.bonusArmor -= bonusArmor;
            c.bonusHealth -= bonusHealth;
            c.bonusMana -= bonusMana;
            c.bonusHealthRegen -= bonusHealthRegen;
            c.bonusManaRegen -= bonusManaRegen;
            c.bonusDeflection -= bonusDeflection;
            c.bonusEvasion -= bonusEvasion;
            if (c.health > c.MaxHealth) c.health = c.MaxHealth;
            if (c.mana > c.MaxMana) c.mana = c.MaxMana;
            c.bonusSpells.RemoveAll(x => bonusSpells.Contains(x));
        }

        public Action<Character> OnEquipped;
        public Action<Character> OnUnequipped;
    }

    public enum EquipmentSlot
    {
        Weapon,
        Shield,
        Armor,
        Trinket,
        Ring
    }
}
