using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SineahBot.Tools
{
    public static class ItemManager
    {
        public static Dictionary<Guid, Item> items = new Dictionary<Guid, Item>();

        public static void LoadItems(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                ItemManager.items[item.id] = item;
            }
        }

        public static Item GetItem(Guid idItem)
        {
            return items[idItem];
        }

        public static Item TestItem
        {
            get
            {
                return new Item()
                {
                    id = Guid.NewGuid(),
                    name = "Doll",
                    description = "A doll lies around.",
                    details = "A little doll shaped out of rags."
                };
            }
        }
    }
}
