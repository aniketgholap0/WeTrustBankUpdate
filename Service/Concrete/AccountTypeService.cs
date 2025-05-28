using Microsoft.OpenApi.Services;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Service.Concrete
{
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IAccountTypeRepository _accountTypeRepository;

        public AccountTypeService(IAccountTypeRepository accountTypeRepository)
        {
            _accountTypeRepository = accountTypeRepository;
        }

        public async Task<int> CreateAccountType(string accountTypeName, string createdBy)
        {
            return await _accountTypeRepository.CreateAccountType(accountTypeName, createdBy);
        }
        

        public async Task<int> UpdateAccountType(int accountTypeId, string accountTypeName, string updatedBy)
        {
            return await _accountTypeRepository.UpdateAccountType(accountTypeId, accountTypeName, updatedBy);
        }

        public async Task<List<AccountType>> GetAllAccountTypes()
        {
            return await _accountTypeRepository.GetAllAccountTypes();
        }

        public Task<AccountType> GetAccountTypeById(int accountTypeId)
        {
            return _accountTypeRepository.GetAccountTypeById(accountTypeId);
        }

        public Task<int> DeleteAccountTypeById(int accountTypeId)
        {
            return _accountTypeRepository.DeleteAccountTypeById(accountTypeId);
        }
    }
}
