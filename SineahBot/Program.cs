using Discord;
using Discord.WebSocket;
using SineahBot.Data;
using SineahBot.Tools;
using System;
using System.Threading.Tasks;

namespace SineahBot
{
    class Program
    {
        public static readonly bool ONLINE = true;

        public static void Main(string[] args)
       => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var world = new World();
            world.LoadWorld();
            if (ONLINE)
                await OnlinePlay();
            else
                OfflinePlay();
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private void OfflinePlay()
        {
            var spawnRoom = RoomManager.GetRoom(RoomManager.GetSpawnRoomId());
            RoomManager.MoveToRoom(PlayerManager.GetPlayer(0).character, spawnRoom);
            string input = "";
            while (input != "quit")
            {
                input = Console.ReadLine();
                CommandManager.ParseUserMessage(0, input);
            }
        }
        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.Id != DiscordClient.CurrentUser.Id)
                CommandManager.ParseUserMessage(message.Author.Id, message.Content, message.Channel.Id);
        }
        public static DiscordSocketClient DiscordClient;
        private async Task OnlinePlay()
        {
            DiscordClient = new DiscordSocketClient();

            DiscordClient.Log += Log;

            // Remember to keep token private or to read it from an 
            // external source! In this case, we are reading the token 
            // from an environment variable. If you do not know how to set-up
            // environment variables, you may find more information on the 
            // Internet or by using other methods such as reading from 
            // a configuration.
            await DiscordClient.LoginAsync(TokenType.Bot, "MjYwMTQxOTY1NTU3ODkxMDcz.CziE_A.mfrsPm2LYOALVQSWC5LKdudW8nI");
            await DiscordClient.StartAsync();

            DiscordClient.MessageReceived += MessageReceived;
            DiscordClient.Disconnected += OnDisconnected;
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private Task OnDisconnected(Exception e) {
            return Task.CompletedTask;
        }
    }
}
