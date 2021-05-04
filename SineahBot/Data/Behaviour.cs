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

        public List<RoomEvent> memory = new List<RoomEvent>();
        public List<RoomEvent> memorySwapped = new List<RoomEvent>();

        public BehaviourMission currentMission;
        protected List<BehaviourMission> missions = new List<BehaviourMission>();

        public virtual void Init(NPC npc)
        {
            this.npc = npc;
        }
        public void MemorizeEvent(RoomEvent e)
        {
            memory.Add(e);
        }
        public void SwapMemory()
        {
            var swap = memorySwapped;
            memorySwapped = memory;
            memory = swap;
            memory.Clear();
        }
        public abstract void ParseMemory();
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

    public class BehaviourMission
    {
        public BehaviourMission(RoomEvent sourceEvent)
        {
            this.sourceEvent = sourceEvent;
        }
        public RoomEvent sourceEvent;
        public int totalAge = 0;
        public int activeAge = 0;
        public float cycleValue { get; private set; }

        public void SetCycleValue(float cycleValue)
        {
            this.cycleValue = cycleValue;
        }

        public class Idle : BehaviourMission
        {
            public Idle(RoomEvent sourceEvent) : base(sourceEvent) { }
        }
        public class Guard : BehaviourMission
        {
            public Guard(RoomEvent sourceEvent) : base(sourceEvent) { }
        }
        public class Roam : BehaviourMission
        {
            public Roam(RoomEvent sourceEvent) : base(sourceEvent) { }
        }
        public class Travel : BehaviourMission
        {
            public Travel(RoomEvent sourceEvent) : base(sourceEvent) { }
        }
        public class Snitch : BehaviourMission
        {
            public Snitch(RoomEvent sourceEvent) : base(sourceEvent) { }
        }
        public class Hunt : BehaviourMission
        {
            public Hunt(RoomEvent sourceEvent) : base(sourceEvent) { }
        }
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
