using Newtonsoft.Json;

namespace CityRates.Core.Domain.NationalBank
{
    public class NationalBank
    {
        [JsonProperty("Cur_ID")]
        public int Cur_ID { get; set; }

        [JsonProperty("Cur_Abbreviation")]
        public string Cur_Abbreviation { get; set; }

        [JsonProperty("Cur_OfficialRate")] 
        public double Cur_OfficialRate { get; set; }
    }
}
