using System.Reflection.Metadata.Ecma335;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Service.Concrete
{
    public class ApplicantsService : IApplicantsService
    {
        private readonly IApplicantsRepository _applicantsRepository;

        public ApplicantsService(IApplicantsRepository applicantRepository)
        {
            _applicantsRepository = applicantRepository;
        }

        public async Task<int> CreateApplicant(ApplicantCreateDto applicantRequestDto)
        {
            return await _applicantsRepository.CreateApplicant(applicantRequestDto);
        }

        public async Task<int> UpdateApplicant(ApplicantUpdateDto applicantUpdateDto)
        {
            return await _applicantsRepository.UpdateApplicant(applicantUpdateDto);
        }

        public async Task<ApplicantDto> GetApplicantById(int applicantId)
        {
            return await _applicantsRepository.GetApplicantById(applicantId);
        }

        public async Task<List<GetAllApplicantsDto>> GetAllApplicants()
        {
            return await _applicantsRepository.GetAllApplicants();
        }

        public async Task<List<ApplicantDto>> GetApplicantsBetween(DateTime startDate, DateTime endDate)
        {
            return await _applicantsRepository.GetApplicantsBetween(startDate, endDate);
        }
    }
}
