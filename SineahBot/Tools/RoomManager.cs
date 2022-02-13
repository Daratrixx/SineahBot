using SineahBot.Commands;
using SineahBot.Data;
using SineahBot.Data.Enums;
using SineahBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBot.Tools
{
    public static class RoomManager
    {
        public static Dictionary<string, Room> Rooms = new Dictionary<string, Room>();
        public static void LoadRooms(IEnumerable<Room> rooms)
        {
            foreach (var room in rooms)
            {
                if (RoomManager.Rooms.TryGetValue(room.id, out var r))
                    Logging.Log($"Warning: room {r.name} will replace room {room.name} with id \"{room.id}\"");
                LoadRoomMessages(room);
                RoomManager.Rooms[room.id] = room;
            }
        }
        public static void LoadRoomMessages(Room room)
        {
            var messages = PersistenceManager.GetRoomMessages(room);
            if (!messages.Any()) return;
            foreach (var message in messages)
            {
                room.AddToRoom(new Display.PlayerMessage(message.idCharacter, message.message), false);
            }
        }
        public static void SaveRooms()
        {
            SaveRoomMessages();
        }
        public static void SaveRoomMessages()
        {
            var displays = Rooms.Values.SelectMany(x => x.displays);
            var messages = displays.Where(x => x is Display.PlayerMessage).Select(x => x as Display.PlayerMessage);
            PersistenceManager.SaveRoomMessages(messages);
        }
        public static void RemoveCharacterMessages(Character character)
        {
            foreach (var message in character.messages)
            {
                var room = Rooms[message.idRoom];
                room.entities.RemoveAll(x => x is Display.PlayerMessage m && m.idWritter == character.id);
            }
        }
        public static void LoadRoomConnections(IEnumerable<RoomConnection> roomConnections)
        {
            foreach (var connection in roomConnections)
            {
                try
                {
                    var roomA = RoomManager.Rooms[connection.idRoomA];
                    var roomB = RoomManager.Rooms[connection.idRoomB];

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
        public static void MoveToRoom(Entity entity, Room room)
        {
            RemoveFromCurrentRoom(entity);
            room.AddToRoom(entity);
        }
        public static bool MoveFromRoom(Character character, Room room, MoveDirection direction)
        {
            var destination = room.GetRoomConnectionInDirection(direction);
            if (destination.locked) return false;
            room.RaiseRoomEvent(new RoomEvent(room, RoomEventType.CharacterLeaves) { source = character, direction = direction }, character);
            room.RemoveFromRoom(character);
            destination.toRoom.AddToRoom(character);
            destination.toRoom.RaiseRoomEvent(new RoomEvent(destination.toRoom, RoomEventType.CharacterEnters) { source = character }, character);
            return true;
        }
        public static void RemoveFromCurrentRoom(Entity entity, bool feedback = true)
        {
            if (entity.currentRoomId != Guid.Empty.ToString())
            {
                Rooms[entity.currentRoomId].RemoveFromRoom(entity, feedback);
            }
        }
        public static Room GetRoomById(string idRoom)
        {
            return Rooms[idRoom];
        }
        public static Room GetRoomByName(string name)
        {
            return Rooms.Values.FirstOrDefault(x => string.Equals(x.name, name, StringComparison.OrdinalIgnoreCase));
        }
        public static string GetSpawnRoomId(CharacterAncestry ancestry)
        {
            return Rooms.Values.Where(x => x.AncestrySpawnRoom == ancestry).GetRandom().id;
        }
    }
}
