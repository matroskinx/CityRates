using System.Collections.Generic;
using CityRates.Core.Domain;
using CityRates.Core.Interfaces.Belagroprombank;

namespace CityRates.Core.Services
{
    public class BelagroprombankService : IBelagroprombankService
    {
        private IBelagroprombankRepository _belagroprombankRepository;

        public BelagroprombankService(IBelagroprombankRepository belagroprombankRepository)
        {
            _belagroprombankRepository = belagroprombankRepository;
        }

        public BelagroprombankDomain GetBelagroprombankInfo()
        {
            return _belagroprombankRepository.GetBelagroprombankInfo();
        }

        public IEnumerable<GlobalDepartment> GetFavoriteDepartments(List<int> favoriteDepartmens)
        {
            return _belagroprombankRepository.GetFavoriteDepartments(favoriteDepartmens);
        }

        public List<GlobalCurrency> GetGlobalCurrencies()
        {
            return _belagroprombankRepository.GetGlobalCurrencies();
        }

        public BelagroprombankDomain UpdateBelagroprombankInfo()
        {
            return _belagroprombankRepository.UpdateBelagroprombankInfo();
        }
    }
}
