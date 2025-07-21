using Microsoft.AspNetCore.Mvc.Routing;
using System.Globalization;
using WeTrustBank.Model;

namespace WeTrustBank.Service.Interface
{
    public interface IApplicantsService
    {
        Task<int> CreateApplicant(ApplicantCreateDto applicantRequestDto);
        Task<int> UpdateApplicant(ApplicantUpdateDto applicantUpdateDto);
        Task<ApplicantDto> GetApplicantById(int applicantId);
        Task<List<GetAllApplicantsDto>> GetAllApplicants();
        Task<List<ApplicantDto>> GetApplicantsBetween(DateTime startDate, DateTime endDate);
        Task<int> UpdateApplicantByAadharCardNumber(ApplicantUpdateByAadharCardNumberDto applicantsUpdateByAadharCardNumberDto);
        Task<int> UpdateApplicantByPanNumber(ApplicantUpdatebyPanNumberDto applicantsUpdateByPanNumberDto);
        Task<int> UpdateApplicantByPhoneNumber(ApplicantUpdateByPhoneNumberDto applicantUpdateByPhoneNumberDto);
       
}   }