using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private MongoClient client { get; set; }

        private IMongoDatabase database { get; set; }


        public CatalogContext(IConfiguration configuration)
        {
            client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
