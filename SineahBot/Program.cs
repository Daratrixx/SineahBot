using AutoMapper;
using Discord;
using Discord.WebSocket;
using SineahBot.Data;
using SineahBot.Data.World;
using SineahBot.Database.Extensions;
using SineahBot.DataContext;
using SineahBot.Tools;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SineahBot
{
    public class Program
    {

        // Scaffold-DbContext "DataSource=../SineahBot.sqlite" Microsoft.EntityFrameworkCore.Sqlite -ContextDir DataContext -OutputDir DataContext -Force

        public static readonly bool ONLINE = true;


        public static void Main(string[] args)
       => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Worlds.LoadWorlds();
            BehaviourManager.StartBehaviourTimer();
            new MudInterval(300, () =>
            {
                PersistenceManager.SaveAll();
            });
            try
            {
                if (ONLINE)
                    await Application.Discord.OnlinePlay();
                else
                    OfflinePlay();
            }
            catch (Exception e)
            {
                Logging.Log(e.Message);
                Logging.Log(e.StackTrace);
                PersistenceManager.SaveAll();
                PersistenceManager.Cleanup();
            }
        }
        private void OfflinePlay()
        {
            throw new ApplicationException("No longer supported.");
            /*var player = PlayerManager.CreateTestPlayer();
            var spawnRoom = RoomManager.GetRoomById(RoomManager.GetSpawnRoomId());
            RoomManager.MoveToRoom(player.character, spawnRoom);
            string input = "";
            while (input != "quit")
            {
                input = Console.ReadLine();
                CommandManager.ParseUserMessage(0, input);
                Player.CommitPlayerMessageBuffers();
            }*/
        }
    }
}
