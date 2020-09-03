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
    public class BoatNicknames
    {
        public string[][] nicknames = new string[78][];

        public BoatNicknames()
        {
            nicknames[0] = new string[] { "admiral_graf_spee","spee", "graf_spee", "grafspee" };
            nicknames[1] = new string[] { "admiral_hipper", "hipper" };
            nicknames[2] = new string[] { "admiral_hipper_idol", "hipper_muse", "hippermuse", "admiral_hipper_muse", "admiralhippermuse" };
            nicknames[3] = new string[] { "akagi_idol", "akagi_muse", "akagimuse" };
            nicknames[4] = new string[] { "an_shan", "anshan" };
            nicknames[5] = new string[] { "ark_royal", "lolicon", "arkroyal" };
            nicknames[6] = new string[] { "baltimore", "balti" };
            nicknames[7] = new string[] { "bache", "thotloli" };
            nicknames[8] = new string[] { "black_prince", "blackprince" };
            nicknames[9] = new string[] { "bremerton", "brem" };
            nicknames[10] = new string[] { "bunker_hill", "bunkerhill" };
            nicknames[11] = new string[] { "chang_chun", "changchun" };
            nicknames[12] = new string[] { "charles_ausburne", "charles", "charlesausburne" };
            nicknames[13] = new string[] { "cleveland_idol", "clevelandmuse", "cleveland_muse" };
            nicknames[14] = new string[] { "conti_di_cavour", "conte", "cavour", "contedicavour" };
            nicknames[15] = new string[] { "dorsetshire", "dorset" };
            nicknames[16] = new string[] { "duke_of_york", "doy", "dork" };
            nicknames[17] = new string[] { "emile_bertin", "emile", "emily", "emilebertin" };
            nicknames[18] = new string[] { "enterprise", "enty" };
            nicknames[19] = new string[] { "fu_shun", "fushun" };
            nicknames[20] = new string[] { "gascogne_idol", "gascogne_muse", "gascognemuse" };
            nicknames[21] = new string[] { "giulio_cesare", "giuliocesare", "giulio", "cesare" };
            nicknames[22] = new string[] { "graf_zeppelin", "grafzeppelin", "zeppelin" };
            nicknames[23] = new string[] { "hasley_powell", "hasley", "powell", "hasleypowell" };
            nicknames[24] = new string[] { "hieichan", "hiei_chan" };
            nicknames[25] = new string[] { "i13", "i-13" };
            nicknames[26] = new string[] { "i19", "i-19" };
            nicknames[27] = new string[] { "i25", "i-25" };
            nicknames[28] = new string[] { "i26", "i-26" };
            nicknames[29] = new string[] { "i58", "i-58" };
            nicknames[30] = new string[] { "i168", "i-168" };
            nicknames[31] = new string[] { "indianapolis", "indy" };
            nicknames[32] = new string[] { "jean_bart", "jean", "jeanbart" };
            nicknames[33] = new string[] { "jeanne_d_arc", "jeanne", "jeannedarc", "saber" };
            nicknames[34] = new string[] {"kaga_bb", "kagabb", "kaga_battleship" };
            nicknames[35] = new string[] { "king_george_v", "kgv", "kinggeorgev" };
            nicknames[36] = new string[] { "opiniatre", "lopiniatre", "lop" };
            nicknames[37] = new string[] { "l_opiniatre", "lagalissonniere", "galissonniere", "gal" };
            nicknames[38] = new string[] { "le_malin", "lemalin", "malin" };
            nicknames[39] = new string[] { "le_mars", "lemars", "mars" };
            nicknames[40] = new string[] { "le_temeraire", "letemeraire", "temeraire", "tem" };
            nicknames[41] = new string[] { "le_triomphant", "letriomphant", "triomphant" };
            nicknames[42] = new string[] { "little_san_diego", "lil_sandy", "lilsandy", "li'lsandy", "li'l_sandy" };
            nicknames[43] = new string[] { "belchan", "lil_bel", "lilbel", "li'lbel", "li'l_bel" };
            nicknames[44] = new string[] { "little_illustrious", "lil_illustrious", "lilillustrious", "li'l_illustrious", "li'lillustrious" };
            nicknames[45] = new string[] { "long_island", "longisland" };
            nicknames[46] = new string[] { "ning_hai", "ning", "ninghai" };
            nicknames[47] = new string[] { "north_carolina", "northcarolina", "carolina" };
            nicknames[48] = new string[] { "pamiat_merkuria", "pamiat", "merkuria", "pamiat_merkuria" };
            nicknames[49] = new string[] { "ping_hai", "ping", "ping_hai" };
            nicknames[50] = new string[] { "prince_of_wales", "pow", "princeofwales" };
            nicknames[51] = new string[] { "prinz_eugen", "prinz", "eugen", "prinzeugen" };
            nicknames[52] = new string[] { "queen_elizabeth", "qe", "elizabeth", "queenelizabeth" };
            nicknames[53] = new string[] { "salt_lake_city", "slc", "salt", "saltlakecity" };
            nicknames[54] = new string[] { "san_deigo", "sandy", "sandeigo" };
            nicknames[55] = new string[] { "san_juan", "sanjuan", "juan" };
            nicknames[56] = new string[] { "shangri_la", "shangrila", "shangri", "grafspee" };
            nicknames[57] = new string[] { "sheffield_idol", "sheffield_muse", "sheffieldmuse" };
            nicknames[58] = new string[] { "south_dakota", "southdakota", "dakota" };
            nicknames[59] = new string[] { "sovetskaya_rossiya", "sovetskaya", "rossiya", "sovetskayarossiya" };
            nicknames[60] = new string[] { "st_louis", "stlouis" };
            nicknames[61] = new string[] { "tai_yuan", "taiyuan" };
            nicknames[62] = new string[] { "u47", "u-47" };
            nicknames[63] = new string[] { "u73", "u-73" };
            nicknames[64] = new string[] { "u81", "u-81" };
            nicknames[65] = new string[] { "u96", "u-96" };
            nicknames[66] = new string[] { "u101", "u-101" };
            nicknames[67] = new string[] { "u110", "u-110" };
            nicknames[68] = new string[] { "u522", "u-522" };
            nicknames[69] = new string[] { "u556", "u-556" };
            nicknames[70] = new string[] { "u557", "u-557" };
            nicknames[71] = new string[] { "vittorio_veneto", "vittorio", "veneto", "vittorioveneto" };
            nicknames[72] = new string[] { "west_virginia", "westvirginia", "virginia" };
            nicknames[73] = new string[] { "yat_sen", "yatsen" };
            nicknames[74] = new string[] { "z23", "nimi" };
            nicknames[75] = new string[] { "zeppelinchan", "zeppy" };
            nicknames[76] = new string[] { "saint_louis", "saintlouis", "sanrui" };
            nicknames[77] = new string[] { "friedrich_der_grosse", "fdg", "germanmommy" };


        }








    }
}
