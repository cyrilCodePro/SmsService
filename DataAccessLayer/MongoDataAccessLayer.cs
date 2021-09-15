using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class MongoDataAccessLayer : IMongoDataAccessLayer
    {
        private protected readonly IMongoDatabase db;
        public async Task InsertAsync<T>(T item) where T : class => await db.GetCollection<T>(item.GetType().Name).InsertOneAsync(item);
        public async Task<List<T>> FilterListAsync<T>(FilterDefinition<T> filter) where T : class => await db.GetCollection<T>(typeof(T).Name).Find(filter).ToListAsync();
    
        public MongoDataAccessLayer(IMongoDatabase database)
        {
            //dbClient = new MongoClient(configuration["MongoConnectionString"]);
            db = database;


        }

        //MongoClientData _client;
        //MongoServer _server;
        //MongoDatabase _db;


    }
}
