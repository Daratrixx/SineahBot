using SineahBot.Commands;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Data.Behaviours
{
    public abstract class HumanoidBase : Behaviour
    {
        public static Regex AttackRumor = new Regex(@$"(Did you hear\?|Have you heard\?|Help!) (\*(.+?)\* attacked (.+?) in (.+))[\?\!\.]?", RegexOptions.IgnoreCase);
        public static Regex KillRumor = new Regex(@$"(Did you hear\?|Have you heard\?|Help!) (\*(.+?)\* killed (.+?) in (.+))[\?\!\.]?", RegexOptions.IgnoreCase);

        protected MemoryTracker memoryTracker = new MemoryTracker();
        protected RumorTracker rumorTracker = new RumorTracker();
        public HumanoidBase() : base() { }

        public virtual void GenerateRoamTravelMission(BehaviourMission.Roam roam)
        {
            var travel = new BehaviourMission.Travel(roam.GetDestination());
            if (travel.destination == null)
                Logging.Log("Error: Roam generated Travel without destination.");
            currentMission = travel;
            missions.Add(currentMission);
        }
        public abstract void ParseHostileEvent(RoomEvent e, Character source);
        public virtual void ParseSpeachEvent(RoomEvent e)
        {
            Match match;
            match = AttackRumor.Match(e.speakingContent);
            if (match.Success)
            {
                OnRumorRegexMatch(e, match);
                return;
            }
            match = KillRumor.Match(e.speakingContent);
            if (match.Success)
            {
                OnRumorRegexMatch(e, match);
                return;
            }
        }
        public virtual Rumor OnRumorRegexMatch(RoomEvent e, Match match)
        {
            var existingRumor = rumorTracker
            .Where(x => x.rumorText == match.Groups[2].Value);
            Rumor rumor;
            if (existingRumor.Count() > 0)
            {
                rumor = existingRumor.First();
            }
            else
            {
                rumor = new Rumor(e, match.Groups[2].Value);
                rumorTracker.Add(rumor);
            }
            SpreadRumor(e.room, rumor);
            return rumor;
        }
        public bool SpreadRumor(Room room, Rumor rumor)
        {
            if (rumor == null || room == null) return false;
            var characters = room.characters.Where(x => x != Npc && !rumor.spreadTo.Contains(x));
            if (characters.Count() > 0)
            {
                rumor.spreadTo.AddRange(characters);
                return true;
            }
            return false;
        }
        public override void ParseMemory(RoomEvent e)
        {
            memoryTracker.Add(e);
            switch (e.type)
            {
                case RoomEventType.CharacterSpeaks:
                    ParseSpeachEvent(e);
                    return;
                case RoomEventType.CharacterCasts:
                    if (!e.spell.aggressiveSpell) return;
                    ParseHostileEvent(e, e.source);
                    return;
                case RoomEventType.CharacterKills:
                    ParseHostileEvent(e, e.source);
                    return;
                case RoomEventType.CharacterAttacks:
                    ParseHostileEvent(e, e.source);
                    return;
                default:
                    break;
            }
        }
        public override void TickMissions()
        {
            base.TickMissions();
            foreach (var r in rumorTracker)
            {
                if (r.totalAge >= 0)
                    ++r.totalAge;
            }
        }
        public override void ElectMission()
        {
            var snitch = missions.FirstOrDefault(x => x is BehaviourMission.Snitch);
            if (snitch != null)
            {
                currentMission = snitch;
                return;
            }
            var travel = missions.FirstOrDefault(x => x is BehaviourMission.Travel);
            if (travel != null)
            {
                currentMission = travel;
                return;
            }
            var roaming = missions.FirstOrDefault(x => x is BehaviourMission.Roam);
            if (roaming != null)
            {
                currentMission = roaming;
                return;
            }
        }
        public override void RunCurrentMission(Room room)
        {
            if (currentMission is BehaviourMission.Snitch snitch)
            {
                if (room != snitch.destination)
                {
                    RunTravel(room, snitch.destination);
                    return;
                }
                // TODO: tell guards
                CompleteCurrentMission();
                return;
            }
            if (currentMission is BehaviourMission.Travel travel)
            {
                if (room != travel.destination)
                {
                    if (random.Next(1, 100) <= 50)
                    {
                        RunTravel(room, travel.destination);
                        return;
                    }
                    var rumor = rumorTracker.GetRandom();
                    if (RunSpreadRumor(room, rumor))
                        return;
                }
                CompleteCurrentMission();
                return;
            }
            if (currentMission is BehaviourMission.Roam roam)
            {
                GenerateRoamTravelMission(roam);
                return;
            }
            if (currentMission == null)
            {
                if (rumorTracker.Count > 0)
                {
                    if (random.Next(1, 100) <= 10)
                    {
                        var rumor = rumorTracker.GetRandom();
                        RunSpreadRumor(room, rumor);
                        return;
                    }
                }
                if (room != originalRoom)
                {
                    if (random.Next(1, 100) <= 50)
                        GenerateTravelToOriginMission();
                    return;
                }
            }
        }
        public bool RunSpreadRumor(Room room, Rumor rumor)
        {
            if (SpreadRumor(room, rumor))
            {
                CommandSay.Say(Npc, room, $"Have you heard? {rumor.rumorText}");
                return true;
            }
            return false;
        }
        public override bool OnEnterRoom(Room room)
        {
            if (currentMission is BehaviourMission.Snitch snitch)
            {
                CommandSay.Say(Npc, room, snitch.GetCrimeRumor());
                return true;
            }
            return false;
        }

        public override string GetRumorKnowledge()
        {
            if (rumorTracker.Count == 0)
                return "I haven't heard any rumors as of late.";
            return $"I have heard {string.Join("; ", rumorTracker.Select(x => x.rumorText))}.";
        }
    }
    public class CitizenBase<SnitchType> : HumanoidBase
    where SnitchType : BehaviourMission.Snitch
    {
        public static Regex AskRoomEvent = new Regex(@$"Did you see (.+?) (enter|leave) (.+)\?", RegexOptions.IgnoreCase);
        public static Regex AskTargetEvent = new Regex(@$"Did you see (.+?) (attack|kill) (.+?) in (.+)\?", RegexOptions.IgnoreCase);

        public CitizenBase() : base() { }

        public override void ParseSpeachEvent(RoomEvent e)
        {
            Match match;
            match = AskRoomEvent.Match(e.speakingContent);
            if (match.Success)
            {
                var source = match.Groups[1].Value;
                var verb = RoomEvent.GetTypeFromVerb(match.Groups[2].Value);
                var room = match.Groups[3].Value;
                Say(RoomManager.GetRoomById(Npc.currentRoomId), memoryTracker.ConfirmMemory(source, verb, room));
            }
            match = AskTargetEvent.Match(e.speakingContent);
            if (match.Success)
            {
                var source = match.Groups[1].Value;
                var verb = RoomEvent.GetTypeFromVerb(match.Groups[2].Value);
                var target = match.Groups[3].Value;
                var room = match.Groups[4].Value;
                Say(RoomManager.GetRoomById(Npc.currentRoomId), memoryTracker.ConfirmMemory(source, verb, target, room));
            }
            base.ParseSpeachEvent(e);
        }

        public override void ParseHostileEvent(RoomEvent e, Character source)
        {
            if (Npc.faction == source.faction) return;
            CommandAct.Act(Npc, e.room, "is horrified.");
            CommandSay.Say(Npc, e.room, "Oh no!");
            missions.Add(BehaviourMission.Snitch.New<SnitchType>(e));
        }
    }
    public class RoamingCitizenBase<SnitchType, RoamType> : CitizenBase<SnitchType>
    where SnitchType : BehaviourMission.Snitch
    where RoamType : BehaviourMission.Roam, new()
    {
        public RoamingCitizenBase() : base() { }

        public override void Init(NPC npc)
        {
            base.Init(npc);
            missions.Add(new RoamType());
        }
    }
    public class BeggarBase<SnitchType, RoamType> : RoamingCitizenBase<SnitchType, RoamType>
    where SnitchType : BehaviourMission.Snitch
    where RoamType : BehaviourMission.Roam, new()
    {
        public BeggarBase() : base() { }

        public override bool OnEnterRoom(Room room)
        {
            if (base.OnEnterRoom(room)) return true;
            if (room.characters.Where(x => x.agent is Player).Count() > 0)
            {
                CommandAct.Act(Npc, room, "notices you and smiles.");
                CommandSay.Say(Npc, room, "Go' a coin for a poor soul?");
                return true;
            }
            return false;
        }
    }
    public class MilitianBase : HumanoidBase
    {
        public List<BehaviourMission.Hunt> targets = new List<BehaviourMission.Hunt>();
        public MilitianBase() : base() { }

        public override void Init(NPC npc)
        {
            base.Init(npc);
        }

        public override void ParseMemory(RoomEvent e)
        {
            switch (e.type)
            {
                case RoomEventType.CharacterEnters:
                    if (targets.Count == 0 || targets.Count(x => x.target == e.source) == 0) return;
                    CombatManager.EngageCombat(Npc, e.source);
                    return;
                case RoomEventType.CharacterSpeaks:
                    ParseSpeachEvent(e);
                    break;
                case RoomEventType.CharacterCasts:
                    if (!e.spell.aggressiveSpell) return;
                    ParseHostileEvent(e, e.source);
                    break;
                case RoomEventType.CharacterKills:
                    ParseHostileEvent(e, e.source);
                    break;
                case RoomEventType.CharacterAttacks:
                    ParseHostileEvent(e, e.source);
                    break;
                default:
                    break;
            }
        }

        public override void ParseHostileEvent(RoomEvent e, Character source)
        {
            if (source.faction == Npc.faction) return;
            //if (FactionManager.GetFactionRelation(source.faction, npc.faction) <= FactionRelation.Neutral) return;
            var targetHunt = targets.FirstOrDefault(x => x.target == source);
            if (targetHunt != null) return;
            targetHunt = new BehaviourMission.Hunt(e, source);
            targets.Add(targetHunt);
            CombatManager.EngageCombat(Npc, source);
        }

        public override void ElectMission()
        {
            if (currentMission is BehaviourMission.Fighting) return; // keep fighting mission
            if (Npc.characterStatus == CharacterStatus.Combat) // initiate fighting mission
            {
                currentMission = new BehaviourMission.Fighting();
                missions.Add(currentMission);
                return;
            }
            if (currentMission != null && currentMission.activeAge <= random.Next(1, 100))
            {
                currentMission.activeAge = 0;
                currentMission = missions.GetRandom();
            }
        }

        public override void RunCurrentMission(Room room)
        {
            if (currentMission is BehaviourMission.Fighting fighting)
            {
                if (Npc.characterStatus != CharacterStatus.Combat)
                {
                    CompleteCurrentMission();
                    return;
                }
                var enemies = CombatManager.GetOpponents(Npc);
                if (enemies == null || enemies.Count() == 0)
                {
                    CompleteCurrentMission();
                    return;
                }
                var target = room.characters.Where(x => enemies.Contains(x)).GetRandom();
                if (target != null)
                    CommandCombatAttack.Attack(Npc, room, target);
                return;
            }
            if (currentMission == null)
            {
                if (rumorTracker.Count > 0 && random.Next(1, 100) <= 10)
                {
                    var rumor = rumorTracker.GetRandom();
                    RunSpreadRumor(room, rumor);
                    return;
                }
            }
        }

        public override bool OnEnterRoom(Room room)
        {
            if (targets.Count == 0) return false;
            var hunted = targets.Where(x => room.characters.Contains(x.target));
            if (hunted.Count() == 0) return false;
            CombatManager.EngageCombat(Npc, hunted.Select(x => x.target).ToArray());
            return true;
        }
    }

    public class AdventurerBase : MilitianBase
    {
        public override void ParseMemory(RoomEvent e)
        {
            switch (e.type)
            {
                case RoomEventType.CharacterKills:
                    Say(RoomManager.GetRoomById(Npc.currentRoomId), "This is bad!");
                    ParseHostileEvent(e, e.source);
                    return;
                default:
                    break;
            }
            base.ParseMemory(e);
        }
    }

    public class GuardBase<ReportType, RestType> : MilitianBase
    where ReportType : BehaviourMission.Report, new()
    where RestType : BehaviourMission.Rest, new()
    {
        public static Regex PatrolRegex = new Regex(@$"Guard (.+?), patrol to (.+)\.", RegexOptions.IgnoreCase);
        public static Regex InvestigateRegex = new Regex(@$"Guard (.+?), investigate (.+?)\.", RegexOptions.IgnoreCase);

        public static Regex InvestigateCrimeRegex = new Regex(@$"\*(.+?)\* (attacked|killed) (.+?) in (.+)[\?\!\.]?", RegexOptions.IgnoreCase);
        protected NPC captain;

        protected List<Character> suspiciousCharacters = new List<Character>();
        protected List<Character> criminalCharacters = new List<Character>();

        public GuardBase(NPC captain)
        {
            this.captain = captain;
        }

        public override void Init(NPC npc)
        {
            base.Init(npc);
            npc.npcStatus = 10;
            CaptainBase.RegisterCaptainGuardAffiliation(captain, npc);
        }

        public override void ParseSpeachEvent(RoomEvent e)
        {
            Match match;
            match = InvestigateRegex.Match(e.speakingContent);
            if (match.Success)
            {
                OnInvestigateRegexMatch(e, match);
                return;
            }
            match = PatrolRegex.Match(e.speakingContent);
            if (match.Success)
            {
                OnPatrolRegexMatch(e, match);
                return;
            }
            base.ParseSpeachEvent(e);
        }

        public BehaviourMission OnInvestigateRegexMatch(RoomEvent e, Match match)
        {
            // check if the order is targeted toward this NPC
            if (string.Equals(match.Groups[1].Value, Npc.npcName, StringComparison.OrdinalIgnoreCase))
            {
                var investigationString = match.Groups[2].Value;
                match = InvestigateCrimeRegex.Match(investigationString);
                if (match.Success)
                {
                    CompleteCurrentMission(); // make sure the report mission is gone...
                    var targetRoom = RoomManager.GetRoomByName(match.Groups[4].Value);
                    if (targetRoom == null) return null;
                    var mission = new BehaviourMission.Investigate(e, targetRoom, match.Groups[1].Value, match.Groups[3].Value);
                    missions.Add(mission);
                    currentMission = mission;
                    return mission;
                }
            }
            return null;
        }

        public BehaviourMission OnPatrolRegexMatch(RoomEvent e, Match match)
        {
            // check if the order is targeted toward this NPC
            if (string.Equals(match.Groups[1].Value, Npc.npcName, StringComparison.OrdinalIgnoreCase))
            {
                var targetRoom = RoomManager.GetRoomByName(match.Groups[2].Value);
                if (targetRoom != null)
                {
                    CompleteCurrentMission(); // make sure the report mission is gone...
                    var mission = new BehaviourMission.Patrol(targetRoom);
                    missions.Add(mission);
                    currentMission = mission;
                    return mission;
                }
            }
            return null;
        }

        public override void ElectMission()
        {
            if (currentMission is BehaviourMission.Fighting) return; // keep fighting mission
            if (Npc.characterStatus == CharacterStatus.Combat) // initiate fighting mission
            {
                currentMission = new BehaviourMission.Fighting();
                missions.Add(currentMission);
                return;
            }
            var investigate = missions.FirstOrDefault(x => x is BehaviourMission.Investigate) as BehaviourMission.Investigate;
            if (investigate != null)
            {
                currentMission = investigate;
                return;
            }
            var patrol = missions.FirstOrDefault(x => x is BehaviourMission.Patrol) as BehaviourMission.Patrol;
            if (patrol != null)
            {
                currentMission = patrol;
                return;
            }
            if (Npc.npcStatus == 0 || currentMission == null || !(currentMission is BehaviourMission.Report))
            {
                currentMission = missions.FirstOrDefault(x => x is ReportType) as ReportType ?? new ReportType();
                if (!missions.Contains(currentMission)) missions.Add(currentMission);
            }
        }

        public override void RunCurrentMission(Room room)
        {
            if (currentMission == null) return;
            if (currentMission is BehaviourMission.Report report)
            {
                if (room != report.reportRoom)
                {
                    RunTravel(room, report.reportRoom);
                    return;
                }
                if (!report.reported)
                {
                    GuardReport(room, report);
                    report.reported = true;
                    Npc.npcStatus = 20;
                }
                Npc.npcStatus += 2; // rest more the longer the guard awaits orders
                return;
            }
            if (currentMission is BehaviourMission.Investigate investigate)
            {
                if (room != investigate.destination)
                {
                    RunTravel(room, investigate.destination);
                    return;
                }
                InvestigateRoom(room, investigate);
                CompleteCurrentMission();
            }
            if (Npc.npcStatus > 0) --Npc.npcStatus;
            if (currentMission is BehaviourMission.Patrol patrol)
            {
                if (room != patrol.destination)
                {
                    RunTravel(room, patrol.destination);
                    return;
                }
                if (Npc.npcStatus == 0)
                    CompleteCurrentMission();
            }
            base.RunCurrentMission(room);
        }

        public string[] emptyReports = new string[] {
            "I have nothing to report.",
            "All is clear.",
            "All is good.",
            "Everything is fine.",
            "Nothing wrong out there.",
        };
        public void GuardReport(Room room, BehaviourMission.Report report)
        {
            List<string> eventReport = new List<string>();
            if (report.toReport != null)
            {
                eventReport.Add(GetInvestigationReport(report.toReport));
                report.toReport = null;
            }
            else
            {
                eventReport.AddRange(rumorTracker.Where(x => x.reported == false).Select(x => $"I have heard {x.rumorText}"));
                foreach (var rumor in rumorTracker)
                    rumor.reported = true;
            }
            if (eventReport.Count == 0)
                eventReport.Add(emptyReports.GetRandom());
            string output = $@"Guard {Npc.npcName} reporting. {string.Join("; ", eventReport.Where(x => !string.IsNullOrWhiteSpace(x)))}";

            CommandSay.Say(Npc, room, output);
        }

        public string GetInvestigationReport(BehaviourMission.Investigate investigate)
        {
            var output = $"I have investigated in {investigate.destination.GetName()}, and found that";
            if (investigate.confirmVictim)
            {
                return output + $" {investigate.victimName} was killed.";
            }
            return output + $" {investigate.victimName} was not killed.";
        }

        public void InvestigateRoom(Room room, BehaviourMission.Investigate investigate)
        {
            CommandAct.Act(Npc, room, "investigates the room.");
            var victimBody = room.FindInRoom<Container>(investigate.victimName);
            investigate.confirmVictim = victimBody != null;
            var report = missions.FirstOrDefault(x => x is ReportType) as ReportType ?? new ReportType();
            report.toReport = investigate;
            if (!missions.Contains(report)) missions.Add(report);
        }
    }

    public class CaptainBase : MilitianBase
    {
        public static Regex ReportRegex = new Regex(@$"Guard .+? reporting\. (.+)", RegexOptions.IgnoreCase);
        public static Regex AttackRumorReport = new Regex(@$"I have heard (\*(.+?)\* attacked (.+?) in (.+))", RegexOptions.IgnoreCase);
        public static Regex KillRumorReport = new Regex(@$"I have heard (\*(.+?)\* killed (.+?) in (.+))", RegexOptions.IgnoreCase);
        public static Regex crimeConfirmationReport = new Regex(@$"I confirm (\*(.+?)\* killed (.+?) in (.+))", RegexOptions.IgnoreCase);
        public static Dictionary<NPC, List<NPC>> captainAffiliation = new Dictionary<NPC, List<NPC>>();
        public static void RegisterCaptainGuardAffiliation(NPC captain, NPC npc)
        {
            if (captainAffiliation.TryGetValue(captain, out var guards))
            {
                guards.Add(npc);
                return;
            }
            guards = captainAffiliation[captain] = new List<NPC>();
            guards.Add(npc);
        }

        protected readonly List<Room> patrolRooms;
        protected List<NPC> guardList;
        protected CrimeTracker crimeTracker = new CrimeTracker();
        protected List<NPC> passiveGuards = new List<NPC>();

        public CaptainBase(IEnumerable<Room> patrolRooms)
        {
            this.patrolRooms = patrolRooms.Shuffle().ToList();
        }
        public override void Init(NPC npc)
        {
            base.Init(npc);
            if (captainAffiliation.TryGetValue(npc, out var guards))
                guardList = guards;
            else
                guardList = captainAffiliation[npc] = new List<NPC>();
            var room = RoomManager.GetRoomById(npc.currentRoomId);
            passiveGuards.AddRange(guardList.Where(x => room.npcs.Contains(x)));
        }

        public override void TickMissions()
        {
            base.TickMissions();
            crimeTracker.TickAffectations();
        }

        public override void ParseSpeachEvent(RoomEvent e)
        {
            Match match;
            match = ReportRegex.Match(e.speakingContent);
            if (match.Success)
            {
                OnReportRegexMatch(e, match);
                return;
            }
        }

        public void OnReportRegexMatch(RoomEvent e, Match match)
        {
            var rumors = match.Groups[1].Value.Split(";").Select(x => x.Trim());
            foreach (var rumor in rumors)
            {
                match = AttackRumorReport.Match(rumor);
                if (match.Success)
                {
                    OnReportRumorRegexMatch(e, match);
                    continue;
                }
                match = KillRumorReport.Match(rumor);
                if (match.Success)
                {
                    OnReportRumorRegexMatch(e, match);
                    continue;
                }
            }
            if (e.source is NPC guard && guardList.Contains(guard))
                passiveGuards.Add(guard);
        }

        public void OnReportRumorRegexMatch(RoomEvent e, Match match)
        {
            var rumorText = match.Groups[1].Value;
            var existingRumor = rumorTracker
            .Where(x => x.rumorText == rumorText);
            if (existingRumor.Count() == 0)
            {
                Rumor rumor = new Rumor(e, rumorText);
                rumorTracker.Add(rumor);
            }
        }

        public void OnReportCrimeRegexMatch(RoomEvent e, Match match)
        {
            var rumorText = match.Groups[1].Value;
            var existingRumor = rumorTracker
            .Where(x => x.rumorText == rumorText);
            if (existingRumor.Count() == 0)
            {
                CommandSay.Say(Npc, e.room, @$"Trying to confirm an unknown rumor: ""{rumorText}""");
                return;
            }
            var rumor = existingRumor.FirstOrDefault();
        }

        public override void RunCurrentMission(Room room)
        {
            if (currentMission is BehaviourMission.Fighting fighting)
            {
                if (Npc.characterStatus != CharacterStatus.Combat)
                {
                    CompleteCurrentMission();
                    return;
                }
                var enemies = CombatManager.GetOpponents(Npc);
                if (enemies == null || enemies.Count() == 0)
                {
                    CompleteCurrentMission();
                    return;
                }
                var target = enemies.Where(x => room.characters.Contains(x)).GetRandom();
                if (target != null)
                    CommandCombatAttack.Attack(Npc, room, target);
                return;
            }
            if (passiveGuards.Count > 0)
            {
                var guard = passiveGuards.Where(x => room.characters.Contains(x)).GetRandom();
                if (guard != null)
                {
                    GiveOrderToGuard(room, guard);
                }
            }
        }

        public void GiveOrderToGuard(Room room, NPC guard)
        {
            var rumor = rumorTracker.Where(x => x.reported == false).FirstOrDefault();
            if (rumor != null)
            {
                // send to investigate
                CommandSay.Say(Npc, room, $"Guard {guard.npcName}, investigate {rumor.rumorText}");
                rumor.reported = true;
            }
            else
            {
                // send to a room
                var patrolRoom = patrolRooms.First();
                CommandSay.Say(Npc, room, $"Guard {guard.npcName}, patrol to {patrolRoom.GetName()}.");
                // requeue room
                patrolRooms.Remove(patrolRoom);
                patrolRooms.Add(patrolRoom);
            }
            passiveGuards.Remove(guard);
        }
    }
}
