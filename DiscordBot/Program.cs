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
using System.Threading.Channels;

public class Program
{
    //declare the client and timer fields
    private static DiscordSocketClient _client;
    private static System.Timers.Timer _timer;

    //Tuple to hold name and ID
    static ValueTuple<string, ulong>[] BlokeList = new (string name, ulong userID)[]
    {
        ("Tom", 197771388415770624),
        ("Darcy", 0),
        ("Louis", 251434937436209152),
        ("Connor", 0)
    };

    static DateTime today = DateTime.Today;
    static int dateAsInt = today.DayOfYear;
    static (string Name, ulong userID) todaysBloke = BlokeList[dateAsInt % 4];

    static void Main(string[] args)
    {
        Program program = new Program();
        program.RunBotAsync().GetAwaiter().GetResult();
    }

    private static void SetTimer()
    {
        //Set default alarm:
        TimeOnly alarmTime = new TimeOnly(23, 00);
        //Current time as a TimeOnly type
        TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
        //Difference between current and alarm time
        TimeSpan difference;

        Console.WriteLine($"alarm time: {alarmTime} \ncurrent time: {currentTime}");

        //Work out the remainder time for the alarm given the current time
        if (currentTime < alarmTime)
        {
            Console.WriteLine("Alarm in the future");
            difference = alarmTime.ToTimeSpan() - currentTime.ToTimeSpan();
        }
        else
        {
            Console.WriteLine("Alarm in the past, resetting for the future");
            difference = (TimeOnly.MaxValue - currentTime).Add(alarmTime.ToTimeSpan());
        }

        Console.WriteLine($"Setting alarm for {alarmTime} which is {difference.TotalHours} hours from now. It's {todaysBloke.Name}'s day today");
        _timer = new System.Timers.Timer(difference.TotalMilliseconds);

        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = false;
        _timer.Enabled = true;
        _timer.Start();
    }

    public async Task RunBotAsync()
    {
        SetTimer();

        //Set intents because message content is not enabled by default
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
            await message.Channel.SendMessageAsync("Today's Bloke to suffer is: " + todaysBloke);
            Console.WriteLine(message.Channel.Id);
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
            }
            else
            {
                await messageUser.Channel.SendMessageAsync("invalid");
            }
        }
    }


    private static async void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        ulong channelID = 1183474990541197356;//to get ID, Remember to right-click on the CHANNEL
        var channel = _client.GetChannel(channelID) as IMessageChannel;

        var user = _client.GetUser(todaysBloke.userID);


        if (todaysBloke.userID == 0)
        {
            Console.WriteLine($"{todaysBloke.Name} does not have a valid user ID");
            await channel.SendMessageAsync($"It's {todaysBloke.Name}'s day for the dishes today");
        }
        else 
        {
            await channel.SendMessageAsync($"It's {todaysBloke.Name}'s day for the dishes today");
            await user.SendMessageAsync($"Hi {todaysBloke.Name}, it's your day for the dishwasher");
        }

        Console.WriteLine("alarm up");


        SetTimer();

    }

    private async Task OnReadyAsync()
    {

        ulong channelID = 1183474990541197356;//Remeber to get the CHANNEL ID not the guild ID
        var channel = _client.GetChannel(channelID) as IMessageChannel;

        if (channel != null)
        {
            Console.WriteLine("Connected to channel"); 
        }
        else
        {
            Console.WriteLine("channel ID not valid");
        }
    }

}