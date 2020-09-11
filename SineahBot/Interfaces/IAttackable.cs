﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IAttackable : IObservable, INamed
    {
        void OnAttacked(IAgent agent);
    }
}
