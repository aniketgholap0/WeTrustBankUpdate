using System.Transactions;
using WeTrustBank.Model;

namespace WeTrustBank.Service.Interface
{
    public interface IAccountTransactionService
    {
        Task<int> InsertAccountTransaction(InsertAccountTransactionDto insertAccountTransactionDto);
        Task<AccountTransaction> GetAccountTransactionByAccountTransactionID(int accountTransactionById);
        Task<List<AccountTransaction>> GetAccountTransactionByAccountNumber(long accountNumber);
        Task<List<AccountTransaction>> GetAccountTransactionByDebitCardNumber(long debitCardNumber);
        Task<IEnumerable<AccountTransaction>> GetAccountTransactionsAsync(int pageNumber, int pageSize);
    }
}
