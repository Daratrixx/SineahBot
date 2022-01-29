using SineahBot.Commands;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Data.Behaviours
{
    public class SummonBase : Behaviour
    {
        public Character Summoner = null;
        public List<BehaviourMission.Hunt> Targets = new List<BehaviourMission.Hunt>();
        public SummonBase(Character summoner) : base()
        {
            this.Summoner = summoner;
        }

        public override void ParseMemory(RoomEvent e)
        {
            switch (e.type)
            {
                case RoomEventType.CharacterEnters:
                    if (Targets.Count == 0 || Targets.Count(x => x.target == e.source) == 0) return;
                    CombatManager.EngageCombat(Npc, e.source);
                    return;
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

        public virtual void ParseHostileEvent(RoomEvent e, Character source)
        {
            if (source != this.Summoner && e.target != this.Summoner && e.target != this.Npc) return; // ignore events not affecting summoner/summon
            var hostile = source == this.Summoner ? e.target : e.target == this.Npc ? source : null;
            if (hostile == this.Summoner || hostile == this.Npc) return; // ignore summon-summoner interactions
            var targetHunt = Targets.FirstOrDefault(x => x.target == hostile);
            if (targetHunt != null) return;
            targetHunt = new BehaviourMission.Hunt(e, hostile);
            if (Targets.Count(x => x.target == hostile) == 0)
                Targets.Add(targetHunt);
            CombatManager.EngageCombat(Npc, hostile);
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
        }

        public override void RunCurrentMission(Room room)
        {
            // make sure the summon is in the same room as the summoner
            if (this.Npc.currentRoomId != this.Summoner.currentRoomId)
            {
                var movementResult = RunTravelMove(room, RoomManager.GetRoomById(this.Summoner.currentRoomId));
                if (movementResult != room) return; // managed to get closer to summoner, nothing else to do
            }

            // if the summon is in the same room as or can't reach the summoner
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
        }

        public override bool OnEnterRoom(Room room)
        {
            if (Targets.Count == 0) return false;
            var hunted = Targets.Where(x => room.characters.Contains(x.target));
            if (hunted.Count() == 0) return false;
            CombatManager.EngageCombat(Npc, hunted.Select(x => x.target).ToArray());
            return true;
        }
    }
}
