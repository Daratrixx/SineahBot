using NUnit.Framework;

namespace SineahBotTestsNUnit
{
    public class Tests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            SineahBot.Tools.PersistenceManager.ForceStaticInit();
            SineahBot.Data.World.Worlds.LoadWorlds();
            SineahBot.Tools.PathBuilder.BuildGraphs();
        }

        [Test]
        public void TestSymmetricalDistances()
        {
            var graph = SineahBot.Tools.PathBuilder.SineahGraph;

            var rooms = SineahBot.Data.World.Sineah.Inn.GetRooms();
            for (int i = 0; i < rooms.Length; ++i)
            {
                Assert.AreEqual(graph.GetDistance(graph.GetNodeForData(rooms[0]), graph.GetNodeForData(rooms[i])),
                graph.GetDistance(graph.GetNodeForData(rooms[i]), graph.GetNodeForData(rooms[0])),
                "Distance is not symmetrical.");
            }
        }
        [Test]
        public void TestSineahDistances()
        {
            SineahBot.Data.Room roomA;
            SineahBot.Data.Room roomB;
            var graph = SineahBot.Tools.PathBuilder.SineahGraph;

            roomA = SineahBot.Data.World.Sineah.Inn.Rooms.SecondFloorLanding;
            roomB = SineahBot.Data.World.Sineah.Inn.Rooms.Kitchen;
            Assert.AreEqual(graph.GetDistance(graph.GetNodeForData(roomA), graph.GetNodeForData(roomB)), 2, "Incoherent distance.");

            roomA = SineahBot.Data.World.Sineah.Inn.Rooms.Bedroom1;
            roomB = SineahBot.Data.World.Sineah.Inn.Rooms.Cellar;
            Assert.AreEqual(graph.GetDistance(graph.GetNodeForData(roomA), graph.GetNodeForData(roomB)), 4, "Incoherent distance.");

            roomA = SineahBot.Data.World.Sineah.Streets.Rooms.EGate;
            roomB = SineahBot.Data.World.Sineah.Streets.Rooms.plaza;
            Assert.AreEqual(graph.GetDistance(graph.GetNodeForData(roomA), graph.GetNodeForData(roomB)), 3, "Incoherent distance.");

            roomA = SineahBot.Data.World.Sineah.University.Rooms.LibraryEntrance;
            roomB = SineahBot.Data.World.Sineah.Underground.Rooms.ConvergenceRoom;
            Assert.AreEqual(graph.GetDistance(graph.GetNodeForData(roomA), graph.GetNodeForData(roomB)), 9, "Incoherent distance.");
        }
    }
}