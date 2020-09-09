using SineahBot.Commands;
using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SineahBot.Tools
{
    public static class RoomManager
    {
        public static Dictionary<Guid, Room> rooms = new Dictionary<Guid, Room>();

        public static void LoadRooms(IEnumerable<Room> rooms)
        {
            foreach (var room in rooms)
            {
                RoomManager.rooms.Add(room.id, room);
            }
        }
        public static void LoadRoomConnections(IEnumerable<RoomConnection> roomConnections)
        {
            foreach (var connection in roomConnections)
            {
                try
                {
                    var roomA = RoomManager.rooms[connection.idRoomA];
                    var roomB = RoomManager.rooms[connection.idRoomB];

                    var connectionA = new RoomConnectionState(roomB) { keyItemName = connection.KeyItemName };
                    var connectionB = new RoomConnectionState(roomA) { keyItemName = connection.KeyItemName };
                    connectionA.mirrorConnection = connectionB;
                    connectionB.mirrorConnection = connectionA;

                    foreach (var direction in connection.directionFromA)
                    {
                        roomA.RegisterDirection(direction, connectionA);
                    }
                    foreach (var direction in connection.directionFromB)
                    {
                        roomB.RegisterDirection(direction, connectionB);
                    }

                }
                catch (Exception e)
                {
                    // Log error: impossible to register the connection between the two rooms
                    Logging.Log($"Impossible to register room connection: {e.Message}");
                }
            }
        }
        public static void LoadCharacters(IEnumerable<Character> characters)
        {

        }
        public static void LoadItems(IEnumerable<Item> items)
        {

        }
        public static void MoveToRoom(Entity entity, Room room)
        {
            RemoveFromCurrentRoom(entity);
            room.AddToRoom(entity);
        }
        public static bool MoveFromRoom(Entity entity, Room room, MoveDirection direction)
        {
            var destination = room.GetRoomConnectionInDirection(direction);
            if (destination.locked) return false;
            room.RemoveFromRoom(entity);
            destination.toRoom.AddToRoom(entity);
            return true;
        }
        public static void RemoveFromCurrentRoom(Entity entity, bool feedback = true)
        {
            if (entity.currentRoomId != Guid.Empty)
            {
                rooms[entity.currentRoomId].RemoveFromRoom(entity, feedback);
            }
        }
        public static Room GetRoom(Guid idRoom)
        {
            return rooms[idRoom];
        }

        public static Guid GetSpawnRoomId()
        {
            return rooms.Values.First(x => x.isSpawnRoom).id;
        }

    }
}
