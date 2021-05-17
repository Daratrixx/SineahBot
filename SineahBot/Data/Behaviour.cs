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
        public void PrepareMemory()
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
            memorySwapped.Clear();
        }
        public abstract void ParseMemory(RoomEvent e);
        public abstract void ElectMission();
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

        public virtual string GetRumorKnowledge()
        {
            return null;
        }

        public virtual Room RunTravelMove(Room from, Room to)
        {
            if (from == to) return from;

            var dir = PathBuilder.GetNextMove(from, to);
            if (dir == MoveDirection.None)
                return from;

            if (npc.characterStatus == CharacterStatus.Combat
            ? Flee(from, dir)
            : Move(from, dir))
            {
                return RoomManager.GetRoom(npc.currentRoomId);
            }

            return from;
        }
        public virtual bool OnEnterRoom(Room room) { return false; }
        public virtual void OnDestinationReached(Room room) { }

        public bool Move(Room from, MoveDirection dir)
        {
            return CommandMove.Move(npc, from, dir);
        }
        public bool Flee(Room from, MoveDirection dir)
        {
           return CommandCombatFlee.Flee(npc, from, dir);
        }
        public void Say(Room room, string say)
        {
            CommandSay.Say(npc, room, say);
        }
        public void Act(Room room, string act)
        {
            CommandAct.Act(npc, room, act);
        }
        public void Attack(Room room, Character target)
        {
            CommandCombatAttack.Attack(npc, room, target);
        }
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
        public class Fighting : BehaviourMission
        {
            public Fighting() : base(null) { }
        }
        
        public abstract class Roam : BehaviourMission
        {
            public Roam() : base(null) { }

            public abstract Room GetDestination();
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
        public class Investigate : BehaviourMission
        {
            public Investigate(RoomEvent e, Room destination, string perpetrator, string victim) : base(e)
            {
                this.destination = destination;
                this.perpetratorName = perpetrator;
                this.victimName = victim;
            }
            public Room destination;
            public string perpetratorName;
            public string victimName;
            public bool confirmPerpetrator;
            public bool confirmVictim;

            public override string ToString()
            {
                return @$"Investigate: {perpetratorName}:{victimName} ({destination})";
            }
        }
        public class Report : BehaviourMission
        {
            public Report() : base(null) { }
            public Room reportRoom;
            public Room restRoom;
            public bool reported;
            public Investigate toReport;
            public override string ToString()
            {
                return @$"Report: to {reportRoom}";
            }
        }
        public class Rest : BehaviourMission
        {
            public Rest() : base(null) { }
            public Room destination;
            public override string ToString()
            {
                return @$"Rest: to {destination}";
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
                        return $"Help! *{sourceEvent.source}* attacked someone in {sourceEvent.room.name}.";
                    case RoomEventType.CharacterKills:
                        return $"Help! *{sourceEvent.source}* killed someone in {sourceEvent.room.name}.";
                    default:
                        return $"Help! Someone committed a crime in {sourceEvent.room.name}.";
                }
            }

            public override string ToString()
            {
                return @$"Snitch: ""{GetCrimeRumor()}"" to {destination}";
            }

            public static Snitch New<T>(RoomEvent e) where T : Snitch
            {
                var ctor = typeof(T).GetConstructor(new Type[] { typeof(RoomEvent) });
                T output = (T)ctor.Invoke(new object[] { e });
                return output;
            }
        }
        public class Hunt : BehaviourMission
        {
            public Hunt(RoomEvent sourceEvent, Character target) : base(sourceEvent)
            {
                this.target = target;
            }
            public Character target;
            public bool reported;
        }
        public class Patrol : BehaviourMission
        {
            public Patrol(Room destination) : base(null) { this.destination = destination; }
            public Room destination;

            public override string ToString()
            {
                return @$"Patrol: {destination}";
            }
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

        public Character source;
        public MoveDirection direction;
        public Character target;
        public Spell spell;
        public string speakingContent;
        public string actingContent;

        public static RoomEventType GetTypeFromVerb(string verb)
        {
            switch (verb)
            {
                case "attack":
                case "attacked":
                case "attacking":
                    return RoomEventType.CharacterAttacks;
                case "kill":
                case "killed":
                case "killing":
                    return RoomEventType.CharacterKills;
                case "enter":
                case "enters":
                case "entered":
                case "entering":
                    return RoomEventType.CharacterEnters;
                case "leave":
                case "leaves":
                case "leaved":
                case "leaving":
                    return RoomEventType.CharacterLeaves;
                default:
                    return RoomEventType.CharacterActs;
            }
        }
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
