using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public class Item : Entity, IObservable
    {
        public string description { get; set; }
        public string details { get; set; }

        public string GetFullDescription(IAgent agent = null)
        {
            return details;
        }

        public string GetShortDescription(IAgent agent = null)
        {
            return description;
        }
    }
}
