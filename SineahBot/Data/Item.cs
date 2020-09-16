using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Item : Entity, IObservable
    {
        public Item() : base()
        {

        }
        public Item(string itemName, string[] alternativeNames = null) : base()
        {
            name = itemName;
            if (alternativeNames != null) this.alternativeNames = alternativeNames.Select(x => x.ToLower()).ToArray();
        }
        public string[] alternativeNames = new string[] { };
        public string description { get; set; }
        public string details { get; set; }
        public bool permanant = true;

        public string GetFullDescription(IAgent agent = null)
        {
            return details + (permanant ? "" : " (*Will be lost upon bot restart*)");
        }

        public string GetShortDescription(IAgent agent = null)
        {
            return description;
        }
    }
}
