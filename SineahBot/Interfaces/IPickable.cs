using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IPickable : INamed
    {
        void OnPicked(IAgent agent);
    }
}
