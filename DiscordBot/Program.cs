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

        await _client.LoginAsync(TokenType.Bot, "MTE4MzA5MTE4NDUyOTMyMjAyNA.Gz5dhw.WcL_uR_8Br1vsIKgHjOPtFyPnpKFL_V-SXoxFk");
        await _client.StartAsync();

        _client.Ready += () =>
        {
            Console.WriteLine("bot connected & ready");
            return Task.CompletedTask;
        };

        _client.MessageReceived += async (message) =>
        {
            Console.WriteLine("message is: " + message.Content);

            if (!message.Author.IsBot)
            {
                string msgContent = message.Content;
                await message.Channel.SendMessageAsync(msgContent);
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