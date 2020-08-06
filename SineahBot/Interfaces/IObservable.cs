using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IObservable
    {
        void OnObserved(IAgent agent);
        void OnSearched(IAgent agent);
        string GetNormalDescription();
        string GetSearchDescription();
        bool IsHidden(int detectionValue);
    }
}
