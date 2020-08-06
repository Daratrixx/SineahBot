using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IObserver
    {
        void OnObserving(IObservable observable);
    }
}
