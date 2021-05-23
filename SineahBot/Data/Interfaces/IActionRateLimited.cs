using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IActionRateLimited
    {
        bool ActionCooldownOver();
        void StartActionCooldown();
    }
}
