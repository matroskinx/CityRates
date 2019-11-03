using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CityRates.Core.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CurrencyType
    {
        BYN, USD, EUR, RUB,
        AUD, BGN, CAD, CZK,
        DKK, UAH, ISK, IRR,
        KWD, MDL, NZD, NOK,
        GBP, XDR, SGD, KGS,
        SEK, CHF, KZT, TRY,
        JPY, CNY, PLN
    }
}
