using System;
using System.Collections.Generic;
using System.Text;

namespace CityRates.Core.Domain
{
    public class ConnectionOptions
    {
        public string EndpointUrl { get; set; }
        public string PrimaryKey { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

        public ConnectionOptions(string endpointUrl, string primaryKey, string databaseName, string collectionName)
        {
            EndpointUrl = endpointUrl;
            PrimaryKey = primaryKey;
            DatabaseName = databaseName;
            CollectionName = collectionName;
        }
    }
}
