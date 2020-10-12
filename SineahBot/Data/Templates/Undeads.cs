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
            baseHealth = 90,
            health = 90,
            name = "Skeleton",
            shortDescription = "A skeleton slowly walks around.",
            longDescription = "Undead fiend.",
            alternativeNames = new string[] { "skl" },
            tags = new List<CharacterTag>() { CharacterTag.Undead },
            knowledgeDefaultResponse = null,
        };
        public static NPC Zombi = new NPC()
        {
            level = 6,
            baseHealth = 110,
            health = 110,
            name = "Zombi",
            gold = 5,
            shortDescription = "A zombi shambles on its legs.",
            longDescription = "Undead fiend.",
            alternativeNames = new string[] { "z" },
            tags = new List<CharacterTag>() { CharacterTag.Undead },
            knowledgeDefaultResponse = null,
        };
        public static NPC Ghoul = new NPC()
        {
            level = 7,
            elite = true,
            baseHealth = 450,
            health = 450,
            experience = 5000,
            gold = 100,
            name = "Ghoul",
            shortDescription = "A ghoul is looking for fresh meat.",
            longDescription = "Dangerous undead fiend.",
            alternativeNames = new string[] { "g","gh" },
            tags = new List<CharacterTag>() { CharacterTag.Undead },
            knowledgeDefaultResponse = "**unearthly screaming**",
        };
        public static NPC Lich = new NPC()
        {
            level = 10,
            elite = true,
            baseHealth = 730,
            health = 730,
            experience = 10000,
            gold = 1000,
            name = "Lich",
            shortDescription = "A lich is throning in the room.",
            longDescription = "Powerful undead fiend.",
            alternativeNames = new string[] { },
            tags = new List<CharacterTag>() { CharacterTag.Undead },
            knowledgeDefaultResponse = "Please leave this place. You're meddling with what you shouldn't.",
        };
    }
}
