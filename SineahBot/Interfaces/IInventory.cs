using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IInventory
    {
        void AddToInventory(Item pickable);
        Item FindInInventory(string name);
        bool IsItemInInventory(string name);
        void RemoveFromInventory(Item pickable);
    }
}
