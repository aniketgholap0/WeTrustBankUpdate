using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using WeTrustBank.Model;
using WeTrustBank.Repository.Concrete;
using WeTrustBank.Repository.Interface;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Service.Concrete
{
    public class NomineeDetailsService : INomineeDetailsService
    {
        private readonly INomineeDetailsRepository _nomineeDetailsReposistory;

        public NomineeDetailsService(INomineeDetailsRepository nomineeDetailsRepository)
        {
            _nomineeDetailsReposistory = nomineeDetailsRepository;

        }

        public  async Task<int> MapNomineeDetailsByApplicantsID(NomineeDetailsByApplicantsID nomineeDetailsByApplicantsID)
        {
            return await _nomineeDetailsReposistory.MapNomineeDetailsByApplicantsID(nomineeDetailsByApplicantsID);
        }

        public async Task<int> MapNomineeDetailsByApplicantAadharCardNumber(MapNomineeDetailsByApplicantAadharCardNumberRequestDto mapNomineeDetailsByApplicantAadharCardNumberRequestDto)
        {
            return await _nomineeDetailsReposistory.MapNomineeDetailsByApplicantAadharCardNumber(mapNomineeDetailsByApplicantAadharCardNumberRequestDto);
        }

        public async Task<int> MapNomineeDetailsByApplicantPanNumber(MapNomineeDetailsByApplicantPanNumberDto mapNomineeDetailsByApplicantPanNumber)
        {
            return await _nomineeDetailsReposistory.MapNomineeDetailsByApplicantPanNumber(mapNomineeDetailsByApplicantPanNumber);
        }

        public async Task<int> UpdateNomineeDetailsByNomineeAadharNumber(NomineeDetailsUpdateByAadharCardNumberDto nomineeDetailsUpdateByAadharCardNumberDto)
        {
            return await _nomineeDetailsReposistory.UpdateNomineeDetailsByNomineeAadharNumber(nomineeDetailsUpdateByAadharCardNumberDto);
        }

        public async Task<int> UpdateNomineeDetailsByNomineePanNumber(NomineeDetailsUpdateByPanNumberDto nomineeDetailsUpdateByPanNumberDto)
        {
            return await _nomineeDetailsReposistory.UpdateNomineeDetailsByNomineePanNumber(nomineeDetailsUpdateByPanNumberDto);
        }

        public  async Task<int> UpdateNomineeDetailsByNomineeDeatilsID(NomineeDetailsUpdateByNomineeDetailsIDDto nomineeDetailsUpdateByNomineeDetailsIDDto)
        {
            return await _nomineeDetailsReposistory.UpdateNomineeDetailsByNoineeDeatilsID(nomineeDetailsUpdateByNomineeDetailsIDDto);
        }

        public async Task<int> DeleteNomineeByNomineeeDetailsID(int nomineeDeatilsID)
        {
           return await _nomineeDetailsReposistory.DeleteNomineeByNomineeeDetailsID(nomineeDeatilsID);
        }

        public async Task<int> DeleteNomineeByNomineeAadharCardNumber(long aadharCardNumber)
        {
            return await _nomineeDetailsReposistory.DeleteNomineeByNomineeAadharCardNumber(aadharCardNumber);
        }

        public async Task<int> DeleteNomineeByNomineePanNumber(string panNumber)
        {
            return await _nomineeDetailsReposistory.DeleteNomineeByNomineePanNumber(panNumber);
        }

        public async Task<int> DeleteNomineeByApplicantsID(int applicantsID)
        {
            return await _nomineeDetailsReposistory.DeleteNomineeByApplicantsID(applicantsID);
        }

        public async  Task<int> DeleteNomineeByApplicantAadharCardNuber(long aadharCardNumber)
        {
            return await _nomineeDetailsReposistory.DeleteNomineeByApplicantAadharCardNumber(aadharCardNumber);
        }

        public async  Task<int> DeleteNomineeByApplicantPanNumber(string PanNumber)
        {
            return await _nomineeDetailsReposistory.DeleteNomineeByNomineePanNumber(PanNumber);
        }
    }
}