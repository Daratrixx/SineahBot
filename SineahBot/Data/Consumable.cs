using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Consumable : Item
    {
        public Consumable() : base()
        {

        }
        public Consumable(string itemName, string[] alternativeNames = null) : base(itemName, alternativeNames)
        {

        }

        public Action<Character> OnConsumed;

        public new Consumable Clone()
        {
            return new Consumable()
            {
                id = Guid.NewGuid(),
                name = name,
                alternativeNames = alternativeNames,
                description = description,
                details = details,
                OnConsumed = OnConsumed
            };
        }
    }
}
