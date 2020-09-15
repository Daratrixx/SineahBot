using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class Undeads
    {
        public static NPC Skeleton = new NPC()
        {
            level = 5,
            maxHealth = 90,
            health = 90,
            name = "Skeleton",
            shortDescription = "A skeleton slowly walks around.",
            longDescription = "Undead fiend.",
            alternativeNames = new string[] { "sk", "cpt" }
        };
        public static NPC Zombi = new NPC()
        {
            level = 6,
            maxHealth = 110,
            health = 110,
            name = "Zombi",
            gold = 5,
            shortDescription = "A zombi shambles on its legs.",
            longDescription = "Undead fiend.",
            alternativeNames = new string[] { "captain", "cpt" }
        };
        public static NPC Ghoul = new NPC()
        {
            level = 7,
            elite = true,
            maxHealth = 300,
            health = 300,
            experience = 5000,
            name = "Ghoul",
            shortDescription = "A ghoul is looking for fresh meat.",
            longDescription = "Dangerous undead fiend.",
            alternativeNames = new string[] { "captain", "cpt" }
        };
        public static NPC Lich = new NPC()
        {
            level = 10,
            elite = true,
            maxHealth = 500,
            health = 500,
            experience = 20000,
            gold = 1000,
            name = "Lich",
            shortDescription = "A lich is throning in the room.",
            longDescription = "Dangerous undead fiend.",
            alternativeNames = new string[] { "captain", "cpt" }
        };
    }
}
