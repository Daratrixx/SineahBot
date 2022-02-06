using SineahBot.Data.Enums;
using System.Collections.Generic;

namespace SineahBot.Extensions
{
    public static class CharacterClassExtensions
    {
        private static Dictionary<CharacterClass, string> classDescription = new Dictionary<CharacterClass, string>() {
            { CharacterClass.Militian, "Citizen following the path of arms, ready to take and give a beating." },
            { CharacterClass.Guard, "Experimented fighter that can hold their ground on their own." },
            { CharacterClass.Footman, "Veteran combatant that can easily dispatch monsters." },
            { CharacterClass.Knight, "Elite soldier, they will be a precious ally or a fierceful adversary." },
            { CharacterClass.Ranger, "Fighter able to use range to their advantage." },
            { CharacterClass.Archer, "Combatant well versed in ranged warfare." },
            { CharacterClass.Sharpshooter, "Elite fighter destroying their ennemies from afar." },
            { CharacterClass.Scholar, "Citizen following the path of knowledge, bound to discover their latent abilities." },
            { CharacterClass.Abbot, "Follower a religious order, capable of minor healing feats." },
            { CharacterClass.Prelate, "Influent figure of a religious order, invoking powerful miracles." },
            { CharacterClass.Bishop, "High ranking figure of a religious order, harnessing the wrath and mercy of gods." },
            { CharacterClass.Enchanter, "A low rank magic user, only capable of causing damages and troubles." },
            { CharacterClass.Mage, "A seasoned magic caster, with a large array of spells at their disposal." },
            { CharacterClass.Wizard, "Incredibly powerful being, capable of wiping armies in on spell." },
            { CharacterClass.Druid, "Mysterious protector of nature, both a fierce warrior and a spiritual guide." },
            { CharacterClass.Shaman, "Mysterious protector of nature, both a fierce warrior and a spiritual guide." },
            { CharacterClass.Necromancer, "Mage dwelling in the dark secrects of the afterlife." },
        };

        public static string GetDescription(this CharacterClass characterClass)
        {
            return classDescription[characterClass];
        }
    }
}
