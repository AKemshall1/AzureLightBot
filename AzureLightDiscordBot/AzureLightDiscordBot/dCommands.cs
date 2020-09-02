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
            await ctx.Channel.SendMessageAsync("Buli? I'm the best!").ConfigureAwait(false);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [Command("equip")]
        [Description("Sends equipment guide per category specified. Commands all lowercase no spaces. E.G.(!equip aa/araux/bbaux/bbgunmain/bbgunaux/cagun/clcaaux/clgun/cvaux/ddaux/ddgunmain/planes/sub/torp)  Guide credit: Nerezza")]
        public async Task EquipmentGuide(CommandContext ctx, string guideType)
        {
            string guideCap = guideType;    //if the passed string doesn't work, bot will return it at the start of a sentence so make the first letter a capital
            if (guideType.Length >= 1)
            {
                guideCap = char.ToUpper(guideType[0]) + guideType.Substring(1);
            }
            
            string filename = guideType + ".jpg";
            Console.WriteLine(@"GUIDE: images\" + filename);
            if (File.Exists(@"images\" + filename))
            {
                await ctx.Channel.SendFileAsync(@"images\" + filename).ConfigureAwait(false);
            }
            else
                await ctx.Channel.SendMessageAsync(guideCap +"? Whats that, buli? (Accepted format: !equip aa/araux/bbaux/bbgunmain/bbgunaux/cagun/clcaaux/clgun/cvaux/ddaux/ddgunmain/planes/sub/torp)").ConfigureAwait(false);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [Command("info")]
        [Description("Returns general infomation about the specified ship. EG. !info hiryuu !info ning_hai")]
        public async Task shipGeneral(CommandContext ctx, string boat)
        {
            string boatCap = boat;  
            string normChibi = "chibi.png";
            string shipCalled = boat + ".json"; //file name for boat called

            if (boat.Length >= 1)   //if the passed string doesn't work, bot will return it at the start of a sentence so make the first letter a capital
            {
                boatCap = char.ToUpper(boat[0]) + boat.Substring(1);
            }
           
            if (File.Exists(@"wikia\Ships\" + shipCalled)) 
            {
                Console.WriteLine("INFO: " + shipCalled);

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

                string testString = jo.SelectToken("stats.120retrofit.hp").ToString();  //if a ship does not have a retrofit, the retrofit hp stat will be blank so use this as a test case
                if (testString != string.Empty) //does have retrofit
                {
                    normChibi = "kaichibi.png"; //adds kai onto the string to get the right chibi
                }

                string boatName = configShip.Name;  //names are not capitalised in the json so correct this
                string Name = " - \n" + "Name: " + configShip.faction + " " + char.ToUpper(configShip.Name[0]) + configShip.Name.Substring(1) + "\n";
                string boatRarity = "Rarity: " + configShip.Rarity + "\n";
                string boatClass = "Class: " + configShip.ShipClass + "\n";
                string boatType = "Type: " + configShip.type + "\n";
                string boatGet = "Obtainable: " + configShip.AcquisitionMethod + "\n";
                string boatVA = "Voice Actress: " + configShip.VoiceActress + "\n";
                string artistPixiv = "Artist Pixiv: " + artist;


                if (File.Exists(@"chibis\" + boatName + normChibi)) //check if the file exists before trying to send it
                    await ctx.Channel.SendFileAsync(@"chibis\" + boatName + normChibi).ConfigureAwait(false); ;

                await ctx.Channel.SendMessageAsync(Name + boatRarity + boatClass + boatType + boatGet + boatVA + artistPixiv).ConfigureAwait(false);
            }
            else
                await ctx.Channel.SendMessageAsync(boatCap +"? Who is she, buli...? (Multiple word ships must have underscores (e.g. ning_hai). Submarines contain no dashes (e.g i58))").ConfigureAwait(false);


        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [Command("stats")]
        [Description("Returns statistics for the specified boat at L120 with retrofit if applicable. EG. !stats hiryuu !stats ark_royal")]
        public async Task shipStats(CommandContext ctx, string boat)
        {
            string hp, firepower, torpedo, antiAir, aviation, reload, hit, evasion, speed, luck, asw, oxygen, ammo, cost, armor;
            string boatCap = boat;
            string normChibi = "chibi.png";
            string shipCalled = boat + ".json"; //file name for boat called

            if (boat.Length >= 1)
            {
                boatCap = char.ToUpper(boat[0]) + boat.Substring(1);
            }

            if (File.Exists(@"wikia\Ships\" + shipCalled))
            {
                Console.WriteLine("STATS: " + shipCalled);
                //read json, then deserialize it
                var json = string.Empty;
                using (var fs = File.OpenRead(@"wikia\Ships\" + shipCalled))
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    json = await sr.ReadToEndAsync().ConfigureAwait(false); //configureAwait false - the thread that starts this task does not have to be the one to continue it - faster
                var configShip = JsonConvert.DeserializeObject<ConfigShip>(json);
                JObject jo = JObject.Parse(json);

                string boatName = configShip.Name;
                string Name = "Name: " + configShip.faction + " " + char.ToUpper(configShip.Name[0]) + configShip.Name.Substring(1) + "\n";

                string testString = jo.SelectToken("stats.120retrofit.hp").ToString();
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
                string armorFinal = "Armor: " + armor;

                if (File.Exists(@"chibis\" + boatName + normChibi))
                    await ctx.Channel.SendFileAsync(@"chibis\" + boatName + normChibi).ConfigureAwait(false); ;
                await ctx.Channel.SendMessageAsync("- \n" + Name + hpFinal + fpFinal + torpFinal + aaFinal + aviFinal + relFinal + evaFinal + speedFinal + luckFinal + aswFinal + oxyFinal + ammoFinal + costFinal + armorFinal).ConfigureAwait(false);
            }
            else
                await ctx.Channel.SendMessageAsync(boatCap + "? Who is she, buli...? (Multiple word ships must have underscores (e.g. ning_hai). Submarines contain no dashes (e.g i58))").ConfigureAwait(false);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [Command("stage")]
        [Description("Returns the enemy level of the stage provided. E.G. (!stage 2-4hard) will provide mob and boss levels for this stage. SOS missions under x-5")]
        public async Task ReturnStageLevel(CommandContext ctx, string stage)
        {
            string stageCap = stage;

            if (stage.Length >= 1)
            {
                stageCap = char.ToUpper(stage[0]) + stage.Substring(1);
            }
            //read json, then deserialize it
            var json = string.Empty;
            using (var fs = File.OpenRead(@"worlds\chapters.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false); //configureAwait false - the thread that starts this task does not have to be the one to continue it - faster

            //var configShip = JsonConvert.DeserializeObject<ConfigShip>(json);
            JObject jo = JObject.Parse(json);

            if (jo.SelectToken(stage) != null)
            {
                await ctx.Channel.SendMessageAsync(stage + ": " + jo.SelectToken(stage)).ConfigureAwait(false); ;
            }
            else
                await ctx.Channel.SendMessageAsync("Commander, I've never been to " + stageCap+" before! Maybe big sis knows.. (Accepted format : !stage 2-4, !stage 2-4hard. SOS stages are listed under x-5.)").ConfigureAwait(false); 

        }
    }
}
