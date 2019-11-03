using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CityRates.Infrastructure.Repositories;
using CityRates.Core.Services;

namespace FunctionApp
{
    public static class GetBelarusbankInfo
    {
        
        [FunctionName("GetBelarusbankInfo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            
            var belarusbankRepo = new BelarusbankRepository();
            var belarusbankService = new BelarusbankService(belarusbankRepo);
            var belarusbank = belarusbankService.GetDepartmentsWithRates();

            return new OkObjectResult(belarusbank);
        }
    }
}
