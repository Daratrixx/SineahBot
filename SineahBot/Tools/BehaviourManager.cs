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
            new MudInterval(5, () =>
            {
                RunBehaviours();
                Player.CommitPlayerMessageBuffers().Wait();
            });
        }

        public static void RegisterNPC<BehaviourClass>(NPC npc, BehaviourClass behaviour) where BehaviourClass : Behaviour
        {
            behaviours.Add(npc, behaviour);
            behaviour.Init(npc);
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
            behaviour.Run(room);
        }
    }
}
