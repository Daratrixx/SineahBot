using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Container : Entity, IObservable, IInventory<Container>
    {
        public Container(string displayName, string[] alternativeNames = null) : base()
        {
            name = displayName;
            if (alternativeNames != null) this.alternativeNames = alternativeNames.Select(x => x.ToLower()).ToArray();
        }
        public string description { get; set; }
        public string details { get; set; }
        public Dictionary<Item, int> items = new Dictionary<Item, int>();

        public virtual string GetFullDescription(IAgent agent = null)
        {
            return details;
        }

        public string GetShortDescription(IAgent agent = null)
        {
            return description;
        }

        public Container AddToInventory(Item item, int count = 1)
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
    }
}
