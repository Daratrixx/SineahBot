using SineahBot.Commands;

namespace SineahBot.Data
{
    public class RoomConnection
    {
        public RoomConnection()
        {

        }
        public RoomConnection(string idRoomA, string idRoomB)
        {
            this.idRoomA = idRoomA;
            this.idRoomB = idRoomB;
        }
        public RoomConnection(Room roomA, Room roomB)
        {
            this.idRoomA = roomA.id;
            this.idRoomB = roomB.id;
        }
        public string idRoomA { get; set; }
        public string idRoomB { get; set; }
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

