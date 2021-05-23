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
        .SetKnowledge(new string[] { "Neraji Desert", "Neraji" }, "This large sandy land used to be inhabited by us humans. That is, until the Kobolds took over...");
        
        public static KnowledgeBase HumansAboutGods = new KnowledgeBase()
        .SetKnowledge(new string[] { "God", "Gods", "Goddess", "Goddesses" }, "We revere Albedo, our goddess of light.")
        .SetKnowledge(new string[] { "Albedo", "the Bright One" }, "Ahh, Albedo, our beloved genetrix, the Bright One... Goddess of light, she protected us against the darkness.")
        .SetKnowledge(new string[] { "Shade", "the Dark One" }, "The Dark one, mother of monsters! Shade's twisted creation are an insult to our goddess of light!")
        .SetKnowledge(new string[] { "Crag", "earth spirit", "stone spirit" }, "Crag, one of the benevolent Elemental Spirits that embody nature. He's the guardian of Earth.")
        .SetKnowledge(new string[] { "Gyre", "sea spirit", "water spirit" }, "Gyre, one of the benevolent Elemental Spirits that embody nature. She's the guardian of Water.")
        .SetKnowledge(new string[] { "Gale", "wind spirit" }, "Gale, one of the benevolent Elemental Spirits that embody nature. He's the guardian of Wind.")
        .SetKnowledge(new string[] { "Cinder", "fire spirit" }, "Cinder, one of the benevolent Elemental Spirits that embody nature. She's the guardian of Fire.")
        .SetKnowledge(new string[] { "Elemental Spirit", "Elemental Spirits" }, "The guardians of the four elements, created by Albedo and Shade during the Sundering");

        public static KnowledgeBase HumansAboutMonsters = new KnowledgeBase()
        .SetKnowledge(new string[] { "Monster", "Monsters" }, "The twisted creations of Shade. They come in all shape and sizes, and they are all trouble.")
        .SetKnowledge(new string[] { "Kobold", "Kobolds" }, "I hate that vermin. Slimy lizard-looking ass folks...")
        .SetKnowledge(new string[] { "Undead", "Skeleton", "Zombie", "Spectre", "Banshee", "Ghoul", "Lich" }, "Never seen one in my life, but it sounds terrifying...")
        .SetKnowledge(new string[] { "Rat", "Rats" }, "A common pest. Any amount you can kill is a service to the community."); 
        
        public static KnowledgeBase HumansAboutMisc = new KnowledgeBase()
        .SetKnowledge(new string[] { "Human", "Humans" }, "We're the proud sons of Albedo! She elevated us above simple animals by her teaching of tradition and civilization.");

        public static KnowledgeBase HumanAll = new KnowledgeBase()
        .MergeKnowledge(HumansAboutPlaces)
        .MergeKnowledge(HumansAboutGods)
        .MergeKnowledge(HumansAboutMonsters)
        .MergeKnowledge(HumansAboutMisc);
    }
}
