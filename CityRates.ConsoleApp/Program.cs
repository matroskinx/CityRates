using System;
using CityRates.Core.Domain;
using CityRates.Core.Services;
using CityRates.Infrastructure.Repositories;

namespace CityRates.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionOptions = new ConnectionOptions(
                "https://city-rates.documents.azure.com:443/",
                "PERNkHuRBu1W9e9oeIznbqZZ6PUDg9OOxp31pIxRfc0gw52p5GRvPo0bToNGTtoN5CQgGPC5Y3b2nDfIyMnMJg==",
                "CityRatesDB",
                "BelagroprombankCollection"
            );

            //var belagroprombankRepo = new BelagroprombankRepository(connectionOptions);
            //var belagroprombankService = new BelagroprombankService(belagroprombankRepo);
            //belagroprombankService.UpdateBelagroprombankInfo();

            var belRepo = new BelarusbankRepository(connectionOptions);
            belRepo.UpdateBelarusbankInfo();
            //var connectionOptions = new ConnectionOptions(
            //    "https://city-rates.documents.azure.com:443/",
            //    "PERNkHuRBu1W9e9oeIznbqZZ6PUDg9OOxp31pIxRfc0gw52p5GRvPo0bToNGTtoN5CQgGPC5Y3b2nDfIyMnMJg==",
            //    "CityRatesDB",
            //    "BelarusbankCollection"
            //);

            //var belarusbankRepository = new BelarusbankRepository(connectionOptions);
            //var belarusbankService = new BelarusbankService(belarusbankRepository);
            //var result = belarusbankService.UpdateBelarusbankInfo();

        }
    }
}
