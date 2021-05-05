﻿using SineahBot.Commands;
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
                Player.CommitPlayerMessageBuffers();
            });
        }

        public static void RegisterNPC<BehaviourClass>(NPC npc, BehaviourClass behaviour) where BehaviourClass : Behaviour
        {
            behaviour.Init(npc);
            lock (behaviours)
            {
                behaviours.Add(npc, behaviour);
            }
        }

        public static void SetActiveForNpc(NPC npc, bool active)
        {
            Behaviour behaviour;
            lock (behaviours)
            {
                if (behaviours.TryGetValue(npc, out behaviour))
                {
                    behaviour.active = active;
                }
            }
        }

        public static void RemoveNPC(NPC npc)
        {
            lock (behaviours)
            {
                behaviours.Remove(npc);
            }
        }

        private static void SwapMemory()
        {
            foreach (var behaviour in behaviours.Values)
            {
                behaviour.SwapMemory();
            }
        }

        public static void RunBehaviours()
        {
            lock (behaviours)
            {
                SwapMemory();
                foreach (var npc in behaviours)
                {
                    RunBehaviour(npc.Value);
                }
            }
        }

        public static void RegisterRoomEventForNPCs(IEnumerable<NPC> npcs, Room room, RoomEvent e)
        {
            lock (behaviours)
            {
                foreach (var npc in npcs)
                {
                    if (behaviours.TryGetValue(npc, out Behaviour behaviour))
                    {
                        behaviour.MemorizeEvent(e);
                    }
                }
            }
        }

        public static void RunBehaviour(Behaviour behaviour)
        {
            if (!behaviour.active)
                return;
            var room = RoomManager.GetRoom(behaviour.npc.currentRoomId);
            behaviour.Run(room);
        }
    }
}
