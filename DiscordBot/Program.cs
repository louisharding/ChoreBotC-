using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;


class Program
{
    private DiscordSocketClient _client;
    static async Task Main(string[] args)
    {
        await new Program().RunBotAsync();
    }

    public async Task RunBotAsync()
    {
        _client = new DiscordSocketClient();

        _client.Log += LogAsync;

        await _client.LoginAsync(TokenType.Bot, "MTE4MzA5MTE4NDUyOTMyMjAyNA.GJW-Yo.HZfzvUlUUYYqwyW81R0SNXyFBo6xK-djnTCVI4");
        await _client.StartAsync();

        _client.Ready += () =>
        {
            Console.WriteLine("bot connected & ready");
            return Task.CompletedTask;
        };

        _client.MessageReceived += async (message) =>
        {
            if (message.Content != null)
            {
                if (message.Author.IsBot)
                {
                    return;
                } else
                {
                    await message.Channel.SendMessageAsync("Hi");
                }

            }
        };


        await Task.Delay(-1);
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log);
        return Task.CompletedTask;
    }

}