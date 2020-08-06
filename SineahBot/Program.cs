using SineahBot.Data;
using SineahBot.Tools;
using System;

namespace SineahBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new World();
            world.LoadWorld();
            var spawnRoom = RoomManager.GetRoom(RoomManager.GetSpawnRoomId());
            RoomManager.MoveToRoom(PlayerManager.GetPlayer("test").character, spawnRoom);
            string input = "";
            while (input != "quit")
            {
                input = Console.ReadLine();
                CommandManager.ParseUserMessage("test", input);
            }
        }
    }
}
