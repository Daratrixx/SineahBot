using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class Kobolds
    {
        public static NPC KoboldScout = new NPC()
        {
            name = "Kobold Scout",
            baseHealth = 50,
            health = 50,
            level = 3,
            shortDescription = "A Kobold Scout watches the area.",
            longDescription = "A rather small Kobold, wearing light armor and weaponry.",
            alternativeNames = new string[] { "kobold", "scout" },
            tags = new List<CharacterTag>() { },
            knowledgeDefaultResponse = null,
        };
        public static NPC KoboldWarrior = new NPC()
        {
            name = "Kobold Warrior",
            baseHealth = 70,
            health = 70,
            level = 4,
            shortDescription = "A Kobold Warrior protects the area.",
            longDescription = "A rather large Kobold, heavily armed and protected.",
            alternativeNames = new string[] { "kobold", "warrior" },
            tags = new List<CharacterTag>() { },
            knowledgeDefaultResponse = null,
        };
        public static NPC KoboldLieutenant = new NPC()
        {
            name = "Kobold Lieutenant",
            baseHealth = 350,
            health = 350,
            level = 5,
            elite = true,
            shortDescription = "A Kobold Lieutenant is here.",
            longDescription = "A rather large Kobold, heavily armed and protected, with extra decorations indicating its rank..",
            alternativeNames = new string[] { "kobold", "lieutenant" },
            tags = new List<CharacterTag>() { },
            knowledgeDefaultResponse = null,
        };
        public static NPC KoboldWarlord = new NPC()
        {
            name = "Kobold Warlord",
            baseHealth = 450,
            health = 450,
            level = 7,
            elite = true,
            shortDescription = "A Kobold Warlord thrones here.",
            longDescription = "A very large Kobold, heavily armed and protected.",
            alternativeNames = new string[] { "kobold", "warlord" },
            tags = new List<CharacterTag>() { },
            knowledgeDefaultResponse = null,
        };
    }
}
