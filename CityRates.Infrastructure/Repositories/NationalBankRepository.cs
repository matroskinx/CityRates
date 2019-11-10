using CityRates.Core.Domain.NationalBank;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using CityRates.Core.Interfaces.Nationalbank;

namespace CityRates.Infrastructure.Repositories
{
    public class NationalBankRepository: INationalBankRepository
    { 
        public List<NationalBankDomain> GetNBRates()
        {
            var apiRequest =
                WebRequest.Create("http://www.nbrb.by/api/exrates/rates?periodicity=0") as HttpWebRequest;

            string apiResponse;
            using (var response = apiRequest?.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
                reader.Close();
            }

            var result = JsonConvert.DeserializeObject<List<NationalBank>>(apiResponse);
            var resultNBDomainList = new List<NationalBankDomain>();
            foreach (var nb in result)
            {
                var nbDomain = new NationalBankDomain
                {
                    Id = nb.Cur_ID,
                    Currency = nb.Cur_Abbreviation,
                    Rate = nb.Cur_OfficialRate
                };
                resultNBDomainList.Add(nbDomain);
            }
            
            return resultNBDomainList;
        }
    }
}
