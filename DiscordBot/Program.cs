using Discord;
using Discord.WebSocket;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DiscordBot;

class Program
{
    private static DiscordSocketClient _client;


    static async Task Main(string[] args)
    {
        //Initialise the bot client
        _client = new DiscordSocketClient();

        //Event handlers registration
        _client.Log += LogAsync;
        _client.Ready += ReadyAsync;
        _client.MessageReceived += MessageReceivedAsync;
    }

 
}