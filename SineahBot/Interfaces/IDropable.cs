using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface IDropable
    {
        void OnDropped(IAgent agent);
    }
}
