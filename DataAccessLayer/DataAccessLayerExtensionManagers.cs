using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class DataAccessLayerExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(s => new MongoClient(configuration["MongoConnectionString"]));
            services.AddSingleton(s =>
            {
                return s.GetRequiredService<IMongoClient>().GetDatabase(configuration["MongoDatabasename"]);
            });
            services.AddSingleton<IMongoDataAccessLayer, MongoDataAccessLayer>();
            services.AddSingleton<ITransactionService, TransactionService>();
            return services;
        }
    }
}
