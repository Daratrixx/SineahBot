using SineahBot.Commands;
using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Tools
{
    public static class BehaviourManager
    {
        private static Dictionary<NPC, Behaviour> behaviours = new Dictionary<NPC, Behaviour>();

        public static void StartBehaviourTimer()
        {
            new MudInterval(10, () =>
            {
                RunBehaviours();
            });
        }

        public static void RegisterNPC<BehaviourClass>(NPC npc, BehaviourClass behaviour) where BehaviourClass : Behaviour
        {
            behaviours.Add(npc, behaviour);
            behaviour.npc = npc;
        }

        public static void RemoveNPC(NPC npc)
        {
            behaviours.Remove(npc);
        }

        public static void RunBehaviours()
        {
            foreach (var npc in behaviours)
            {
                RunBehaviour(npc.Value);
            }
        }

        public static void RunBehaviour(Behaviour behaviour)
        {
            var room = RoomManager.GetRoom(behaviour.npc.currentRoomId);
            behaviour.RunBehaviour(room);
        }

        public class Behaviour
        {
            public Room destination;
            public NPC npc;

            public List<object> memory = new List<object>();

            public void RunBehaviour(Room room)
            {
                if (RunTravelBehaviour(room, destination))
                {
                    destination = Data.World.Sineah.Streets.GetRooms().GetRandom();
                    return;
                }
            }

            public bool RunTravelBehaviour(Room from, Room to)
            {
                if (destination == null) return true;
                if (from == to) return true;

                var dir = PathBuilder.GetNextMove(from, to);
                if (dir == MoveDirection.None)
                    return true;

                Logging.Log("NPC is moving.");
                return !CommandMove.MoveCharacter(npc, from, dir);
            }
        }
    }
}
