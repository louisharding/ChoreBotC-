using Discord;
using Discord.WebSocket;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DiscordBot;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Timers;
using System.Diagnostics.CodeAnalysis;

public class Program
{
    //declare the client class, using class naming conventions with _
    private DiscordSocketClient _client;
    private static System.Timers.Timer _timer;

    //Bloke Logic
    String[] BlokeList = { "Tom", "Darcy", "Louis", "Connor" };
    static DateTime today = DateTime.Today;
    static string dateAsString = today.ToString("dd");
    static int dateAsInt = int.Parse(dateAsString);
    //Default alarm time
    static TimeOnly alarmClockTime = new TimeOnly(23,0);

    static void Main(string[] args)
    {
        Program program = new Program();
        program.RunBotAsync().GetAwaiter().GetResult();

        //Set default alarm:
        TimeOnly alarmTime = new TimeOnly(23,0);

        //Current time as a TimeOnly type
        TimeOnly currentTime = TimeOnly.FromDateTime(today);

        //Difference between current and alarm time
        TimeSpan difference;

        if(currentTime < alarmTime)
        {
            difference = alarmTime.ToTimeSpan() - currentTime.ToTimeSpan();
        }
        else
        {
            difference = (TimeOnly.MaxValue - currentTime).Add(alarmTime.ToTimeSpan());
        }

        _timer = new System.Timers.Timer(difference.TotalMilliseconds);

        _timer.Elapsed += OnTimedEvent;

        _timer.AutoReset = true;
        _timer.Start();
        
    }

    //running the bot asynchronously, public because methods need to be accessed in main
    public async Task RunBotAsync()
    {

        //Set intents because message content is not enabled apparently...
        var setIntentConfig = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.All
        };


        //initialise the _client field
        _client = new DiscordSocketClient(setIntentConfig);

        //subscribing to the MessageReceived event:
        _client.MessageReceived += MessageReceivedAsync;

        _client.MessageReceived += HandleMessgeAsync;

        //Bot token
        string key = discordbot.key;

        //Login to discord
        await _client.LoginAsync(TokenType.Bot, key);

        //Start the client 
        await _client.StartAsync();

        //Send a message to a channel after bot is ready
        _client.Ready += OnReadyAsync;

        //keep bot running
        await Task.Delay(-1);
    }


    //Handles incoming messages
    private async Task MessageReceivedAsync(SocketMessage message)
    {
        //Assure no feedback loop:
        if (message.Author.IsBot) return;


        //Bot 'command' lol
        if (message.Content == "!chore")
        {
            await message.Channel.SendMessageAsync("Today's Bloke to suffer is: " + BlokeList[dateAsInt % 4]);
        }
    }


    //Handles sending of private messages
    private async Task HandleMessgeAsync(SocketMessage message)
    {
        // Ignore system messages and bot messages
        if (message is not SocketUserMessage messageUser || messageUser.Author.IsBot) 
        { 
            return; 
        }

        //SetAlarm(alarmClockTime, messageUser.Channel, message.Author);

        //command prefix of "!dm"
        if (messageUser.Content.StartsWith("!alarm"))
        {
            //Send a DM to 
            //Parse the alarm time from the messag
            var commandParts = messageUser.Content.Split(' ');
            if (commandParts.Length == 2 && TimeSpan.TryParse(commandParts[1], out var alarmTime))
            {
                await messageUser.Channel.SendMessageAsync($"Alarm set for {alarmTime} from now.");

                //Schedule the alarm
                //SetAlarm(alarmTime, messageUser.Channel, messageUser.Author);
            }
            else
            {
                await messageUser.Channel.SendMessageAsync("invalid");
            }
        }
    }

    private static async void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        Console.WriteLine("Alarm up, it's: {0}", e.SignalTime);
        //send a message to the channel

    }

    private async Task OnReadyAsync()
    {
        Console.WriteLine("Connected");

        ulong channelID = 1183474990541197352;
        var channel = _client.GetChannel(channelID) as IMessageChannel;

        if (channel != null)
        {
            await channel.SendMessageAsync("Bot says"); 
        }
        else
        {
            Console.WriteLine("no channel");
        }

    }

}