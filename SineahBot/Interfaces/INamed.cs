using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface INamed
    {
        string GetName(IAgent agent = null);
    }
}
