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

            var belagroprombankRepo = new BelagroprombankRepository(connectionOptions);
            belagroprombankRepo.GetGlobalCurrencies();
            //var belagroprombankService = new BelagroprombankService(belagroprombankRepo);
            //belagroprombankService.UpdateBelagroprombankInfo();

            //var connectionOptions = new ConnectionOptions(
            //    "https://city-rates.documents.azure.com:443/",
            //    "PERNkHuRBu1W9e9oeIznbqZZ6PUDg9OOxp31pIxRfc0gw52p5GRvPo0bToNGTtoN5CQgGPC5Y3b2nDfIyMnMJg==",
            //    "CityRatesDB",
            //    "BelarusbankCollection"
            //);

            //var belarusbankRepository = new BelarusbankRepository(connectionOptions);
            //belarusbankRepository.GetGlobalCurrencies();

            //var belarusbankService = new BelarusbankService(belarusbankRepository);
            //var result = belarusbankService.UpdateBelarusbankInfo();

            //var nationalBankRepository = new NationalBankRepository();
            //var nationalBankService = new NationalBankService(nationalBankRepository);
            //nationalBankService.GetNBRates();


        }
    }
}
