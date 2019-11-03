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
using System.Linq;

namespace CityRates.Infrastructure.Repositories
{
    public class BelarusbankRepository
    {
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
                    globalCurrencies.Add(new GlobalCurrency() { 
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


        public List<GlobalDepartment> GetDepartmentsWithRates()
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

                globalDep.Lat = float.Parse(bank.GpsX, CultureInfo.InvariantCulture);
                globalDep.Lng = float.Parse(bank.GpsY, CultureInfo.InvariantCulture);

                foreach (var currency in group)
                {
                    globalDep.Currencies.Add(currency);
                }
                globalDepartments.Add(globalDep);
            }

            return globalDepartments;
        }
    }
}
