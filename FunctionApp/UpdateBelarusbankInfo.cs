using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CityRates.Core.Domain;
using CityRates.Core.Services;
using CityRates.Infrastructure.Repositories;

namespace FunctionApp
{
    public static class UpdateBelarusbankInfo
    {
        [FunctionName("UpdateBelarusbankInfo")]
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

            var belarusbankRepository = new BelarusbankRepository(connectionOptions);
            var belarusbankService = new BelarusbankService(belarusbankRepository);
            var result = belarusbankService.UpdateBelarusbankInfo();

            return new OkObjectResult(result);
        }
    }
}
