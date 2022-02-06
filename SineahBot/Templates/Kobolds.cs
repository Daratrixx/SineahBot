using SineahBot.Data;
using SineahBot.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Templates
{
    public static class Kobolds
    {
        public static NPC KoboldScout = new NPC()
        {
            name = "Kobold Scout",
            baseHealth = 50,
            health = 50,
            level = 3,
            gold = 10,
            shortDescription = "A Kobold Scout watches the area.",
            longDescription = "A rather small Kobold, wearing light armor and weaponry.",
            alternativeNames = new string[] { "kobold", "scout" },
            tags = new List<CharacterTag>() { },
            knowledgeDefaultResponse = null,
            weaknesses = new List<DamageType> { DamageType.Cold },
            resistances = new List<DamageType> { DamageType.Slashing }
            ,
        };
        public static NPC KoboldWarrior = new NPC()
        {
            name = "Kobold Warrior",
            baseHealth = 70,
            health = 70,
            level = 4,
            gold = 20,
            shortDescription = "A Kobold Warrior protects the area.",
            longDescription = "A rather large Kobold, heavily armed and protected.",
            alternativeNames = new string[] { "kobold", "warrior" },
            tags = new List<CharacterTag>() { },
            knowledgeDefaultResponse = null,
            weaknesses = new List<DamageType> { DamageType.Cold },
            resistances = new List<DamageType> { DamageType.Slashing }
        };
        public static NPC KoboldLieutenant = new NPC()
        {
            name = "Kobold Lieutenant",
            baseHealth = 350,
            health = 350,
            level = 5,
            gold = 200,
            elite = true,
            shortDescription = "A Kobold Lieutenant is here.",
            longDescription = "A rather large Kobold, heavily armed and protected, with extra decorations indicating its rank..",
            alternativeNames = new string[] { "kobold", "lieutenant" },
            tags = new List<CharacterTag>() { },
            knowledgeDefaultResponse = null,
            weaknesses = new List<DamageType> { DamageType.Cold },
            resistances = new List<DamageType> { DamageType.Slashing }
        };
        public static NPC KoboldWarlord = new NPC()
        {
            name = "Kobold Warlord",
            baseHealth = 450,
            health = 450,
            level = 7,
            gold = 500,
            elite = true,
            shortDescription = "A Kobold Warlord thrones here.",
            longDescription = "A towering, intimidating Kobold. Heavily armed and protected. Very dangerous.",
            alternativeNames = new string[] { "kobold", "warlord" },
            tags = new List<CharacterTag>() { },
            knowledgeDefaultResponse = null,
            weaknesses = new List<DamageType> { DamageType.Cold },
            resistances = new List<DamageType> { DamageType.Slashing }
        };
    }
}
