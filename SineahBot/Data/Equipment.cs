using SineahBot.Data.Enums;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;

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

        public DamageType? weaponDamageOverwrite = null;

        public List<Spell> bonusSpells = new List<Spell>();

        public EquipmentSlot slot { get; private set; }
        public Equipment(string itemName, EquipmentSlot slot, string[] alternativeNames = null) : base(itemName, alternativeNames)
        {
            this.slot = slot;
        }
        public override string GetFullDescription(IAgent agent = null)
        {
            if(weaponDamageOverwrite != null)
            {
                return $"(**{weaponDamageOverwrite}**) *{base.GetFullDescription(agent)} (equipment: {slot})*";
            }
            return $"*{base.GetFullDescription(agent)} (equipment: {slot})*";
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
            if (weaponDamageOverwrite != null && c.weaponDamageOverwrite == null)
                c.weaponDamageOverwrite = weaponDamageOverwrite;
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
            if (weaponDamageOverwrite != null && c.weaponDamageOverwrite == weaponDamageOverwrite)
                c.weaponDamageOverwrite = null;
        }

        public Action<Character> OnEquipped;
        public Action<Character> OnUnequipped;
    }
}
