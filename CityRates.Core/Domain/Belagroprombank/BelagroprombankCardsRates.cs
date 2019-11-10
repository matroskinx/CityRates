using System.Collections.Generic;
using System.Xml.Serialization;

namespace CityRates.Core.Domain.Belagroprombank
{
    [XmlRoot(ElementName = "Currency")]
    public class Currency
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "CharCode")]
        public string CharCode { get; set; }

        [XmlElement(ElementName = "RateBuy")]
        public double RateBuy { get; set; }

        [XmlElement(ElementName = "RateSell")]
        public double RateSell { get; set; }
    }

    [XmlRoot(ElementName = "DailyExCards")]
    public class DailyExCards
    {
        [XmlElement(ElementName = "Currency")]
        public List<Currency> Currency { get; set; }

        [XmlAttribute(AttributeName = "Date")]
        public string Date { get; set; }
    }
}


