using System;
using System.Collections.Generic;
using System.Text;
using CityRates.Core.Domain;
using CityRates.Core.Interfaces.Belagroprombank;
using CityRates.Core.Interfaces.Belarusbank;

namespace CityRates.Core.Services
{
    public class BelarusbankService : IBelarusbankService
    {
        private IBelarusbankRepository _belarusbankRepository;

        public BelarusbankService(IBelarusbankRepository belarusbankRepository)
        {
            _belarusbankRepository = belarusbankRepository;
        }

        public BelarusbankDomain GetBelarusbankInfo()
        {
            return _belarusbankRepository.GetBelarusbankInfo();
        }

        public IEnumerable<GlobalDepartment> GetFavoriteDepartments(List<int> favoriteDepartmens)
        {
            return _belarusbankRepository.GetFavoriteDepartments(favoriteDepartmens);
        }

        public List<GlobalCurrency> GetGlobalCurrencies()
        {
            return _belarusbankRepository.GetGlobalCurrencies();
        }

        public BelarusbankDomain UpdateBelarusbankInfo()
        {
            return _belarusbankRepository.UpdateBelarusbankInfo();
        }
    }
}
