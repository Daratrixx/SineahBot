﻿using SineahBot.Data.Enums;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.World
{
    public static class NerajiDesert
    {
        public static readonly string RegionPrefix = "NERAJI";
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
        public static class Desert
        {
            public static class Rooms
            {
                public static Room DesertRoad2 = new Room("Neraji desert", $"{RegionPrefix}_DE_R2")
                {
                    description = $"Looking West you can see a road. Every other direction looks like endless sand."
                };
                public static Room DesertRoad3 = new Room("Neraji emptiness", $"{RegionPrefix}_DE_R3")
                {
                    description = $"You are surrounded by sand in every direction. You can make out a road to the West."
                };
                public static Room DesertRoad4 = new Room("Neraji dunes", $"{RegionPrefix}_DE_R4")
                {
                    description = $"You are surrounded by sand in every direction."
                };
                public static Room DesertRoad5 = new Room("Neraji plateau", $"{RegionPrefix}_DE_R5")
                {
                    description = $"You are surrounded by sand in every direction. You see an oasis to the East. Or is it a mirage ?"
                };
                public static Room DesertRoad6 = new Room("Neraji plateau", $"{RegionPrefix}_DE_R6")
                {
                    description = $"You are surrounded by sand in every direction. You see an oasis to the East. Or is it a mirage ?"
                };
                public static Room DesertRoad7 = new Room("Neraji plateau", $"{RegionPrefix}_DE_R7")
                {
                    description = $"You are surrounded by sand in every direction. You see an oasis to the East. Or is it a mirage ?"
                };
                public static Room DesertRoad8 = new Room("Neraji plateau", $"{RegionPrefix}_DE_R8")
                {
                    description = $"You are surounded by sand in every direction. You see an oasis to the East. Or is it a mirage ?"
                };
                public static Room DesertRoad9 = new Room("Neraji oasis", $"{RegionPrefix}_DE_R9")
                {
                    description = $"You reached the oasis. The tall palmtrees offer a welcome shelter from the burning sun, and the clear water will quench your thirst."
                };
            }
            public static Room[] GetRooms()
            {
                return new Room[]{
                    Rooms.DesertRoad2,
                    Rooms.DesertRoad3,
                    Rooms.DesertRoad4,
                    Rooms.DesertRoad5,
                    Rooms.DesertRoad6,
                    Rooms.DesertRoad7,
                    Rooms.DesertRoad8,
                    Rooms.DesertRoad9,
                };
            }

            public static class Connections
            {
                public static RoomConnection DR2_3 = new RoomConnection(Rooms.DesertRoad2, Rooms.DesertRoad3)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
                public static RoomConnection DR3_4 = new RoomConnection(Rooms.DesertRoad3, Rooms.DesertRoad4)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
                public static RoomConnection DR4_5 = new RoomConnection(Rooms.DesertRoad4, Rooms.DesertRoad5)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
                public static RoomConnection DR5_6 = new RoomConnection(Rooms.DesertRoad5, Rooms.DesertRoad6)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
                public static RoomConnection DR6_7 = new RoomConnection(Rooms.DesertRoad6, Rooms.DesertRoad7)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
                public static RoomConnection DR7_8 = new RoomConnection(Rooms.DesertRoad7, Rooms.DesertRoad8)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
                public static RoomConnection DR8_9 = new RoomConnection(Rooms.DesertRoad8, Rooms.DesertRoad9)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West },
                };
            }
            public static RoomConnection[] GetConnections()
            {
                return new RoomConnection[] {
                    Connections.DR2_3,
                    Connections.DR3_4,
                    Connections.DR4_5,
                    Connections.DR5_6,
                    Connections.DR6_7,
                    Connections.DR7_8,
                    Connections.DR8_9,
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
                public static Item secretLizardPet = Templates.Familiars.Lizard;
            }
            public static Item[] GetItems()
            {
                return new Item[] {
                    Items.secretLizardPet,
                };
            }
        }
        public static class CityOfAsh
        {
            public static class Rooms
            {
                public static Room road0 = new Room("Antique stone path", $"{RegionPrefix}_CA_R0")
                {
                    description = "You find an incredibly old stone path running through the dunes. It leads up North to a mountain range."
                };
                public static Room road1 = new Room("Antique road", $"{RegionPrefix}_CA_R1")
                {
                    description = "Standing on the timeless stones of this road, you can spot the ruins of a city up North. The ruins are cradled under a mountain burnt by the sun. You can make out the shape of a few streets between the destroyed buildings. Only a singular tower still stands. There is a gigantic pit, larger than the city itself, bordering the ruins."
                };
                public static Room gate = new Room("Crumbled gate", $"{RegionPrefix}_CA_CG")
                {
                    description = "Once the only access through the fortifications of this ancient city, the great gate now lays crumbled on the ground, its strong wooden door reduced to dust long ago. The road leads South away from the city, while the main street can be accessed North."
                };
                public static Room streets = new Room("Collapsed streets", $"{RegionPrefix}_CA_CS0")
                {
                    description = "This wide street is littered with stones and debris. North is the centre of the ruins, going south will lead to the city gates."
                };
                public static Room plaza = new Room("Ruined plaza", $"{RegionPrefix}_CA_RP0")
                {
                    description = "Heart of the ruins. Crumbled statues and ravaged monuments evoke images of a once powerful city. North leads to a tall Spire, south reaches the main street connecting the plaza to the city gate."
                };
                public static Room spire = new Room("Scorched spire", $"{RegionPrefix}_CA_ST")
                {
                    AncestrySpawnRoom = CharacterAncestry.Kobold,
                    description = "This towering spire is the last building standing in the city. Its walls are charred as if a intense fire engulfed the place, contrasting highly with the white stones of the other ruins. South is the central plaza. East is a platform overlooking the pit, with stairs leading down."
                };
                public static Room stairs = new Room("Corroded starway", $"{RegionPrefix}_CA_CS1")
                {
                    description = ""
                };
                public static Room platform = new Room("Ravaged platform", $"{RegionPrefix}_CA_RP1")
                {
                    description = ""
                };
            }
            public static Room[] GetRooms()
            {
                return new Room[] {
                    Rooms.road0,
                    Rooms.road1,
                    Rooms.gate,
                    Rooms.streets,
                    Rooms.plaza,
                    Rooms.spire,
                    Rooms.stairs,
                    Rooms.platform,
                };
            }

            public static class Connections
            {
                public static RoomConnection R0_1 = new RoomConnection(Rooms.road0, Rooms.road1)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South },
                };
                public static RoomConnection R1Gate = new RoomConnection(Rooms.road1, Rooms.gate)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South },
                };
                public static RoomConnection gateStreet = new RoomConnection(Rooms.gate, Rooms.streets)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South },
                };
                public static RoomConnection streetPlaza = new RoomConnection(Rooms.streets, Rooms.plaza)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South },
                };
                public static RoomConnection plazaTower = new RoomConnection(Rooms.plaza, Rooms.spire)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.North },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.South },
                };
                public static RoomConnection plazaStairs = new RoomConnection(Rooms.plaza, Rooms.stairs)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Down },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Up },
                };
                public static RoomConnection stairsPlatform = new RoomConnection(Rooms.stairs, Rooms.platform)
                {
                    directionFromA = new Commands.MoveDirection[] { Commands.MoveDirection.East, Commands.MoveDirection.Down },
                    directionFromB = new Commands.MoveDirection[] { Commands.MoveDirection.West, Commands.MoveDirection.Up },
                };
            }
            public static RoomConnection[] GetConnections()
            {
                return new RoomConnection[] {
                    Connections.R0_1,
                    Connections.R1Gate,
                    Connections.gateStreet,
                    Connections.streetPlaza,
                    Connections.plazaTower,
                    Connections.plazaStairs,
                    Connections.stairsPlatform,
                };
            }

            public static class Characters
            {
                public static NPC scout0 = Templates.Kobolds.KoboldScout.Clone().SetFaction(FactionManager.Kobolds)
                .SetEquipment(Templates.Equipments.Weapons.Spear, Templates.Equipments.Armor.KoboldTunic);
                public static NPC scout1 = Templates.Kobolds.KoboldScout.Clone().SetFaction(FactionManager.Kobolds)
                .SetEquipment(Templates.Equipments.Weapons.Spear, Templates.Equipments.Armor.KoboldTunic);
                public static NPC warrior0 = Templates.Kobolds.KoboldWarrior.Clone().SetFaction(FactionManager.Kobolds)
                .SetEquipment(Templates.Equipments.Weapons.CurvedSword, Templates.Equipments.Armor.KoboldTunic);
                public static NPC warrior1 = Templates.Kobolds.KoboldWarrior.Clone().SetFaction(FactionManager.Kobolds)
                .SetEquipment(Templates.Equipments.Weapons.CurvedSword, Templates.Equipments.Armor.KoboldTunic);
                public static NPC warrior2 = Templates.Kobolds.KoboldWarrior.Clone().SetFaction(FactionManager.Kobolds)
                .SetEquipment(Templates.Equipments.Weapons.CurvedSword, Templates.Equipments.Armor.KoboldTunic);
                public static NPC lieutenant = Templates.Kobolds.KoboldLieutenant.Clone().SetFaction(FactionManager.Kobolds)
                .SetEquipment(Templates.Equipments.Weapons.CurvedSword, Templates.Equipments.Armor.KoboldArmor, Templates.Equipments.Rings.ClawRing);
            }
            public static Character[] GetCharacters()
            {
                return new Character[] {
                    Characters.scout0,
                    Characters.scout1,
                    Characters.warrior0,
                    Characters.warrior1,
                    Characters.warrior2,
                    Characters.lieutenant,
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

        public static RoomConnection[] GetAreaConnections() => new RoomConnection[] {
            // desert to city of ash
            new RoomConnection(Desert.Rooms.DesertRoad5, CityOfAsh.Rooms.road0) {
                directionFromA = new Commands.MoveDirection[]{ Commands.MoveDirection.North },
                directionFromB = new Commands.MoveDirection[]{ Commands.MoveDirection.South },
            }
        };

        public static void LoadWorld()
        {
            // LOAD ROADS
            // load desert
            RoomManager.LoadRooms(Desert.GetRooms());
            RoomManager.LoadRoomConnections(Desert.GetConnections());
            CharacterManager.LoadCharacters(Desert.GetCharacters());
            // load city of ash
            RoomManager.LoadRooms(CityOfAsh.GetRooms());
            RoomManager.LoadRoomConnections(CityOfAsh.GetConnections());
            CharacterManager.LoadCharacters(CityOfAsh.GetCharacters());

            // CONNECT ROADS
            RoomManager.LoadRoomConnections(GetAreaConnections());

            // PLACE CONTAINERS IN ROOMS


            // PLACE ITEMS IN ROOMS
            Desert.Rooms.DesertRoad9.AddToRoom(Desert.Items.secretLizardPet);

            // PLACE ITEMS IN CONTAINERS


            // POPULATE ROOMS WITH NPCS
            // city of ash
            CityOfAsh.Rooms.road1.AddToRoom(CityOfAsh.Characters.scout0);
            CityOfAsh.Rooms.gate.AddToRoom(CityOfAsh.Characters.scout1);
            CityOfAsh.Rooms.plaza.AddToRoom(CityOfAsh.Characters.warrior0);
            CityOfAsh.Rooms.plaza.AddToRoom(CityOfAsh.Characters.warrior1);
            CityOfAsh.Rooms.spire.AddToRoom(CityOfAsh.Characters.lieutenant);
            CityOfAsh.Rooms.stairs.AddToRoom(CityOfAsh.Characters.warrior2);

            // signs


            // books


            // populate global room list for graph construction
            rooms = Desert.GetRooms()
            .Union(CityOfAsh.GetRooms())
            .ToArray();

            // register behaviours
            BehaviourManager.RegisterNPC(CityOfAsh.Characters.scout0, new Behaviours.MonsterBase());
            BehaviourManager.RegisterNPC(CityOfAsh.Characters.scout1, new Behaviours.MonsterBase());
            BehaviourManager.RegisterNPC(CityOfAsh.Characters.warrior0, new Behaviours.MonsterBase());
            BehaviourManager.RegisterNPC(CityOfAsh.Characters.warrior1, new Behaviours.MonsterBase());
            BehaviourManager.RegisterNPC(CityOfAsh.Characters.warrior2, new Behaviours.MonsterBase());
            BehaviourManager.RegisterNPC(CityOfAsh.Characters.lieutenant, new Behaviours.MonsterBase());

        }
    }
}
