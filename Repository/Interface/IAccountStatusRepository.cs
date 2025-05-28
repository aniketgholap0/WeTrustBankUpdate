using WeTrustBank.Model;

namespace WeTrustBank.Repository.Interface
{
    public interface IAccountStatusRepository
    {
        Task<int> CreateAccountStatus(String accountStausName , string createdBy);
        Task<int> UpdateAccountStatus(int accountStatusId, string accountStatusName, string updatedBy);
        Task<List<AccountStatus>> GetAllAccountStatus();
        Task<AccountStatus> GetAccountStatusId(int accountStatusById);
        Task<int> DeleteAccountStatusById(int accountStatusById);
    }
}
