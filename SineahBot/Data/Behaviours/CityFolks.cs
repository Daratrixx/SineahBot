using SineahBot.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Behaviours
{
    public abstract class CityFolks : Behaviour
    {
        public override bool OnRoomEvent(Room room, RoomEvent e)
        {
            switch (e.type)
            {
                case RoomEventType.CharacterKills:
                    CommandAct.Act(npc, room, "is horrified.");
                    CommandSay.Say(npc, room, "Oh no!");
                    break;
                case RoomEventType.CharacterAttacks:
                    destination = World.Sineah.Streets.Rooms.outerMilitary;
                    break;
                default:
                    break;
            }
        }
        public class Beggar : CityFolks
        {
            public Beggar()
            {
                defaultBehaviourState = currentBehaviourState = new DefaultBehaviour();
            }

            public override void Init(NPC npc)
            {
                base.Init(npc);

                destination = World.Sineah.Streets.GetRooms().GetRandom();
            }
            public override void Run(Room room)
            {
                if (random.Next() % 2 == 0)
                    RunTravel(room);
            }

            public override void OnDestinationReached(Room room)
            {
                destination = World.Sineah.Streets.GetRooms().GetRandom();
            }

            public override void OnEnterRoom(Room room)
            {
                CommandAct.Act(npc, room, "notices you and smiles.");
                CommandSay.Say(npc, room, "Go' a coin for a poor soul?");
            }
            public class DefaultBehaviour : BehaviourState
            {

            }
        }
    }
}
