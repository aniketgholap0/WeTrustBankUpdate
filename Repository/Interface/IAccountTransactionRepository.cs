using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Transactions;
using WeTrustBank.Model;

namespace WeTrustBank.Repository.Interface
{
    public interface IAccountTransactionRepository
    {
        public Task<int> InsertAccountTransaction(InsertAccountTransactionDto insertAccountTransactionDto);
        public Task<AccountTransaction> GetAccountTransactionByAccountTransactionID(int accountTransactionId);
        Task<List<AccountTransaction>> GetAccountTransactionByAccountNumber(long accountNumber);
        Task<List<AccountTransaction>> GetAccountTransactionsByDebitCardNumber(long debitCardNumber);
        Task<IEnumerable<AccountTransaction>> GetAccountTransactionsAsync(int pageNumber, int pageSize);
    }
}
