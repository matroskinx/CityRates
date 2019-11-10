using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CityRates.Core.Domain;
using CityRates.Infrastructure.Repositories;
using CityRates.Core.Services;

namespace FunctionApp
{
    public static class GetGlobalCurrenciesBelarusbank
    {
        [FunctionName("GetGlobalCurrenciesBelarusbank")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var connectionOptions = new ConnectionOptions(
                "https://city-rates.documents.azure.com:443/",
                "PERNkHuRBu1W9e9oeIznbqZZ6PUDg9OOxp31pIxRfc0gw52p5GRvPo0bToNGTtoN5CQgGPC5Y3b2nDfIyMnMJg==",
                "CityRatesDB",
                "BelarusbankCollection"
            );

            var belarusbankRepo = new BelarusbankRepository(connectionOptions);
            var belarusbankService = new BelarusbankService(belarusbankRepo);
            var belarusbank = belarusbankService.GetGlobalCurrencies();

            return new OkObjectResult(belarusbank);
        }
    }
}
