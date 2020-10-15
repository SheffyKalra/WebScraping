using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraping_Docker.Entity
{
    public class WebScrappingDatabaseSettings : IWebScrappingDatabaseSettings
    {
        public string WebScrapingCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Database { get; set; }
        public bool IsContained { get; set; }
        public string Container { get; set; }
    }

    public interface IWebScrappingDatabaseSettings
    {
        public string WebScrapingCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Database { get; set; }
        public bool IsContained { get; set; }
        public string Container { get; set; }
    }
}
