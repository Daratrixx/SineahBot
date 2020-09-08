using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IObservable
    {
        string GetShortDescription(IAgent agent = null);
        string GetFullDescription(IAgent agent = null);
        string GetName(IAgent agent = null);
    }
}
