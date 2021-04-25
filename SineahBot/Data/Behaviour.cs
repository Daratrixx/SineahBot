using SineahBot.Commands;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public abstract class Behaviour
    {
        public bool active = true;
        public Room destination;
        public NPC npc;

        public List<object> memory = new List<object>();

        public virtual void Init(NPC npc)
        {
            this.npc = npc;
        }
        public abstract void Run(Room room);

        public virtual void RunTravel(Room room)
        {
            var newRoom = RunTravelMove(room, destination);
            if (newRoom != room)
            {
                room = newRoom;
                OnEnterRoom(room);
            }
            if (room == destination)
            {
                destination = null;
                OnDestinationReached(room);
            }
        }

        public virtual Room RunTravelMove(Room from, Room to)
        {
            if (destination == null) return from;
            if (from == to) return from;

            var dir = PathBuilder.GetNextMove(from, to);
            if (dir == MoveDirection.None)
                return from;

            if (CommandMove.MoveCharacter(npc, from, dir))
            {
                return RoomManager.GetRoom(npc.currentRoomId);
            }

            return from;
        }

        public virtual void OnEnterRoom(Room room) { }
        public virtual void OnDestinationReached(Room room) { }
        public virtual void OnStartFighting(Room room) { }
        public virtual void OnAlterationGained(Room room) { }
        public virtual void OnAlterationLost(Room room) { }
        public virtual void OnAttacked(Room room) { }
        public virtual void OnEndFighting(Room room) { }
    }
}
