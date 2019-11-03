using CityRates.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityRates.Core.Domain
{
    public class GlobalDepartment
    {
        public BankType BankType { get; set; }

        public float Lat { get; set; }

        public float Lng { get; set; }

        public List<GlobalCurrency> Currencies { get; set; } = new List<GlobalCurrency>();

        // TODO: get work hours
    }
}
