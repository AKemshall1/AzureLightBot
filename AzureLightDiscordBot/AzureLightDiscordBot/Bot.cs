using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
//using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureLightDiscordBot
{
    //Async = can run other code at the same time - don't have to wait for that code block to end

    public class Bot
    {
        public DiscordClient Client { get; private set; }    //can get the client from anywhere, but can only set it from this class;
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false); //configureAwait false - the thread that starts this task does not have to be the one to continue it - faster

            var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);


            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
              
            };

            Client = new DiscordClient(config);

            //when the bot is turned on, execute a function
            Client.Ready += OnClientReady;

            var commandsConfig = new CommandsNextConfiguration
            {
                //how commands are set up
                StringPrefixes = new string[] {configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true  //can mention bot insted of !
                
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<dCommands>();

            await Client.ConnectAsync(); //connect to the server
            await Task.Delay(-1); //the bot can quit early, this prevents it.
        }

        //task is the async version of void
        private Task OnClientReady(ReadyEventArgs e)
        {
            //send message saying the bot is online
            return Task.CompletedTask;
        }

    }
}
