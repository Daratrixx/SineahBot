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

            #region ITEMS
            // The Inn
            var innItems = new Item[] {
                new Item("Innkeepers office key", new string[] { "key", "office key" }) {
                    description = "A key is lying around.",
                    details = "A key giving access to the Four Winds innkeepers office, behind the private meeting room."
                },
                new Item("Room #1 key", new string[] { "key", "room key" }) {
                    description = "A key is lying around.",
                    details = "A key giving access to the room #1 at the Four Winds inn, on the second floor."
                },
                new Item("Room #3 key", new string[] { "key", "room key" }) {
                    description = "A key is lying around.",
                    details = "A key giving access to the room #3 at the Four Winds inn, on the second floor."
                },
                new Item("Ragoll", new string[] { "doll" }) {
                    description = "A ragdoll lies around.",
                    details = "A little doll shaped out of rags."
                }
            };
            #endregion
            #region ROOMS
            // The Inn
            var innRooms = new Room[]{
                new Room("The Four Winds common room")
                {
                    isSpawnRoom = true,
                    description = "Several tables and chairs take most of the space in this large room. Next to the eastern door leading to the back room, a fireplace warms the place. The large western door leads outside, the small northern door leads to the kitchen. On the south wall, a flight of stairs leads to the bedrooms upstairs."
                },
                new Room("Kitchen")
                {
                    description = "A room steaming with all the cooking taking place here. A large furnace and an imposing chauldron are actively exhaling the smell of food cooking. At the northern end of the room is a flight of stairs leading down to the cellar."
                },
                new Room("Cellar")
                {
                    description = "A very simple room, cramped with rows of barrels, boxes, and racks. In the north wall, a flight of stairs leads back to the kitchen."
                },
                new Room("Meeting room")
                {
                    description = "Only one large table occupy the space of this room reserved for private meetings and dinners. Cut from the main room by the eastern door, the northern door leads to the inkeepers office."
                },
                new Room("Innkeepers office")
                {
                    description = "This small room only has a chair, a desk, a simple but comfortable futon, and a heavy, solid looking, locked chest."
                },
                new Room("Second floor landing")
                {
                    description = "The start of the corridor has two doors for bedrooms #1 and #2 on the eastern and western wall respectively. A flight of stairs go south back down to the main hall, and the corridor extends north to more rooms."
                },
                new Room("Second floor corridor")
                {
                    description = "The end section of the corridor has two doors for bedrooms #3 and #4 on the eastern and western wall respectively."
                },
                new Room("Bedroom #1")
                {
                    description = "A smaller bedroom, granting privacy for up to two people."
                },
                new Room("Bedroom #2")
                {
                    description = "A larger shared dormitory, that can hold up to 8 people."
                },
                new Room("Bedroom #3")
                {
                    description = "A smaller bedroom, granting privacy for up to two people."
                },
                new Room("Bedroom #4")
            {
                description = "A larger shared dormitory, that can hold up to 8 people."
            },
            };
            #endregion

            #region CONNECTIONS
            // The Inn
            var innConnections = new RoomConnection[] {
                new RoomConnection(innRooms[0].id,innRooms[1].id) // to kitchen
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                },
                new RoomConnection(innRooms[1].id, innRooms[2].id) // to cellar
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In, Commands.MoveDirection.Down },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.Out, Commands.MoveDirection.Up }
                },
                new RoomConnection(innRooms[0].id,innRooms[3].id) // to meeting room
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
                },
                new RoomConnection(innRooms[3].id,innRooms[4].id) // to inkeepers office
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out },
                    KeyItemName = "Innkeepers office key"
                },
                new RoomConnection(innRooms[0].id,innRooms[5].id) // to 2nd floor landing
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.In, Commands.MoveDirection.Up },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out, Commands.MoveDirection.Down }
                },
                new RoomConnection(innRooms[5].id,innRooms[6].id) // to 2nd floor corridor
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                },
                new RoomConnection(innRooms[5].id,innRooms[7].id) // to bedroom 1
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out },
                    KeyItemName = "Room #1 key"
                },
                new RoomConnection(innRooms[5].id,innRooms[8].id) // to bedroom 2
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                },
                new RoomConnection(innRooms[6].id,innRooms[9].id) // to bedroom 3
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out },
                    KeyItemName = "Room #3 key"
                },
                new RoomConnection(innRooms[6].id,innRooms[10].id) // to bedroom 4
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                }
            };
            #endregion

            #region CHARACTERS

            var innCharacters = new Character[]{
                new NPC() {
                    id = Guid.NewGuid(),
                    characterStatus = CharacterStatus.Normal,
                    level = 3,
                    maxHealth = 50,
                    health = 50,
                    gold = 50,
                    name = "Bartender",
                    experience = 50,
                    shortDescription = "The bartender is ready to serve drinks at the bars",
                    longDescription = "The bartender hangs behind the bar, cleaning glasses and serving drinks to the customers of the inn.",
                    alternativeNames = new string[] { "host" }
                },
                new NPC() {
                    id = Guid.NewGuid(),
                    characterStatus = CharacterStatus.Normal,
                    level = 1,
                    maxHealth = 11,
                    health = 11,
                    gold = 0,
                    name = "Giant rat",
                    shortDescription = "A giant rat prowls in the room.",
                    longDescription = "A huge ass rat. Dangerous enough to take out an adventurer.",
                    alternativeNames = new string[] { "rat", "huge rat" }
                },
            };

            #endregion

            RoomManager.LoadRooms(innRooms);
            RoomManager.LoadRoomConnections(innConnections);
            ItemManager.LoadItems(innItems);
            CharacterManager.LoadCharacters(innCharacters);


            innRooms[0].AddToRoom(innCharacters[0]);
            innRooms[2].AddToRoom(innCharacters[1]);

            innRooms[4].AddToRoom(innItems[0]);
            innRooms[4].AddToRoom(innItems[1]);
            innRooms[4].AddToRoom(innItems[2]);
            innRooms[9].AddToRoom(innItems[3]);
        }


    }
}
