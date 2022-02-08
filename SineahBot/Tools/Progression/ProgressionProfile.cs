using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBot.Tools.Progression
{
    public abstract class ProgressionProfile<TProgressionClass, TProgressionEnumType>
    where TProgressionClass : ProgressionProfile<TProgressionClass, TProgressionEnumType>
    where TProgressionEnumType : Enum
    {
        public ProgressionProfile(TProgressionEnumType progressionType)
        {
            this.progressionType = progressionType;
        }

        public TProgressionEnumType progressionType;

        public int BaseHealth;
        public double IncrementHealth;

        public int BaseMana;
        public double IncrementMana;

        public int BasePhysicalPower;
        public double IncrementPhysicalPower;

        public int BaseMagicalPower;
        public double IncrementMagicalPower;

        public int BaseHealthRegen;
        public double IncrementHealthRegen;

        public int BaseManaRegen;
        public double IncrementManaRegen;

        public Dictionary<Spell, int> AvailableSpells = new Dictionary<Spell, int>();

        public TProgressionClass SetHealth(int baseValue, double levelIncrement = 0)
        {
            this.BaseHealth = baseValue;
            this.IncrementHealth = levelIncrement;
            return (TProgressionClass)this;
        }

        public TProgressionClass SetMana(int baseValue, double levelIncrement = 0)
        {
            this.BaseMana = baseValue;
            this.IncrementMana = levelIncrement;
            return (TProgressionClass)this;
        }

        public TProgressionClass SetPhysicalPower(int baseValue, double levelIncrement = 0)
        {
            this.BasePhysicalPower = baseValue;
            this.IncrementPhysicalPower = levelIncrement;
            return (TProgressionClass)this;
        }

        public TProgressionClass SetMagicalPower(int baseValue, double levelIncrement = 0)
        {
            this.BaseMagicalPower = baseValue;
            this.IncrementMagicalPower = levelIncrement;
            return (TProgressionClass)this;
        }

        public TProgressionClass SetHealthRegen(int baseValue, double levelIncrement = 0)
        {
            this.BaseHealthRegen = baseValue;
            this.IncrementHealthRegen = levelIncrement;
            return (TProgressionClass)this;
        }

        public TProgressionClass SetManaRegen(int baseValue, double levelIncrement = 0)
        {
            this.BaseManaRegen = baseValue;
            this.IncrementManaRegen = levelIncrement;
            return (TProgressionClass)this;
        }

        public TProgressionClass AddSpell(Spell spell, int level)
        {
            this.AvailableSpells[spell] = level;
            return (TProgressionClass)this;
        }

        public void ApplyToCharacter(Character character, bool maximize = false)
        {
            var statLevel = character.level - 1;
            character.baseHealth += this.BaseHealth + (int)Math.Round(statLevel * this.IncrementHealth);
            character.baseMana += this.BaseMana + (int)Math.Round(statLevel * this.IncrementMana);
            character.basePhysicalPower += this.BasePhysicalPower + (int)Math.Round(statLevel * this.IncrementPhysicalPower);
            character.baseMagicalPower += this.BaseMagicalPower + (int)Math.Round(statLevel * this.IncrementMagicalPower);
            character.baseHealthRegen += this.BaseHealthRegen + (int)Math.Round(statLevel * this.IncrementHealthRegen);
            character.baseManaRegen += this.BaseManaRegen + (int)Math.Round(statLevel * this.IncrementManaRegen);
            character.baseSpells.AddRange(this.AvailableSpells.Where(x => x.Value <= character.level).Select(x => x.Key));
            if (maximize)
            {
                character.health = character.MaxHealth;
                character.mana = character.MaxMana;
            }
        }
    }
}
