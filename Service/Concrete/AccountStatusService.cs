using WeTrustBank.Model;
using WeTrustBank.Repository.Concrete;
using WeTrustBank.Repository.Interface;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Service.Concrete
{
    public class AccountStatusService : IAccountStatusService
    {
        private readonly IAccountStatusRepository _accountStatusRepository;
       
        public AccountStatusService(IAccountStatusRepository accountStatusRepository)
        {
            _accountStatusRepository = accountStatusRepository;
        }

        public async Task<int> CreateAccountStatus(string accountStatusName, string createdBy)
        {
            return await _accountStatusRepository.CreateAccountStatus(accountStatusName, createdBy);
        }

        public Task<int> UpdateAccountStatus(int accountStatusId, string accountStatusName, string updatedBy)
        {
            return _accountStatusRepository.UpdateAccountStatus(accountStatusId, accountStatusName, updatedBy);
        }

        public Task<List<AccountStatus>> GetAllAccountStatus()
        {
            return _accountStatusRepository.GetAllAccountStatus();
        }

        public Task<AccountStatus> GetAccountStatusId(int accountStatusById)
        {
            return _accountStatusRepository.GetAccountStatusId(accountStatusById);
        }

        public Task<int> DeleteAccountStatusId(int accountStatusById)
        {
           return _accountStatusRepository.DeleteAccountStatusById(accountStatusById);
        }
    }  
}
