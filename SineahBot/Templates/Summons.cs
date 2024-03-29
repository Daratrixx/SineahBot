﻿using SineahBot.Data;
using SineahBot.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Templates
{
    public static class Summons
    {
        public static NPC FleshGolem = new NPC()
        {
            level = 7,
            baseHealth = 130,
            health = 130,
            name = "Flesh golem",
            shortDescription = "A flesh golem stands guard.",
            longDescription = "Dangerous undead summon.",
            alternativeNames = new string[] { "fleshgolem", "fgolem", "golem" },
            tags = new List<CharacterTag>() { CharacterTag.Summon, CharacterTag.Undead }
        };
        public static NPC Ent = new NPC()
        {
            level = 6,
            baseHealth = 110,
            health = 110,
            name = "Ent",
            shortDescription = "An ent stands guard.",
            longDescription = "Summoned protecter of nature.",
            alternativeNames = new string[] { "treant", "tree" },
            bonusDamage = 5,
            defaultAttackDamageType = DamageType.Bludgeoning,
            tags = new List<CharacterTag>() { CharacterTag.Summon, CharacterTag.Plant }
        };
        public static NPC Guardian = new NPC()
        {
            level = 5,
            baseHealth = 90,
            health = 90,
            name = "Guardian",
            shortDescription = "A guardian stands guard.",
            longDescription = "Pure magic summon.",
            alternativeNames = new string[] { },
            tags = new List<CharacterTag>() { CharacterTag.Summon }
        };
    }
}
