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
        public static SineahDbContext Database { get; private set; }
        public static IMapper Mapper { get; private set; }


        public static void Main(string[] args)
       => new Program().MainAsync().GetAwaiter().GetResult();

        public static void ConfigureDatabase()
        {
            Database = new SineahDbContext();
        }

        public static void ConfigureAutomapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(Program).Assembly));

            Mapper = config.CreateMapper();
        }

        public static void SaveData()
        {
            CharacterManager.SavePlayerCharacters();
            PlayerManager.SavePlayers();
            RoomManager.SaveRooms();
            Database.ApplyChanges();
        }

        public async Task MainAsync()
        {
            ConfigureDatabase();
            ConfigureAutomapper();
            Worlds.LoadWorlds();
            BehaviourManager.StartBehaviourTimer();
            new MudInterval(300, () =>
            {
                SaveData();
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
                SaveData();
                Database.Cleanup();
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
