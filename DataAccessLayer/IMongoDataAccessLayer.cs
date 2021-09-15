using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IMongoDataAccessLayer
    {
        Task<List<T>> FilterListAsync<T>(FilterDefinition<T> filter) where T : class;

        Task InsertAsync<T>(T item) where T : class;
       
    }
}