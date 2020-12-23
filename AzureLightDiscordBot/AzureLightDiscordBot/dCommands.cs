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
using System.Linq;

namespace AzureLightDiscordBot
{
    class dCommands:BaseCommandModule
    {
        public BoatNicknames bn;
        [Command("bestgirl")]
        [Description("Tells you who the true best girl is.")]
        public async Task BestGirl(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Buli? I'm the best!").ConfigureAwait(false);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [Command("equip")]
        [Description("Sends equipment guide imgur gallery. Guide credit: Nerezza")]
        public async Task EquipmentGuide(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("https://imgur.com/a/TNpH1rL").ConfigureAwait(false);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [Command("info")]
        [Description("Returns general infomation about the specified ship. EG. !info hiryuu !info ninghai")]
        public async Task shipGeneral(CommandContext ctx, string boat)
        {
            bn = new BoatNicknames(); 
            string chibi = "chibi.png"; //string to append to make filename - if ship is a retrofit this is changed edited later

            boat = boatNickname(boat);  //If the name passed was a nickname, turn it into the filename and store it
            string shipCalled = boat + ".json"; //create full file name for boat called
            string boatCap = formatName(boat);  //format boat name into user friendly aesthetic


            if (File.Exists(@"wikia\Ships\" + shipCalled)) 
            {
                #region JSONDESERIALISE
                //read json, then deserialize it
                var json = string.Empty;
                using (var fs = File.OpenRead(@"wikia\Ships\" + shipCalled))
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    json = await sr.ReadToEndAsync().ConfigureAwait(false);
                var configShip = JsonConvert.DeserializeObject<ConfigShip>(json);
              
                //for nested json objects, parse specifically
                JObject jo = JObject.Parse(json);
                #endregion

                #region VALUEFORMATTING

                //if a ship does not have a retrofit, the retrofit hp stat will be blank so use this as a test case
                if (jo.SelectToken("stats.120retrofit.hp").ToString() != string.Empty) //does have retrofit
                    chibi = "kaichibi.png"; //adds kai onto the string to get the right chibi
                


                string artist = jo.SelectToken("artist.pixiv").ToString();
                artist = (artist == string.Empty) ? artist : "Not Found";
               
                string boatName = configShip.Name;
                string Name = "Name: " + configShip.faction + " " + boatCap + "\n";
                string boatRarity = "Rarity: " + configShip.Rarity + "\n";
                string boatClass = "Class: " + configShip.ShipClass + "\n";
                string boatType = "Type: " + configShip.type + "\n";
                string boatGet = "Obtainable: " + configShip.AcquisitionMethod + "\n";
                string boatVA = "Voice Actress: " + configShip.VoiceActress + "\n";
                artist = "Artist Pixiv: " + artist;
                #endregion

                if (File.Exists(@"chibis\" + boatName + chibi)) //check if the file exists before trying to send it
                    await ctx.Channel.SendFileAsync(@"chibis\" + boatName + chibi).ConfigureAwait(false);

                await ctx.Channel.SendMessageAsync(Name + boatRarity + boatClass + boatType + boatGet + boatVA + artist).ConfigureAwait(false);
            }
            else
                await ctx.Channel.SendMessageAsync(boatCap + "? Who is she, buli...? (Please check your spelling. Nickname not recognised? @piggyapocalypse with the boat and nickname. New boats may not be available)").ConfigureAwait(false);


        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [Command("stats")]
        [Description("Returns statistics for the specified boat at L120 with retrofit if applicable. EG. !stats hiryuu !stats arkroyal")]
        public async Task shipStats(CommandContext ctx, string boat)
        {
            bn = new BoatNicknames();
            string hp, firepower, torpedo, antiAir, aviation, reload, hit, evasion, speed, luck, asw, oxygen, ammo, cost, armor;
            string normChibi = "chibi.png";
           
            boat = boatNickname(boat);
            string shipCalled = boat + ".json"; //file name for boat called
            string boatCap = formatName(boat);

            if (File.Exists(@"wikia\Ships\" + shipCalled))
            {
                #region JSONDESERALISE
                //read json, then deserialize it
                var json = string.Empty;
                using (var fs = File.OpenRead(@"wikia\Ships\" + shipCalled))
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    json = await sr.ReadToEndAsync().ConfigureAwait(false); //configureAwait false - the thread that starts this task does not have to be the one to continue it - faster
                var configShip = JsonConvert.DeserializeObject<ConfigShip>(json);
                JObject jo = JObject.Parse(json);
                #endregion

                string boatName = configShip.Name;
                string Name = "Name: " + configShip.faction + " " + boatCap + "\n";

                if (jo.SelectToken("stats.120retrofit.hp").ToString() == string.Empty) //doesn't have retrofit
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

                hp = "Health: " + hp + "\n";
                firepower = "Firepower: " + firepower + "\n";
                torpedo = "Torpedo: " + torpedo + "\n";
                antiAir = "Anti Air: " + antiAir + "\n";
                aviation = "Aviation: " + aviation + "\n";
                reload = "Reload: " + reload + "\n";
                hit = "Hit: " + hit + "\n";
                evasion = "Evasion: " + evasion + "\n";
                speed = "Speed: " + speed + "\n";
                luck = "Luck: " + luck + "\n";
                asw = "ASW: " + asw + "\n";
                oxygen = "Oxygen: " + oxygen + "\n";
                ammo = "Ammo: " + ammo + "\n";
                cost = "Cost: " + cost + "\n";
                armor = "Armor: " + armor;

                if (File.Exists(@"chibis\" + boatName + normChibi))
                    await ctx.Channel.SendFileAsync(@"chibis\" + boatName + normChibi).ConfigureAwait(false); ;
                await ctx.Channel.SendMessageAsync("- \n" + Name + hp + firepower + torpedo + antiAir + aviation + reload + evasion + speed + luck + asw + oxygen + ammo + cost + armor).ConfigureAwait(false);
            }
            else
                await ctx.Channel.SendMessageAsync(boatCap + "? Who is she, buli...? (Please check your spelling. Nickname not recognised? @piggyapocalypse with the boat and nickname. New boats may not be available)").ConfigureAwait(false);
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
                await ctx.Channel.SendMessageAsync(stage + ": " + jo.SelectToken(stage)).ConfigureAwait(false);
            }
            else
                await ctx.Channel.SendMessageAsync("Commander, I've never been to " + stageCap+" before! Maybe big sis knows.. (Accepted format : !stage 2-4, !stage 2-4hard. SOS stages are listed under x-5.)").ConfigureAwait(false); 

        }
      
        [Command ("skills")]
        [Description("Returns the skills for the boat provided. e.g. !skills Hiryuu")]
        public async Task ReturnSkills(CommandContext ctx, string boat)
        {
            bn = new BoatNicknames();
            string chibi = "chibi.png"; //string to append to make filename - if ship is a retrofit this is changed edited later

            boat = boatNickname(boat);  //If the name passed was a nickname, turn it into the filename and store it
            string shipCalled = boat + ".json"; //create full file name for boat called
            string boatCap = formatName(boat);  //format boat name into user friendly aesthetic



            if (File.Exists(@"wikia\Ships\" + shipCalled))
            {
                #region JSONDESERIALISE
                //read json, then deserialize it
                var json = string.Empty;
                using (var fs = File.OpenRead(@"wikia\Ships\" + shipCalled))
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    json = await sr.ReadToEndAsync().ConfigureAwait(false);
                var configShip = JsonConvert.DeserializeObject<ConfigShip>(json);

                //for nested json objects, parse specifically
                JObject jo = JObject.Parse(json);
                #endregion

                #region VALUEFORMATTING

                //if a ship does not have a retrofit, the retrofit hp stat will be blank so use this as a test case
                if (jo.SelectToken("stats.120retrofit.hp").ToString() != string.Empty) //does have retrofit
                    chibi = "kaichibi.png"; //adds kai onto the string to get the right chibi

                string boatName = configShip.Name;

                string skill1 = (jo.SelectToken("skill.1.description").ToString() != string.Empty) ? jo.SelectToken("skill.1.name").ToString() + "\n" + jo.SelectToken("skill.1.type").ToString() + "\n" + jo.SelectToken("skill.1.description").ToString() + "\n \n" :
                    "Skill 1 not found \n \n";

                string skill2 = (jo.SelectToken("skill.2.description").ToString() != string.Empty) ? jo.SelectToken("skill.2.name").ToString() + "\n" + jo.SelectToken("skill.2.type").ToString() + "\n" + jo.SelectToken("skill.2.description").ToString() + "\n \n":
                "Skill 2 not found \n \n";

                string skill3 = (jo.SelectToken("skill.3.description").ToString() != string.Empty) ? jo.SelectToken("skill.3.name").ToString() + "\n" + jo.SelectToken("skill.3.type").ToString() + "\n" + jo.SelectToken("skill.3.description").ToString() + "\n \n" :
                "Skill 3 not found \n \n";


                #endregion

                if (File.Exists(@"chibis\" + boatName + chibi)) //check if the file exists before trying to send it
                    await ctx.Channel.SendFileAsync(@"chibis\" + boatName + chibi).ConfigureAwait(false);

                await ctx.Channel.SendMessageAsync(skill1 + skill2 + skill3).ConfigureAwait(false);
            }
            else
                await ctx.Channel.SendMessageAsync(boatCap + "? Who is she, buli...? (Please check your spelling. Nickname not recognised? @piggyapocalypse with the boat and nickname. New boats may not be available)").ConfigureAwait(false);


        }




        //---------UTILITIES----------------------

        public string boatNickname(string boatInput)
        { 
            for (int i = 0; i < bn.nicknames.Length; i++)
            {       
                if (bn.nicknames[i].Contains(boatInput))
                {        
                    return bn.nicknames[i][0];
                }
            
            }
            return boatInput;
        }

        public string formatName(string boat)
        {
            string boatCap = boat;

            if (boat.Length >= 1)
            {
                boatCap = boat.Replace("_", " ");
                boatCap = char.ToUpper(boatCap[0]) + boatCap.Substring(1);  //capitalises the first letter and replaces all the underscores with spaces
                string tempString = string.Empty;

                //iterate through the string
                //if the char before the current one is a space, make the current char a capital e.g. Prinz eugen to Prinz Eugen
                for (int i = 0; i < boatCap.Length; i++)
                {
                    if (i == 0)
                        tempString += boatCap[i];   //always print the first letter before checking so don't go out of the array
                    else if (boatCap[i - 1] == ' ')
                        tempString += char.ToUpper(boatCap[i]);
                    else
                        tempString += boatCap[i];
                }
                return tempString;
            }
            return boat;

        }

    }
}
