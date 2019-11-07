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

        public BelagroprombankDomain UpdateBelagroprombankInfo()
        {
            return _belagroprombankRepository.UpdateBelagroprombankInfo();
        }
    }
}
