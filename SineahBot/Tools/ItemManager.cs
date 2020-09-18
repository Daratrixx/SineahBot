using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SineahBot.Tools
{
    public static class ItemManager
    {
        public static Dictionary<string, Item> items = new Dictionary<string, Item>();

        public static Item GetItem(string itemName)
        {
            return items[itemName];
        }
    }
}
