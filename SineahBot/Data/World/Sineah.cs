using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SineahBot.Data.World
{
    public static class Sineah
    {
        public static NPC AddSineahCommonKnowledge(this NPC npc)
        {
            return npc
                .RegisterKnowlede(new string[] { "town", "city", "sineah" }, "\"**Sineah is a nice city to live in. You can learn about the history of the town at the Library.**\"")
                .RegisterKnowlede(new string[] { "guard", "guards", "militia", "militian" }, "\"**The guards are keeping the city safe from danger, both from external menace and internal chaos.**\"")
                .RegisterKnowlede(new string[] { "inn", "four winds", "four winds inn" }, "\"**The Four Winds inn is renowned even outside of Sineah. It's a popular stop for travellers and adventurers alike. You have to try their speciality!**\"");
        }
        public static NPC AddSineahInnKnowledge(this NPC npc)
        {
            return npc
                .RegisterKnowlede(new string[] { "inn speciality", "inn blanquette", "speciality", "blanquette" }, "\"**Ah, that's the famous dish of the place. It will fill you right up and give you the energy to crush a hundred kobolds!**\"")
                .RegisterKnowlede(new string[] { "room", "rooms", "stay" }, "\"**Sorry, the inn is fully book for another few days, no rooms are available at the moment.**\"");
        }

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
        public static class Barracks
        {
            public static class Rooms
            {
                public static Room Entrance = new Room("Barracks entrance")
                {
                    description = "This narrow, easily defendable corridor connects the barracks to the streets."
                };
                public static Room Hall = new Room("Barracks hall")
                {
                    description = "Heart of barracks, branching to the other sections. North is the guards room, east is the captain room, west is the equipment room, and south leads back out."
                };
                public static Room GuardsRoom = new Room("Guards room")
                {
                    description = "Main gathering place for guards."
                };
                public static Room LivingQuarters = new Room("Guards living quarters")
                {
                    description = "Military quarters housing the city's garrison. Rows of beds are lined up, and a few cupboards are used to store the guards personal effects."
                };
                public static Room CaptainRoom = new Room("Captain room")
                {
                    description = "The room serves both as a meeting room and as the captains office. A small altar thrones in a corner."
                };
                public static Room EquipmentRoom = new Room("Equipment storage room")
                {
                    description = "Room cramped with standard guard equipment and other supplies."
                };
            }
            public static Room[] GetRooms()
            {
                return new Room[]{
                    Rooms.Entrance,
                    Rooms.Hall,
                    Rooms.GuardsRoom,
                    Rooms.LivingQuarters,
                    Rooms.CaptainRoom,
                    Rooms.EquipmentRoom,
                };
            }

            public static class Connections
            {
                public static RoomConnection EntranceHall = new RoomConnection(Rooms.Entrance.id, Rooms.Hall.id) // to guards room
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection HallGuardsRoom = new RoomConnection(Rooms.Hall.id, Rooms.GuardsRoom.id) // to guards room
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection GuardRoomsLivingQuarters = new RoomConnection(Rooms.GuardsRoom.id, Rooms.LivingQuarters.id) // to guards living quarters
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection HallCaptainRoom = new RoomConnection(Rooms.Hall.id, Rooms.CaptainRoom.id) // to captain room
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
                };
                public static RoomConnection HallEquipmentRoom = new RoomConnection(Rooms.Hall.id, Rooms.EquipmentRoom.id) // to equipment room
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                };
            }
            public static RoomConnection[] GetConnections()
            {
                return new RoomConnection[] {
                    Connections.EntranceHall,
                    Connections.HallGuardsRoom,
                    Connections.GuardRoomsLivingQuarters,
                    Connections.HallCaptainRoom,
                    Connections.HallEquipmentRoom,
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
        public static class Church
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
        public static class Inn
        {
            public static class Containers
            {
                public static Container Room1Chest = Templates.Containers.Chest.Clone();
                public static Container Room3Chest = Templates.Containers.Chest.Clone();
                public static Container OfficeDesk = Templates.Containers.Desk.Clone();
                public static Container OfficeChest = Templates.Containers.IronChest.Clone()
                .SetKeyItemName("Iron chest key");
            }
            public static class Shops
            {
                public static Shop Bartender = new Shop()
                .RegisterEntry(Templates.Consumables.Water, 1, null)
                .RegisterEntry(Templates.Consumables.Beer, 2, null)
                .RegisterEntry(Templates.Consumables.Wine, 8, null);
                public static Shop Waiter = new Shop()
                .RegisterEntry(Templates.Consumables.Water, 1, null)
                .RegisterEntry(Templates.Consumables.Beer, 2, null)
                .RegisterEntry(Templates.Consumables.Wine, 8, null)
                .RegisterEntry(Templates.Consumables.FourWindsBlanquette, 10, null);
                public static Shop Cook = new Shop()
                .RegisterEntry(Templates.Goods.BreadBundle, null, 10)
                .RegisterEntry(Templates.Goods.MeatPackage, null, 20);
                public static Shop ShadyConsumer = new Shop()
                .RegisterEntry(Templates.Consumables.HealthPotion, 25, null)
                .RegisterEntry(Templates.Consumables.ManaPotion, 25, null)
                .RegisterEntry(Templates.Equipments.Armor.ShadowCloak, 500, null);
            }
            public static class Rooms
            {
                public static Room CommonRoom = new Room("The Four Winds common room")
                {
                    isSpawnRoom = true,
                    description = "Several tables and chairs take most of the space in this large room. Next to the eastern door leading to the back room, a fireplace warms the body and soul on weary travellers. The large western door leads outside, the small northern door leads to the kitchen. On the south wall, a flight of stairs leads to the bedrooms above."
                };
                public static Room Kitchen = new Room("Kitchen")
                {
                    description = "A room steaming with all the cooking taking place here. A large furnace and an imposing chauldron are actively exhaling the smell of food cooking. At the northern end of the room is a flight of stairs leading down to the cellar."
                };
                public static Room Cellar = new Room("Cellar")
                {
                    description = "A very simple room, cramped with rows of barrels, boxes, and racks. In the north wall, a flight of stairs leads back to the kitchen."
                };
                public static Room MeetingRoom = new Room("Meeting room")
                {
                    description = "Only one large table occupy the space of this room reserved for private meetings and dinners. Cut from the main room by the eastern door, the northern door leads to the inkeepers office."
                };
                public static Room InnkeeprsOffice = new Room("Innkeepers office")
                {
                    description = "This small room only has a chair, a desk, a simple but comfortable futon, and a heavy, solid looking, locked chest."
                };
                public static Room SecondFloorLanding = new Room("Second floor landing")
                {
                    description = "The start of the corridor has two doors for bedrooms #1 and #2 on the eastern and western wall respectively. A flight of stairs go south back down to the main hall, and the corridor extends north to more rooms."
                };
                public static Room SecondFloorCorridor = new Room("Second floor corridor")
                {
                    description = "The end section of the corridor has two doors for bedrooms #3 and #4 on the eastern and western wall respectively."
                };
                public static Room Bedroom1 = new Room("Bedroom #1")
                {
                    description = "A smaller bedroom, granting privacy for up to two people."
                };
                public static Room Bedroom2 = new Room("Bedroom #2")
                {
                    description = "A larger shared dormitory, that can hold up to 8 people."
                };
                public static Room Bedroom3 = new Room("Bedroom #3")
                {
                    description = "A smaller bedroom, granting privacy for up to two people."
                };
                public static Room Bedroom4 = new Room("Bedroom #4")
                {
                    description = "A larger shared dormitory, that can hold up to 8 people."
                };
            }
            public static Room[] GetRooms()
            {
                return new Room[]{
                    Rooms.CommonRoom,
                    Rooms.Kitchen,
                    Rooms.Cellar,
                    Rooms.MeetingRoom,
                    Rooms.InnkeeprsOffice,
                    Rooms.SecondFloorLanding,
                    Rooms.SecondFloorCorridor,
                    Rooms.Bedroom1,
                    Rooms.Bedroom2,
                    Rooms.Bedroom3,
                    Rooms.Bedroom4
                };
            }

            public static class Connections
            {
                public static RoomConnection CommonRoomKitchen = new RoomConnection(Inn.Rooms.CommonRoom.id, Inn.Rooms.Kitchen.id) // to kitchen
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection KitchenCellar = new RoomConnection(Inn.Rooms.Kitchen.id, Inn.Rooms.Cellar.id) // to cellar
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In, Commands.MoveDirection.Down },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.Out, Commands.MoveDirection.Up }
                };
                public static RoomConnection CommonRoomMeetingRoom = new RoomConnection(Inn.Rooms.CommonRoom.id, Inn.Rooms.MeetingRoom.id) // to meeting room
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
                };
                public static RoomConnection MeetingRoomOffice = new RoomConnection(Inn.Rooms.MeetingRoom.id, Inn.Rooms.InnkeeprsOffice.id) // to inkeepers office
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out },
                    KeyItemName = "Innkeepers office key"
                };
                public static RoomConnection CommonRoomSecondFloorLanding = new RoomConnection(Inn.Rooms.CommonRoom.id, Inn.Rooms.SecondFloorLanding.id) // to 2nd floor landing
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.In, Commands.MoveDirection.Up },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out, Commands.MoveDirection.Down }
                };
                public static RoomConnection SecondFloorLandingSecondFloorCorridor = new RoomConnection(Inn.Rooms.SecondFloorLanding.id, Inn.Rooms.SecondFloorCorridor.id) // to 2nd floor corridor
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection SecondFloorLandingBedroom1 = new RoomConnection(Inn.Rooms.SecondFloorLanding.id, Inn.Rooms.Bedroom1.id) // to bedroom 1
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out },
                    KeyItemName = "Room #1 key"
                };
                public static RoomConnection SecondFloorLandingBedroom2 = new RoomConnection(Inn.Rooms.SecondFloorLanding.id, Inn.Rooms.Bedroom2.id) // to bedroom 2
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                };
                public static RoomConnection SecondFloorCorridorBedroom3 = new RoomConnection(Inn.Rooms.SecondFloorCorridor.id, Inn.Rooms.Bedroom3.id) // to bedroom 3
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out },
                    KeyItemName = "Room #3 key"
                };
                public static RoomConnection SecondFloorCorridorBedroom4 = new RoomConnection(Inn.Rooms.SecondFloorCorridor.id, Inn.Rooms.Bedroom4.id) // to bedroom 4
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                };
            }
            public static RoomConnection[] GetConnections()
            {
                return new RoomConnection[] {
                    Connections.CommonRoomKitchen,
                    Connections.KitchenCellar,
                    Connections.CommonRoomMeetingRoom,
                    Connections.MeetingRoomOffice,
                    Connections.CommonRoomSecondFloorLanding,
                    Connections.SecondFloorLandingSecondFloorCorridor,
                    Connections.SecondFloorLandingBedroom1,
                    Connections.SecondFloorLandingBedroom2,
                    Connections.SecondFloorCorridorBedroom3,
                    Connections.SecondFloorCorridorBedroom4
                };
            }

            public static class Characters
            {
                public static Character Bartender = Templates.CityFolks.Bartender.Clone()
                    .RegisterShop(Shops.Bartender)
                    .GenerateTraderKnowledge()
                    .AddSineahCommonKnowledge()
                    .AddSineahInnKnowledge()
                    .RegisterKnowlede(new string[] { "food", "meal" }, "\"**Have a seat and order from the **waiter**. Nothing beats a full stomach on a hard day.**\"")
                    .RegisterKnowlede(new string[] { "drink", "drinks" }, "\"**We can serve you a refreshing drink to help you gather your thoughts.**\"")
                    .RegisterKnowlede("drunk", "\"**Oh, he's been here everyday for weeks... I wonder what happened to that poor soul. They used to have a good life. They open up, though.**\"")
                    .RegisterKnowlede("shady consumer", "\"**Leave them alone, unless you want to get into trouble. I'm sure they dwel in some illegal activities...**\"")
                    .RegisterKnowlede("waiter", "\"**Ask them for drinks, or the inn's specility.**\"");
                public static Character Waiter = Templates.CityFolks.Waiter.Clone()
                    .RegisterShop(Shops.Waiter)
                    .GenerateTraderKnowledge()
                    .AddSineahCommonKnowledge()
                    .AddSineahInnKnowledge()
                    .RegisterKnowlede(new string[] { "food", "meal", "drink", "drinks" }, "\"**I can serve you food and drinks if you order.**\"");
                public static Character Cook = Templates.CityFolks.Cook.Clone()
                    .RegisterShop(Shops.Cook)
                    .GenerateTraderKnowledge()
                    .AddSineahCommonKnowledge()
                    .RegisterKnowlede(new string[] { "food", "meal", "drink", "drinks" }, "\"**The **waiter** can serve you food and drinks if you order.**\"")
                    .RegisterKnowlede(new string[] { "speciality", "blanquette" }, "*The cook smiles at you.* \"**My blanquette is the best. Family recipe! Order it from the waiter and taste it for yourself.**\"");
                public static Character Drunk = Templates.CityFolks.Drunk.Clone()
                    .RegisterKnowlede(new string[] { "sineah", "city", "town" }, "\"**I used to love this city. I fought for her for many years. Maybe I should leave now though...**\"")
                    .RegisterKnowlede("guards", "*They smile for a bit.*\n\"**I used to be a guard myself.**\"")
                    .RegisterKnowlede("sewer", "*They shiver, visibly scared.*\n\"**Don't go down there. You'll regret it.**\"")
                    .RegisterKnowlede("undead", "*Their eyes widen for a second.*\n\"**They are better left alone. But the church won't do anything about it...**\"" +
                    "\n*They shake their head.*\n\"**Just don't go there. It's not safe. I can't go there...**\"");
                public static Character Customer = Templates.CityFolks.Customer.Clone()
                    .AddSineahCommonKnowledge();
                public static Character ShadyConsumer = Templates.CityFolks.ShadyConsumer.Clone()
                    .RegisterShop(Shops.ShadyConsumer);
                public static Character Rat1 = Templates.Critters.Rat.Clone();
                public static Character Rat2 = Templates.Critters.Rat.Clone();
                public static Character Rat3 = Templates.Critters.Rat.Clone();
            }
            public static Character[] GetCharacters()
            {
                return new Character[] {
                    Characters.Bartender,
                    Characters.Drunk,
                    Characters.Customer,
                    Characters.ShadyConsumer,
                    Characters.Rat1,
                    Characters.Rat2,
                    Characters.Rat3
                };
            }

            public static class Items
            {
                public static Item IronChestKey = new Item("Iron chest key", new string[] { "key", "iron key" })
                {
                    description = "A heavy iron key is laying around.",
                    details = "A key giving access to the iron chest in the Four Winds innkeepers office.",
                    permanant = false
                };
                public static Item OfficeKey = new Item("Innkeepers office key", new string[] { "key", "office key" })
                {
                    description = "An ornate key is lying around.",
                    details = "A key giving access to the Four Winds innkeepers office, behind the private meeting room.",
                    permanant = false
                };
                public static Item Room1Key = new Item("Room #1 key", new string[] { "key", "room key" })
                {
                    description = "A room key is lying around.",
                    details = "A key giving access to the room #1 at the Four Winds inn, on the second floor.",
                    permanant = false
                };
                public static Item Room3Key = new Item("Room #3 key", new string[] { "key", "room key" })
                {
                    description = "A room key is lying around.",
                    details = "A key giving access to the room #3 at the Four Winds inn, on the second floor.",
                    permanant = false
                };
                public static Item Ragdoll = new Item("Ragdoll", new string[] { "doll", "rdoll" })
                {
                    description = "A ragdoll lies around.",
                    details = "A little doll shaped out of rags.",
                    permanant = false
                };
            }
            public static Item[] GetItems()
            {
                return new Item[] {
                    Items.OfficeKey,
                    Items.Room1Key,
                    Items.Room3Key,
                    Items.Ragdoll
                };
            }
        }
        public static class University
        {
            public static class Shops
            {
            }
            public static class Rooms
            {
                public static Room MainHall = new Room("University main hall")
                {
                    isSpawnRoom = true,
                    description = "This large hall serve as both an entry point and a gathering area. The only door opened to the public is the western door labeled \"**Library**\""
                };
                public static Room LibraryEntrance = new Room("Library entrance")
                {
                    isSpawnRoom = true,
                    description = "This is the entrance area for the library. It's not possible to enter yet, but a few books are displayed here for the public to read."
                };
            }
            public static Room[] GetRooms()
            {
                return new Room[]{
                    Rooms.MainHall,
                    Rooms.LibraryEntrance,
                };
            }

            public static class Connections
            {
                public static RoomConnection HallLibrary = new RoomConnection(Rooms.MainHall.id, Rooms.LibraryEntrance.id) // to kitchen
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                };
            }
            public static RoomConnection[] GetConnections()
            {
                return new RoomConnection[] {
                    Connections.HallLibrary,
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
        public static class Underground
        {
            public static class Rooms
            {
                public static Room SewerAccess = new Room("Illegal sewer access")
                {
                    description = "This damp underground room is an illegitimate entry point to the sewer system. Contaminated water flows from the western tunnel, and runs into to the brighter eastern tunnel. Up north is a ladder leading out of the sewer system."
                };
                public static Room DischargeTunnel = new Room("Discharge tunnel")
                {
                    description = "This steep tunnel evacuate the water coming from the western tunnel out of the city sewer system. Daylight can be seen at the eastern end."
                };
                public static Room SewerTunnel = new Room("Sewer tunnel")
                {
                    description = "A tunnel part of the sewer system. Water flows from the north and drains to the east."
                };
                public static Room ConvergenceRoom = new Room("Convergence room")
                {
                    description = "This massive underground room has multiple tunnels discharging their wastes in a collection of large pools, all connected to the southern exit. Water from the west seems way more polluted than the water from the north and the east."
                };
                public static Room ToxicDrain = new Room("Toxic drain")
                {
                    description = "The water going down this tunnel is unusually tainted. It comes from the western collector and leaks into the eastern tunnel."
                };
                public static Room ToxicWaste = new Room("Toxic waste")
                {
                    description = "This collector is filled to the brim with corrupted mater, result of the Spire experiments. It affects the sewer dwellers. The waste slowly drains to the east."
                };
                public static Room SteepDrain = new Room("Steep drain")
                {
                    description = "This tunnel comes from the north, the angle allowing the numerous wastes to be evacuated more easily. Water is drained south."
                };
                public static Room CollectingRoom1 = new Room("Collecting room")
                {
                    description = "This collecting room is relatively clean thanks to the effective water drain evacuating the water down south."
                };
                public static Room EasternDrain = new Room("Eastern drain")
                {
                    description = "This tunnel drains the water from the eastern waste collecting room to the western convergence room. Another tunnel opens on the northern wall, but no water comes from it."
                };
                public static Room CollectingRoom2 = new Room("Collecting room")
                {
                    description = "This collector is in decent condition. Its water drains westward."
                };
                public static Room DeterioratedDrain = new Room("Deteriorated drain")
                {
                    description = "This tunnel is very damaged, and dry. Its collecting room is up north, and the waste would run down south if there was any."
                };
                public static Room CrumbledCollector = new Room("Crumbled collector")
                {
                    description = "This waste collector hasn't been used in years and is perfectly dry. However, a cold, moist atmosphere seemingly leaks out of the eastern wall. In the past, water would have run down the southern tunnel."
                };
                public static Room ForgottenPassage = new Room("Forgotten passage")
                {
                    description = "This gallery seems way older than the sewer system. The access to the sewers is westward. After a tight turn, the tunnel goes north, deeper into the unknown."
                };
                public static Room LowArchway = new Room("Low archway")
                {
                    description = "The southern gallery led to a low-ceiling tunnel, with sculpted arches every few steps. It seems to open up to a room further north."
                };
                public static Room Heart = new Room("Heart of the catacombs")
                {
                    description = "This vast underground room is littered with bones and remains. Pillars support large arches, and smaller arches decorate the other openings, situated at the western, northern, and eastern ends of the room."
                };
                public static Room Underway1 = new Room("Underway")
                {
                    description = "This underway leads from the eastern Heart and continues westward."
                };
                public static Room Crypt = new Room("Ancient crypt gate")
                {
                    description = "The tunnel ends on massive stones gate. They seem unmovable. The only exit is the eastern tunnel you came from."
                };
                public static Room Underway2 = new Room("Underway")
                {
                    description = "This underway leads from the southern Heart and continues northward."
                };
                public static Room Ossuary = new Room("Ossuary")
                {
                    description = "This wide, low-ceiling room walls and floor are covered in bones. No visible exit beside the southern tunnel."
                };
                public static Room Underway3 = new Room("Underway")
                {
                    description = "This underway leads from the western Heart and continues eastward."
                };
                public static Room Altar = new Room("Destroyed altar")
                {
                    description = "The broken altar dedicated to the practicices of an unknown cult. The only visible exit is westward."
                };
            }
            public static Room[] GetRooms()
            {
                return new Room[]{
                    Rooms.SewerAccess,
                    Rooms.DischargeTunnel,
                    Rooms.SewerTunnel,
                    Rooms.ConvergenceRoom,
                    Rooms.ToxicDrain,
                    Rooms.ToxicWaste,
                    Rooms.SteepDrain,
                    Rooms.CollectingRoom1,
                    Rooms.EasternDrain,
                    Rooms.CollectingRoom2,
                    Rooms.DeterioratedDrain,
                    Rooms.CrumbledCollector,
                    Rooms.ForgottenPassage,
                    Rooms.LowArchway,
                    Rooms.Heart,
                    Rooms.Underway1,
                    Rooms.Crypt,
                    Rooms.Underway2,
                    Rooms.Ossuary,
                    Rooms.Underway3,
                    Rooms.Altar,
                };
            }

            public static class Connections
            {
                public static RoomConnection AccessDischarge = new RoomConnection(Rooms.SewerAccess.id, Rooms.DischargeTunnel.id) // to discharge point
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.In }
                };
                public static RoomConnection AccessTunnel = new RoomConnection(Rooms.SewerAccess.id, Rooms.SewerTunnel.id) // to sewer tunnel
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                };
                public static RoomConnection TunnelConvergence = new RoomConnection(Rooms.SewerTunnel.id, Rooms.ConvergenceRoom.id) // to convergence room
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection ConvergenceToxic = new RoomConnection(Rooms.ConvergenceRoom.id, Rooms.ToxicDrain.id) // to toxic
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                };
                public static RoomConnection ToxicToxic = new RoomConnection(Rooms.ToxicDrain.id, Rooms.ToxicWaste.id) // to toxic
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                };
                public static RoomConnection ConvergenceSteep = new RoomConnection(Rooms.ConvergenceRoom.id, Rooms.SteepDrain.id) // to steep
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection SteepCollector = new RoomConnection(Rooms.SteepDrain.id, Rooms.CollectingRoom1.id) // to collecting
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection ConvergenceEastern = new RoomConnection(Rooms.ConvergenceRoom.id, Rooms.EasternDrain.id) // to eastern
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
                };
                public static RoomConnection EasternCollector = new RoomConnection(Rooms.EasternDrain.id, Rooms.CollectingRoom2.id) // to collecting
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
                };
                public static RoomConnection EasternDeteriorated = new RoomConnection(Rooms.EasternDrain.id, Rooms.DeterioratedDrain.id) // to deteriorated
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection DeterioratedCrumbled = new RoomConnection(Rooms.DeterioratedDrain.id, Rooms.CrumbledCollector.id) // to crumbled
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection CrumbledForgotten = new RoomConnection(Rooms.CrumbledCollector.id, Rooms.ForgottenPassage.id) // to forgotten
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
                };
                public static RoomConnection ForgottenLow = new RoomConnection(Rooms.ForgottenPassage.id, Rooms.LowArchway.id) // to low
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection LowHeart = new RoomConnection(Rooms.LowArchway.id, Rooms.Heart.id) // to heart
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection HeartUnderway1 = new RoomConnection(Rooms.Heart.id, Rooms.Underway1.id) // to underway
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                };
                public static RoomConnection UnderwayCrypt = new RoomConnection(Rooms.Underway1.id, Rooms.Crypt.id) // crypt
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                };
                public static RoomConnection HeartUnderway2 = new RoomConnection(Rooms.Heart.id, Rooms.Underway2.id) // to underway
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection UnderwayOssuary = new RoomConnection(Rooms.Underway2.id, Rooms.Ossuary.id) // to ossuary
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection HeartUnderway3 = new RoomConnection(Rooms.Heart.id, Rooms.Underway3.id) // to underway
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
                };
                public static RoomConnection UnderwayAltar = new RoomConnection(Rooms.Underway3.id, Rooms.Altar.id) // to altar
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
                };
            }
            public static RoomConnection[] GetConnections()
            {
                return new RoomConnection[] {
                    Connections.AccessDischarge,
                    Connections.AccessTunnel,
                    Connections.TunnelConvergence,
                    Connections.ConvergenceToxic,
                    Connections.ToxicToxic,
                    Connections.ConvergenceSteep,
                    Connections.SteepCollector,
                    Connections.ConvergenceEastern,
                    Connections.EasternCollector,
                    Connections.EasternDeteriorated,
                    Connections.DeterioratedCrumbled,
                    Connections.CrumbledForgotten,
                    Connections.ForgottenLow,
                    Connections.LowHeart,
                    Connections.HeartUnderway1,
                    Connections.UnderwayCrypt,
                    Connections.HeartUnderway2,
                    Connections.UnderwayOssuary,
                    Connections.HeartUnderway3,
                    Connections.UnderwayAltar,
                };
            }

            public static class Characters
            {
                public static Character Rat1 = Templates.Critters.Rat.Clone(); // tunnel
                public static Character Rat2 = Templates.Critters.Rat.Clone(); // convergence
                public static Character Rat3 = Templates.Critters.Rat.Clone(); // convergence
                public static Character RabidRat1 = Templates.Critters.RabidRat.Clone(); // convergence
                public static Character RabidRat2 = Templates.Critters.RabidRat.Clone(); // toxic tunnel
                public static Character RabidRat3 = Templates.Critters.RabidRat.Clone(); // toxic tunnel
                public static Character GiantRat1 = Templates.Critters.GiantRat.Clone(); // toxic waste
                public static Character GiantRat2 = Templates.Critters.GiantRat.Clone(); // toxic waste
                public static Character RabidRat4 = Templates.Critters.RabidRat.Clone(); // toxic waste
                public static Character RabidRat5 = Templates.Critters.RabidRat.Clone(); // toxic waste
                public static Character Rat4 = Templates.Critters.Rat.Clone(); // collector 1
                public static Character RabidRat6 = Templates.Critters.RabidRat.Clone(); // deteriorated
                public static Character Skeleton1 = Templates.Undeads.Skeleton.Clone(); // crumbled
                public static Character Skeleton2 = Templates.Undeads.Skeleton.Clone(); // heart
                public static Character Skeleton3 = Templates.Undeads.Skeleton.Clone(); // heart
                public static Character Zombi1 = Templates.Undeads.Zombi.Clone(); // heart
                public static Character Zombi2 = Templates.Undeads.Zombi.Clone(); // heart
                public static Character Skeleton4 = Templates.Undeads.Skeleton.Clone(); // crypt
                public static Character Skeleton5 = Templates.Undeads.Skeleton.Clone(); // crypt
                public static Character Ghoul = Templates.Undeads.Ghoul.Clone(); // ossuary
                public static Character Lich = Templates.Undeads.Lich.Clone() // altar
                .RegisterKnowlede(new string[] { "Lich", "become lich" }, "\"**You shouldn't aim to become a lich. Trust me, it's not pleasant. But if one becomes a __Necromancer__**, they can reach that state if they are powerful enough.\"")
                .RegisterKnowlede(new string[] { "Necromancer", "become necromancer" }, "\"**Through rebirth, you can become a `Necromancer`.\"")
                .RegisterKnowlede(new string[] { "Ernaldz", "Bishop Ernaldz" }, "\"**Oh, I assume you came accross the work of that old agent of the church propaganda ?`\"")
                .RegisterKnowlede(new string[] { "Sineah", "History of Sineah" }, "\"**Don't believe that book. It is filled with lies and a lot of the story isn't told at all in it.\"");
            }
            public static Character[] GetCharacters()
            {
                return new Character[] {
                    Characters.Rat1,
                    Characters.Rat2,
                    Characters.Rat3,
                    Characters.RabidRat1,
                    Characters.RabidRat2,
                    Characters.RabidRat3,
                    Characters.GiantRat1,
                    Characters.GiantRat2,
                    Characters.RabidRat4,
                    Characters.RabidRat5,
                    Characters.Rat4,
                    Characters.RabidRat6,
                    Characters.Skeleton1,
                    Characters.Skeleton2,
                    Characters.Skeleton3,
                    Characters.Zombi1,
                    Characters.Zombi2,
                    Characters.Skeleton4,
                    Characters.Skeleton5,
                    Characters.Ghoul,
                    Characters.Lich,
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
        public static class Streets
        {
            public static class Shops
            {
                public static Shop Baker = new Shop()
                .RegisterEntry(Templates.Consumables.Bread, 1, null)
                .RegisterEntry(Templates.Goods.BreadBundle, 8, null);
                public static Shop Pharmacist = new Shop()
                .RegisterEntry(Templates.Consumables.HealingHerbs, 10, null)
                .RegisterEntry(Templates.Consumables.HealthPotion, 20, null)
                .RegisterEntry(Templates.Consumables.AntiBurnCream, 5, null)
                .RegisterEntry(Templates.Consumables.Antidote, 10, null)
                .RegisterEntry(Templates.Consumables.Medicine, 15, null);
                public static Shop ChurchAttendant = new Shop()
                .RegisterEntry(Templates.Equipments.Armor.PriestRobes, 150, null)
                .RegisterEntry(Templates.Equipments.Armor.TemplarRobes, 400, null)
                .RegisterEntry(Templates.Equipments.Armor.FanaticRobes, 400, null);
                public static Shop MagicVendor = new Shop()
                .RegisterEntry(Templates.Equipments.Weapons.Staff, 100, null)
                .RegisterEntry(Templates.Equipments.Armor.EnchanterCloak, 150, null)
                .RegisterEntry(Templates.Equipments.Armor.MageCloak, 500, null)
                .RegisterEntry(Templates.Equipments.Armor.WizardCloak, 1000, null);
                public static Shop WeaponSmith = new Shop()
                .RegisterEntry(Templates.Equipments.Weapons.Dagger, 50, 10)
                .RegisterEntry(Templates.Equipments.Weapons.Sword, 200, 40)
                .RegisterEntry(Templates.Equipments.Weapons.Axe, 150, 30)
                .RegisterEntry(Templates.Equipments.Weapons.Mace, 150, 30)
                .RegisterEntry(Templates.Equipments.Weapons.Spear, 200, 40);
                public static Shop ArmorSmith = new Shop()
                .RegisterEntry(Templates.Equipments.Armor.MilitianArmor, 150, 75)
                .RegisterEntry(Templates.Equipments.Armor.GuardArmor, 500, 250)
                .RegisterEntry(Templates.Equipments.Armor.KnightArmor, 1000, 500);
                public static Shop Trader = new Shop()
                .RegisterEntry(Templates.Equipments.Weapons.Dagger, 50, 10)
                .RegisterEntry(Templates.Equipments.Armor.TravellingCloak, 100, null)
                .RegisterEntry(Templates.Consumables.DriedMeat, 3, null)
                .RegisterEntry(Templates.Consumables.Water, 1, null);
                public static Shop Jeweler = new Shop()
                .RegisterEntry(Templates.Equipments.Rings.BladeRing, 100, null)
                .RegisterEntry(Templates.Equipments.Rings.HealthRing, 200, null)
                .RegisterEntry(Templates.Equipments.Rings.ManaRing, 200, null)
                .RegisterEntry(Templates.Equipments.Rings.IronRing, 200, null)
                .RegisterEntry(Templates.Equipments.Rings.AmethystRing, 200, null);
            }
            public static class Rooms
            {
                public static Room WGate = new Room("Sineah western city gate")
                {
                    isSpawnRoom = false,
                    description = "Massive gates marking the entrance to the city-state of Sineah."
                };
                public static Room NGate = new Room("Sineah northen city gate")
                {
                    isSpawnRoom = false,
                    description = "Massive gates marking the entrance to the city-state of Sineah."
                };
                public static Room EGate = new Room("Sineah eastern city gate")
                {
                    isSpawnRoom = false,
                    description = "Massive gates marking the entrance to the city-state of Sineah, facing the Neraji desert to the east, unforgiving land of the dangerous Kobolds."
                };
                public static Room SGate = new Room("Sineah southern city gate")
                {
                    isSpawnRoom = false,
                    description = "Massive gates marking the entrance to the city-state of Sineah."
                };
                public static Room outerScience = new Room("Sineah outer science district")
                {
                    isSpawnRoom = false,
                    description = "This long district reaches from the western gate all the way to the central plaza. North is the University. South is the Arcane Spire [CLOSED]. To the west is the city gate. The district continues further east, ending on the plaza."
                };
                public static Room outerTraveller = new Room("Sineah outer travellers avenue")
                {
                    isSpawnRoom = false,
                    description = "This avenue stretches from the northen gate to the central plaza. West is a shop [CLOSED]. East is the Church [CLOSED]. To the north is the city gate. The avenue continues further south, ending on the plaza."
                };
                public static Room outerMilitary = new Room("Sineah outer military path")
                {
                    isSpawnRoom = false,
                    description = "This fortified path connects the eastern gate to the city. North are the city Barracks. To the east is the city gate. The path continues further west, leading to the plaza."
                };
                public static Room outerCommercial = new Room("Sineah commercial street")
                {
                    isSpawnRoom = false,
                    description = "The nice smell of freshly baked bread fills the street. This street is a direct access from the southern gate to the central plaza. West is a Shop [CLOSED]. To the south is the city gate. North is the plaza."
                };
                public static Room innerScience = new Room("Sineah inner science district")
                {
                    isSpawnRoom = false,
                    description = "This long district reaches from the western gate all the way to the central plaza. South is the Workshop [CLOSED]. The district continues further west, ending at the city gate. East is the plaza."
                };
                public static Room innerTraveller = new Room("Sineah inner travellers avenue")
                {
                    isSpawnRoom = false,
                    description = "This avenue stretches from the northen gate to the central plaza. East is the Four winds inn. The avenue continues further north, ending at the city gate. South is the plaza."
                };
                public static Room innerMilitary = new Room("Sineah inner military path")
                {
                    isSpawnRoom = false,
                    description = "This fortified path connects the eastern gate to the city. North is the Armory [CLOSED]. South is the Blacksmith [CLOSED]. The path continues further east, ending at city gate. West is the plaza."
                };
                public static Room shady = new Room("Sineah hidden street")
                {
                    isSpawnRoom = false,
                    description = "A shady back alley. West will lead back to the safety of the Commercial street. A manhole is pried open."
                };
                public static Room plaza = new Room("Sineah central plaza")
                {
                    isSpawnRoom = false,
                    description = "The heart of Sineah, center of the city, hub of activity. Many guards, merchants, street artists, and adventurers go about their daily lifes."
                };
            }
            public static Room[] GetRooms()
            {
                return new Room[] {
                    Rooms.WGate,
                    Rooms.NGate,
                    Rooms.EGate,
                    Rooms.SGate,
                    Rooms.outerScience,
                    Rooms.outerTraveller,
                    Rooms.outerMilitary,
                    Rooms.outerCommercial,
                    Rooms.innerScience,
                    Rooms.innerTraveller,
                    Rooms.innerMilitary,
                    Rooms.shady,
                    Rooms.plaza
                };
            }

            public static class Connections
            {
                public static RoomConnection wGateToOuter = new RoomConnection(Rooms.WGate.id, Rooms.outerScience.id) // to outer science
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
                };
                public static RoomConnection nGateToOuter = new RoomConnection(Rooms.NGate.id, Rooms.outerTraveller.id) // to outer travellers
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.Out }
                };
                public static RoomConnection eGateToOuter = new RoomConnection(Rooms.EGate.id, Rooms.outerMilitary.id) // to outer military
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                };
                public static RoomConnection sGateToOuter = new RoomConnection(Rooms.SGate.id, Rooms.outerCommercial.id) // to outer commercial
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.In },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
                };
                public static RoomConnection wOuterToInner = new RoomConnection(Rooms.outerScience.id, Rooms.innerScience.id) // to inner science
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
                };
                public static RoomConnection nOuterToInner = new RoomConnection(Rooms.outerTraveller.id, Rooms.innerTraveller.id) // to inner travellers
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.South },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.North, Commands.MoveDirection.Out }
                };
                public static RoomConnection eOuterToInner = new RoomConnection(Rooms.outerMilitary.id, Rooms.innerMilitary.id) // to inner military
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Out }
                };
                public static RoomConnection sOuterToShady = new RoomConnection(Rooms.outerCommercial.id, Rooms.shady.id) // to hidden commercial
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
                };
                public static RoomConnection wInnerToPlaza = new RoomConnection(Rooms.innerScience.id, Rooms.plaza.id) // to plaza
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West }
                };
                public static RoomConnection nInnerToPlaza = new RoomConnection(Rooms.innerTraveller.id, Rooms.plaza.id) // to plaza
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.South },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.North }
                };
                public static RoomConnection eInnerToPlaza = new RoomConnection(Rooms.innerMilitary.id, Rooms.plaza.id) // to plaza
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.East }
                };
                public static RoomConnection sOuterToPlaza = new RoomConnection(Rooms.outerCommercial.id, Rooms.plaza.id) // to plaza
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South }
                };
            }
            public static RoomConnection[] GetConnections()
            {
                return new RoomConnection[] {
                    Connections.wGateToOuter,
                    Connections.nGateToOuter,
                    Connections.eGateToOuter,
                    Connections.sGateToOuter,
                    Connections.wOuterToInner,
                    Connections.nOuterToInner,
                    Connections.eOuterToInner,
                    Connections.sOuterToShady,
                    Connections.wInnerToPlaza,
                    Connections.nInnerToPlaza,
                    Connections.eInnerToPlaza,
                    Connections.sOuterToPlaza
                };
            }

            public static class Characters
            {
                public static NPC beggar = Templates.CityFolks.Beggar.Clone();
                public static NPC wgGuard = Templates.CityFolks.Guard.Clone();
                public static NPC wgMilitian = Templates.CityFolks.Militian.Clone();
                public static NPC ngGuard = Templates.CityFolks.Guard.Clone();
                public static NPC ngMilitian = Templates.CityFolks.Militian.Clone();
                public static NPC egGuard1 = Templates.CityFolks.Guard.Clone();
                public static NPC egGuard2 = Templates.CityFolks.Guard.Clone();
                public static NPC egMilitian = Templates.CityFolks.Militian.Clone();
                public static NPC sgGuard = Templates.CityFolks.Guard.Clone();
                public static NPC sgMilitian = Templates.CityFolks.Militian.Clone();
                public static NPC innerGuard = Templates.CityFolks.Guard.Clone();
                public static NPC plazaGuard1 = Templates.CityFolks.Guard.Clone();
                public static NPC plazaGuard2 = Templates.CityFolks.Guard.Clone();
                public static NPC plazaMilitian1 = Templates.CityFolks.Militian.Clone();
                public static NPC plazaMilitian2 = Templates.CityFolks.Militian.Clone();
                public static NPC shadyRat = Templates.Critters.Rat.Clone();
                public static NPC baker = Templates.CityFolks.Baker.Clone()
                    .RegisterShop(Shops.Baker)
                    .GenerateTraderKnowledge();
                public static NPC weaponSeller = Templates.CityFolks.WeaponSeller.Clone()
                    .RegisterShop(Shops.WeaponSmith)
                    .GenerateTraderKnowledge();
                public static NPC armorSeller = Templates.CityFolks.ArmorSeller.Clone()
                    .RegisterShop(Shops.ArmorSmith)
                    .GenerateTraderKnowledge();
                public static NPC pharmacist = Templates.CityFolks.Pharmacian.Clone()
                    .RegisterShop(Shops.Pharmacist)
                    .GenerateTraderKnowledge();
                public static NPC churchAttendant = Templates.CityFolks.ChurchAttendant.Clone()
                    .RegisterShop(Shops.ChurchAttendant)
                    .GenerateTraderKnowledge();
                public static NPC magicVendor = Templates.CityFolks.MagicVendor.Clone()
                    .RegisterShop(Shops.MagicVendor)
                    .GenerateTraderKnowledge();
                public static NPC trader = Templates.CityFolks.Trader.Clone()
                    .RegisterShop(Shops.Trader)
                    .GenerateTraderKnowledge();
                public static NPC jeweler = Templates.CityFolks.Jeweler.Clone()
                    .RegisterShop(Shops.Jeweler)
                    .GenerateTraderKnowledge();
            }
            public static Character[] GetCharacters()
            {
                return new Character[] {
                    Characters.wgGuard,
                    Characters.wgMilitian,
                    Characters.ngGuard,
                    Characters.ngMilitian,
                    Characters.egGuard1,
                    Characters.egGuard2,
                    Characters.egMilitian,
                    Characters.sgGuard,
                    Characters.sgMilitian,
                    Characters.innerGuard,
                    Characters.plazaGuard1,
                    Characters.plazaGuard2,
                    Characters.plazaMilitian1,
                    Characters.plazaMilitian2,
                    Characters.shadyRat,
                    Characters.baker,
                    Characters.weaponSeller,
                    Characters.armorSeller,
                    Characters.pharmacist,
                    Characters.churchAttendant,
                    Characters.magicVendor,
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
        public static RoomConnection[] cityToBuildingConnections = new RoomConnection[] {
            new RoomConnection(Streets.Rooms.outerMilitary.id, Barracks.Rooms.Entrance.id) // outer military to barracks
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
            },
            new RoomConnection(Streets.Rooms.innerTraveller.id, Inn.Rooms.CommonRoom.id) // inner traveller to inn
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.In },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Out }
            },
            new RoomConnection(Streets.Rooms.shady.id, Underground.Rooms.SewerAccess.id) // shady to underground
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.Down },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.Up, Commands.MoveDirection.North, Commands.MoveDirection.Out }
            },
            new RoomConnection(Streets.Rooms.outerScience.id, University.Rooms.MainHall.id) // outer science to university
            {
                directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South, Commands.MoveDirection.Out }
            }
        };
        #endregion

        public static void LoadWorld()
        {
            // LOAD BUILDINGS
            // load barracks
            RoomManager.LoadRooms(Barracks.GetRooms());
            RoomManager.LoadRoomConnections(Barracks.GetConnections());
            CharacterManager.LoadCharacters(Barracks.GetCharacters());
            // load inn
            RoomManager.LoadRooms(Inn.GetRooms());
            RoomManager.LoadRoomConnections(Inn.GetConnections());
            CharacterManager.LoadCharacters(Inn.GetCharacters());
            // load underground
            RoomManager.LoadRooms(Underground.GetRooms());
            RoomManager.LoadRoomConnections(Underground.GetConnections());
            CharacterManager.LoadCharacters(Underground.GetCharacters());
            // load university
            RoomManager.LoadRooms(University.GetRooms());
            RoomManager.LoadRoomConnections(University.GetConnections());

            // LOAD STREETS
            RoomManager.LoadRooms(Streets.GetRooms());
            RoomManager.LoadRoomConnections(Streets.GetConnections());
            CharacterManager.LoadCharacters(Streets.GetCharacters());

            // CONNECT BUILDINGS TO STREETS
            RoomManager.LoadRoomConnections(cityToBuildingConnections);

            // PLACE CONTAINERS IN ROOMS
            Inn.Rooms.InnkeeprsOffice.AddToRoom(Inn.Containers.OfficeDesk);
            Inn.Rooms.InnkeeprsOffice.AddToRoom(Inn.Containers.OfficeChest);
            Inn.Rooms.Bedroom1.AddToRoom(Inn.Containers.Room1Chest);
            Inn.Rooms.Bedroom3.AddToRoom(Inn.Containers.Room3Chest);

            // PLACE ITEMS IN ROOMS
            Inn.Rooms.InnkeeprsOffice.AddToRoom(Inn.Items.Room1Key); // bedroom key 1 in office
            Inn.Rooms.InnkeeprsOffice.AddToRoom(Inn.Items.Room3Key); // bedroom key 3 in office

            // PLACE ITEMS IN CONTAINERS
            Inn.Containers.OfficeDesk.AddToInventory(Inn.Items.IronChestKey); // iron chest key in office
            Inn.Containers.OfficeChest.AddToInventory(Inn.Items.OfficeKey); // office key in office
            Inn.Containers.Room3Chest.AddToInventory(Inn.Items.Ragdoll); // office key in office

            // POPULATE ROOMS WITH NPCS
            // populate inn
            Inn.Rooms.CommonRoom.AddToRoom(Inn.Characters.Bartender); // bartender in common room
            Inn.Rooms.CommonRoom.AddToRoom(Inn.Characters.Waiter); // waiter in common room
            Inn.Rooms.CommonRoom.AddToRoom(Inn.Characters.Drunk); // drunk in common room
            Inn.Rooms.CommonRoom.AddToRoom(Inn.Characters.Customer); // customer in common room
            Inn.Rooms.CommonRoom.AddToRoom(Inn.Characters.ShadyConsumer); // shady consumer in common room
            Inn.Rooms.Kitchen.AddToRoom(Inn.Characters.Cook); // cook in kitchen
            Inn.Rooms.Cellar.AddToRoom(Inn.Characters.Rat1); // rat in cellar
            Inn.Rooms.Cellar.AddToRoom(Inn.Characters.Rat2); // rat in cellar
            Inn.Rooms.Cellar.AddToRoom(Inn.Characters.Rat3); // rat in cellar

            // populate underground
            Underground.Rooms.SewerTunnel.AddToRoom(Underground.Characters.Rat1); // rat in sewer tunnel
            Underground.Rooms.ConvergenceRoom.AddToRoom(Underground.Characters.Rat2); // rat in convergence room
            Underground.Rooms.ConvergenceRoom.AddToRoom(Underground.Characters.Rat3); // rat in convergence room
            Underground.Rooms.ConvergenceRoom.AddToRoom(Underground.Characters.RabidRat1); // rabid rat in convergence room
            Underground.Rooms.ToxicDrain.AddToRoom(Underground.Characters.RabidRat2); // rabid rat in toxic
            Underground.Rooms.ToxicDrain.AddToRoom(Underground.Characters.RabidRat3); // rabid rat in toxic
            Underground.Rooms.ToxicWaste.AddToRoom(Underground.Characters.GiantRat1); // giant rat in toxic 2
            Underground.Rooms.ToxicWaste.AddToRoom(Underground.Characters.GiantRat2); // giant rat in toxic 2
            Underground.Rooms.ToxicWaste.AddToRoom(Underground.Characters.RabidRat4); // rabid rat in toxic 2
            Underground.Rooms.ToxicWaste.AddToRoom(Underground.Characters.RabidRat5); // rabid rat in toxic 2
            Underground.Rooms.CollectingRoom1.AddToRoom(Underground.Characters.Rat4); // rat in collector 1
            Underground.Rooms.DeterioratedDrain.AddToRoom(Underground.Characters.RabidRat6); // rabid rat in deteriorated
            Underground.Rooms.CrumbledCollector.AddToRoom(Underground.Characters.Skeleton1); // skeleton in crumbled
            Underground.Rooms.Heart.AddToRoom(Underground.Characters.Skeleton2); // skeleton in heart
            Underground.Rooms.Heart.AddToRoom(Underground.Characters.Skeleton3); // skeleton in heart
            Underground.Rooms.Heart.AddToRoom(Underground.Characters.Zombi1); // zombi in heart
            Underground.Rooms.Heart.AddToRoom(Underground.Characters.Zombi2); // zombi in heart
            Underground.Rooms.Crypt.AddToRoom(Underground.Characters.Skeleton4); // skeleton in crypt
            Underground.Rooms.Crypt.AddToRoom(Underground.Characters.Skeleton5); // skeleton in crypt
            Underground.Rooms.Ossuary.AddToRoom(Underground.Characters.Ghoul); // ghoul in ossuary
            Underground.Rooms.Altar.AddToRoom(Underground.Characters.Lich); // lich in altar

            // populate streets
            Streets.Rooms.plaza.AddToRoom(Streets.Characters.beggar); // guard at west gate
            Streets.Rooms.WGate.AddToRoom(Streets.Characters.wgGuard); // guard at west gate
            Streets.Rooms.WGate.AddToRoom(Streets.Characters.wgMilitian); // militian at west gate
            Streets.Rooms.NGate.AddToRoom(Streets.Characters.ngGuard); // guard at north gate
            Streets.Rooms.NGate.AddToRoom(Streets.Characters.ngMilitian); // militian at north gate
            Streets.Rooms.EGate.AddToRoom(Streets.Characters.egGuard1); // guard at east gate
            Streets.Rooms.EGate.AddToRoom(Streets.Characters.egGuard2); // guard at east gate
            Streets.Rooms.EGate.AddToRoom(Streets.Characters.egMilitian); // militian at east gate
            Streets.Rooms.SGate.AddToRoom(Streets.Characters.sgGuard); // guard at south gate
            Streets.Rooms.SGate.AddToRoom(Streets.Characters.sgMilitian); // militian at south gate
            Streets.Rooms.innerMilitary.AddToRoom(Streets.Characters.innerGuard); // guard at inner military
            Streets.Rooms.plaza.AddToRoom(Streets.Characters.plazaGuard1); // guard at plaza
            Streets.Rooms.plaza.AddToRoom(Streets.Characters.plazaGuard2); // guard at plaza
            Streets.Rooms.plaza.AddToRoom(Streets.Characters.plazaMilitian1); // militian at plaza
            Streets.Rooms.plaza.AddToRoom(Streets.Characters.plazaMilitian2); // militian at plaza
            Streets.Rooms.shady.AddToRoom(Streets.Characters.shadyRat); // rat at shady
            Streets.Rooms.outerCommercial.AddToRoom(Streets.Characters.baker); // baker at commercial
            Streets.Rooms.outerCommercial.AddToRoom(Streets.Characters.jeweler); // jeweler at commercial
            Streets.Rooms.innerMilitary.AddToRoom(Streets.Characters.weaponSeller); // weaponsmith at inner military
            Streets.Rooms.innerMilitary.AddToRoom(Streets.Characters.armorSeller); // armorsmith at inner military
            Streets.Rooms.outerTraveller.AddToRoom(Streets.Characters.churchAttendant); // church attendant at outer traveller
            Streets.Rooms.outerTraveller.AddToRoom(Streets.Characters.trader); // trader at outer traveller
            Streets.Rooms.outerScience.AddToRoom(Streets.Characters.magicVendor); // magic vendor at outer science
            Streets.Rooms.outerScience.AddToRoom(Streets.Characters.pharmacist); // pharmacist at outer science

            // signs
            Streets.Rooms.outerScience.AddToRoom(Display.Sign("University notice sign",
            "The reconstruction of the university is still underway. However, some part of the library are ready to welcome curious minds once again.\nKeep in mind that most books still need to be restored, and some might be moved to different sections as more aisle are being reopened."));

            // books
            University.Rooms.LibraryEntrance.AddToRoom(Templates.Books.SineahHistory);
            University.Rooms.LibraryEntrance.AddToRoom(Templates.Books.PurgeTheUndead);
            University.Rooms.LibraryEntrance.AddToRoom(Templates.Books.CityCritters);


            // populate global room list for graph construction
            rooms = Inn.GetRooms()
            .Union(University.GetRooms())
            .Union(Underground.GetRooms())
            .Union(Streets.GetRooms())
            .ToArray();

            // register behaviours
            BehaviourManager.RegisterNPC(Streets.Characters.beggar, new Behaviours.CityFolks.Beggar());
        }
    }
}
