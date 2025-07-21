using WeTrustBank.Model;

namespace WeTrustBank.Repository.Interface
{
    public interface IDebitCardDetailsRepository
    {
        public Task<int> GenerateDebitCardDetails(GenerateDebitCardDetails generateDebitCardDetails);
        public Task<int> GenerateBulkDebitCardNumbers(long debitCardSeriesNumber, int maxRows);
        public Task<int> MapApplicantDebitCardMappingByPanNumber(string panNumber, string createdBy);
        public Task<int> MapApplicantDebitCardMappingByAadharCardNumber(string aadharCardNumber, string createdBy);
        public Task<int> UpdateBalanceByAccountNumber(UpdateBalanceByAccountNumberDto updateBalanceByAccountNumberDto);
    }

}
