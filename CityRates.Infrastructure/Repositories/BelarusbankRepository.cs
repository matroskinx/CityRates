using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using CityRates.Core.Domain.Belarusbank;
using CityRates.Core.Enums;
using System.Globalization;
using CityRates.Core.Domain;
using CityRates.Core.Utils;
using System.Linq;
using CityRates.Core.Interfaces.Belarusbank;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using System.Threading.Tasks;

namespace CityRates.Infrastructure.Repositories
{
    public class BelarusbankRepository : IBelarusbankRepository
    {
        private DocumentClient _client;
        private ConnectionOptions _connectionOptions;

        public BelarusbankRepository(ConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions;
            _client = new DocumentClient(new Uri(_connectionOptions.EndpointUrl), _connectionOptions.PrimaryKey);
        }

        public IEnumerable<GlobalDepartment> GetFavoriteDepartments(List<int> favoriteDepartmens)
        {
            var belarusbankDomain = GetBelarusbankInfo();
            var result = belarusbankDomain.Departments.Where(t2 => favoriteDepartmens.Any(t1 => t2.Id == t1));

            return result;
        }

        public List<GlobalCurrency> GetGlobalCurrencies()
        {
            var apiRequest =
                WebRequest.Create("https://belarusbank.by/api/kurs_cards") as HttpWebRequest;

            string apiResponse;
            using (var response = apiRequest?.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
                reader.Close();
            }

            var result = JsonConvert.DeserializeObject<List<BelarusbankCardsRates>>(apiResponse)[0];
            var globalCurrencyList = new List<GlobalCurrency>();
            var USDCurrency = new GlobalCurrency
            {
                BankType = BankType.Belarusbank,
                FromCurrency = CurrencyType.BYN,
                ToCurrency = CurrencyType.USD,
                BankBuysAt = result.USDCARD_in,
                BankSellsAt = result.USDCARD_out
            };
            globalCurrencyList.Add(USDCurrency);
            var EURCurrency = new GlobalCurrency
            {
                BankType = BankType.Belarusbank,
                FromCurrency = CurrencyType.BYN,
                ToCurrency = CurrencyType.EUR,
                BankBuysAt = result.EURCARD_in,
                BankSellsAt = result.EURCARD_out
            };
            globalCurrencyList.Add(EURCurrency);
            var RUBCurrency = new GlobalCurrency
            {
                BankType = BankType.Belarusbank,
                FromCurrency = CurrencyType.BYN,
                ToCurrency = CurrencyType.RUB,
                BankBuysAt = result.RUBCARD_in,
                BankSellsAt = result.RUBCARD_out
            };
            globalCurrencyList.Add(RUBCurrency);

            return globalCurrencyList;
        }

        public BelarusbankDomain GetBelarusbankInfo()
        {
            var json = _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(_connectionOptions.DatabaseName,
                _connectionOptions.CollectionName, "Belarusbank")).Result;
            BelarusbankDomain belagroprombankDomain = JsonConvert.DeserializeObject<BelarusbankDomain>(json.Resource.ToString());
            return belagroprombankDomain;
        }

        public BelarusbankDomain UpdateBelarusbankInfo()
        {
            var globalDepartments = new List<GlobalDepartment>();
            var bankCurrencies = GetCurrencies();
            var bankDepartments = GetBankDepartments();

            var groupedCurrencies = bankCurrencies.GroupBy(c => c.FilialId);

            foreach (var group in groupedCurrencies)
            {
                var globalDep = new GlobalDepartment { BankType = BankType.Belarusbank };
                var bank = bankDepartments.Single(dep => dep.FilialId == group.Key);

                if (string.IsNullOrEmpty(bank.GpsX) || string.IsNullOrEmpty(bank.GpsY))
                {
                    continue;
                }

                try
                {
                    globalDep.WorkInfo = WorkTimeUtils.parseDateTimeFromBelarusbank(bank);
                    globalDep.IsOpen = WorkTimeUtils.isWorkingNow(globalDep.WorkInfo);
                }
                catch (Exception)
                {
                    Console.WriteLine("Caught exception for worktime: " + bank.InfoWorktime);
                    continue;
                }


                globalDep.Latitude = float.Parse(bank.GpsX, CultureInfo.InvariantCulture);
                globalDep.Longitude = float.Parse(bank.GpsY, CultureInfo.InvariantCulture);

                foreach (var currency in group)
                {
                    globalDep.Currencies.Add(currency);
                }

                globalDep.Address = bank.NameType + " " + bank.Name + " " + bank.StreetType + " " + bank.Street + " " +
                                    bank.HomeNumber;
                globalDep.Id = bank.FilialId;
                globalDepartments.Add(globalDep);
            }

            var belarucbankDomain = new BelarusbankDomain()
            {
                Id = "Belarusbank",
                Departments = globalDepartments
            };

            BelarusbankRepository b = new BelarusbankRepository(_connectionOptions);
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

            CreateAsqDocumentIfNotExists(_connectionOptions.DatabaseName, _connectionOptions.CollectionName, belarucbankDomain).Wait();


            return belarucbankDomain;
        }

