using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Item : Entity, IObservable
    {
        public Item()
        {

        }
        public Item(string itemName, string[] alternativeNames = null)
        {
            id = Guid.NewGuid();
            name = itemName;
            if (alternativeNames != null) this.alternativeNames = alternativeNames.Select(x => x.ToLower()).ToArray();
        }
        public string[] alternativeNames = new string[] { };
        public string description { get; set; }
        public string details { get; set; }

        public string GetFullDescription(IAgent agent = null)
        {
            return details;
        }

        public string GetShortDescription(IAgent agent = null)
        {
            return description;
        }

        public string GetName(IAgent agent = null)
        {
            return name;
        }
    }
}
