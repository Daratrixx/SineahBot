using SineahBot.Commands;
using SineahBot.Interfaces;
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
        public Random random = new Random();

        public BehaviourState defaultBehaviourState;
        public BehaviourState currentBehaviourState;

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

            if (npc.characterStatus == CharacterStatus.Combat
            ? CommandCombatFlee.Flee(npc, from, dir)
            : CommandMove.MoveCharacter(npc, from, dir))
            {
                return RoomManager.GetRoom(npc.currentRoomId);
            }

            return from;
        }

        public virtual void OnEnterRoom(Room room) { }
        public virtual void OnDestinationReached(Room room) { }
        public virtual void OnAttacked(Room room) { }
        public virtual bool OnRoomEvent(Room room, RoomEvent e)
        {
            return currentBehaviourState?.OnRoomEvent?.Invoke(this, room, e) == true;
        }
    }
    public class BehaviourState
    {
        public Action<Behaviour, Room, RoomEvent> OnEnterRoom;
        public Action<Behaviour, Room, RoomEvent> OnDestinationReached;
        public Action<Behaviour, Room, RoomEvent> OnAttacked;
        public Func<Behaviour, Room, RoomEvent, bool> OnRoomEvent;
    }

    public class RoomEvent
    {
        public RoomEvent(Room room, RoomEventType type)
        {
            this.room = room;
            this.type = type;
        }

        public Room room;
        public RoomEventType type;

        public Character enteringCharacter;
        public MoveDirection enteringDurection;

        public Character leavingCharacter;
        public MoveDirection leavingDurection;

        public Character castingCharacter;
        public Entity castingTarget;
        public Spell castingSpell;

        public Character attackingCharacter;
        public IAttackable attackTarget;

        public Character killingCharacter;
        public IKillable killedTarget;

        public Character speakingCharacter;
        public string speakingContent;

        public Character actingCharacter;
        public string actingContent;
    }

    public enum RoomEventType
    {
        CharacterEnters,
        CharacterLeaves,
        CharacterCasts,
        CharacterAttacks,
        CharacterKills,
        CharacterSpeaks,
        CharacterActs,
    }
}
