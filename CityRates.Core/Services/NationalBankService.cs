using System;
using System.Collections.Generic;
using CityRates.Core.Domain.NationalBank;
using CityRates.Core.Interfaces.Nationalbank;

namespace CityRates.Core.Services
{
    public class NationalBankService : INationalBankService
    {
        private INationalBankRepository _nationalBankRepository;

        public NationalBankService(INationalBankRepository nationalBankRepository)
        {
            _nationalBankRepository = nationalBankRepository;
        }

        public List<NationalBankDomain> GetNBRates()
        {
            return _nationalBankRepository.GetNBRates();
        }
    }
}

