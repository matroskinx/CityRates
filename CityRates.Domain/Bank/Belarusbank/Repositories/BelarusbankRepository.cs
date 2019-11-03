using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace CityRates.Domain.Bank.Belarusbank.Repositories
{
    public class BelarusbankRepository
    {
        private List<BelarusbankDomain> GetBankDepartments()
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

            var result = JsonConvert.DeserializeObject<List<BelarusbankDomain>>(apiResponse);

            return result;

        }

        private List<BelarusbankCurrencyDomain> GetBankRates()
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

            var result = JsonConvert.DeserializeObject<List<BelarusbankCurrencyDomain>>(apiResponse);

            return result;
        }

        private List<GlobalCurrencyDomain> GetCurrencies()
        {
            var bankRates = GetBankRates();
            var globalCurrencies = new List<GlobalCurrencyDomain>();

            foreach (var department in bankRates)
            {
                // todo add USD_EUR and such pairs
                if (department.CAD_in != 0 && department.CAD_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                            CurrencyType.BYN, CurrencyType.CAD,
                            department.CAD_out, department.CAD_in,
                            department.filial_id, BankType.Belarus
                        ));
                }

                if (department.CHF_in != 0 && department.CHF_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.CHF,
                        department.CHF_out, department.CHF_in,
                        department.filial_id, BankType.Belarus
                    ));
                }

                if (department.CNY_in != 0 && department.CNY_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.CNY,
                        department.CNY_out, department.CNY_in,
                        department.filial_id, BankType.Belarus
                    ));
                }

                if (department.CZK_in != 0 && department.CZK_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.CZK,
                        department.CZK_out, department.CZK_in,
                        department.filial_id, BankType.Belarus
                    ));
                }

                if (department.GBP_in != 0 && department.GBP_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.GBP,
                        department.GBP_out, department.GBP_in,
                        department.filial_id, BankType.Belarus
                    ));
                }

                if (department.EUR_in != 0 && department.EUR_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.EUR,
                        department.EUR_out, department.EUR_in,
                        department.filial_id, BankType.Belarus
                    ));
                }

                if (department.JPY_in != 0 && department.JPY_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.JPY,
                        department.JPY_out, department.JPY_in,
                        department.filial_id, BankType.Belarus
                    ));
                }


                if (department.NOK_in != 0 && department.NOK_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.NOK,
                        department.NOK_out, department.NOK_in,
                        department.filial_id, BankType.Belarus
                    ));
                }

                if (department.PLN_in != 0 && department.PLN_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.PLN,
                        department.PLN_out, department.PLN_in,
                        department.filial_id, BankType.Belarus
                    ));
                }

                if (department.UAH_in != 0 && department.UAH_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.UAH,
                        department.UAH_out, department.UAH_in,
                        department.filial_id, BankType.Belarus
                    ));
                }

                if (department.USD_in != 0 && department.USD_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.USD,
                        department.USD_out, department.USD_in,
                        department.filial_id, BankType.Belarus
                    ));
                }

                if (department.SEK_in != 0 && department.SEK_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.SEK,
                        department.SEK_out, department.SEK_in,
                        department.filial_id, BankType.Belarus
                    ));
                }

                if (department.RUB_in != 0 && department.RUB_out != 0)
                {
                    globalCurrencies.Add(new GlobalCurrencyDomain(
                        CurrencyType.BYN, CurrencyType.RUB,
                        department.RUB_out, department.RUB_in,
                        department.filial_id, BankType.Belarus
                    ));
                }

            }

            return globalCurrencies;
        }


        public List<GlobalDepartmentDomain> GetDepartmentsWithRates()
        {
            var globalDepartments = new List<GlobalDepartmentDomain>();
            var bankCurrencies = GetCurrencies();
            var bankDepartments = GetBankDepartments();

            var groupedCurrencies = bankCurrencies.GroupBy(c => c.FilialId);

            foreach (var group in groupedCurrencies)
            {
                var globalDep = new GlobalDepartmentDomain { BankType = BankType.Belarus };
                var bank = bankDepartments.Single(dep => dep.FilialId == group.Key);

                if (String.IsNullOrEmpty(bank.GpsX) || String.IsNullOrEmpty(bank.GpsY))
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
