using CityRates.Core.Enums;

namespace CityRates.Core.Domain
{
    public class GlobalCurrency
    {
        public int FilialId { get; set; }

        public CurrencyType FromCurrency { get; set; }

        public CurrencyType ToCurrency { get; set; }

        public double BankSellsAt { get; set; }

        public double BankBuysAt { get; set; }

        public BankType BankType { get; set; }

        public GlobalCurrency(
            CurrencyType fromCurrency,
            CurrencyType toCurrency,
            double bankSellsAt,
            double bankBuysAt,
            BankType bankType,
            int filialId = 0
        )
        {
            FilialId = filialId;
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;
            bankSellsAt = BankSellsAt;
            bankBuysAt = BankBuysAt;
            BankType = bankType;
        }

        public GlobalCurrency() { }
    }
}
