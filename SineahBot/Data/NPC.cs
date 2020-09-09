using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class NPC : Character
    {
        public string shortDescription { get; set; }
        public string longDescription { get; set; }
        public string[] alternativeNames { get; set; }

        public override string GetShortDescription(IAgent agent = null)
        {
            return shortDescription;
        }

        public override string GetFullDescription(IAgent agent = null)
        {
            return longDescription;
        }
    }
}
