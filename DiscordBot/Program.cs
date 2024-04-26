
using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

class Program
{
private DiscordSocketClient _client;
    public static async Task Main(string[] args)
    {
        await new Program().RunBotAsync();
    }
}