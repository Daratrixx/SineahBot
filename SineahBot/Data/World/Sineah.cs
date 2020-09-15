using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SineahBot.Data.World
{
    public static class Sineah
    {

        #region ITEMS
        // The Inn
        public static Item[] innItems = new Item[] {
            new Item("Innkeepers office key", new string[] { "key", "office key" }) {
                description = "An ornate key is lying around.",
                details = "A key giving access to the Four Winds innkeepers office, behind the private meeting room."
            },
            new Item("Room #1 key", new string[] { "key", "room key" }) {
                description = "A room key is lying around.",
                details = "A key giving access to the room #1 at the Four Winds inn, on the second floor."
            },
            new Item("Room #3 key", new string[] { "key", "room key" }) {
                description = "A room key is lying around.",
                details = "A key giving access to the room #3 at the Four Winds inn, on the second floor."
            },
            new Item("Ragdoll", new string[] { "doll", "rdoll" }) {
                description = "A ragdoll lies around.",
                details = "A little doll shaped out of rags."
            }
        };
        public static Item[] sineahItems = new Item[] {
        };

        #endregion

        #region ROOMS
        // SINEAH (city)
        // The Inn - inner traveller
        public static Room[] innRooms = new Room[]{
            new Room("The Four Winds common room")
            {
                isSpawnRoom = true,
                description = "Several tables and chairs take most of the space in this large room. Next to the eastern door leading to the back room, a fireplace warms the body and soul on weary travellers. The large western door leads outside, the small northern door leads to the kitchen. On the south wall, a flight of stairs leads to the bedrooms above."
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
            }
        };
        // The traveller shop - outer travellers
        // The church - outer travellers

        // The university - outer/inner science
        // The arcane spire - outer science
        // The workshop - inner science

        // The barracks - outer military
        // The weapon stack - inner military
        // The armory - inner military

        // The trader - outer commercial
        // The fence - hidden commercial


        public static Room[] sineahRooms = new Room[] {
            new Room("Sineah western city gate")
            {
                isSpawnRoom = false,
                description = "Massive gates marking the entrance to the city-state of Sineah."
            },
            new Room("Sineah northen city gate")
            {
                isSpawnRoom = false,
                description = "Massive gates marking the entrance to the city-state of Sineah."
            },
            new Room("Sineah eastern city gate")
            {
                isSpawnRoom = false,
                description = "Massive gates marking the entrance to the city-state of Sineah, facing the Neraji desert to the east, unforgiving land of the dangerous Kobolds."
            },
            new Room("Sineah southern city gate")
            {
                isSpawnRoom = false,
                description = "Massive gates marking the entrance to the city-state of Sineah."
            },
            new Room("Sineah outer science district")
            {
                isSpawnRoom = false,
                description = "This long district reaches from the western gate all the way to the central plaza. North is the University [CLOSED]. South is the Arcane Spire [CLOSED]. To the west is the city gate. The district continues further east, ending on the plaza."
            },
            new Room("Sineah outer travellers avenue")
            {
                isSpawnRoom = false,
                description = "This avenue stretches from the northen gate to the central plaza. West is a shop [CLOSED]. East is the Church [CLOSED]. To the north is the city gate. The avenue continues further south, ending on the plaza."
            },
            new Room("Sineah outer military path")
            {
                isSpawnRoom = false,
                description = "This fortified path connects the eastern gate to the city. North are the primary Barracks [CLOSED]. South are the secondary Barracks [CLOSED]. To the east is the city gate. The path continues further west, ending the plaza."
            },
            new Room("Sineah commercial street")
            {
                isSpawnRoom = false,
                description = "This street is a direct access from the southern gate to the central plaza. East is a Shop [CLOSED]. To the south is the city gate. North is the plaza."
            },
            new Room("Sineah inner science district")
            {
                isSpawnRoom = false,
                description = "This long district reaches from the western gate all the way to the central plaza. South is the Workshop [CLOSED]. The district continues further west, ending at the city gate. East is the plaza."
            },
            new Room("Sineah inner travellers avenue")
            {
                isSpawnRoom = false,
                description = "This avenue stretches from the northen gate to the central plaza. East is the Four winds inn. The avenue continues further north, ending at the city gate. South is the plaza."
            },
            new Room("Sineah inner military path")
            {
                isSpawnRoom = false,
                description = "This fortified path connects the eastern gate to the city. North is the Armory [CLOSED]. South is the Blacksmith [CLOSED]. The path continues further east, ending at city gate. West is the plaza."
            },
            new Room("Sineah hidden street")
            {
                isSpawnRoom = false,
                description = "A shady back alley. West will lead back to the security of the Commercial street."
            },
            new Room("Sineah central plaza")
            {
                isSpawnRoom = false,
                description = "The heart of Sineah, center of the city, hub of activity. Many guards, merchants, street artists, and adventurers go about their daily lifes."
            }
        };
        #endregion

        #region CONNECTIONS
        // The Inn
        public static RoomConnection[] innConnections = new RoomConnection[] {
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
        public static RoomConnection[] sineahConnections = new RoomConnection[] {
            new RoomConnection(sineahRooms[0].id, sineahRooms[4].id) // to outer science
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.In },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
            },
            new RoomConnection(sineahRooms[1].id, sineahRooms[5].id) // to outer travellers
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.In },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.Out }
            },
            new RoomConnection(sineahRooms[2].id, sineahRooms[6].id) // to outer military
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.In },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
            },
            new RoomConnection(sineahRooms[3].id, sineahRooms[7].id) // to outer commercial
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
            },
            new RoomConnection(sineahRooms[4].id, sineahRooms[8].id) // to inner science
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
            },
            new RoomConnection(sineahRooms[5].id, sineahRooms[9].id) // to inner travellers
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.South },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.Out }
            },
            new RoomConnection(sineahRooms[6].id, sineahRooms[10].id) // to inner military
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
            },
            new RoomConnection(sineahRooms[7].id, sineahRooms[11].id) // to hidden commercial
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
            },
            new RoomConnection(sineahRooms[8].id, sineahRooms[12].id) // to plaza
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West }
            },
            new RoomConnection(sineahRooms[9].id, sineahRooms[12].id) // to plaza
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.South },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.North }
            },
            new RoomConnection(sineahRooms[10].id, sineahRooms[12].id) // to plaza
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East }
            },
            new RoomConnection(sineahRooms[7].id, sineahRooms[12].id) // to plaza
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South }
            }
        };

        public static RoomConnection[] sineahBuildingConnections = new RoomConnection[] {
            new RoomConnection(sineahRooms[9].id, innRooms[0].id) // to inn
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.In },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
            }
        };
        #endregion

        #region CHARACTERS

        public static Character[] innCharacters = new Character[] {
            Templates.CityFolks.Bartender.Clone(),
            Templates.CityFolks.Drunk.Clone(),
            Templates.CityFolks.Customer.Clone(),
            Templates.CityFolks.ShadyConsumer.Clone(),
            Templates.Critters.Rat.Clone(),
            Templates.Critters.Rat.Clone(),
            Templates.Critters.Rat.Clone()
        };
        public static Character[] sineahCharacters = new Character[] {
            // w gate
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.Militian.Clone(),
            // n gate
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.Militian.Clone(),
            // e gate
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.Militian.Clone(),
            // s gate
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.Militian.Clone(),
            // inner
            Templates.CityFolks.Guard.Clone(),
            // plaza
            Templates.CityFolks.Militian.Clone(),
            Templates.CityFolks.Militian.Clone(),
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.Guard.Clone()
        };
        public static Character[] barrackCharacters = new Character[] {
            Templates.CityFolks.Militian.Clone(),
            Templates.CityFolks.Militian.Clone(),
            Templates.CityFolks.Militian.Clone(),
            Templates.CityFolks.Militian.Clone(),
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.Guard.Clone(),
            Templates.CityFolks.GuardCaptain.Clone()
        };

        #endregion
        public static void LoadWorld()
        {
            // LOAD BUILDINGS
            // load inn
            RoomManager.LoadRooms(innRooms);
            RoomManager.LoadRoomConnections(innConnections);
            ItemManager.LoadItems(innItems);
            CharacterManager.LoadCharacters(innCharacters);


            // LOAD STREETS
            RoomManager.LoadRooms(sineahRooms);
            RoomManager.LoadRoomConnections(sineahConnections);
            ItemManager.LoadItems(sineahItems);
            CharacterManager.LoadCharacters(sineahCharacters);
            // CONNECT BUILDINGS TO STREETS
            RoomManager.LoadRoomConnections(sineahBuildingConnections);

            // PLACE ITEMS IN ROOMS
            innRooms[4].AddToRoom(innItems[0]); // office key in office
            innRooms[4].AddToRoom(innItems[1]); // bedroom key 1 in office
            innRooms[4].AddToRoom(innItems[2]); // bedroom key 3 in office
            innRooms[9].AddToRoom(innItems[3]); // ragdoll in bedroom 3

            // POPULATE ROOMS WITH NPCS
            // populate inn
            innRooms[0].AddToRoom(innCharacters[0]); // bartender in common room
            innRooms[0].AddToRoom(innCharacters[1]); // drunk in common room
            innRooms[0].AddToRoom(innCharacters[2]); // customer in common room
            innRooms[0].AddToRoom(innCharacters[3]); // shady consumer in common room
            innRooms[2].AddToRoom(innCharacters[4]); // rat in cellar
            innRooms[2].AddToRoom(innCharacters[5]); // rat in cellar
            innRooms[2].AddToRoom(innCharacters[6]); // rat in cellar

            sineahRooms[0].AddToRoom(sineahCharacters[0]); // guard at west gate
            sineahRooms[0].AddToRoom(sineahCharacters[1]); // militian at west gate
            sineahRooms[1].AddToRoom(sineahCharacters[2]); // guard at north gate
            sineahRooms[1].AddToRoom(sineahCharacters[3]); // militian at north gate
            sineahRooms[2].AddToRoom(sineahCharacters[4]); // guard at east gate
            sineahRooms[2].AddToRoom(sineahCharacters[5]); // guard at east gate
            sineahRooms[2].AddToRoom(sineahCharacters[6]); // militian at east gate
            sineahRooms[3].AddToRoom(sineahCharacters[7]); // guard at south gate
            sineahRooms[3].AddToRoom(sineahCharacters[8]); // militian at south gate
            sineahRooms[10].AddToRoom(sineahCharacters[9]); // guard at inner military
            sineahRooms[12].AddToRoom(sineahCharacters[10]); // guard at plaza
            sineahRooms[12].AddToRoom(sineahCharacters[11]); // guard at plaza
            sineahRooms[12].AddToRoom(sineahCharacters[12]); // militian at plaza
            sineahRooms[12].AddToRoom(sineahCharacters[13]); // militian at plaza
        }
    }
}
