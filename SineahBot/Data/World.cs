using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SineahBot.Data
{
    public class World
    {
        public void LoadWorld()
        {
            var r1 = new Room() { id = Guid.NewGuid(), name = "Spawn room", isSpawnRoom = true, description = "> A very simple, empty room. On the north wall, a door leads out of this room into a corridor." };
            var r2 = new Room() { id = Guid.NewGuid(), name = "Corridor", description = "> A tight, poorly lit corridor. It extends north and south, with a door on the eastern wall." };
            var r3 = new Room() { id = Guid.NewGuid(), name = "Bedroom", description = "> This small room only has a very old bed and a delapidated chest. It is dimly lit by a dirty window. The only door connects back into the corridor." };
            var r4 = new Room() { id = Guid.NewGuid(), name = "Spicy boy arena", description = "> You don't want to know..." };

            var c1 = new RoomConnection()
            {
                idRoomA = r1.id,
                idRoomB = r2.id,
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.Out },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South }
            };
            var c2 = new RoomConnection()
            {
                idRoomA = r2.id,
                idRoomB = r3.id,
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.In },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
            };
            var c3 = new RoomConnection()
            {
                idRoomA = r2.id,
                idRoomB = r4.id,
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South }
            };

            RoomManager.LoadRooms(new Room[] { r1, r2, r3, r4 });
            RoomManager.LoadRoomConnections(new RoomConnection[] { c1, c2, c3 });
        }
    }
}
