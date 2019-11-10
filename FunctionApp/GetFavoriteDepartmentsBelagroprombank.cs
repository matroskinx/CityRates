using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using CityRates.Core.Domain;
using CityRates.Core.Services;
using CityRates.Infrastructure.Repositories;

namespace FunctionApp
{
    public static class GetFavoriteDepartmentsBelagroprombank
    {
        [FunctionName("GetFavoriteDepartmentsBelagroprombank")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            string belagroprombank = "";

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            List<int> belagropromankDepartmentsIdList = JsonConvert.DeserializeObject<List<int>>(data?.belagroprombank.ToString());

            var connectionOptions = new ConnectionOptions(
                "https://city-rates.documents.azure.com:443/",
                "PERNkHuRBu1W9e9oeIznbqZZ6PUDg9OOxp31pIxRfc0gw52p5GRvPo0bToNGTtoN5CQgGPC5Y3b2nDfIyMnMJg==",
                "CityRatesDB",
                "BelagroprombankCollection"
            );

            var belagroprombankRepo = new BelagroprombankRepository(connectionOptions);
            var belagroprombankService = new BelagroprombankService(belagroprombankRepo);
            var result = belagroprombankService.GetFavoriteDepartments(belagropromankDepartmentsIdList);


            return new OkObjectResult(result);
        }
    }
}
