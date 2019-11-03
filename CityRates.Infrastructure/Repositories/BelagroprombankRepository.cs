using CityRates.Core.Domain;
using CityRates.Core.Domain.Belagroprombank;
using CityRates.Core.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace CityRates.Infrastructure.Repositories
{
    public class BelagroprombankRepository
    {
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
            Console.WriteLine(date);
            var apiRequest =
                WebRequest.Create("https://belapb.by/CashExRatesDaily.php?ondate=" + date) as HttpWebRequest;

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

        public List<GlobalDepartment> GetDepartmentsWithRates()
        {
            var globalDepartments = new List<GlobalDepartment>();
            var bankCurrencies = GetCurrencies();
            var bankDepartments = GetBankDepartments();

            var groupedCurrencies = bankCurrencies.GroupBy(c => c.FilialId);

            foreach (var group in groupedCurrencies)
            {
                var globalDep = new GlobalDepartment { BankType = BankType.Belagroprombank };
                var bank = bankDepartments.Single(dep => dep.Id == group.Key.ToString());

                if (string.IsNullOrEmpty(bank.BankLatitude) || string.IsNullOrEmpty(bank.BankLongitude))
                {
                    continue;
                }

                globalDep.Lat = float.Parse(bank.BankLatitude, CultureInfo.InvariantCulture);
                globalDep.Lng = float.Parse(bank.BankLongitude, CultureInfo.InvariantCulture);

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
