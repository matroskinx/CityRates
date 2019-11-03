using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CityRates.Core.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BankType
    {
        Belagroprombank,
        Belarusbank
    }
}
