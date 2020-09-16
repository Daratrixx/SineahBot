using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class Consumables
    {
        // health
        public static Consumable Bread = new Consumable("Bread", new string[] { })
        {
            description = "There's a piece of bread.",
            details = "Consuming this will restore a little bit of health.",
            OnConsumed = (character) => { character.RestoreHealth(10, Bread); }
        };
        public static Consumable DriedMeat = new Consumable("Dried meat", new string[] { })
        {
            description = "There's some dried meat.",
            details = "Consuming this will restore some health.",
            OnConsumed = (character) => { character.RestoreHealth(30, DriedMeat); }
        };
        public static Consumable HealingHerbs = new Consumable("Healing herbs", new string[] { })
        {
            description = "There's some healing herbs.",
            details = "Consuming this will restore a decent amount of health.",
            OnConsumed = (character) => { character.RestoreHealth(50, HealingHerbs); }
        };
        public static Consumable HealthPotion = new Consumable("Health potion", new string[] { })
        {
            description = "There's a healing potion.",
            details = "Consuming this will retore a lot of health.",
            OnConsumed = (character) => { character.RestoreHealth(70, HealthPotion); }
        };

        // mana
        public static Consumable Water = new Consumable("Water", new string[] { })
        {
            description = "Here's a bottle of water.",
            details = "Consuming this will restore a little bit of mana.",
            OnConsumed = (character) => { character.RestoreMana(5, Water); }
        };
        public static Consumable Beer = new Consumable("Beer", new string[] { })
        {
            description = "Here's a mug of beer.",
            details = "Consuming this will restore some mana.",
            OnConsumed = (character) => { character.RestoreMana(15, Beer); }
        };
        public static Consumable Wine = new Consumable("Wine", new string[] { })
        {
            description = "Here's a bottle of wine.",
            details = "Consuming this will restore a decent amount of mana.",
            OnConsumed = (character) => { character.RestoreMana(25, Wine); }
        };
        public static Consumable ManaPotion = new Consumable("Mana potion", new string[] { })
        {
            description = "Here's a mana potion.",
            details = "Consuming this will restore a lot of mana.",
            OnConsumed = (character) => { character.RestoreMana(35, ManaPotion); }
        };

        // mixte
        public static Consumable Candy = new Consumable("Candy", new string[] { })
        {
            description = "Here's some candy.",
            details = "Consuming this will restore a little bit of health and mana.",
            OnConsumed = (character) => { character.RestoreHealth(10, Candy); character.RestoreMana(5, Candy); }
        };
        public static Consumable Stimulants = new Consumable("Stimulants", new string[] { })
        {
            description = "Here's some stimulants.",
            details = "Consuming this will resotre a little bit of health and some mana.",
            OnConsumed = (character) => { character.RestoreHealth(10, Stimulants); character.RestoreMana(15, Stimulants); }
        };
    }
}
