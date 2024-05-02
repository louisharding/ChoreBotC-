
using Discord;
using Discord.WebSocket;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using static Program;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("waiting for an asynchronous request to return...");
        await completeAcynchronousTask();
        Console.WriteLine("completed ");
    }

    static void myMethod()
    {
        Console.WriteLine("myMethod");
    }


    static async Task completeAcynchronousTask()
    {
        await Task.Delay(2000);
        Console.WriteLine("Returning");
    }
}

