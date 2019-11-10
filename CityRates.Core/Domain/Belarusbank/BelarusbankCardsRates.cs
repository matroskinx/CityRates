using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityRates.Core.Domain.Belarusbank
{
    public class BelarusbankCardsRates
    {
        [JsonProperty("USDCARD_in")]
        public double USDCARD_in { get; set; }

        [JsonProperty("USDCARD_out")]
        public double USDCARD_out { get; set; }

        [JsonProperty("EURCARD_in")]
        public double EURCARD_in { get; set; }

        [JsonProperty("EURCARD_out")]
        public double EURCARD_out { get; set; }

        [JsonProperty("RUBCARD_in")]
        public double RUBCARD_in { get; set; }

        [JsonProperty("RUBCARD_out")]
        public double RUBCARD_out { get; set; }
    }
}
