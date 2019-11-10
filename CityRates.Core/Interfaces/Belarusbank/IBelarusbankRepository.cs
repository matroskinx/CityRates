using CityRates.Core.Domain;
using System.Collections.Generic;

namespace CityRates.Core.Interfaces.Belarusbank
{
    public interface IBelarusbankRepository
    {
        BelarusbankDomain UpdateBelarusbankInfo();
        BelarusbankDomain GetBelarusbankInfo();
        List<GlobalCurrency> GetGlobalCurrencies();
        IEnumerable<GlobalDepartment> GetFavoriteDepartments(List<int> favoriteDepartmens);
    }
}
