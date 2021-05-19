using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SineahBot.Data
{
    public class Entity : DataItem, INamed
    {
        public Entity()
        {
            id = Guid.NewGuid();
        }
        public Guid currentRoomId;

        public virtual string GetName(IAgent agent = null)
        {
            return name;
        }
    }
}
