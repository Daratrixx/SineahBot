using Discord;
using Discord.WebSocket;
using SineahBot.Data;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SineahBot.Application
{
    public static class Discord
    {
        public static DiscordSocketClient DiscordClient;
        public static async Task OnlinePlay()
        {
            DiscordClient = new DiscordSocketClient(new DiscordSocketConfig()
            {
                AlwaysDownloadUsers = true,
                GuildSubscriptions = false,
                DefaultRetryMode = RetryMode.RetryRatelimit,
            });

            DiscordClient.Log += Log;

            await DiscordClient.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("SineahBotToken"));
            await DiscordClient.StartAsync();

            DiscordClient.UserJoined += UserJoined;
            DiscordClient.MessageReceived += MessageReceived;
            DiscordClient.Disconnected += OnDisconnected;
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private static readonly ulong[] ignoredChannels = new ulong[] { 741728973318389790 };
        private static Task MessageReceived(SocketMessage message)
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

        private static Task UserJoined(SocketGuildUser arg)
        {
            var guild = arg.Guild;
            var user = arg;
            return CreatePrivateChannel(guild, user);
        }

        public static Task CreatePrivateChannel(ulong guildId, ulong userId)
        {
            var guild = DiscordClient.GetGuild(guildId);
            // try manually fetching user to add ensure its presence in cache?
            //DiscordClient.GetUser(userId);
            var user = guild.GetUser(userId);
            return CreatePrivateChannel(guild, user);
        }

        public static Task CreatePrivateChannel(SocketGuild guild, SocketGuildUser user)
        {
            var userId = user.Id;
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

        private static Task OnDisconnected(Exception e)
        {
            return Task.CompletedTask;
        }
    }
}
