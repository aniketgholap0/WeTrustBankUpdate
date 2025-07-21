using System.Transactions;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Service.Concrete
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly IAccountTransactionRepository _accountTransactionRepository;

        public AccountTransactionService(IAccountTransactionRepository accountTransactionRepository)
        {
           _accountTransactionRepository = accountTransactionRepository;
        }

        public async Task<int> InsertAccountTransaction(InsertAccountTransactionDto insertAccountTransactionDto)
        {
            return await _accountTransactionRepository.InsertAccountTransaction(insertAccountTransactionDto);
        }

        public async  Task<AccountTransaction> GetAccountTransactionByAccountTransactionID(int accountTransactionById)
        {
            return await _accountTransactionRepository.GetAccountTransactionByAccountTransactionID(accountTransactionById);
        }

        public async Task<List<AccountTransaction>> GetAccountTransactionByAccountNumber(long accountNumber)
        {
            return await _accountTransactionRepository.GetAccountTransactionByAccountNumber(accountNumber);
        }

        public async Task<List<AccountTransaction>> GetAccountTransactionByDebitCardNumber(long debitCardNumber)
        {
            return await _accountTransactionRepository.GetAccountTransactionsByDebitCardNumber(debitCardNumber);
        }

        public async Task<IEnumerable<AccountTransaction>> GetAccountTransactionsAsync(int pageNumber, int pageSize)
        {
            return await _accountTransactionRepository.GetAccountTransactionsAsync(pageNumber, pageSize);
        }
    }
}
