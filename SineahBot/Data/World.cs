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
            // The Inn
            #region ROOMS
            var r1 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Storage room",
                description = "A very simple room, cramped with rows of barrels, boxes, and racks. In the north wall, a flight of stairs leads out of the room."
            };
            var r2 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Corridor",
                description = "A tight, poorly lit corridor. It extends north and south, with a door on the eastern wall."
            };
            var r3 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Innkeeper's quarters",
                description = "This small room only has a very old bed and a delapidated chest. It is dimly lit by a dirty window. The only door connects back into the corridor."
            };
            var r4 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Common room",
                isSpawnRoom = true,
                description = "Several tables and chairs take most of the space in this room. A fireplace enlight and warm the place up."
            };
            var r5 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Backroom",
                description = "TODO"
            };
            var r6 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Kitchen",
                description = "TODO"
            };
            var r7 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Stables",
                description = "TODO"
            };
            var r8 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Upstair corridor",
                description = ""
            };
            var r9 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Room 1",
                description = ""
            };
            var r10 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Room 2",
                description = ""
            };
            var r11 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Room 3",
                description = ""
            };
            var r12 = new Room()
            {
                id = Guid.NewGuid(),
                name = "Room 4",
                description = ""
            };
            #endregion

            #region CONNECTIONS
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
            #endregion

            var i1 = new Item()
            {
                id = Guid.NewGuid(),
                name = "Doll",
                description = "A doll lies around.",
                details = "A little doll shaped out of rags."
            };


            RoomManager.LoadRooms(new Room[] { r1, r2, r3, r4 });
            RoomManager.LoadRoomConnections(new RoomConnection[] { c1, c2, c3 });
            ItemManager.LoadItems(new Item[] { i1 });

            r3.AddToRoom(i1);
        }


    }
}
