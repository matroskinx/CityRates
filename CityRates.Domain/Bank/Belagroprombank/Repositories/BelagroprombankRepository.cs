using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using static System.String;

namespace CityRates.Domain.Bank.Belagroprombank.Repositories
{
    public class BelagroprombankRepository
    {
        private List<BelagroprombankDomain> GetBankDepartments()
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

        private List<GlobalCurrencyDomain> GetCurrencies()
        {
            var bankRates = GetBankRates();

            List<GlobalCurrencyDomain> globalCurrencies = new List<GlobalCurrencyDomain>();

            foreach (var currency in bankRates.Currency)
            {
                //Colors color = (Colors)System.Enum.Parse(typeof(Colors), "Green");

                CurrencyType type = (CurrencyType)Enum.Parse(typeof(CurrencyType), currency.CharCode);


                var globalDomain = new GlobalCurrencyDomain(
                        fromCurrency: CurrencyType.BYN,
                        toCurrency: type,
                        sellsAt: currency.RateSell,
                        buysAt: currency.RateBuy,
                        filialId: currency.BankId,
                        bankType: BankType.Belagro
                    );

                globalCurrencies.Add(globalDomain);
            }

            Console.WriteLine(globalCurrencies);

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
                var globalDep = new GlobalDepartmentDomain { BankType = BankType.Belagro };
                var bank = bankDepartments.Single(dep => dep.Id == group.Key.ToString());

                if (IsNullOrEmpty(bank.BankLatitude) || IsNullOrEmpty(bank.BankLongitude))
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
