using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebScraping.Models;
using WebScraping_Docker.Entity;
namespace WebScraping_Docker.Services
{
    public class WebScrapingService
    {
        private readonly IMongoCollection<ScrapingResult> _scarpingResult;
        public WebScrapingService(IWebScrappingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _scarpingResult = database.GetCollection<ScrapingResult>("ScrapingResult");
        }

        public List<ScrapingResult> Get()
        {
            List<ScrapingResult> res = new List<ScrapingResult>();
            res = _scarpingResult.Find(emp => true).ToList();
            return res;
        }
        public ScrapingResult Create(ScrapingResult scapingData)
        {
            _scarpingResult.InsertOne(scapingData);
            return scapingData;
        }
        public ScrapingResult Get(string id) =>
            _scarpingResult.Find<ScrapingResult>(emp => emp.Id == id).FirstOrDefault();
    }
}
