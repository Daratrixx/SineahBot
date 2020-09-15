using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class CityFolks
    {
        public static NPC Drunk = new NPC()
        {
            level = 3,
            maxHealth = 50,
            health = 50,
            gold = 5,
            name = "Drunk",
            experience = 0,
            shortDescription = "A drunk stumbles around.",
            longDescription = "They can barely keep their balance, smell alcool from meters away, and seem to be looking for a fight.",
            alternativeNames = new string[] { "drnk" }
        };
        public static NPC Customer = new NPC()
        {
            level = 2,
            maxHealth = 30,
            health = 30,
            gold = 10,
            experience = 0,
            name = "Customer",
            shortDescription = "A customer enjoys the establishment.",
            longDescription = "This okay-looking individual is relaxing, not causing troubles.",
            alternativeNames = new string[] { "consumer" }
        };
        public static NPC ShadyConsumer = new NPC()
        {
            level = 4,
            elite = true,
            maxHealth = 100,
            health = 100,
            gold = 100,
            name = "Shady consumer",
            experience = 1000,
            shortDescription = "A shady looking consumer is brooming in a corner.",
            longDescription = "This individual radiates danger. They are most likely armed, and better left alone.",
            alternativeNames = new string[] { "shady looking consumer", "shadyconsumer", "shadycons", "shadyc", "sconsumer", "scons", "sc" }
        };
        public static NPC Bartender = new NPC()
        {
            level = 2,
            maxHealth = 20,
            health = 20,
            gold = 50,
            name = "Bartender",
            experience = 0,
            shortDescription = "The bartender is ready to serve drinks at the bar.",
            longDescription = "The bartender hangs behind the bar, cleaning glasses and serving drinks to the customers of the inn.",
            alternativeNames = new string[] { "host" }
        };
        public static NPC Militian = new NPC()
        {
            level = 3,
            maxHealth = 50,
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
            maxHealth = 90,
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
            maxHealth = 200,
            health = 200,
            gold = 500,
            experience = 5000,
            name = "Captain",
            shortDescription = "The captain is ordering the guards.",
            longDescription = "The captain of the city guards. Their equipment is of the highest quality, and their are a renowned fighter.",
            alternativeNames = new string[] { "captain", "cpt" }
        };
    }
}
