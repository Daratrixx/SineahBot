using SineahBot.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Behaviours
{
    public abstract class CityFolks : Behaviour
    {
        public class SineahSnitch : BehaviourMission.Snitch
        {
            public SineahSnitch(RoomEvent e) : base(e)
            {
                destination = World.Sineah.Barracks.Rooms.Hall;
            }
        }
        public class SineahRoam : BehaviourMission.Roam
        {
            public SineahRoam(RoomEvent e) : base(e) { }
        }
        public override void ParseMemory(RoomEvent e)
        {
            switch (e.type)
            {
                case RoomEventType.CharacterKills:
                case RoomEventType.CharacterAttacks:
                    if (e.attackingCharacter is NPC) return; // try to ignore guard retaliation
                    CommandAct.Act(npc, e.room, "is horrified.");
                    CommandSay.Say(npc, e.room, "Oh no!");
                    missions.Add(new SineahSnitch(e));
                    break;
                default:
                    break;
            }
        }

        public override void ElectMission()
        {
            var snitch = missions.FirstOrDefault(x => x is BehaviourMission.Snitch);
            if (snitch != null)
            {
                currentMission = snitch;
            }
            base.ElectMission();
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
                    if (random.Next() % 2 == 0)
                        RunTravel(room, travel.destination);
                    return;
                }
                CompleteCurrentMission();
                return;
            }
        }

        public class Beggar : CityFolks
        {
            public Beggar()
            {
                missions.Add(new SineahRoam(null));
            }

            public override void Init(NPC npc)
            {
                base.Init(npc);
            }
            public override void RunCurrentMission(Room room)
            {
                if (currentMission is SineahRoam roam)
                {
                    currentMission = new BehaviourMission.Travel(World.Sineah.Streets.GetRooms().GetRandom());
                    return;
                }
                base.RunCurrentMission(room);
            }

            public override void OnDestinationReached(Room room)
            {
            }

            public override void OnEnterRoom(Room room)
            {
                if (room.characters.Where(x => x.agent is Player).Count() > 0)
                {
                    CommandAct.Act(npc, room, "notices you and smiles.");
                    CommandSay.Say(npc, room, "Go' a coin for a poor soul?");
                }
            }
        }
    }
}
