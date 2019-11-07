using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CityRates.Core.Domain
{
    [DataContract(Name = "Belarusbank", Namespace = "http://functions")]
    public class BelarusbankDomain
    {
        [JsonProperty(PropertyName = "id")]
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public List<GlobalDepartment> Departments { get; set; }
    }
}
