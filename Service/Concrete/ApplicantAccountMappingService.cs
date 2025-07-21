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

        public Task<int> UpdateApplicantAccountMappingByApplicantId(int applicantsId, string accountStatusName, string upadtedBy)
        {
            return _applicantAccountMappingRepository.UpdateApplicantAccountMappingByApplicantId(applicantsId, accountStatusName, upadtedBy);
        }

        public async  Task<int> UpdateApplicantAccountMappingByPanNumber(string panNumber, string accountStatusName, string UpdatedBy)
        {
            return await _applicantAccountMappingRepository.UpdateApplicantAccountMappingByPanNumber(panNumber, accountStatusName, UpdatedBy);
        }

        public async Task<ApplicantAccountMappingRequestDto> GetApplicantAccountMappingByAadharCardNumberAsync(long aadharCardNumber)
        {
            return await _applicantAccountMappingRepository.GetApplicantAccountMappingByAadharCardNumberDtoAsync(aadharCardNumber);
        }

        public async Task<ApplicantAccountMappingRequestDto> GetMappingByPanNumberAsync(string panNumber)
        {
            return await _applicantAccountMappingRepository.GetMappingByPanNumberDtoAsync(panNumber);
        }

        public async  Task<ApplicantAccountMappingRequestDto> GetApplicantAccountMappingByPhoneNumberAsync(string phoneNumber)
        {
            return await _applicantAccountMappingRepository.GetApplicantAccountMappingByPhoneNumberAsync(phoneNumber);
        }

        public async  Task<ApplicantAccountMappingRequestDto> GetApplicantAccountByApplicantNameAsync(string FirstName, string LastName)
        {
            return await _applicantAccountMappingRepository.GetApplicantAccountMappingByApplicantNameAsync(FirstName, LastName);
        }

        public async Task<List<GetAllApplicantAccountMappingRequestDto>> GetAllApplicantAccountingMappingsAsync()
        {
            return await _applicantAccountMappingRepository.GetAllApplicantAccountMappingsAsync();
        }

        public async Task<int> MapApplicantAccountMapping()
        {
            return await _applicantAccountMappingRepository.MapApplicantAccountMapping();
        }
    }
}
