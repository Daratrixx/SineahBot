using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Interfaces
{
    public interface IInteractable
    {
        void OnInteracted(IAgent agent, Interaction interaction);
    }

    public enum Interaction {
        Look,
        PickUp,
        Drop,
        Attack,
        Carry,
        Use,
        Consume,
        Equip
    }
}
