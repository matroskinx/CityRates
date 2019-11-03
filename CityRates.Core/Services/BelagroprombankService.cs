using System.Collections.Generic;
using CityRates.Core.Domain;
using CityRates.Core.Interfaces.Belagroprombank;

namespace CityRates.Core.Services
{
    public class BelagroprombankService : IBelagroprombankRepository
    {
        private IBelagroprombankRepository _belagroprombankRepository;

        public BelagroprombankService(IBelagroprombankRepository belagroprombankRepository)
        {
            _belagroprombankRepository = belagroprombankRepository;
        }
        public List<GlobalDepartment> GetDepartmentsWithRates()
        {
            return _belagroprombankRepository.GetDepartmentsWithRates();
        }
    }
}
