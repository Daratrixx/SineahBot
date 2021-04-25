using SineahBot.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Behaviours
{
    public abstract class CitiFolks : Behaviour
    {

        public class Beggar : CitiFolks
        {
            public override void Init(NPC npc)
            {
                base.Init(npc);

                destination = World.Sineah.Streets.GetRooms().GetRandom();
            }
            public override void Run(Room room)
            {
                RunTravel(room);
            }

            public override void OnDestinationReached(Room room)
            {
                destination = World.Sineah.Streets.GetRooms().GetRandom();
            }

            public override void OnEnterRoom(Room room)
            {
                CommandAct.Act(npc, room, "notices you and smiles.");
                CommandSay.Say(npc, room, "Got a coin for a poor soul?");
            }
        }
    }
}
