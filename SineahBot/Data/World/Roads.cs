using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.World
{
    public static class Roads
    {
        public static class s
        {
            public static class Rooms
            {
            }
            public static Room[] GetRooms()
            {
                return new Room[]{
                };
            }

            public static class Connections
            {
            }
            public static RoomConnection[] GetConnections()
            {
                return new RoomConnection[] {
                };
            }

            public static class Characters
            {
            }
            public static Character[] GetCharacters()
            {
                return new Character[] {
                };
            }

            public static class Items
            {
            }
            public static Item[] GetItems()
            {
                return new Item[] {
                };
            }
        }
        public static class SineahToDesert
        {
            public static class Rooms
            {
                private static string description = "You are on the road connecting Sineah to the Neraji desert";
                public static Room EasternRoad0 = new Room("Eastern road")
                {
                    description = $"{description}. West is the Eastern Gate leading into the city. Following the road East will eventually lead you to the Neraji desert."
                };
                public static Room EasternRoad1 = new Room("Eastern road stretch")
                {
                    description = $"{description}. Close by to the West is the city of Sineah. Following the road East will lead you to the Neraji desert."
                };
                public static Room EasternRoad2 = new Room("Eastern road highway")
                {
                    description = $"{description}. In the distance to the West, you can see the city of Sineah. Following the road East will lead you to the Neraji desert."
                };
                public static Room EasternRoad3 = new Room("Eastern road summit")
                {
                    description = $"{description}. Far to the West is the city of Sineah. To the East, you can see the Neraji desert stretch across the horizon."
                };
                public static Room DesertRoad0 = new Room("Desertic road")
                {
                    description = $"{description}. Following the road West will lead you to Sineah. Heading East, the road leads deeper into the Neraji desert."
                };
                public static Room DesertRoad1 = new Room("Neraji road")
                {
                    description = $"{description}. Following the road West will lead you to Sineah. Heading East, the road ends into the Neraji desert."
                };
            }
            public static Room[] GetRooms()
            {
                return new Room[]{
                Rooms.EasternRoad0,
                Rooms.EasternRoad1,
                Rooms.EasternRoad2,
                Rooms.EasternRoad3,
                Rooms.DesertRoad0,
                Rooms.DesertRoad1,
                };
            }

            public static class Connections
            {
                public static RoomConnection ER0_1 = new RoomConnection(Rooms.EasternRoad0, Rooms.EasternRoad1)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
                public static RoomConnection ER1_2 = new RoomConnection(Rooms.EasternRoad1, Rooms.EasternRoad2)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
                public static RoomConnection ER2_3 = new RoomConnection(Rooms.EasternRoad2, Rooms.EasternRoad3)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
                public static RoomConnection ER3_DR0 = new RoomConnection(Rooms.EasternRoad3, Rooms.DesertRoad0)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
                public static RoomConnection DR0_1 = new RoomConnection(Rooms.DesertRoad0, Rooms.DesertRoad1)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
            }
            public static RoomConnection[] GetConnections()
            {
                return new RoomConnection[] {
                    Connections.ER0_1,
                    Connections.ER1_2,
                    Connections.ER2_3,
                    Connections.ER3_DR0,
                    Connections.DR0_1,
                };
            }

            public static class Characters
            {
            }
            public static Character[] GetCharacters()
            {
                return new Character[] {
                };
            }

            public static class Items
            {
            }
            public static Item[] GetItems()
            {
                return new Item[] {
                };
            }
        }

        public static IEnumerable<Room> rooms;
        #region CONNECTIONS
        public static RoomConnection[] roadConnections = new RoomConnection[] {
            // sineah to eastern road
            new RoomConnection(Sineah.Streets.Rooms.EGate, SineahToDesert.Rooms.EasternRoad0)
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
            },
            // eastern road to desert
            new RoomConnection(SineahToDesert.Rooms.DesertRoad1, NerajiDesert.Desert.Rooms.DesertRoad2)
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
            },
        };
        #endregion
        public static void LoadWorld()
        {
            // LOAD ROADS
            // load eastern road
            RoomManager.LoadRooms(SineahToDesert.GetRooms());
            RoomManager.LoadRoomConnections(SineahToDesert.GetConnections());
            CharacterManager.LoadCharacters(SineahToDesert.GetCharacters());

            // CONNECT ROADS
            RoomManager.LoadRoomConnections(roadConnections);

            // PLACE CONTAINERS IN ROOMS


            // PLACE ITEMS IN ROOMS


            // PLACE ITEMS IN CONTAINERS


            // POPULATE ROOMS WITH NPCS


            // signs


            // books


            // populate global room list for graph construction
            rooms = SineahToDesert.GetRooms()
            .ToArray();

            // register behaviours

        }
    }
}
