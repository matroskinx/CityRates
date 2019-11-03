using System.Collections.Generic;
using System.Xml.Serialization;


namespace CityRates.Domain.Bank.Belagroprombank { 
    [XmlRoot(ElementName = "Bank")]
    public class BelagroprombankDomain
    {
        [XmlElement(ElementName = "BankTitleRu")]
        public string BankTitleRu { get; set; }
        [XmlElement(ElementName = "CityId")]
        public string CityId { get; set; }
        [XmlElement(ElementName = "CityTitleRu")]
        public string CityTitleRu { get; set; }
        [XmlElement(ElementName = "RegionId")]
        public string RegionId { get; set; }
        [XmlElement(ElementName = "RegionTitleRu")]
        public string RegionTitleRu { get; set; }
        [XmlElement(ElementName = "BankAddressRu")]
        public string BankAddressRu { get; set; }
        [XmlElement(ElementName = "BankPhone")]
        public string BankPhone { get; set; }
        [XmlElement(ElementName = "BankWorkTimeRu")]
        public string BankWorkTimeRu { get; set; }
        [XmlElement(ElementName = "BankLatitude")]
        public string BankLatitude { get; set; }
        [XmlElement(ElementName = "BankLongitude")]
        public string BankLongitude { get; set; }
        [XmlElement(ElementName = "BankType")]
        public string BankType { get; set; }
        [XmlElement(ElementName = "BankServisesRu")]
        public string BankServicesRu { get; set; }
        [XmlElement(ElementName = "BankCurrencyAvailability")]
        public string BankCurrencyAvailability { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "ExBanksList")]
    public class ExBanksList
    {
        [XmlElement(ElementName = "Bank")]
        public List<BelagroprombankDomain> Banks { get; set; }
    }
}