using System;

namespace DPP.WebApp.Db
{
    public class DataStorageServiceOptions
    {
        public Uri Endpoint { get; set; }
        public string ApiKey { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
