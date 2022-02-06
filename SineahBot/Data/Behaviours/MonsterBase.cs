using SineahBot.Commands;
using SineahBot.Data.Enums;
using SineahBot.Extensions;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBot.Data.Behaviours
{
    public class MonsterBase : Behaviour
    {
        public List<BehaviourMission.Hunt> targets = new List<BehaviourMission.Hunt>();
        public MonsterBase() : base() { }

        public override void ParseMemory(RoomEvent e)
        {
            switch (e.type)
            {
                case RoomEventType.CharacterEnters:
                    if (FactionManager.GetFactionRelation(e.source.faction, Npc.faction) <= FactionRelation.Hostile) // always engage hostile characters
                    {
                        if (targets.Count(x => x.target == e.source) == 0)
                            targets.Add(new BehaviourMission.Hunt(e, e.source));
                        CombatManager.EngageCombat(Npc, e.source);
                        return;
                    }
                    if (targets.Count == 0 || targets.Count(x => x.target == e.source) == 0) return;
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
            if (source.faction == Npc.faction) return; // ignore same-faction events
            var targetHunt = targets.FirstOrDefault(x => x.target == source);
            if (targetHunt != null) return;
            targetHunt = new BehaviourMission.Hunt(e, source);
            if (targets.Count(x => x.target == source) == 0)
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
        }

        public override bool OnEnterRoom(Room room)
        {
            foreach (var character in room.characters.Where(x => FactionManager.GetFactionRelation(x.faction, Npc.faction) <= FactionRelation.Hostile))
            {
                if (targets.Count(x => x.target == character) == 0)
                    targets.Add(new BehaviourMission.Hunt(null, character));
            }
            if (targets.Count == 0) return false;
            var hunted = targets.Where(x => room.characters.Contains(x.target));
            if (hunted.Count() == 0) return false;
            CombatManager.EngageCombat(Npc, hunted.Select(x => x.target).ToArray());
            return true;
        }
    }
}
