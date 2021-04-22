using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IInventory<Inventory> where Inventory: IInventory<Inventory>
    {
        Inventory AddToInventory(Item item, int count = 1);
        Item FindInInventory(string name);
        bool IsItemInInventory(string name);
        int CountInInventory(string name);
        int CountInInventory(Item item);
        void RemoveFromInventory(Item item, int count = 1);

        IEnumerable<KeyValuePair<Item, int>> ListItems();
    }
}
