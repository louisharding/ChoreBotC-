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
        return;
    }

    //running the bot asynchronously, public because methods need to be accessed in main
    public async Task RunBotAsync()
    {
        //initialise the _client field
        _client = new DiscordSocketClient();

        //subscribing to the MessageReceived event:
        //_client.MessageReceived += MessageReceivedAsync;

        //Bot token
        string key = discordbot.key;

        //now send a message to the chat
        
        //Login to discord
        await _client.LoginAsync(TokenType.Bot, key);

        //Start the client 
        await _client.StartAsync();
        //keep bot running
        await Task.Delay(-1);
    }

    //Handles incoming messages
    private async Task bob(SocketMessage message)
    {

    }
}