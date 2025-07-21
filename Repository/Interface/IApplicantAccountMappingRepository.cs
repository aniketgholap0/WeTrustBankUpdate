using System.Globalization;
using WeTrustBank.Model;

namespace WeTrustBank.Repository.Interface
{
    public interface IApplicantAccountMappingRepository
    {
        Task<int> CreateApplicantAccountMapping(ApplicantAccountMappingDto applicantAccountMappingDto);

        Task<int> UpdateApplicantAccountMappingByAadharCardNumber(long aadharCardNumber, string accountStatusName, string updatedBy);
         Task<int> UpdateApplicantAccountMappingByPanNumber(string panNumber, string accountStatusName, string updatedBy);
        Task<int> UpdateApplicantAccountMappingByApplicantId(int applicantsId, string accountStatusName, string upadtedBy);
        Task<ApplicantAccountMappingRequestDto> GetApplicantAccountMappingByAadharCardNumberDtoAsync(long aadharCardNumber);
        Task<ApplicantAccountMappingRequestDto> GetMappingByPanNumberDtoAsync(string panNumber);
        Task<ApplicantAccountMappingRequestDto> GetApplicantAccountMappingByPhoneNumberAsync(string phoneNumber);
        Task<ApplicantAccountMappingRequestDto> GetApplicantAccountMappingByApplicantNameAsync(string firstName, string lastName);
        Task<List<GetAllApplicantAccountMappingRequestDto>> GetAllApplicantAccountMappingsAsync();
        Task<int> MapApplicantAccountMapping();
    }
}
