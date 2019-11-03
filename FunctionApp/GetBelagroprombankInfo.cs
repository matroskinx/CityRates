using System.Threading.Tasks;
using CityRates.Core.Services;
using CityRates.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public static class GetBelagroprombankInfo
    {
        [FunctionName("GetBelagroprombankInfo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            var belagroprombankRepo = new BelagroprombankRepository();
            var belagroprombankService = new BelagroprombankService(belagroprombankRepo);
            var belagroprombank = belagroprombankService.GetDepartmentsWithRates();

            return new OkObjectResult(belagroprombank);
        }
    }
}
