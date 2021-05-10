using SineahBot.Commands;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Data.Behaviours
{
    public static class SineahBehaviour
    {
        public class SineahSnitch : BehaviourMission.Snitch
        {
            public SineahSnitch(RoomEvent e) : base(e)
            {
                destination = World.Sineah.Barracks.Rooms.Hall;
            }
        }
        public class SineahReport : BehaviourMission.Report
        {
            public SineahReport() : base(null)
            {
                destination = World.Sineah.Barracks.Rooms.Hall;
            }
        }
        public class SineahRest : BehaviourMission
        {
            public SineahRest() : base(null)
            {
                destination = World.Sineah.Barracks.Rooms.GuardsRoom;
            }
            public Room destination;
        }

        public class Citizen : Humanoid
        {
            public Citizen() : base() { }

            public override void Init(NPC npc)
            {
                base.Init(npc);
                missions.Add(new BehaviourMission.Idle());
            }

            public override void GenerateRoamTravelMission()
            {
                currentMission = new BehaviourMission.Travel(World.Sineah.Streets.GetRooms().GetRandom());
            }

            public override void ParseSpeachMemory(RoomEvent e)
            {
                base.ParseSpeachMemory(e);
            }

            public override void ParseCrimeMemory(RoomEvent e)
            {
                CommandAct.Act(npc, e.room, "is horrified.");
                CommandSay.Say(npc, e.room, "Oh no!");
                missions.Add(new SineahSnitch(e));
            }
        }
        public class Beggar : Citizen
        {
            public Beggar() : base() { }

            public override void Init(NPC npc)
            {
                base.Init(npc);
                missions.Add(new BehaviourMission.Roam());
            }

            public override bool OnEnterRoom(Room room)
            {
                if (base.OnEnterRoom(room)) return true;
                if (room.characters.Where(x => x.agent is Player).Count() > 0)
                {
                    CommandAct.Act(npc, room, "notices you and smiles.");
                    CommandSay.Say(npc, room, "Go' a coin for a poor soul?");
                    return true;
                }
                return false;
            }
        }
        public class RoamingCitizen : Citizen
        {
            public RoamingCitizen() : base() { }

            public override void Init(NPC npc)
            {
                base.Init(npc);
                missions.Add(new BehaviourMission.Roam());
            }
        }
        public class Militian : Citizen
        {
            public List<BehaviourMission.Hunt> targets = new List<BehaviourMission.Hunt>();
            public Militian() : base() { }

            public override void Init(NPC npc)
            {
                base.Init(npc);
            }

            public override void ParseMemory(RoomEvent e)
            {
                switch (e.type)
                {
                    case RoomEventType.CharacterEnters:
                        if (targets.Count() == 0 || targets.Count(x => x.target == e.enteringCharacter) == 0) return;
                        CombatManager.EngageCombat(npc, e.enteringCharacter);
                        return;
                    case RoomEventType.CharacterAttacks:
                        RegisterTarget(e, e.attackingCharacter);
                        return;
                    case RoomEventType.CharacterCasts:
                        if (!e.castingSpell.aggressiveSpell) break;
                        RegisterTarget(e, e.castingCharacter);
                        return;
                    case RoomEventType.CharacterKills:
                        RegisterTarget(e, e.killingCharacter);
                        return;
                    default:
                        break;
                }
                base.ParseMemory(e);
            }

            public void RegisterTarget(RoomEvent e, Character target)
            {
                if (target.faction == npc.faction) return;
                var targetHunt = targets.FirstOrDefault(x => x.target == target);
                if (targetHunt != null) return;
                targetHunt = new BehaviourMission.Hunt(e, target);
                targets.Add(targetHunt);
            }

            public override void ElectMission()
            {
                if (currentMission is BehaviourMission.Fighting) return; // keep fighting mission
                if (npc.characterStatus == CharacterStatus.Combat) // initiate fighting mission
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
                    if (npc.characterStatus != CharacterStatus.Combat)
                    {
                        CompleteCurrentMission();
                        return;
                    }
                    var enemies = CombatManager.GetOpponents(npc);
                    if (enemies == null || enemies.Count() == 0)
                    {
                        CompleteCurrentMission();
                        return;
                    }
                    var target = room.characters.Where(x => enemies.Contains(x)).GetRandom();
                    if (target != null)
                        CommandCombatAttack.Attack(npc, room, target);
                    return;
                }
                base.RunCurrentMission(room);
            }

            public override bool OnEnterRoom(Room room)
            {
                if (targets.Count == 0) return false;
                var hunted = targets.Where(x => room.characters.Contains(x.target));
                if (hunted.Count() == 0) return false;
                CombatManager.EngageCombat(npc, hunted.Select(x => x.target).ToArray());
                return true;
            }
        }

        public static Regex PatrolRegex = new Regex(@$"Guard (.+?), patrol to (.+).", RegexOptions.IgnoreCase);
        public static Regex InvestigateRegex = new Regex(@$"Guard (.+?), investigate (.+).", RegexOptions.IgnoreCase);
        public class Guard : Militian
        {
            protected NPC captain;

            protected List<Character> suspiciousCharacters = new List<Character>();
            protected List<Character> criminalCharacters = new List<Character>();

            public Guard(NPC captain)
            {
                this.captain = captain;
            }

            public override void Init(NPC npc)
            {
                base.Init(npc);
                npc.npcStatus = 20;
                Captain.RegisterCaptainGuardAffiliation(captain, npc);
            }

            public override void ParseSpeachMemory(RoomEvent e)
            {
                Match match;
                match = InvestigateRegex.Match(e.speakingContent);
                if (match.Success)
                {
                    OnRumorRegexMatch(e, match);
                    return;
                }
                match = PatrolRegex.Match(e.speakingContent);
                if (match.Success)
                {
                    OnPatrolRegexMatch(e, match);
                    return;
                }
                base.ParseSpeachMemory(e);
            }

            public BehaviourMission OnPatrolRegexMatch(RoomEvent e, Match match)
            {
                // check if the order is targeted toward this NPC
                if (string.Equals(match.Groups[1].Value, npc.npcName, StringComparison.OrdinalIgnoreCase))
                {
                    var targetRoom = RoomManager.GetRoom(match.Groups[2].Value);
                    if (targetRoom != null)
                    {
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
                if (npc.characterStatus == CharacterStatus.Combat) // initiate fighting mission
                {
                    currentMission = new BehaviourMission.Fighting();
                    missions.Add(currentMission);
                    return;
                }
                var patrol = missions.FirstOrDefault(x => x is BehaviourMission.Patrol) as BehaviourMission.Patrol;
                if (patrol != null)
                {
                    currentMission = patrol;
                    return;
                }
                if (npc.npcStatus == 0 || currentMission == null || !(currentMission is SineahReport))
                {
                    currentMission = missions.FirstOrDefault(x => x is SineahReport) as SineahReport ?? new SineahReport();
                }
            }

            public override void RunCurrentMission(Room room)
            {
                if (currentMission == null) return;
                if (currentMission is SineahReport report)
                {
                    if (room != report.destination)
                    {
                        RunTravel(room, report.destination);
                        return;
                    }
                    if (!report.reported)
                    {
                        GuardReport(room);
                        report.reported = true;
                        npc.npcStatus = 50;
                    }
                    npc.npcStatus += 3; // rest more the longer the guard awaits orders
                    return;
                }
                if (npc.npcStatus > 0) --npc.npcStatus;
                if (currentMission is BehaviourMission.Patrol patrol)
                {
                    if (room != patrol.destination)
                    {
                        RunTravel(room, patrol.destination);
                        return;
                    }
                    if (npc.npcStatus == 0)
                        CompleteCurrentMission();
                }
                base.RunCurrentMission(room);
            }

            public void GuardReport(Room room)
            {
                string eventReport;
                if (rumors.Count == 0)
                    eventReport = "I have nothing to report.";
                else
                {
                    eventReport = string.Join("; ", rumors.Where(x => x.reported == false).Select(x => $"I have heard {x.rumorText}"));
                    rumors.ForEach(x => x.reported = true);
                }
                string report = $@"Guard {npc.npcName} reporting. {eventReport}";

                CommandSay.Say(npc, room, report);
            }
        }

        public static Regex ReportRegex = new Regex(@$"Guard .+? reporting\. (.+)", RegexOptions.IgnoreCase);
        public static Regex AttackRumorReport = new Regex(@$"I have heard (\*\*(.+?)\*\* attacked (.+?) in (.+))", RegexOptions.IgnoreCase);
        public static Regex KillRumorReport = new Regex(@$"I have heard (\*\*(.+?)\*\* killed (.+?) in (.+))", RegexOptions.IgnoreCase);

        public class Captain : Militian
        {
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
            protected List<Character> suspiciousCharacters = new List<Character>();
            protected List<Character> criminalCharacters = new List<Character>();
            protected List<NPC> passiveGuards = new List<NPC>();

            public Captain(IEnumerable<Room> patrolRooms)
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
                var room = RoomManager.GetRoom(npc.currentRoomId);
                passiveGuards.AddRange(guardList.Where(x => room.npcs.Contains(x)));
            }

            public override void ParseSpeachMemory(RoomEvent e)
            {
                Match match;
                match = ReportRegex.Match(e.speakingContent);
                if (match.Success)
                {
                    OnReportRegexMatch(e, match);
                    return;
                }
                //base.GenerateRumorMission(e);
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
                if (e.speakingCharacter is NPC guard && guardList.Contains(guard))
                    passiveGuards.Add(guard);
            }

            public void OnReportRumorRegexMatch(RoomEvent e, Match match)
            {
                var rumorText = match.Groups[1].Value;
                var existingRumor = rumors
                .Where(x => x.sourceEvent.speakingContent == rumorText);
                if (existingRumor.Count() == 0)
                {
                    BehaviourMission.Rumor rumor = new BehaviourMission.Rumor(e, rumorText);
                    rumors.Add(rumor);
                }
            }

            public override void RunCurrentMission(Room room)
            {
                if (currentMission is BehaviourMission.Fighting fighting)
                {
                    if (npc.characterStatus != CharacterStatus.Combat)
                    {
                        CompleteCurrentMission();
                        return;
                    }
                    var enemies = CombatManager.GetOpponents(npc);
                    if (enemies == null || enemies.Count() == 0)
                    {
                        CompleteCurrentMission();
                        return;
                    }
                    var target = enemies.Where(x => room.characters.Contains(x)).GetRandom();
                    if (target != null)
                        CommandCombatAttack.Attack(npc, room, target);
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
                var rumor = rumors.Where(x => x.reported == false).FirstOrDefault();
                if (rumor != null)
                {
                    // send to investigate
                    CommandSay.Say(npc, room, $"Guard {guard.npcName}, investigate {rumor.rumorText}");
                    rumor.reported = true;
                }
                else
                {
                    // send to a room
                    var patrolRoom = patrolRooms.First();
                    CommandSay.Say(npc, room, $"Guard {guard.npcName}, patrol to {patrolRoom.GetName()}.");
                    // requeue room
                    patrolRooms.Remove(patrolRoom);
                    patrolRooms.Add(patrolRoom);
                }
                passiveGuards.Remove(guard);
            }
        }
    }
}
