using SineahBot.Commands;
using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBot.Tools
{
    public static class RoomManager
    {
        public static Dictionary<string, Room> rooms = new Dictionary<string, Room>();
        public static void LoadRooms(IEnumerable<Room> rooms)
        {
            foreach (var room in rooms)
            {
                if (RoomManager.rooms.TryGetValue(room.id, out var r))
                    Logging.Log($"Warning: room {r.name} will replace room {room.name} with id \"{room.id}\"");
                LoadRoomMessages(room);
                RoomManager.rooms[room.id] = room;
            }
        }
        public static void LoadRoomMessages(Room room)
        {
            var messages = Program.database.CharacterMessages.AsQueryable().Where(x => x.idRoom == room.id).ToArray();
            if (messages.Count() == 0) return;
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
            // erase existing messages
            Program.database.CharacterMessages.RemoveRange(Program.database.CharacterMessages);
            foreach (var room in rooms.Values)
            {
                // insert current messages
                var messages = room.displays.Where(x => x is Display.PlayerMessage).Select(x => x as Display.PlayerMessage);
                Program.database.CharacterMessages.AddRange(messages.Select(x => new CharacterMessage() { id = Guid.NewGuid(), idCharacter = x.idWritter, idRoom = room.id, message = x.content.First() }));
            }
        }
        public static void RemoveCharacterMessages(Character character)
        {
            foreach (var message in character.messages)
            {
                var room = rooms[message.idRoom];
                room.entities.RemoveAll(x => x is Display.PlayerMessage m && m.idWritter == character.id);
            }
            Program.database.CharacterMessages.RemoveRange(Program.database.CharacterMessages.AsQueryable().Where(x => x.idCharacter == character.id));
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
                rooms[entity.currentRoomId].RemoveFromRoom(entity, feedback);
            }
        }
        public static Room GetRoomById(string idRoom)
        {
            return rooms[idRoom];
        }
        public static Room GetRoomByName(string name)
        {
            return rooms.Values.FirstOrDefault(x => string.Equals(x.name, name, StringComparison.OrdinalIgnoreCase));
        }
        public static string GetSpawnRoomId()
        {
            return rooms.Values.First(x => x.isSpawnRoom).id;
        }
    }
}
