using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Consumable : Item
    {
        public bool combatConsumable = false;
        public Consumable(string itemName, string[] alternativeNames = null) : base(itemName, alternativeNames)
        {

        }

        public Action<Character> OnConsumed;

        public override string GetFullDescription(IAgent agent = null)
        {
            return base.GetFullDescription(agent)
            + (combatConsumable ? " *(can be used in combat)*" : "") ;
        }
    }
}
