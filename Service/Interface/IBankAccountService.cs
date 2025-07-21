using WeTrustBank.Model;

namespace WeTrustBank.Service.Interface
{
    public interface IBankAccountService
    {
        Task<int> GenerateBankAccount(string createdBy);
        Task<List<BankAccount>> GetAllBankAccounts();
        Task<BankAccount> GetBankAccountById(int bankAccountId);
        Task<int> BankAccountNumberByAccountNumber(long accountNumber);
        Task<int> GenerateBulkBankAccounts(long bankAccountSeries,int maxRows);
    }
}
