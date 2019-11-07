using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CityRates.Core.Domain.Belagroprombank
{
    [XmlRoot(ElementName = "Bank")]
    public class Belagroprombank
    {
        [XmlAttribute(AttributeName = "Id")]
        [DataMember]
        public string Id { get; set; }

        [XmlElement(ElementName = "BankTitleRu")]
        [DataMember]
        public string BankTitleRu { get; set; }

        [XmlElement(ElementName = "CityId")]
        [DataMember]
        public string CityId { get; set; }

        [XmlElement(ElementName = "CityTitleRu")]
        [DataMember]
        public string CityTitleRu { get; set; }

        [XmlElement(ElementName = "RegionId")]
        [DataMember]
        public string RegionId { get; set; }

        [XmlElement(ElementName = "RegionTitleRu")]
        [DataMember]
        public string RegionTitleRu { get; set; }

        [XmlElement(ElementName = "BankAddressRu")]
        [DataMember]
        public string BankAddressRu { get; set; }

        [XmlElement(ElementName = "BankPhone")]
        [DataMember]
        public string BankPhone { get; set; }

        [XmlElement(ElementName = "BankWorkTimeRu")]
        public string BankWorkTimeRu { get; set; }

        [XmlElement(ElementName = "BankLatitude")]
        [DataMember]
        public string BankLatitude { get; set; }

        [XmlElement(ElementName = "BankLongitude")]
        [DataMember]
        public string BankLongitude { get; set; }

        [XmlElement(ElementName = "BankType")]
        [DataMember]
        public string BankType { get; set; }

        [XmlElement(ElementName = "BankServisesRu")]
        [DataMember]
        public string BankServicesRu { get; set; }

        [XmlElement(ElementName = "BankCurrencyAvailability")]
        [DataMember]
        public string BankCurrencyAvailability { get; set; }
        
    }

    [XmlRoot(ElementName = "ExBanksList")]
    public class ExBanksList
    {
        [XmlElement(ElementName = "Bank")]
        [DataMember]
        public List<Belagroprombank> Banks { get; set; }
    }
}
