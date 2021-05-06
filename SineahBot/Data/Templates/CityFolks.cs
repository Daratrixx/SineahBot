using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class CityFolks
    {
        public static NPC Citizen = new NPC()
        {
            level = 1,
            baseHealth = 20,
            health = 20,
            gold = 0,
            name = "Citizen",
            experience = 0,
            shortDescription = "A citizen walks by.",
            longDescription = "Just your average citizen. They seem busy.",
            alternativeNames = new string[] { "dude" }
        };
        public static NPC Beggar = new NPC()
        {
            level = 1,
            baseHealth = 20,
            health = 20,
            gold = 0,
            name = "Beggar",
            experience = 0,
            shortDescription = "A beggar hassles over.",
            longDescription = "They are clothed in rags, they look hungry, and noticed you were looking at them.\n\"**Can you toss a poor soul a coin?**\"",
            alternativeNames = new string[] { "beg", "poor" }
        };
        public static NPC Drunk = new NPC()
        {
            level = 3,
            baseHealth = 50,
            health = 50,
            gold = 5,
            name = "Drunk",
            experience = 0,
            shortDescription = "A drunk stumbles around.",
            longDescription = "They can barely keep their balance, and smell alcool from meters away.",
            alternativeNames = new string[] { "drnk" }
        };
        public static NPC Customer = new NPC()
        {
            level = 2,
            baseHealth = 30,
            health = 30,
            gold = 10,
            experience = 0,
            name = "Customer",
            shortDescription = "A customer enjoys the establishment.",
            longDescription = "This okay-looking individual is relaxing, not causing troubles.",
            alternativeNames = new string[] { }
        };
        public static NPC ShadyConsumer = new NPC()
        {
            level = 4,
            elite = true,
            baseHealth = 100,
            health = 100,
            gold = 100,
            name = "Shady consumer",
            experience = 1000,
            shortDescription = "A shady looking consumer is brooding in a corner.",
            longDescription = "This individual radiates danger. They are most likely armed, and better left alone.",
            alternativeNames = new string[] { "shady looking consumer", "shadyconsumer", "shadycons", "shadyc", "sconsumer", "scons", "consumer", "sc", "shady" },
            knowledgeDefaultResponse = "*They look at you with piercing eyes and stay silent.*"
        };
        public static NPC Waiter = new NPC()
        {
            level = 2,
            baseHealth = 20,
            health = 20,
            gold = 10,
            name = "Waiter",
            experience = 0,
            shortDescription = "A waiter is serving customers.",
            longDescription = "The waiter walks back and forth, taking orders and serving customers.",
            alternativeNames = new string[] { "waitress" }
        };
        public static NPC Cook = new NPC()
        {
            level = 2,
            baseHealth = 20,
            health = 20,
            gold = 20,
            name = "Cook",
            experience = 0,
            shortDescription = "A cook is working here.",
            longDescription = "The cook is busy making food for the customers.",
            alternativeNames = new string[] { },
            knowledgeDefaultResponse = "\"**Sorry, I'm a bit busy.**\""
        };
        public static NPC Bartender = (NPC)new NPC()
        {
            level = 2,
            baseHealth = 20,
            health = 20,
            gold = 50,
            name = "Bartender",
            experience = 0,
            shortDescription = "The bartender is ready to serve drinks at the bar.",
            longDescription = "The bartender hangs behind the bar, cleaning glasses and serving drinks to the customers of the inn.",
            alternativeNames = new string[] { "host" }
        }.AddToInventory(Consumables.Candy, 2);
        public static NPC Baker = new NPC()
        {
            level = 2,
            baseHealth = 30,
            health = 30,
            gold = 40,
            name = "Baker",
            experience = 0,
            shortDescription = "A baker is here to trade.",
            longDescription = "The baker is ready to trade.",
            alternativeNames = new string[] { }
        };
        public static NPC WeaponSeller = new NPC()
        {
            level = 2,
            baseHealth = 30,
            health = 30,
            gold = 40,
            name = "Weapon seller",
            experience = 0,
            shortDescription = "A weapon seller is here to trade.",
            longDescription = "The weapon seller is ready to trade.",
            alternativeNames = new string[] { "weaponseller", "weapons", "weapon", "weaponsmith" }
        };
        public static NPC ArmorSeller = new NPC()
        {
            level = 2,
            baseHealth = 30,
            health = 30,
            gold = 40,
            name = "Armor seller",
            experience = 0,
            shortDescription = "An armor seller is here to trade.",
            longDescription = "The armor seller is ready to trade.",
            alternativeNames = new string[] { "armorseller", "armors", "armor", "armorsmith" }
        };
        public static NPC ChurchAttendant = new NPC()
        {
            level = 2,
            baseHealth = 30,
            health = 30,
            gold = 40,
            name = "Church attendant",
            experience = 0,
            shortDescription = "A church attendant is here to trade.",
            longDescription = "The church attendant is ready to trade.",
            alternativeNames = new string[] { "churchattendant", "churchatt", "churcha", "cattendant", "ca" }
        };
        public static NPC MagicVendor = new NPC()
        {
            level = 2,
            baseHealth = 30,
            health = 30,
            gold = 40,
            name = "Magic vendor",
            experience = 0,
            shortDescription = "A magic vendor is here to trade.",
            longDescription = "The magic vendor is ready to trade.",
            alternativeNames = new string[] { "magicvendor", "magicv", "mvendor", "cattendant", "mv" }
        };
        public static NPC Pharmacian = new NPC()
        {
            level = 2,
            baseHealth = 30,
            health = 30,
            gold = 40,
            name = "Pharmacian",
            experience = 0,
            shortDescription = "A pharmacian is here to trade.",
            longDescription = "The pharmacian is ready to trade.",
            alternativeNames = new string[] { "pharma", "doctor", "heal", "pha", "ph", "p" }
        };
        public static NPC Trader = new NPC()
        {
            level = 2,
            baseHealth = 30,
            health = 30,
            gold = 40,
            name = "Trader",
            experience = 0,
            shortDescription = "A trader is here to trade.",
            longDescription = "The trader is ready to trade.",
            alternativeNames = new string[] { "trader", "j" }
        };
        public static NPC Jeweler = new NPC()
        {
            level = 2,
            baseHealth = 30,
            health = 30,
            gold = 40,
            name = "Jeweler",
            experience = 0,
            shortDescription = "A jeweler is here to trade.",
            longDescription = "The jeweler is ready to trade.",
            alternativeNames = new string[] { "jewel", "j" }
        };
        public static NPC Militian = new NPC()
        {
            level = 3,
            baseHealth = 50,
            health = 50,
            gold = 5,
            experience = 0,
            name = "Militian",
            shortDescription = "A militian patrols the area.",
            longDescription = "Citizen enlisted in the city defence. Lightly armed and protected, but can easily dispatch trouble makers.",
            alternativeNames = new string[] { "militia" }
        };
        public static NPC Guard = new NPC()
        {
            level = 5,
            baseHealth = 90,
            health = 90,
            gold = 15,
            experience = 0,
            name = "Guard",
            shortDescription = "A guard patrols the area.",
            longDescription = "Defender of the city. Heavily armed, well protected, and military trained.",
            alternativeNames = new string[] { "guard", "grd" }
        };
        public static NPC GuardCaptain = new NPC()
        {
            level = 7,
            elite = true,
            baseHealth = 450,
            health = 450,
            gold = 500,
            experience = 5000,
            name = "Captain",
            shortDescription = "The captain is ordering the guards.",
            longDescription = "The captain of the city guards. Their equipment is of the highest quality, and their are a renowned fighter.",
            alternativeNames = new string[] { "captain", "cpt" }
        };
    }
}
