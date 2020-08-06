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

                    foreach (var direction in connection.directionFromA)
                    {
                        roomA.RegisterDirection(direction, roomB);
                    }
                    foreach (var direction in connection.directionFromB)
                    {
                        roomB.RegisterDirection(direction, roomA);
                    }

                }
                catch (Exception e)
                {
                    // Log error: impossible to register the connection between the two rooms
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
        public static void MoveFromRoom(Entity entity, Room room, MoveDirection direction)
        {
            var destination = room.GetRoomInDirection(direction);
            room.RemoveFromRoom(entity);
            destination.AddToRoom(entity);
        }
        public static void RemoveFromCurrentRoom(Entity entity)
        {
            if (entity.currentRoomId != Guid.Empty)
            {
                rooms[entity.currentRoomId].RemoveFromRoom(entity);
            }
        }
        public static Room GetRoom(Guid idRoom)
        {
            return rooms[idRoom];
        }

        public static Guid GetSpawnRoomId() {
            return rooms.Values.First(x => x.isSpawnRoom).id;
        }

    }
}
