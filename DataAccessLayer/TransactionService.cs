using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class TransactionService : ITransactionService
    {
        private readonly IMongoDataAccessLayer _mongoDataAccessLayer;

        public TransactionService(IMongoDataAccessLayer mongoDataAccessLayer)
        {
            _mongoDataAccessLayer = mongoDataAccessLayer;
        }
        public async Task AddNewTransactions(TransactionsModel transaction)
        {
            await _mongoDataAccessLayer.InsertAsync(transaction);
        }
        public async Task<bool> CheckIfItExists(Guid id)
        {
            var filter = Builders<TransactionsModel>.Filter.Eq(x => x.IdempotenceKey, id);
            var list = await _mongoDataAccessLayer.FilterListAsync(filter);
            return list.Any();
        }
    }
}
