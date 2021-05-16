using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Tools
{
    public static class KnowledgeManager
    {
        public static NPC RegisterGlobalKnowledge(this NPC npc) {
            return npc
            .RegisterKnowlede(new string[] { "zombo.com", "zombocom", "zombo com", "zombo" }, Zombo.text);
        }
    }
}
