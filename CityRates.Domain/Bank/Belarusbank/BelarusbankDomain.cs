using Newtonsoft.Json;

namespace CityRates.Domain.Bank.Belarusbank
{
    public class BelarusbankDomain
    {
        [JsonProperty("filial_id")]
        public int FilialId { get; set; }
        [JsonProperty("sap_id")]
        public string SapId { get; set; }
        [JsonProperty("filial_name")]
        public string FilialName { get; set; }
        [JsonProperty("info_text")]
        public string InfoText { get; set; }
        [JsonProperty("info_worktime")]
        public string InfoWorktime { get; set; }
        [JsonProperty("GPS_X")]
        public string GpsX { get; set; }
        [JsonProperty("GPS_Y")]
        public string GpsY { get; set; }
        [JsonProperty("phone_info")]
        public string PhoneInfo { get; set; }
        [JsonProperty("filial_num")]
        public string FilialNum { get; set; }
        [JsonProperty("cbu_num")]
        public string CbuNum { get; set; }
        [JsonProperty("otd_num")]
        public string OtdNum { get; set; }
    }
}