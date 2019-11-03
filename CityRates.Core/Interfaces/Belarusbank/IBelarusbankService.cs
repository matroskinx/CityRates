using CityRates.Core.Domain;
using System.Collections.Generic;

namespace CityRates.Core.Interfaces.Belarusbank
{
    public interface IBelarusbankService
    {
        List<GlobalDepartment> GetDepartmentsWithRates();
    }
}
