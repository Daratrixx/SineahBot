﻿using SineahBot.Commands;
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
    }
}
