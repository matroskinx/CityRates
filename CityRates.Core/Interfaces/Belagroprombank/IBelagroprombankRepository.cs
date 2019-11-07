using CityRates.Core.Domain;
using System.Collections.Generic;

namespace CityRates.Core.Interfaces.Belagroprombank
{
    public interface IBelagroprombankRepository
    {
        BelagroprombankDomain UpdateBelagroprombankInfo();
        BelagroprombankDomain GetBelagroprombankInfo();
    }
}
