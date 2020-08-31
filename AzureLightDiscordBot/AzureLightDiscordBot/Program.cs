using System;

namespace AzureLightDiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();    //get awaiter - async run bot
            //Console.WriteLine("Ran the bot");
        }
    }
}
