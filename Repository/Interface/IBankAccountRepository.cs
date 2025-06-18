using WeTrustBank.Model;

namespace WeTrustBank.Repository.Interface
{
    public interface IBankAccountRepository
    {
        Task<int> GenerateBankAccount(long generatedAccountNumber, string createdBy);
        Task<List<BankAccount>> GetAllBankAccounts();
        Task<BankAccount> GetBankAccountById(int bankAccountId);
        Task<int> BankAccountNumberByAccountNumber(long accountNumber);
    }
}
