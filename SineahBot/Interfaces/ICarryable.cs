using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    interface ICarryable : INamed
    {
        void OnCarried(IAgent agent);
    }
}
