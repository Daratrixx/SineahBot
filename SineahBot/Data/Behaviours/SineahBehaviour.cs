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
        public class SineahRoam : BehaviourMission.Roam
        {
            public override Room GetDestination()
            {
                return World.Sineah.Streets.GetRooms().GetRandom();
            }
        }
        public class SineahReport : BehaviourMission.Report
        {
            public SineahReport() : base()
            {
                reportRoom = World.Sineah.Barracks.Rooms.Hall;
                restRoom = World.Sineah.Barracks.Rooms.GuardsRoom;
            }
        }
        public class SineahRest : BehaviourMission.Rest
        {
            public SineahRest() : base()
            {
                destination = World.Sineah.Barracks.Rooms.GuardsRoom;
            }
        }

        public class Citizen : CitizenBase<SineahSnitch>
        {
            public Citizen() : base() { }
        }
        public class RoamingCitizen : RoamingCitizenBase<SineahSnitch, SineahRoam>
        {
            public RoamingCitizen() : base() { }
        }
        public class Beggar : BeggarBase<SineahSnitch, SineahRoam>
        {
            public Beggar() : base() { }
        }
        public class Militian : MilitianBase
        {
            public Militian() : base() { }
        }
        public class Guard : GuardBase<SineahReport, SineahRest>
        {
            public Guard(NPC captain) : base(captain) { }
        }
        public class Captain : CaptainBase
        {
            public Captain() : base(World.Sineah.Streets.GetRooms()) { }
        }
    }
}
