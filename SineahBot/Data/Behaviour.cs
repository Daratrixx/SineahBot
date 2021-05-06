using SineahBot.Commands;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public abstract class Behaviour
    {
        public bool active = true;

        public NPC npc;
        public Random random = new Random();
        public Room originalRoom;

        public List<RoomEvent> memory = new List<RoomEvent>();
        public List<RoomEvent> memorySwapped = new List<RoomEvent>();

        public BehaviourMission currentMission;
        protected List<BehaviourMission> missions = new List<BehaviourMission>();

        public Behaviour() { }

        public virtual void Init(NPC npc)
        {
            this.npc = npc;
            originalRoom = RoomManager.GetRoom(npc.currentRoomId);
        }

        public virtual void GenerateTravelToOriginMission()
        {
            currentMission = new BehaviourMission.Travel(originalRoom);
            missions.Add(currentMission);
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
        public void ParseMemories()
        {
            foreach (var e in memorySwapped)
                ParseMemory(e);
        }
        public abstract void ParseMemory(RoomEvent e);
        public virtual void ElectMission()
        {
            if (currentMission == null && missions.Count == 0)
            {
                currentMission = new BehaviourMission.Idle();
                missions.Add(currentMission);
                return;
            }
            if (currentMission == null && missions.Count == 1)
            {
                currentMission = missions.FirstOrDefault();
                return;
            }
            if (currentMission == null)
            {
                currentMission = missions.FirstOrDefault(x => x is BehaviourMission.Idle);
                if (currentMission != null)
                    return;
            }
            if (currentMission != null)
            {
                if (currentMission.activeAge < 3)
                    return;
                currentMission = missions.GetRandom();
                return;
            }
        }
        public abstract void RunCurrentMission(Room room);
        public void CompleteCurrentMission()
        {
            missions.Remove(currentMission);
            currentMission = null;
        }
        public virtual void TickMissions()
        {
            foreach (var m in missions)
            {
                if (m.totalAge >= 0)
                    ++m.totalAge;
                if (m == currentMission)
                    ++m.activeAge;
            }
        }
        public void Run(Room room)
        {

            TickMissions();
            ParseMemories();
            ElectMission();
            RunCurrentMission(room);
        }
        public virtual Room RunTravel(Room room, Room destination)
        {
            var newRoom = RunTravelMove(room, destination);
            if (newRoom != room)
            {
                room = newRoom;
                OnEnterRoom(room);
            }
            if (room == destination)
            {
                OnDestinationReached(room);
            }
            return newRoom;
        }

        public virtual Room RunTravelMove(Room from, Room to)
        {
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

        public virtual bool OnEnterRoom(Room room) { return false; }
        public virtual void OnDestinationReached(Room room) { }
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
            public Idle() : base(null) { }
        }
        public class Fighting : BehaviourMission
        {
            public Fighting() : base(null) { }
        }
        public class Rumor : BehaviourMission
        {
            public Rumor(RoomEvent sourceEvent, string rumorText) : base(sourceEvent) { this.rumorText = rumorText; }
            public string rumorText;
            public List<Character> spreadTo = new List<Character>();

            public override string ToString()
            {
                return @$"Rumor: ""{rumorText}"" (from {sourceEvent?.speakingCharacter})";
            }
        }
        public class Guard : BehaviourMission
        {
            public Guard(RoomEvent sourceEvent) : base(sourceEvent) { }
        }
        public class Roam : BehaviourMission
        {
            public Roam() : base(null) { }
        }
        public class Follow : BehaviourMission
        {
            public Follow(Character target) : base(null) { this.target = target; }
            public Character target;

            public override string ToString()
            {
                return @$"Follow: {target}";
            }
        }
        public class Travel : BehaviourMission
        {
            public Travel(Room destination) : base(null) { this.destination = destination; }
            public Room destination;

            public override string ToString()
            {
                return @$"Travel: {destination}";
            }
        }
        public class Snitch : BehaviourMission
        {
            public Snitch(RoomEvent sourceEvent) : base(sourceEvent) { }
            public Room destination;

            public string GetCrimeRumor()
            {
                switch (sourceEvent.type)
                {
                    case RoomEventType.CharacterAttacks:
                        return $"**{sourceEvent.attackingCharacter}** attacked someone in *{sourceEvent.room.name}*!";
                    case RoomEventType.CharacterKills:
                        return $"**{sourceEvent.killingCharacter}** killed someone in *{sourceEvent.room.name}*!";
                    default:
                        return $"Someone commited a crime in *{sourceEvent.room.name}*";
                }
            }

            public override string ToString()
            {
                return @$"Snitch: ""{GetCrimeRumor()}"" to {destination}";
            }
        }
        public class Hunt : BehaviourMission
        {
            public Hunt(RoomEvent sourceEvent) : base(sourceEvent) { }
            public Character target;
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
        public MoveDirection enteringDirection;

        public Character leavingCharacter;
        public MoveDirection leavingDirection;

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
