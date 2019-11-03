using System;
using CityRates.Core.Services;
using CityRates.Infrastructure.Repositories;

namespace CityRates.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var belarusbankRepo = new BelarusbankRepository();
            var belarusbankService = new BelarusbankService(belarusbankRepo);
            belarusbankService.GetDepartmentsWithRates();

            var belagroprombankRepo = new BelagroprombankRepository();
            var belagroprombankService = new BelagroprombankService(belagroprombankRepo);
            belagroprombankService.GetDepartmentsWithRates();

        }
    }
}
