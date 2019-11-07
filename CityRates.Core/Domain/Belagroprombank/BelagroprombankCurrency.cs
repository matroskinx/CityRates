using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CityRates.Core.Domain.Belagroprombank
{
    [XmlRoot(ElementName = "Currency")]
    public class BelagroprombankCurrency
    {
        [XmlElement(ElementName = "NumCode")]
        [DataMember]
        public int NumCode { get; set; }

        [XmlElement(ElementName = "CharCode")]
        [DataMember]
        public string CharCode { get; set; }

        [XmlElement(ElementName = "Scale")]
        [DataMember]
        public int Scale { get; set; }

        [XmlElement(ElementName = "Name")]
        [DataMember]
        public string Name { get; set; }

        [XmlElement(ElementName = "RateBuy")]
        [DataMember]
        public double RateBuy { get; set; }

        [XmlElement(ElementName = "RateSell")]
        [DataMember]
        public double RateSell { get; set; }

        [XmlElement(ElementName = "CityId")]
        [DataMember]
        public int CityId { get; set; }

        [XmlElement(ElementName = "BankId")]
        [DataMember]
        public int BankId { get; set; }

        [XmlAttribute(AttributeName = "Id")]
        [DataMember]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "DailyExRates")]
    public class DailyExRates
    {
        [XmlElement(ElementName = "Currency")]
        [DataMember]
        public List<BelagroprombankCurrency> Currency { get; set; }

        [XmlAttribute(AttributeName = "Date")]
        [DataMember]
        public string Date { get; set; }
    }
}
