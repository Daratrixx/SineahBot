using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IPickable
    {
        void OnPicked(IAgent agent);
    }
}
