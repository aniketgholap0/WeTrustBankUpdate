using WeTrustBank.Model;
using WeTrustBank.Service.Concrete;

namespace WeTrustBank.Service.Interface
{
    public interface IDebitCardDetailsService 
    {
       Task<int> GenerateDebitCardDetails(GenerateDebitCardDetails generateDebitCardDetails);
       Task<int> GenerateBulkDebitCardNumber(long debitCardNumberSeries, int maxRows);
       Task<int> MapApplicantDebitCardNumberByPanNumber(string panNumber, string createdBy);
       Task<int> MapApplicantsDebitCardNumberByAadharCardNumber(string aadharCardNumber, string createdBy);
       Task<int> UpdateBalanceByAccountNuber(UpdateBalanceByAccountNumberDto updateBalanceByAccountNumberDto);
    }
}