        private async Task GetStartedDemo()
        {
            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _connectionOptions.DatabaseName });
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_connectionOptions.DatabaseName), new DocumentCollection { Id = _connectionOptions.CollectionName });
        }

        private async Task CreateAsqDocumentIfNotExists(string databaseName, string collectionName, BelarusbankDomain belarusbankDomain)
        {
            try
            {
                await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, belarusbankDomain.Id));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), belarusbankDomain);
                }
                else
                {
                    throw;
                }
            }
        }

        private List<Belarusbank> GetBankDepartments()
        {
            var apiRequest =
                WebRequest.Create("https://belarusbank.by/api/filials_info") as HttpWebRequest;

            string apiResponse;
            using (var response = apiRequest?.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
                reader.Close();
            }

            var result = JsonConvert.DeserializeObject<List<Belarusbank>>(apiResponse);

            return result;

        }

        private List<BelarusbankCurrency> GetBankRates()
        {
            var apiRequest =
                WebRequest.Create("https://belarusbank.by/api/kursExchange") as HttpWebRequest;

            string apiResponse;
            using (var response = apiRequest?.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
                reader.Close();
            }

            var result = JsonConvert.DeserializeObject<List<BelarusbankCurrency>>(apiResponse);

            return result;
        }

        private List<GlobalCurrency> GetCurrencies()
        {
            var bankRates = GetBankRates();
            var globalCurrencies = new List<GlobalCurrency>();

            foreach (var department in bankRates)
            {
                // todo add USD_EUR and such pairs
                if (department.CAD_in != 0 && department.CAD_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.CAD,
                        BankSellsAt = department.CAD_out,
                        BankBuysAt = department.CAD_in,
                        BankType = BankType.Belarusbank
                    });
                }

                if (department.CHF_in != 0 && department.CHF_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.CHF,
                        BankSellsAt = department.CHF_out,
                        BankBuysAt = department.CHF_in,
                        BankType = BankType.Belarusbank
                    });
                }

                if (department.CNY_in != 0 && department.CNY_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.CNY,
                        BankSellsAt = department.CNY_out,
                        BankBuysAt = department.CNY_in,
                        BankType = BankType.Belarusbank
                    });
                }

                if (department.CZK_in != 0 && department.CZK_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.CZK,
                        BankSellsAt = department.CZK_out,
                        BankBuysAt = department.CZK_in,
                        BankType = BankType.Belarusbank
                    });
                }

                if (department.GBP_in != 0 && department.GBP_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.GBP,
                        BankSellsAt = department.GBP_out,
                        BankBuysAt = department.GBP_in,
                        BankType = BankType.Belarusbank
                    });
                }

                if (department.EUR_in != 0 && department.EUR_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.EUR,
                        BankSellsAt = department.EUR_out,
                        BankBuysAt = department.EUR_in,
                        BankType = BankType.Belarusbank
                    });
                }

                if (department.JPY_in != 0 && department.JPY_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.JPY,
                        BankSellsAt = department.JPY_out,
                        BankBuysAt = department.JPY_in,
                        BankType = BankType.Belarusbank
                    });
                }


                if (department.NOK_in != 0 && department.NOK_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.NOK,
                        BankSellsAt = department.NOK_out,
                        BankBuysAt = department.NOK_in,
                        BankType = BankType.Belarusbank
                    });
                }

                if (department.PLN_in != 0 && department.PLN_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.PLN,
                        BankSellsAt = department.PLN_out,
                        BankBuysAt = department.PLN_in,
                        BankType = BankType.Belarusbank
                    });
                }

                if (department.UAH_in != 0 && department.UAH_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.UAH,
                        BankSellsAt = department.UAH_out,
                        BankBuysAt = department.UAH_in,
                        BankType = BankType.Belarusbank
                    });
                }

                if (department.USD_in != 0 && department.USD_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.USD,
                        BankSellsAt = department.USD_out,
                        BankBuysAt = department.USD_in,
                        BankType = BankType.Belarusbank
                    });
                }

                if (department.SEK_in != 0 && department.SEK_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.SEK,
                        BankSellsAt = department.SEK_out,
                        BankBuysAt = department.SEK_in,
                        BankType = BankType.Belarusbank
                    });
                }

                if (department.RUB_in != 0 && department.RUB_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrency()
                    {
                        FilialId = department.filial_id,
                        FromCurrency = CurrencyType.BYN,
                        ToCurrency = CurrencyType.RUB,
                        BankSellsAt = department.RUB_out,
                        BankBuysAt = department.RUB_in,
                        BankType = BankType.Belarusbank
                    });
                }
            }

            return globalCurrencies;
        }
    }
}
