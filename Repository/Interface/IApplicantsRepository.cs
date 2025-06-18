using WeTrustBank.Model;

namespace WeTrustBank.Repository.Interface
{
    public interface IApplicantsRepository
    {

        Task<int> CreateApplicant(ApplicantCreateDto applicantRequestDto);
        Task<int> UpdateApplicant(ApplicantUpdateDto applicantUpdateDto);
        Task<ApplicantDto> GetApplicantById(int applicantId);
        Task<List<GetAllApplicantsDto>> GetAllApplicants();
        Task<List<ApplicantDto>> GetApplicantsBetween(DateTime startDate, DateTime endDate);
    }
}
