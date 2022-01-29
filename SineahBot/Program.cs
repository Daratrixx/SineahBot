﻿using AutoMapper;
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

        public static void SaveData()
        {
            CharacterManager.SavePlayerCharacters();
            PlayerManager.SavePlayers();
            RoomManager.SaveRooms();
            Database.ApplyChanges();
        }

        public static void Main(string[] args)
       => new Program().MainAsync().GetAwaiter().GetResult();

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
                    await OnlinePlay();
                else
                    OfflinePlay();
            }
            catch (Exception e)
            {
                Logging.Log(e.Message);
                Logging.Log(e.StackTrace);
                SaveData();
            }
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private void OfflinePlay()
        {
            var player = PlayerManager.CreateTestPlayer();
            var spawnRoom = RoomManager.GetRoomById(RoomManager.GetSpawnRoomId());
            RoomManager.MoveToRoom(player.character, spawnRoom);
            string input = "";
            while (input != "quit")
            {
                input = Console.ReadLine();
                CommandManager.ParseUserMessage(0, input);
                Player.CommitPlayerMessageBuffers();
            }
        }
        public static DiscordSocketClient DiscordClient;
        private async Task OnlinePlay()
        {
            DiscordClient = new DiscordSocketClient();

            DiscordClient.Log += Log;

            await DiscordClient.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("SineahBotToken"));
            await DiscordClient.StartAsync();

            DiscordClient.UserJoined += UserJoined;
            DiscordClient.MessageReceived += MessageReceived;
            DiscordClient.Disconnected += OnDisconnected;
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private ulong[] ignoredChannels = new ulong[] { 741728973318389790 };
        private Task MessageReceived(SocketMessage message)
        {
            try
            {
                if (message.Author.Id != DiscordClient.CurrentUser.Id && !ignoredChannels.Contains(message.Channel.Id))
                    CommandManager.ParseUserMessage(message.Author.Id, message.Content, message.Channel.Id);
            }
            catch (Exception e)
            {
                Logging.Log(e);
                message.Channel.SendMessageAsync("Error : " + e.Message);
            }
            Player.CommitPlayerMessageBuffers();
            return Task.CompletedTask;
        }

        private Task UserJoined(SocketGuildUser arg)
        {
            var userId = arg.Id;
            var guild = arg.Guild;
            // look for an existing channel for the user
            var existingChannel = guild.TextChannels.FirstOrDefault(x => x.Topic == userId.ToString());
            if (existingChannel == null)
            {
                // get the private room category
                var privateRooms = guild.CategoryChannels.FirstOrDefault(x => x.Name == "private-rooms");
                // create a new private channel for the user
                var privateChanel = guild.CreateTextChannelAsync("private-" + arg.Username.Split(" ")[0] + "-" + (userId % 10000),
                (properties) =>
                {
                    properties.CategoryId = privateRooms.Id;
                    properties.Name = "private-" + arg.Username.Split(" ")[0] + "-" + (userId % 10000);
                    properties.Topic = userId.ToString();
                }).Result;
                // create permissions for user
                CreatePrivateChannelpermission(privateChanel, arg).Wait();
                // send the welcome message
                return privateChanel.SendMessageAsync($"Hello <@{userId}> and welcome to the server!\nPlease checkout <#754769525584560349> to know what's up.\nYou can talk to me here or in private messages to start your adventure!\n> Type anywhere to start.");
            }
            else
            {
                // renew user permissions
                CreatePrivateChannelpermission(existingChannel, arg).Wait();
                return existingChannel.SendMessageAsync($"Welcome back <@{userId}>!\nPlease checkout <#754769525584560349> again!\nYou can resume your adventure by talking to me here or in private messages!\n> Type anywhere to start.");
            }
        }

        public static Task CreatePrivateChannel(ulong guildId, ulong userId)
        {
            var guild = DiscordClient.GetGuild(guildId);
            var user = guild.GetUser(userId);
            // look for an existing channel for the user
            var existingChannel = guild.TextChannels.FirstOrDefault(x => x.Topic == userId.ToString());
            if (existingChannel == null)
            {
                // get the private room category
                var privateRooms = guild.CategoryChannels.FirstOrDefault(x => x.Name == "private-rooms");
                // create a new private channel for the user
                var privateChanel = guild.CreateTextChannelAsync("private-" + user.Username.Split(" ")[0] + "-" + (userId % 10000),
                (properties) =>
                {
                    properties.CategoryId = privateRooms.Id;
                    properties.Name = "private-" + user.Username.Split(" ")[0] + "-" + (userId % 10000);
                    properties.Topic = userId.ToString();
                }).Result;
                // create permissions for user
                CreatePrivateChannelpermission(privateChanel, user).Wait();
                // send the welcome message
                return privateChanel.SendMessageAsync($"Hello <@{userId}> and welcome to the server!\nPlease checkout <#754769525584560349> to know what's up.\nYou can talk to me here or in private messages to start your adventure!\n> Type anywhere to start.");
            }
            else
            {
                // renew user permissions
                CreatePrivateChannelpermission(existingChannel, user).Wait();
                return existingChannel.SendMessageAsync($"Welcome back <@{userId}>!\nPlease checkout <#754769525584560349> again!\nYou can resume your adventure by talking to me here or in private messages!\n> Type anywhere to start.");
            }
        }

        private static Task CreatePrivateChannelpermission(IGuildChannel channel, IGuildUser user)
        {
            return channel.AddPermissionOverwriteAsync(user,
                new OverwritePermissions(
                    PermValue.Inherit, // create instant invite
                    PermValue.Inherit, // manage channel
                    PermValue.Allow, // add reactions
                    PermValue.Allow, // view channel
                    PermValue.Allow, // send messages
                    PermValue.Inherit, // send TTS
                    PermValue.Inherit, // manage message
                    PermValue.Allow, // embed links
                    PermValue.Allow, // attach files
                    PermValue.Allow, // read message history
                    PermValue.Inherit, // mention @everyone
                    PermValue.Allow // use external emojis
                ));
        }

        private Task OnDisconnected(Exception e)
        {
            return Task.CompletedTask;
        }

        public static void ConfigureDatabase()
        {
            Database = new SineahDbContext();
        }

        public static void ConfigureAutomapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(Program).Assembly));

            Program.Mapper = config.CreateMapper();
        }
    }
}
