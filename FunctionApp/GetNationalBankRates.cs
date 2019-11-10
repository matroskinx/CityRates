using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CityRates.Infrastructure.Repositories;
using CityRates.Core.Services;

namespace FunctionApp
{
    public static class GetNationalBankRates
    {
        [FunctionName("GetNationalBankRates")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var nationalBankRepository = new NationalBankRepository();
            var nationalBankService = new NationalBankService(nationalBankRepository);
            var result = nationalBankService.GetNBRates();

            return new OkObjectResult(result);
        }
    }
}
