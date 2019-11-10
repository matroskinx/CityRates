﻿using CityRates.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityRates.Core.Domain
{
    public class GlobalDepartment
    {
        public int Id { get; set; }

        public BankType BankType { get; set; }

        public string Address { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public List<GlobalCurrency> Currencies { get; set; } = new List<GlobalCurrency>();

        // TODO: get work hours
    }
}
