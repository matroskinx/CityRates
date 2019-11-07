using System.Threading.Tasks;
using CityRates.Core.Services;
using CityRates.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CityRates.Core.Domain;

namespace FunctionApp
{
    public static class GetBelagroprombankInfo
    {
        [FunctionName("GetBelagroprombankInfo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            var connectionOptions = new ConnectionOptions(
                "https://city-rates.documents.azure.com:443/",
                "PERNkHuRBu1W9e9oeIznbqZZ6PUDg9OOxp31pIxRfc0gw52p5GRvPo0bToNGTtoN5CQgGPC5Y3b2nDfIyMnMJg==",
                "CityRatesDB",
                "BelagroprombankCollection"
            );

            var belagroprombankRepo = new BelagroprombankRepository(connectionOptions);
            var belagroprombankService = new BelagroprombankService(belagroprombankRepo);
            var result = belagroprombankService.GetBelagroprombankInfo();

            return new OkObjectResult(result);
        }
    }
}
