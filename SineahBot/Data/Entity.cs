using SineahBot.Interfaces;
using System;

namespace SineahBot.Data
{
    public class Entity : INamed
    {
        public Guid id { get; set; }
        public string name { get; set; }

        public string[] alternativeNames = new string[] { };
        public Entity()
        {
            id = Guid.NewGuid();
        }
        public string currentRoomId;

        public virtual string GetName(IAgent agent = null)
        {
            return name;
        }
    }
}
