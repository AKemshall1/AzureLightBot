using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AzureLightDiscordBot
{
    class dCommands:BaseCommandModule
    {
        [Command("bestgirl")]
        [Description("Tells you who the true best girl is.")]
        public async Task BestGirl(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("IJN Hiryuu!").ConfigureAwait(false);
        }

        [Command("plane")]
        [Description("Provides plane equipment guide image")]
        public async Task Plane(CommandContext ctx)
        {
            await ctx.Channel.SendFileAsync(@"images\planes.jpg").ConfigureAwait(false);
        }

        [Command("bbgun")]
        [Description("Provides BB main gun guide image")]
        public async Task BBGun(CommandContext ctx)
        {
            await ctx.Channel.SendFileAsync(@"images\bbMain.jpg").ConfigureAwait(false);
        }

        [Command("cagun")]
        [Description("Provides CA main gun guide")]
        public async Task CAGun(CommandContext ctx)
        {
            await ctx.Channel.SendFileAsync(@"images\caGun.jpg").ConfigureAwait(false);
        }

        [Command("clgun")]
        [Description("Provides CL main gun guide.")]
        public async Task CLGun(CommandContext ctx)
        {
            await ctx.Channel.SendFileAsync(@"images\clGun.png").ConfigureAwait(false);
        }

        [Command("ddgun")]
        [Description("Provides Destroyer main gun guide. Also functions as a CL/CA aux guide")]
        public async Task DDGun(CommandContext ctx)
        {
            await ctx.Channel.SendFileAsync(@"images\ddGun.jpg").ConfigureAwait(false);
        }

        [Command("torpedo")]
        [Description("Provides torpedo guide")]
        public async Task Torpedo(CommandContext ctx)
        {
            await ctx.Channel.SendFileAsync(@"images\torpedo.png").ConfigureAwait(false);
        }

        [Command("bbauxgun")]
        [Description("Provides aux BB Gun equipment guide image")]
        public async Task BBAuxGun(CommandContext ctx)
        {
            await ctx.Channel.SendFileAsync(@"images\bbAuxGun.png").ConfigureAwait(false);
        }

        [Command("aa")]
        [Description("Provides plane equipment guide image")]
        public async Task AAGun(CommandContext ctx)
        {
            await ctx.Channel.SendFileAsync(@"images\aa.jpg").ConfigureAwait(false);
        }

        [Command("cvaux")]
        [Description("Provides aux CV equipment guide image")]
        public async Task CVAux(CommandContext ctx)
        {
            await ctx.Channel.SendFileAsync(@"images\cvAux.png").ConfigureAwait(false);
        }

        [Command("bbaux")]
        [Description("Provides aux BB equipment guide image")]
        public async Task BBAux(CommandContext ctx)
        {
            await ctx.Channel.SendFileAsync(@"images\bbAux.png").ConfigureAwait(false);
        }

        [Command("araux")]
        [Description("Provides AR aux equipment guide image")]
        public async Task ARAux(CommandContext ctx)
        {
            await ctx.Channel.SendFileAsync(@"images\arAux.png").ConfigureAwait(false);
        }

        [Command("info")]
        [Description("Returns general infomation about the specified ship. EG. !info hiryuu !info ning_hai")]
        public async Task shipGeneral(CommandContext ctx, string boat)
        {
            string shipCalled = boat + ".json"; //file name for boat called
            //read json, then deserialize it
            var json = string.Empty;
            using (var fs = File.OpenRead(@"wikia\Ships\" + shipCalled))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false); //configureAwait false - the thread that starts this task does not have to be the one to continue it - faster
                 
            var configShip = JsonConvert.DeserializeObject<ConfigShip>(json);

            //for nested json objects, parse specifically
            JObject jo = JObject.Parse(json);
            string artist = jo.SelectToken("artist.pixiv").ToString();
            if (artist == string.Empty)
                artist = "Not found";

            string boatName = " - " +"\n"+ "Name: "+ configShip.faction + configShip.Name + "\n";
            string boatRarity = "Rarity: " + configShip.Rarity + "\n";
            string boatClass = "Class: " + configShip.ShipClass + "\n";
            string boatType = "Type: " + configShip.type + "\n";
            string boatGet = "Obtainable: " + configShip.AcquisitionMethod + "\n";
            string boatVA = "Voice Actress: " + configShip.VoiceActress + "\n";
            string artistPixiv = "Artist Pixiv: " + artist;
    
            await ctx.Channel.SendMessageAsync(boatName+boatRarity+boatClass+boatType+boatGet+boatVA+artistPixiv).ConfigureAwait(false);
        }


        //[Command("stats")]
        //[Description("Returns statistics for the specified boat. EG. !stats hiryuu !stats ark_royal")]
        //public async Task shipStats(CommandContext ctx, string boat)
        //{
        //    string shipCalled = boat + ".json"; //file name for boat called
        //    //read json, then deserialize it
        //    var json = string.Empty;
        //    using (var fs = File.OpenRead(@"wikia\Ships\" + shipCalled))
        //    using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
        //        json = await sr.ReadToEndAsync().ConfigureAwait(false); //configureAwait false - the thread that starts this task does not have to be the one to continue it - faster

        //    var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);
        //    JObject jo = JObject.Parse(json);




        //}
    }
}
