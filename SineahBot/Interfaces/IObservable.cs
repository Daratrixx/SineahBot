using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IObservable : INamed
    {
        string GetShortDescription(IAgent agent = null);
        string GetFullDescription(IAgent agent = null);
    }
}
