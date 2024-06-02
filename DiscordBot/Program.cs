using Discord;
using Discord.WebSocket;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DiscordBot;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Collections.Specialized;

class Program
{
    //declare the client class, using class naming conventions with _
    private DiscordSocketClient _client;
    static void Main()
    {
        Console.WriteLine(discordbot.key);
    }

    //running the bot asynchronously, public because methods need to be accessed in main
    public async Task RunBotAsync()
    {
        //initialise the _client field
        _client = new DiscordSocketClient();

        //subscribing to the MessageReceived event
        //_client.MessageReceived += MessageReceivedAsync;

        //Bot token
        string key = discordbot.key;
        

    }
}