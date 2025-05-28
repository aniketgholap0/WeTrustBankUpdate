using WeTrustBank.Model;

namespace WeTrustBank.Service.Interface
{
    public interface IAccountStatusService
    {
      Task<int> CreateAccountStatus(string accountStatusName, String createdBy);
      Task<int> UpdateAccountStatus(int accountStatusId,string accountStatusName, String updatedBy);
      Task<List<AccountStatus>> GetAllAccountStatus();
      Task<AccountStatus> GetAccountStatusId(int accountStatusById);
      Task<int> DeleteAccountStatusId(int accountStatusById);
    }
}
