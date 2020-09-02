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
using System.Text;
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
           
            string normChibi = "chibi.png";
            boat = boatNickname(boat);
            string boatCap = boat;  //to capitalise the name of the boat
            string shipCalled = boat + ".json"; //file name for boat called
            

            if (boat.Length >= 1)   //if the passed string doesn't work, bot will return it at the start of a sentence so make the first letter a capital
            {
                boatCap = boat.Replace("_", " ");
                boatCap = char.ToUpper(boatCap[0]) + boatCap.Substring(1);


                string tempString = string.Empty;
                //iterate through the string
                //if the char before the current one is a space, make the current char a capital

                for (int i = 0; i < boatCap.Length; i++)    //start at 1 so we never replace the first letter
                {
                    if (i == 0)
                    {
                      
                        tempString += boatCap[i];
                     

                    }
                    else if (boatCap[i - 1] == ' ')
                    {
                      
                        tempString += char.ToUpper(boatCap[i]);
                      


                    }
                    else
                    {
                      
                        tempString += boatCap[i];
                      
                    }


                   
                }
                boatCap = tempString;
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

                string boatName = configShip.Name;
                string Name = "Name: " + configShip.faction + " " + boatCap + "\n";
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
           
            boat = boatNickname(boat);
            string normChibi = "chibi.png";
            string boatCap = boat;
            string shipCalled = boat + ".json"; //file name for boat called

            if (boat.Length >= 1)
            {
                boatCap = boat.Replace("_", " ");
                boatCap = char.ToUpper(boatCap[0]) + boatCap.Substring(1);

                string tempString = string.Empty;
                for (int i = 0; i < boatCap.Length; i++)    //start at 1 so we never replace the first letter
                {
                    if (i == 0)
                    {
                        tempString += boatCap[i];
                    }
                    else if (boatCap[i - 1] == ' ')
                    {
                        tempString += char.ToUpper(boatCap[i]);
                    }
                    else
                    {
                        tempString += boatCap[i];
                    }
                }
                boatCap = tempString;
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
                string Name = "Name: " + configShip.faction + " " + boatCap + "\n";

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


        public string boatNickname(string boatInput)
        {
            string fixedName;
            if (boatInput == "spee" || boatInput == "graf_spee" || boatInput == "grafspee")
            {
                fixedName = "admiral_graf_spee";
                return fixedName;
            }
            else if (boatInput == "hipper")
            {
                fixedName = "admiral_hipper";
                return fixedName;
            }
            else if (boatInput == "hipper_muse" || boatInput == "hippermuse" || boatInput == "admiral_hipper_muse" || boatInput == "admiralhippermuse")
            {
                fixedName = "admiral_hipper_idol";
                return fixedName;
            }
            else if (boatInput == "akagi_muse" || boatInput == "akagimuse")
            {
                fixedName = "akagi_idol";
                return fixedName;
            }
            else if (boatInput == "anshan" )
            {
                fixedName = "an_shan";
                return fixedName;
            }
            else if (boatInput == "lolicon" || boatInput == "arkroyal")
            {
                fixedName = "ark_royal";
                return fixedName;
            }
            else if (boatInput == "balti")
            {
                fixedName = "baltimore";
                return fixedName;
            }
            else if (boatInput == "thotloli")
            {
                fixedName = "bache";
                return fixedName;
            }
            else if (boatInput == "blackprince")
            {
                fixedName = "black_prince";
                return fixedName;
            }
            else if (boatInput == "brem")
            {
                fixedName = "bremerton";
                return fixedName;
            }
            else if (boatInput == "bunkerhill")
            {
                fixedName = "bunker_hill";
                return fixedName;
            }
            else if (boatInput == "changchun")
            {
                fixedName = "chang_chun";
                return fixedName;
            }
            else if (boatInput == "charles" || boatInput == "charlesausburne" )
            {
                fixedName = "charles_ausburne";
                return fixedName;
            }
            else if (boatInput == "clevelandmuse" || boatInput=="cleveland_muse")
            {
                fixedName = "cleveland_idol";
                return fixedName;
            }
            else if (boatInput == "conte" || boatInput == " cavour" || boatInput == "contedicavour")
            {
                fixedName = "conti_di_cavour";
                return fixedName;
            }
            else if (boatInput == "dorset")
            {
                fixedName = "dorsetshire";
                return fixedName;
            }
            else if (boatInput == "doy" || boatInput == "dork")
            {
                fixedName = "duke_of_york";
                return fixedName;
            }
            else if (boatInput == "emily" || boatInput == "emile" || boatInput == "emilebertin")
            {
                fixedName = "emile_bertin";
                return fixedName;
            }
            else if (boatInput == "enty")
            {
                fixedName = "enterprise";
                return fixedName;
            }
            else if (boatInput == "fushun" )
            {
                fixedName = "fu_shun";
                return fixedName;
            }
            else if (boatInput == "gascogne_muse" || boatInput == "gascognemuse")
            {
                fixedName = "gascogne_idol";
                return fixedName;
            }
            else if (boatInput == "giuliocesare" || boatInput == "giulio" || boatInput == "cesare")
            {
                fixedName = "giulio_cesare";
                return fixedName;
            }
            else if (boatInput == "zeppelin" )
            {
                fixedName = "graf_zeppelin";
                return fixedName;
            }
            else if (boatInput == "halsey" || boatInput == "halseypowell")
            {
                fixedName = "halsey_powell";
                return fixedName;
            }
            else if (boatInput == "hiei_chan")
            {
                fixedName = "hieichan";
                return fixedName;
            }
            else if (boatInput == "i-13")
            {
                fixedName = "i13";
                return fixedName;
            }
            else if (boatInput == "i-19")
            {
                fixedName = "i19";
                return fixedName;
            }
            else if (boatInput == "i-25")
            {
                fixedName = "i25";
                return fixedName;
            }
            else if (boatInput == "i-26")
            {
                fixedName = "i26";
                return fixedName;
            }
            else if (boatInput == "i-56")
            {
                fixedName = "i56";
                return fixedName;
            }
            else if (boatInput == "i-58")
            {
                fixedName = "i58";
                return fixedName;
            }
            else if (boatInput == "i-168")
            {
                fixedName = "i168";
                return fixedName;
            }
            else if (boatInput == "lusty")
            {
                fixedName = "illustrious";
                return fixedName;
            }
            else if (boatInput == "indy")
            {
                fixedName = "indianapolis";
                return fixedName;
            }
            else if (boatInput == "jean" || boatInput == "jeanbart")
            {
                fixedName = "jean_bart";
                return fixedName;
            }
            else if (boatInput == "jeanne" || boatInput == "jeannedarc")
            {
                fixedName = "jeanne_d_arc";
                return fixedName;
            }
            else if (boatInput == "kaga_battleship")
            {
                fixedName = "kaga_bb";
                return fixedName;
            }
            else if (boatInput == "kgv" || boatInput == "george")
            {
                fixedName = "king_george_v";
                return fixedName;
            }
            else if (boatInput == "lopiniatre")
            {
                fixedName = "l_opiniatre";
                return fixedName;
            }
            else if (boatInput == "lagalissonniere" || boatInput == "galissonniere")
            {
                fixedName = "la_galissonniere";
                return fixedName;
            }
            else if (boatInput == "malin" || boatInput == "lemalin")
            {
                fixedName = "le_malin";
                return fixedName;
            }
            else if (boatInput == "mars" || boatInput=="lemars" )
            {
                fixedName = "le_mars";
                return fixedName;
            }
            else if (boatInput == "temeraire" || boatInput == "letemeraire")
            {
                fixedName = "le_temeraire";
                return fixedName;
            }
            else if (boatInput == "triomphant" || boatInput == "letriomphant")
            {
                fixedName = "le_triomphant";
                return fixedName;
            }
            else if (boatInput == "lil_sandy" || boatInput == "lilsandy")
            {
                fixedName = "little_san_diego";
                return fixedName;
            }
            else if (boatInput == "little_bel" || boatInput == "littlebel")
            {
                fixedName = "belchan";
                return fixedName;
            }
            else if (boatInput == "lil_illustrious" || boatInput == "littleillustrious")
            {
                fixedName = "little_illustrious";
                return fixedName;
            }
            else if (boatInput == "longisland")
            {
                fixedName = "long_island";
                return fixedName;
            }
            else if (boatInput == "ninghai" || boatInput == "ning" )
            {
                fixedName = "ning_hai";
                return fixedName;
            }
            else if (boatInput == "northcarolina" || boatInput == "carolina" )
            {
                fixedName = "north_carolina";
                return fixedName;
            }
            else if (boatInput == "pamiat" || boatInput == "merkuria" || boatInput == "pamiatmerkuria")
            {
                fixedName = "pamiat_merkuria";
                return fixedName;
            }
            else if (boatInput == "ping" || boatInput == "pinghai")
            {
                fixedName = "ping_hai";
                return fixedName;
            }
            else if (boatInput == "pow" || boatInput == "princeofwales")
            {
                fixedName = "prince_of_wales";
                return fixedName;
            }
            else if (boatInput == "prinz" || boatInput == "eugen" || boatInput == "prinzeugen")
            {
                fixedName = "prinz_eugen";
                return fixedName;
            }
            else if (boatInput == "qe" || boatInput == "elizabeth" || boatInput =="queenelizabeth")
            {
                fixedName = "queen_elizabeth";
                return fixedName;
            }
            else if (boatInput == "slc" || boatInput == "salt" || boatInput == "saltlakecity")
            {
                fixedName = "salt_lake_city";
                return fixedName;
            }
            else if (boatInput == "sandy" || boatInput == "sandiego")
            {
                fixedName = "san_diego";
                return fixedName;
            }
            else if (boatInput == "sanjuan" || boatInput == "juan")
            {
                fixedName = "san_juan";
                return fixedName;
            }
            else if (boatInput == "shangrila" || boatInput=="shangri")
            {
                fixedName = "shangri-la";
                return fixedName;
            }
            else if (boatInput == "sheffield_muse" || boatInput == "sheffieldmuse")
            {
                fixedName = "sheffield_idol";
                return fixedName;
            }
            else if (boatInput == "southdakota" || boatInput == "dakota")
            {
                fixedName = "south_dakota";
                return fixedName;
            }
            else if (boatInput == "rossiya" || boatInput == "sovetskaya" || boatInput == "sovetskayarossiya")
            {
                fixedName = "sovetskaya_rossiya";
                return fixedName;
            }
            else if (boatInput == "stlouis")
            {
                fixedName = "st_louis";
                return fixedName;
            }
            else if (boatInput == "taiyuan")
            {
                fixedName = "tai_yuan";
                return fixedName;
            }
            else if (boatInput ==  "u-47")
            {
                fixedName = "u47";
                return fixedName;
            }
            else if (boatInput == "u-73")
            {
                fixedName = "u73";
                return fixedName;
            }
            else if (boatInput == "u-81")
            {
                fixedName = "u81";
                return fixedName;
            }
            else if (boatInput == "u-96")
            {
                fixedName = "u96";
                return fixedName;
            }
            else if (boatInput == "u-101")
            {
                fixedName = "u101";
                return fixedName;
            }
            else if (boatInput == "u-110")
            {
                fixedName = "u110";
                return fixedName;
            }
            else if (boatInput =="u-522" )
            {
                fixedName = "u522";
                return fixedName;
            }
            else if (boatInput == "u-556")
            {
                fixedName = "u556";
                return fixedName;
            }
            else if (boatInput =="u-557")
            {
                fixedName = "u557";
                return fixedName;
            }
            else if (boatInput == "vittorio" || boatInput == "veneto" || boatInput == "vittorioveneto")
            {
                fixedName = "vittorio_veneto";
                return fixedName;
            }
            else if (boatInput == "westvirginia" || boatInput == "virginia")
            {
                fixedName = "west_virginia";
                return fixedName;
            }
            else if (boatInput == "yatsen")
            {
                fixedName = "yat_sen";
                return fixedName;
            }
            else if (boatInput == "nimi")
            {
                fixedName = "z23";
                return fixedName;

            }
            else if (boatInput == "zeppy")
            {
                fixedName = "zeppelinchan";
                return fixedName;
            }
            else if (boatInput == "saintlouis" || boatInput == "sanrui")
            {
                fixedName = "saint_louis";
                return fixedName;
            }
            else if (boatInput == "fdg" ||boatInput == "germanmommy")
            {
                fixedName = "friedrich_der_grosse";
                return fixedName;
            }
        



            return boatInput;
        }
    }
}
