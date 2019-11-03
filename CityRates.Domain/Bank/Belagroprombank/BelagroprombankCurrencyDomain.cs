using System.Collections.Generic;
using System.Xml.Serialization;

namespace CityRates.Domain.Bank.Belagroprombank
{
    [XmlRoot(ElementName = "Currency")]
    public class BelagroprombankCurrencyDomain
    {
        [XmlElement(ElementName = "NumCode")]
        public int NumCode { get; set; }
        [XmlElement(ElementName = "CharCode")]
        public string CharCode { get; set; }
        [XmlElement(ElementName = "Scale")]
        public int Scale { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "RateBuy")]
        public double RateBuy { get; set; }
        [XmlElement(ElementName = "RateSell")]
        public double RateSell { get; set; }
        [XmlElement(ElementName = "CityId")]
        public int CityId { get; set; }
        [XmlElement(ElementName = "BankId")]
        public int BankId { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "DailyExRates")]
    public class DailyExRates
    {
        [XmlElement(ElementName = "Currency")]
        public List<BelagroprombankCurrencyDomain> Currency { get; set; }
        [XmlAttribute(AttributeName = "Date")]
        public string Date { get; set; }
    }
}