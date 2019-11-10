using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CityRates.Core.Domain;
using CityRates.Infrastructure.Repositories;
using CityRates.Core.Services;

namespace FunctionApp
{
    public static class GetFavoriteDepartmentsBelarusbank
    {
        [FunctionName("GetFavoriteDepartmentsBelarusbank")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        { 
            log.LogInformation("C# HTTP trigger function processed a request.");

            string belarusbank = "";

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            List<int> belarusbankDepartmentsIdList = JsonConvert.DeserializeObject<List<int>>(data?.belarusbank.ToString());

            var connectionOptions = new ConnectionOptions(
                "https://city-rates.documents.azure.com:443/",
                "PERNkHuRBu1W9e9oeIznbqZZ6PUDg9OOxp31pIxRfc0gw52p5GRvPo0bToNGTtoN5CQgGPC5Y3b2nDfIyMnMJg==",
                "CityRatesDB",
                "BelarusbankCollection"
            );

            var belarusbankRepo = new BelarusbankRepository(connectionOptions);
            var belarusbankService = new BelarusbankService(belarusbankRepo);
            var result = belarusbankService.GetFavoriteDepartments(belarusbankDepartmentsIdList);


            return new OkObjectResult(result);
        }
    }
}
