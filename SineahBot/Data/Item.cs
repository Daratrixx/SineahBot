using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Item : Entity, IObservable
    {
        public Item(string itemName, string[] alternativeNames = null) : base()
        {
            name = itemName;
            if (alternativeNames != null) this.alternativeNames = alternativeNames.Select(x => x.ToLower()).ToArray();
            ItemManager.items.Add(name, this);
        }
        public string description { get; set; }
        public string details { get; set; }

        public bool permanent = true; // non-permanant items are removed from players inventory when the bot resets.

        public virtual string GetFullDescription(IAgent agent = null)
        {
            return details 
            + (permanent ? "" : " (*Will be lost upon bot restart*)");
        }

        public string GetShortDescription(IAgent agent = null)
        {
            return description;
        }
    }
}
