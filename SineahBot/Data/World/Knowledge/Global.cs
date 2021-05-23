using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.World.Knowledge
{
    public static class Global
    {
        public static KnowledgeBase HumansAboutPlaces = new KnowledgeBase()
        .SetKnowledge("Sineah", "It's a great city located close to the pass leading to the Neraji Desert. Besides their Kobold problem, it's a lovely city. You've got to eat at the Four Winds inn!")
        .SetKnowledge("Four Winds inn", "The famous inn of Sineah. Great food, great drinks, great staff. You have to try the blanquette there!")
        .SetKnowledge(new string[] { "Neraji Desert", "Neraji" }, "This large sandy land used to be inhabited by us humans. That is, until the Kobolds took over...")
        .SetKnowledge(new string[] { "taverns", "tavern", "inns", "inn" }, "You can usually find food, drinks, rooms and companions in most taverns.");
        
        public static KnowledgeBase HumansAboutGods = new KnowledgeBase()
        .SetKnowledge(new string[] { "Goddesses", "Goddess", "God", "Gods" }, "We revere Albedo, our goddess of light.")
        .SetKnowledge(new string[] { "Albedo", "the Bright One" }, "Ahh, Albedo, our beloved genetrix, the Bright One... Goddess of light, she protects us against the darkness and corruption.")
        .SetKnowledge(new string[] { "Shade", "the Dark One" }, "The Dark one, mother of monsters! Shade's twisted creation are an insult to our goddess of light!")
        .SetKnowledge(new string[] { "Crag", "earth spirit", "stone spirit", "spirit of earth" }, "Crag, one of the benevolent Elemental Spirits that embody nature. He's the guardian of Earth.")
        .SetKnowledge(new string[] { "Gyre", "sea spirit", "water spirit", "spirit of water" }, "Gyre, one of the benevolent Elemental Spirits that embody nature. She's the guardian of Water.")
        .SetKnowledge(new string[] { "Gale", "wind spirit", "spirit of wind" }, "Gale, one of the benevolent Elemental Spirits that embody nature. He's the guardian of Wind.")
        .SetKnowledge(new string[] { "Cinder", "fire spirit", "spirit of fire" }, "Cinder, one of the benevolent Elemental Spirits that embody nature. She's the guardian of Fire.")
        .SetKnowledge(new string[] { "Elemental Spirits", "Elemental Spirit", "Spirits" }, "The guardians of the four elements, created by Albedo and Shade during the Sundering.");

        public static KnowledgeBase HumansAboutMonsters = new KnowledgeBase()
        .SetKnowledge(new string[] { "Monsters", "Monster" }, "The twisted creations of Shade. They come in all shape and sizes, and they are all trouble. *spits on the ground*")
        .SetKnowledge(new string[] { "Kobolds", "Kobold" }, "I hate that vermin. Slimy lizard-looking ass folks... *spits on the ground*")
        .SetKnowledge(new string[] { "Undead", "Skeleton", "Zombie", "Spectre", "Banshee", "Ghoul", "Lich" }, "Never seen one in my life, but it sounds terrifying... *shivers*")
        .SetKnowledge(new string[] { "Rats", "Rat" }, "A common pest. Any of those you can kill is a service to the community.")
        .SetKnowledge(new string[] { "Dragons", "Dragon" }, "You mean Wyverns right? There is only one dragon and it's the protected of Gyre.")
        .SetKnowledge(new string[] { "Wyverns", "Wyvern" }, "Some strong flying monster. Big wings and big troubles. I've heard they can breath fire!")
        .SetKnowledge(new string[] { "Sprites", "Sprite" }, "Those are very rare occurences. It is believed that sometimes, animals gain the ability to channel the energy of an element. Those are called Sprites."); 
        
        public static KnowledgeBase HumansAboutMisc = new KnowledgeBase()
        .SetKnowledge(new string[] { "Humans", "Human" }, "We humans are the proud children of Albedo! She elevated us above simple animals by her teaching of tradition and civilization.")
        .SetKnowledge(new string[] { "guards", "militia", "militian" }, "Some brave souls who take upon themselves the duty to protect the innocent.");

        public static KnowledgeBase HumanAll = new KnowledgeBase()
        .MergeKnowledge(HumansAboutPlaces)
        .MergeKnowledge(HumansAboutGods)
        .MergeKnowledge(HumansAboutMonsters)
        .MergeKnowledge(HumansAboutMisc);
    }
}
