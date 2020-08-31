using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AzureLightDiscordBot
{
    public struct ConfigJSON
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public string Prefix { get; private set; }

    }
    public struct ConfigShip
    { 

        [JsonProperty("name_reference")]
        public string Name { get; private set; }

        [JsonProperty("rarity")]
        public string Rarity { get; private set; }

        [JsonProperty("buildTime")]
        public string Buildtime { get; private set; }

        [JsonProperty("class")]
        public string ShipClass { get; private set; }

        [JsonProperty("voiceActress")]
        public string VoiceActress { get; private set; }

        [JsonProperty("acquisitionMethod")]
        public string AcquisitionMethod { get; private set; }

        [JsonProperty("hull")]
        public string type { get; private set; }


        [JsonProperty("prefix")]
        public string faction { get; private set; }




    }
}
