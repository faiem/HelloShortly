using HelloShortly.UrlDistributerRestApi.Data.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.Data.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private ILogger<ShortUrlRepository> _logger;
        private readonly IMongoCollection<ShortUrl> _shortUrls;

        public ShortUrlRepository(ILogger<ShortUrlRepository> logger)
        {
            _logger = logger;
            //var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONN"));
            //var database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DB_NAME"));
            //_shortUrls = database.GetCollection<ShortUrl>(Environment.GetEnvironmentVariable("Mongo_COLLECTION_NAME"));

            var client = new MongoClient("mongodb+srv://mymongo:39039820@shortlycluster.k5hmd.mongodb.net?retryWrites=true&w=majority");
            var database = client.GetDatabase("test");
            _shortUrls = database.GetCollection<ShortUrl>("Urls");


        }

        public ShortUrl Create(ShortUrl url)
        {
            _shortUrls.InsertOne(url);
            return url;
        }
        public IList<ShortUrl> Read() =>
            _shortUrls.Find(sub => true).ToList();

        public ShortUrl Find(string id) =>
            _shortUrls.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(ShortUrl url) =>
            _shortUrls.ReplaceOne(sub => sub.Id == url.Id, url);

        public void Delete(string id) =>
            _shortUrls.DeleteOne(sub => sub.Id == id);
    }
}
