using CityRates.Core.Domain.NationalBank;
using System.Collections.Generic;

namespace CityRates.Core.Interfaces.Nationalbank
{
    public interface INationalBankRepository
    {
        List<NationalBankDomain> GetNBRates();
    }
}
