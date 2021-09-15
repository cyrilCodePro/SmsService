using System;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface ITransactionService
    {
        Task AddNewTransactions(TransactionsModel transaction);
        Task<bool> CheckIfItExists(Guid id);
    }
}