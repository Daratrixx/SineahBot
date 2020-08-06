using SineahBot.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public class RoomConnection
    {
        public Guid idRoomA { get; set; }
        public Guid idRoomB { get; set; }
        public MoveDirection[] directionFromA { get; set; } // list of direction in room A that will lead to room B
        public MoveDirection[] directionFromB { get; set; } // list of direction in room B that will lead to room A
    }
}
