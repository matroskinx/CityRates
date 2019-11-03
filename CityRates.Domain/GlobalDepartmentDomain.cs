using System.Collections.Generic;
using CityRates.Domain.Bank;

namespace CityRates.Domain
{
    public class GlobalDepartmentDomain
    {
        public List<GlobalCurrencyDomain> Currencies { get; set; } = new List<GlobalCurrencyDomain>();

        public BankType BankType { get; set; }

        public float Lat { get; set; }

        public float Lng { get; set; }

        // todo get work hours
    }
}
