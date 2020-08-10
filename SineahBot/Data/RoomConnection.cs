using SineahBot.Commands;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public class RoomConnection
    {
        public RoomConnection()
        {

        }
        public RoomConnection(Guid idRoomA, Guid idRoomB)
        {
            this.idRoomA = idRoomA;
            this.idRoomB = idRoomB;
        }
        public Guid idRoomA { get; set; }
        public Guid idRoomB { get; set; }
        public MoveDirection[] directionFromA { get; set; } // list of direction in room A that will lead to room B
        public MoveDirection[] directionFromB { get; set; } // list of direction in room B that will lead to room A
        public string KeyItemName { get; set; }
    }
    public class RoomConnectionState
    {
        public RoomConnectionState(Room room)
        {
            toRoom = room;
        }
        public Room toRoom;
        public void Lock()
        {
            if (!locked) locked = true;
            if (mirrorConnection != null && !mirrorConnection.locked)
            {
                mirrorConnection.Lock();
            }
        }
        public void Unlock()
        {
            if (locked) locked = false;
            if (mirrorConnection != null && mirrorConnection.locked)
            {
                mirrorConnection.Unlock();
            }
        }
        public bool locked { get; private set; }
        public RoomConnectionState mirrorConnection;
        public string keyItemName = null;
    }
}

