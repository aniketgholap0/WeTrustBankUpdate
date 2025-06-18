using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;

namespace WeTrustBank.Service.Interface
{
    public interface IApplicantAccountMappingService
    {
        Task<int> CreateApplicantAccountMapping(ApplicantAccountMappingDto applicantAccountMappingDto);
        Task<int> UpdateApplicantAccountMappingByAadharCardNumber(long aadharCardNumber, string accountStatusName, string updatedBy);
        Task<int> UpdateApplicantAccountMappingByPanNumber(string panNumber, string accountStatusName, string updatedBy);
        Task<int> UpdateApplicantAccountMappingByApplicantId(int applicantsId, string accountStatusName, string upadtedBy);
    }
}
