using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class Goods
    {
        // health
        public static Consumable BreadBundle = new Consumable("Bread bundle", new string[] { "bundle of bread", "breadbundle" })
        {
            description = "Here lies a bundle of bread.",
            details = "This bundle of bread contains 10 loafs.",
            OnConsumed = (c) =>
            {
                c.AddToInventory(Consumables.Bread, 10);
                c.Message("Received *x10* **Bread** from the bundle.");
            }
        };
        public static Item MeatPackage = new Item("Meat package", new string[] { "package of meat", "meatpackage" })
        {
            description = "Here lies a package of meat.",
            details = "This package of meat contains 10 portions of meat."
        };
    }
}
