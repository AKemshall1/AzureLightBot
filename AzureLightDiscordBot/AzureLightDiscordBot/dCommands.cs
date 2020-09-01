using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

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

        [Command("equip")]
        [Description("Sends equipment guide per category specified. Commands all lowercase no spaces. E.G.(!equip aa/araux/bbaux/bbgunmain/bbgunaux/cagun/clcaaux/clgun/cvaux/ddaux/ddgunmain/planes/sub/torp)  Guide credit: Nerezza")]
        public async Task EquipmentGuide(CommandContext ctx, string guideType)
        {
            string filename = guideType + ".jpg";
            await ctx.Channel.SendFileAsync(@"images\"+filename).ConfigureAwait(false);
        }

        [Command("info")]
        [Description("Returns general infomation about the specified ship. EG. !info hiryuu !info ning_hai")]
        public async Task shipGeneral(CommandContext ctx, string boat)
        {
            string normChibi = "chibi.png";

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

            string testString = jo.SelectToken("stats.120retrofit.hp").ToString();
            if (testString != string.Empty) //does have retrofit
            {
                normChibi = "kaichibi.png";
            }
           
      
            string boatName = configShip.Name;
            string Name = " - \n"+ "Name: "+ configShip.faction +" " + configShip.Name + "\n";
            string boatRarity = "Rarity: " + configShip.Rarity + "\n";
            string boatClass = "Class: " + configShip.ShipClass + "\n";
            string boatType = "Type: " + configShip.type + "\n";
            string boatGet = "Obtainable: " + configShip.AcquisitionMethod + "\n";
            string boatVA = "Voice Actress: " + configShip.VoiceActress + "\n";
            string artistPixiv = "Artist Pixiv: " + artist;


            if(File.Exists(@"chibis\" + boatName + normChibi))
                await ctx.Channel.SendFileAsync(@"chibis\" + boatName + normChibi);
           
            await ctx.Channel.SendMessageAsync(Name+boatRarity+boatClass+boatType+boatGet+boatVA+artistPixiv).ConfigureAwait(false);
        }


        [Command("stats")]
        [Description("Returns statistics for the specified boat at L120 with retrofit if applicable. EG. !stats hiryuu !stats ark_royal")]
        public async Task shipStats(CommandContext ctx, string boat)
        {
            string hp, firepower, torpedo, antiAir, aviation, reload, hit, evasion, speed, luck, asw, oxygen, ammo, cost, armor;
            string normChibi = "chibi.png";
           

            string shipCalled = boat + ".json"; //file name for boat called
            //read json, then deserialize it
            var json = string.Empty;
            using (var fs = File.OpenRead(@"wikia\Ships\" + shipCalled))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false); //configureAwait false - the thread that starts this task does not have to be the one to continue it - faster

            var configShip = JsonConvert.DeserializeObject<ConfigShip>(json);
            JObject jo = JObject.Parse(json);

            string boatName = configShip.Name;
            string Name = "Name: " + configShip.faction + " " + configShip.Name + "\n";

            string testString =jo.SelectToken("stats.120retrofit.hp").ToString();
            if (testString == string.Empty) //doesn't have retrofit
            {
                hp = jo.SelectToken("stats.120.hp").ToString();
                firepower = jo.SelectToken("stats.120.firepower").ToString();
                torpedo = jo.SelectToken("stats.120.torpedo").ToString();
                antiAir = jo.SelectToken("stats.120.antiAir").ToString();
                aviation = jo.SelectToken("stats.120.aviation").ToString();
                reload = jo.SelectToken("stats.120.reload").ToString();
                hit = jo.SelectToken("stats.120.hit").ToString();
                evasion = jo.SelectToken("stats.120.evasion").ToString();
                speed = jo.SelectToken("stats.120.speed").ToString();
                luck = jo.SelectToken("stats.120.luck").ToString();
                asw = jo.SelectToken("stats.120.asw").ToString();
                oxygen = jo.SelectToken("stats.120.oxygen").ToString();
                ammo = jo.SelectToken("stats.120.ammo").ToString();
                cost = jo.SelectToken("stats.120.cost").ToString();
                armor = jo.SelectToken("stats.120.armor").ToString();
            }
            else 
            {
                hp = jo.SelectToken("stats.120.hp").ToString();
                firepower = jo.SelectToken("stats.120retrofit.firepower").ToString();
                torpedo = jo.SelectToken("stats.120retrofit.torpedo").ToString();
                antiAir = jo.SelectToken("stats.120retrofit.antiAir").ToString();
                aviation = jo.SelectToken("stats.120retrofit.aviation").ToString();
                reload = jo.SelectToken("stats.120retrofit.reload").ToString();
                hit = jo.SelectToken("stats.120retrofit.hit").ToString();
                evasion = jo.SelectToken("stats.120retrofit.evasion").ToString();
                speed = jo.SelectToken("stats.120retrofit.speed").ToString();
                luck = jo.SelectToken("stats.120retrofit.luck").ToString();
                asw = jo.SelectToken("stats.120retrofit.asw").ToString();
                oxygen = jo.SelectToken("stats.120retrofit.oxygen").ToString();
                ammo = jo.SelectToken("stats.120retrofit.ammo").ToString();
                cost = jo.SelectToken("stats.120retrofit.cost").ToString();
                armor = jo.SelectToken("stats.120retrofit.armor").ToString();
                normChibi = "kaichibi.png";
            }

            string hpFinal = "Health: " + hp + "\n";
            string fpFinal = "Firepower: " + firepower + "\n";
            string torpFinal = "Torpedo: " + torpedo + "\n";
            string aaFinal = "AntiAir: " + antiAir + "\n";
            string aviFinal = "Aviation: " + aviation + "\n";
            string relFinal = "Reload: " + reload + "\n";
            string hitFinal = "Hit: " + hit + "\n";
            string evaFinal = "Evasion: " + evasion + "\n";
            string speedFinal = "Speed: " + speed + "\n";
            string luckFinal = "Luck: " + luck + "\n";
            string aswFinal = "ASW: " + asw + "\n";
            string oxyFinal = "Oxygen: " + oxygen + "\n";
            string ammoFinal = "Ammo: " + ammo + "\n";
            string costFinal = "Cost: " + cost + "\n";
            string armorFinal = "Amor: " + armor;

            await ctx.Channel.SendFileAsync(@"chibis\" + boatName + normChibi);
            await ctx.Channel.SendMessageAsync("- \n"+ Name+ hpFinal + fpFinal +torpFinal + aaFinal + aviFinal + relFinal + evaFinal+speedFinal+luckFinal+aswFinal+oxyFinal+ammoFinal+costFinal+armorFinal).ConfigureAwait(false);
        }

        [Command("stage")]
        [Description("Returns the enemy level of the stage provided. E.G. (!stage 2-4hard) will provide mob and boss levels for this stage. SOS missions under x-5")]
        public async Task ReturnStageLevel(CommandContext ctx, string stage)
        {
            //read json, then deserialize it
            var json = string.Empty;
            using (var fs = File.OpenRead(@"worlds\chapters.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false); //configureAwait false - the thread that starts this task does not have to be the one to continue it - faster

            //var configShip = JsonConvert.DeserializeObject<ConfigShip>(json);
            JObject jo = JObject.Parse(json);

            await ctx.Channel.SendMessageAsync(stage +": " + jo.SelectToken(stage));

        }
    }
}
