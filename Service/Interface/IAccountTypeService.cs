using WeTrustBank.Model;

namespace WeTrustBank.Service.Interface
{
    public interface IAccountTypeService
    {
        Task<int> CreateAccountType(string accountTypeName, string createdBy);
        Task<int> UpdateAccountType(int accountTypeId, string accountTypeName, string updatedBy);
        Task<List<AccountType>> GetAllAccountTypes();
        Task<AccountType> GetAccountTypeById(int accountTypeId);
        Task<int> DeleteAccountTypeById(int accountTypeId);
    }
}
