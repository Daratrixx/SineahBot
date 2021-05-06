using SineahBot.Commands;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Behaviours
{
    public static class Sineah
    {
        public class SineahSnitch : BehaviourMission.Snitch
        {
            public SineahSnitch(RoomEvent e) : base(e)
            {
                destination = World.Sineah.Barracks.Rooms.Hall;
            }
        }

        public class SineahCitizen : Humanoid
        {
            public SineahCitizen() : base() { }

            public override void Init(NPC npc)
            {
                base.Init(npc);
                missions.Add(new BehaviourMission.Idle());
            }

            public override void GenerateRoamTravelMission()
            {
                currentMission = new BehaviourMission.Travel(World.Sineah.Streets.GetRooms().GetRandom());
            }

            public override void GenerateRumorMission(RoomEvent e)
            {
                base.GenerateRumorMission(e);
            }

            public override void GenerateSnitchMission(RoomEvent e)
            {
                CommandAct.Act(npc, e.room, "is horrified.");
                CommandSay.Say(npc, e.room, "Oh no!");
                missions.Add(new SineahSnitch(e));
            }
        }
        public class SineahBeggar : SineahCitizen
        {
            public SineahBeggar() : base() { }

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
        public class SineahRoamingCitizen : SineahCitizen
        {
            public SineahRoamingCitizen() : base() { }

            public override void Init(NPC npc)
            {
                base.Init(npc);
                missions.Add(new BehaviourMission.Roam());
            }
        }
        public class GuardCitizen : SineahCitizen
        {
            public GuardCitizen() : base() { }

            public override void Init(NPC npc)
            {
                base.Init(npc);
                missions.Add(new BehaviourMission.Guard(null));
            }

            public override void ParseMemory(RoomEvent e)
            {
                switch (e.type)
                {
                    case RoomEventType.CharacterEnters:
                        var hunting = missions.Where(x => x is BehaviourMission.Hunt).Select(x => (x as BehaviourMission.Hunt).target);
                        if (hunting.Count() == 0 || !hunting.Contains(e.enteringCharacter)) return;
                        CombatManager.EngageCombat(npc, e.enteringCharacter);
                        return;
                    case RoomEventType.CharacterAttacks:
                        missions.Add(missions.FirstOrDefault(x => x is BehaviourMission.Hunt hunt && hunt.target == e.attackingCharacter)
                        ?? new BehaviourMission.Hunt(e) { target = e.attackingCharacter });
                        return;
                    case RoomEventType.CharacterCasts:
                        if (!e.castingSpell.aggressiveSpell) break;
                        missions.Add(missions.FirstOrDefault(x => x is BehaviourMission.Hunt hunt && hunt.target == e.castingCharacter)
                        ?? new BehaviourMission.Hunt(e) { target = e.castingCharacter });
                        return;
                    case RoomEventType.CharacterKills:
                        missions.Add(missions.FirstOrDefault(x => x is BehaviourMission.Hunt hunt && hunt.target == e.killingCharacter)
                        ?? new BehaviourMission.Hunt(e) { target = e.killingCharacter });
                        return;
                    default:
                        break;
                }
                base.ParseMemory(e);
            }

            public override void ElectMission()
            {
                if (currentMission is BehaviourMission.Fighting) return; // keep fighting mission
                if (npc.characterStatus == CharacterStatus.Combat)
                {
                    currentMission = new BehaviourMission.Fighting();
                    missions.Add(currentMission);
                    return;
                }
            }

            public override bool OnEnterRoom(Room room)
            {
                var hunting = missions.Where(x => x is BehaviourMission.Hunt).Select(x => (x as BehaviourMission.Hunt).target);
                if (hunting.Count() == 0) return false;
                var hunted = room.characters.Where(x => hunting.Contains(x));
                if (hunted.Count() == 0) return false;
                CombatManager.EngageCombat(npc, hunted.ToArray());
                return true;
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
                    if(target != null)
                    CommandCombatAttack.Attack(npc, room, target);
                }
                base.RunCurrentMission(room);
            }
        }
    }
}
