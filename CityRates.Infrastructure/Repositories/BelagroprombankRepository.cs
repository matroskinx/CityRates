using CityRates.Core.Domain;
using CityRates.Core.Domain.Belagroprombank;
using CityRates.Core.Enums;
using CityRates.Core.Interfaces.Belagroprombank;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CityRates.Infrastructure.Repositories
{
    public class BelagroprombankRepository: IBelagroprombankRepository
    {
        private DocumentClient _client;
        private ConnectionOptions _connectionOptions;

        public BelagroprombankRepository(ConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions;
            _client = new DocumentClient(new Uri(_connectionOptions.EndpointUrl), _connectionOptions.PrimaryKey);
        }

        public IEnumerable<GlobalDepartment> GetFavoriteDepartments(List<int> favoriteDepartmens)
        {
            var belagroprombankDomain = GetBelagroprombankInfo();
            var result = belagroprombankDomain.Departments.Where(t2 => favoriteDepartmens.Any(t1 => t2.Id == t1));

            return result;
        }

        public List<GlobalCurrency> GetGlobalCurrencies()
        {
            var apiRequest =
                WebRequest.Create("https://belapb.by/ExCardsDaily.php?ondate=11/06/2019") as HttpWebRequest;

            string apiResponse;
            using (var response = apiRequest?.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
                reader.Close();
            }

            DailyExCards result;

            var serializer = new XmlSerializer(typeof(DailyExCards));
            using (TextReader reader = new StringReader(apiResponse))
            {
                result = (DailyExCards)serializer.Deserialize(reader);
            }

            var globalCurrencyList = new List<GlobalCurrency>();
           
            foreach (var res in result.Currency)
            {
                if (res.CharCode == "USD" || res.CharCode == "EUR" || res.CharCode == "RUB")
                {
                    CurrencyType type = (CurrencyType)Enum.Parse(typeof(CurrencyType), res.CharCode);
                    var currency = new GlobalCurrency
                    {
                        BankType = BankType.Belagroprombank,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = type,
                        BankBuysAt = res.RateBuy,
                        BankSellsAt = res.RateSell
                    };
                    globalCurrencyList.Add(currency);
                }
            }
            return globalCurrencyList;
        }
        
        public BelagroprombankDomain GetBelagroprombankInfo()
        {
            var json = _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(_connectionOptions.DatabaseName, 
                _connectionOptions.CollectionName, "Belagroprombank")).Result;
            BelagroprombankDomain belagroprombankDomain = JsonConvert.DeserializeObject<BelagroprombankDomain>(json.Resource.ToString());
            return belagroprombankDomain;
        }

        public BelagroprombankDomain UpdateBelagroprombankInfo()
        {
            var globalDepartments = new List<GlobalDepartment>();
            var bankCurrencies = GetCurrencies();
            var bankDepartments = GetBankDepartments();

            var groupedCurrencies = bankCurrencies.GroupBy(c => c.FilialId);

            foreach (var group in groupedCurrencies)
            {
                var globalDep = new GlobalDepartment { BankType = BankType.Belagroprombank };

                var bank = bankDepartments.Find(dep => dep.Id == group.Key.ToString());

                if (bank == null || string.IsNullOrEmpty(bank.BankLatitude) || string.IsNullOrEmpty(bank.BankLongitude))
                {
                    continue;
                }

                globalDep.Latitude = float.Parse(bank.BankLatitude, CultureInfo.InvariantCulture);
                globalDep.Longitude = float.Parse(bank.BankLongitude, CultureInfo.InvariantCulture);

                foreach (var currency in group)
                {
                    globalDep.Currencies.Add(currency);
                }

                globalDep.Address = bank.BankAddressRu;
                globalDep.Id = Convert.ToInt32(bank.Id);
                globalDepartments.Add(globalDep);
            }

            var belagroprombankDomain = new BelagroprombankDomain()
            {
                Id = "Belagroprombank",
                Departments = globalDepartments
            };

            BelagroprombankRepository b = new BelagroprombankRepository(_connectionOptions);
            try
            {
                b.GetStartedDemo().Wait();
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
            }
            
            CreateAsqDocumentIfNotExists(_connectionOptions.DatabaseName, _connectionOptions.CollectionName, belagroprombankDomain).Wait();

            return belagroprombankDomain;
        }
        
        private async Task GetStartedDemo()
        {
            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _connectionOptions.DatabaseName });
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_connectionOptions.DatabaseName), new DocumentCollection { Id = _connectionOptions.CollectionName });
        }

        private async Task CreateAsqDocumentIfNotExists(string databaseName, string collectionName, BelagroprombankDomain belagroprombankDomain)
        {
            try
            {
                await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, belagroprombankDomain.Id));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), belagroprombankDomain);
                }
                else
                {
                    throw;
                }
            }
        }

        private List<Belagroprombank> GetBankDepartments()
        {
            var apiRequest =
                WebRequest.Create("https://belapb.by/ExBanks.php") as HttpWebRequest;

            string apiResponse;
            using (var response = apiRequest?.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
                reader.Close();
            }

            ExBanksList result;

            var serializer = new XmlSerializer(typeof(ExBanksList));
            using (TextReader reader = new StringReader(apiResponse))
            {
                result = (ExBanksList)serializer.Deserialize(reader);
            }

            return result.Banks;
        }

        private DailyExRates GetBankRates()
        {
            var date = DateTime.Now.ToString("MM/dd/yyyy");
            var apiRequest =
                WebRequest.Create("https://belapb.by/CashExRatesDaily.php?ondate=11/08/2019") as HttpWebRequest;

            string apiResponse;
            using (var response = apiRequest?.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
                reader.Close();
            }

            DailyExRates result;

            var serializer = new XmlSerializer(typeof(DailyExRates));
            using (TextReader reader = new StringReader(apiResponse))
            {
                result = (DailyExRates)serializer.Deserialize(reader);
            }

            return result;
        }

        private List<GlobalCurrency> GetCurrencies()
        {
            var bankRates = GetBankRates();

            List<GlobalCurrency> globalCurrencies = new List<GlobalCurrency>();

            foreach (var currency in bankRates.Currency)
            {
                CurrencyType type = (CurrencyType)Enum.Parse(typeof(CurrencyType), currency.CharCode);


                var globalDomain = new GlobalCurrency()
                {
                    FilialId = currency.BankId,
                    FromCurrency = CurrencyType.BYN,
                    ToCurrency = type,
                    BankSellsAt = currency.RateSell,
                    BankBuysAt = currency.RateBuy,
                    BankType = BankType.Belagroprombank
                };

                globalCurrencies.Add(globalDomain);
            }

            return globalCurrencies;

        }
        
    }
}
