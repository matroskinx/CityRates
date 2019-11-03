namespace CityRates.Domain.Bank
{
    public class GlobalCurrencyDomain
    {
        public CurrencyType FromCurrency { get; set; }

        public CurrencyType ToCurrency { get; set; }

        public double BankSellsAt { get; set; }

        public double BankBuysAt { get; set; }

        public int FilialId { get; set; }

        public BankType BankType { get; set; }

        public GlobalCurrencyDomain(CurrencyType fromCurrency, CurrencyType toCurrency, double sellsAt, double buysAt, int filialId, BankType bankType)
        {
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;
            BankSellsAt = sellsAt;
            BankBuysAt = buysAt;
            FilialId = filialId;
            BankType = bankType;
        }

    }
}
