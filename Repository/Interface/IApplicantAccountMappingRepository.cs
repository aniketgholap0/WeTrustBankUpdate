using System.Globalization;
using WeTrustBank.Model;

namespace WeTrustBank.Repository.Interface
{
    public interface IApplicantAccountMappingRepository
    {
        public Task<int> CreateApplicantAccountMapping(ApplicantAccountMappingDto applicantAccountMappingDto);

        public Task<int> UpdateApplicantAccountMappingByAadharCardNumber(long aadharCardNumber, string accountStatusName, string updatedBy);
        public Task<int> UpdateApplicantAccountMappingByPanNumber(string panNumber, string accountStatusName, string updatedBy);
        public Task<int> UpdateApplicantAccountMappingByApplicantId(int applicantsId, string accountStatusName, string upadtedBy);
    }
   
}
