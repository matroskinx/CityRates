using CityRates.Core.Domain;
using System.Collections.Generic;

namespace CityRates.Core.Interfaces.Belagroprombank
{
    public interface IBelagroprombankService
    {
        BelagroprombankDomain UpdateBelagroprombankInfo();
        BelagroprombankDomain GetBelagroprombankInfo();
        List<GlobalCurrency> GetGlobalCurrencies();
        IEnumerable<GlobalDepartment> GetFavoriteDepartments(List<int> favoriteDepartmens);
    }
}
