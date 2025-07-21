using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Service.Concrete
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly Random _random = new();

        public BankAccountService(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<int> GenerateBankAccount(string createdBy)
        {
            int result;
            long generatedAccountNumber;
            int maxRetries = 10;
            int attempt = 0;
            do
            {
                generatedAccountNumber = _random.NextInt64(100000000000, 999999999999);
                result = await _bankAccountRepository.GenerateBankAccount(generatedAccountNumber, createdBy);
                attempt++;
            }
            while (result == -1 && attempt < maxRetries);

            if (result == -1)
                throw new Exception("Failed to generate a unique 12-digit account number");

            return result;
        }

        public async Task<List<BankAccount>> GetAllBankAccounts()
        {
            return await _bankAccountRepository.GetAllBankAccounts();
        }

        public async  Task<BankAccount> GetBankAccountById(int bankAccountId)
        {
            return await _bankAccountRepository.GetBankAccountById(bankAccountId);
        }

        public async Task<int> BankAccountNumberByAccountNumber(long accountNumber)
        {
            return await _bankAccountRepository.BankAccountNumberByAccountNumber(accountNumber);
        }

        public async Task<int> GenerateBulkBankAccounts(long bankAccountSeries,int maxRows)
        {
            return await _bankAccountRepository.GenerateBulkBankAccounts(bankAccountSeries,maxRows);
        }
    }
}