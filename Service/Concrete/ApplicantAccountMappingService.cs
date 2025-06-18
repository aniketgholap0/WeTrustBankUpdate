using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Service.Concrete
{
    public class ApplicantAccountMappingService : IApplicantAccountMappingService
    {
        private readonly IApplicantAccountMappingRepository _applicantAccountMappingRepository;
        
        public ApplicantAccountMappingService(IApplicantAccountMappingRepository applicantAccountMappingRepository)
        {
            _applicantAccountMappingRepository = applicantAccountMappingRepository;
        }

        public async  Task<int> CreateApplicantAccountMapping(ApplicantAccountMappingDto applicantAccountMappingDto)
        {
            return await _applicantAccountMappingRepository.CreateApplicantAccountMapping(applicantAccountMappingDto);
        }

        public async Task<int> UpdateApplicantAccountMappingByAadharCardNumber(long aadharCardNumber, string accountStatusName, string updatedBy)
        {
            return await _applicantAccountMappingRepository.UpdateApplicantAccountMappingByAadharCardNumber(aadharCardNumber, accountStatusName, updatedBy);
        }

        public async  Task<int> UpdateApplicantAccountMappingByPanNumber(string panNumber, string accountStatusName, string UpdatedBy)
        {
            return await _applicantAccountMappingRepository.UpdateApplicantAccountMappingByPanNumber(panNumber, accountStatusName, UpdatedBy);
        }

        public async Task<int> UpdateApplicantAccountMappingByApplicantId(int applicantsId, string accountStatusName, string upadtedBy)
        {
           return await _applicantAccountMappingRepository.UpdateApplicantAccountMappingByApplicantId(applicantsId, accountStatusName, upadtedBy);
        }
    }
}
