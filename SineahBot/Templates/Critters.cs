using SineahBot.Data;
using SineahBot.Data.Enums;

namespace SineahBot.Templates
{
    public static class Critters
    {

        public static NPC Rat = new NPC()
        {
            characterStatus = CharacterStatus.Normal,
            level = 1,
            baseHealth = 11,
            health = 11,
            gold = 0,
            name = "Rat",
            shortDescription = "A rat silently scrabbles for food.",
            longDescription = "A regular rat, common pest.",
            alternativeNames = new string[] { "rat", "small rat", "smallrat", "srat", "sr" },
            knowledgeDefaultResponse = null,
        }.AddInventory(Items.RatTail);
        public static NPC RabidRat = new NPC()
        {
            characterStatus = CharacterStatus.Normal,
            level = 2,
            baseHealth = 20,
            health = 20,
            gold = 0,
            name = "Rabid rat",
            shortDescription = "An aggressive rat frantically moves around.",
            longDescription = "This red eyed rat seems very aggressive and territorial. His bight might infect you.",
            alternativeNames = new string[] { "rat", "aggressive rat", "aggressive", "rrabid", "rabid", "rrat", "rr" },
            knowledgeDefaultResponse = null,
        }.AddInventory(Items.RatTail);
        public static NPC GiantRat = new NPC()
        {
            characterStatus = CharacterStatus.Normal,
            level = 3,
            baseHealth = 30,
            health = 30,
            gold = 0,
            name = "Giant rat",
            shortDescription = "A giant rat prowls in the room.",
            longDescription = "A huge ass rat. Dangerous enough to take out an adventurer.",
            alternativeNames = new string[] { "rat", "huge rat", "grat", "gr" },
            knowledgeDefaultResponse = null,
        }.AddInventory(Items.RatTail, 2);
    }
}
