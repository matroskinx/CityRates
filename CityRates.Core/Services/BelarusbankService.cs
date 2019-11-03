using System.Collections.Generic;
using CityRates.Core.Domain;
using CityRates.Core.Interfaces.Belagroprombank;
using CityRates.Core.Interfaces.Belarusbank;

namespace CityRates.Core.Services
{
    public class BelarusbankService : IBelagroprombankService
    {
        private IBelarusbankRepository _belarusbankRepository;

        public BelarusbankService(IBelarusbankRepository belarusbankRepository)
        {
            _belarusbankRepository = belarusbankRepository;
        }
        public List<GlobalDepartment> GetDepartmentsWithRates()
        {
            return _belarusbankRepository.GetDepartmentsWithRates();
        }
    }
}
