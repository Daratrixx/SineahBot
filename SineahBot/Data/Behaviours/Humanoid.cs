using SineahBot.Commands;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Data.Behaviours
{
    public abstract class Humanoid : Behaviour
    {
        public static Regex AttackRumor = new Regex(@$"Have you heard\? (\*\*(.+?)\*\* attacked (.+?) in (.+))[\?\!\.]?", RegexOptions.IgnoreCase);
        public static Regex KillRumor = new Regex(@$"Have you heard\? (\*\*(.+?)\*\* killed (.+?) in (.+))[\?\!\.]?", RegexOptions.IgnoreCase);

        protected List<BehaviourMission.Rumor> rumors = new List<BehaviourMission.Rumor>();
        public Humanoid() : base() { }

        public override void Init(NPC npc)
        {
            base.Init(npc);
            missions.Add(new BehaviourMission.Idle());
        }
        public abstract void GenerateRoamTravelMission();
        public abstract void ParseCrimeMemory(RoomEvent e);
        public virtual void ParseSpeachMemory(RoomEvent e)
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
        public virtual BehaviourMission.Rumor OnRumorRegexMatch(RoomEvent e, Match match)
        {
            var existingRumor = rumors
            .Where(x => x.sourceEvent.speakingContent == match.Groups[2].Value);
            BehaviourMission.Rumor rumor;
            if (existingRumor.Count() > 0)
            {
                rumor = existingRumor.First();
            }
            else
            {
                rumor = new BehaviourMission.Rumor(e, match.Groups[2].Value);
                rumors.Add(rumor);
            }
            SpreadRumor(e.room, rumor);
            return rumor;
        }
        public bool SpreadRumor(Room room, BehaviourMission.Rumor rumor)
        {
            if (rumor == null || room == null) return false;
            var characters = room.characters.Where(x => x != npc && !rumor.spreadTo.Contains(x));
            if (characters.Count() > 0)
            {
                rumor.spreadTo.AddRange(characters);
                return true;
            }
            return false;
        }
        public override void ParseMemory(RoomEvent e)
        {
            switch (e.type)
            {
                case RoomEventType.CharacterSpeaks:
                    ParseSpeachMemory(e);
                    break;
                case RoomEventType.CharacterCasts:
                    if (!e.castingSpell.aggressiveSpell) return;
                    if (npc.faction == e.attackingCharacter.faction) return; // ignore actions from same faction
                    ParseCrimeMemory(e);
                    break;
                case RoomEventType.CharacterKills:
                    if (npc.faction == e.killingCharacter.faction) return; // ignore actions from same action
                    ParseCrimeMemory(e);
                    break;
                case RoomEventType.CharacterAttacks:
                    if (npc.faction == e.attackingCharacter.faction) return; // ignore actions from same action
                    ParseCrimeMemory(e);
                    break;
                default:
                    break;
            }
        }
        public override void TickMissions()
        {
            base.TickMissions();
            foreach (var r in rumors)
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
            base.ElectMission();
        }
        public override void RunCurrentMission(Room room)
        {
            if (currentMission == null) return;
            if (currentMission is BehaviourMission.Roam roam)
            {
                GenerateRoamTravelMission();
                return;
            }
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
                    var rumor = rumors.GetRandom();
                    if (RunSpreadRumor(room, rumor))
                        return;
                }
                CompleteCurrentMission();
                return;
            }
            if (currentMission is BehaviourMission.Idle idle)
            {
                if (rumors.Count > 0 && random.Next(1, 100) <= 10)
                {
                    var rumor = rumors.GetRandom();
                    RunSpreadRumor(room, rumor);
                    return;
                }
                if (room != originalRoom)
                {
                    if (random.Next(1, 100) <= 50)
                        GenerateTravelToOriginMission();
                    return;
                }
            }
        }
        public bool RunSpreadRumor(Room room, BehaviourMission.Rumor rumor)
        {
            if (SpreadRumor(room, rumor))
            {
                CommandSay.Say(npc, room, $"Have you heard? {rumor.rumorText}");
                return true;
            }
            return false;
        }
        public override bool OnEnterRoom(Room room)
        {
            if (currentMission is BehaviourMission.Snitch snitch)
            {
                CommandSay.Say(npc, room, snitch.GetCrimeRumor());
                return true;
            }
            return false;
        }
    }
}
