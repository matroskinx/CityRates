using CityRates.Core.Domain;
using System.Collections.Generic;

namespace CityRates.Core.Interfaces.Belagroprombank
{
    public interface IBelagroprombankService
    {
        List<GlobalDepartment> GetDepartmentsWithRates();
    }
}
