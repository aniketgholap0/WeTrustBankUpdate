using WeTrustBank.Model;

namespace WeTrustBank.Repository.Interface
{
    public interface INomineeDetailsRepository 
    {
        Task<int> MapNomineeDetailsByApplicantsID(NomineeDetailsByApplicantsID nomineeDetailsByApplicantsID);
        Task<int> MapNomineeDetailsByApplicantAadharCardNumber(MapNomineeDetailsByApplicantAadharCardNumberRequestDto mapNomineeDetailsByApplicantAadharCardNumberRequestDto);
        Task<int> MapNomineeDetailsByApplicantPanNumber(MapNomineeDetailsByApplicantPanNumberDto mapNomineeDetailsByApplicantPanNumber);
        Task<int> UpdateNomineeDetailsByNomineeAadharNumber(NomineeDetailsUpdateByAadharCardNumberDto nomineeDetailsUpdateByAadharCardNumberDto);
        Task<int> UpdateNomineeDetailsByNomineePanNumber(NomineeDetailsUpdateByPanNumberDto nomineeDetailsUpdateByPanNumberDto);
        Task<int> UpdateNomineeDetailsByNoineeDeatilsID(NomineeDetailsUpdateByNomineeDetailsIDDto nomineeDetailsUpdateByNomineeDetailsIDDto);
        Task<int> DeleteNomineeByNomineeeDetailsID(int nomineeDeatilsID);
        Task<int> DeleteNomineeByNomineeAadharCardNumber(long aadharCardNumber);
        Task<int> DeleteNomineeByNomineePanNumber(string panNumber);
        Task<int> DeleteNomineeByApplicantsID(int applicantsID);
        Task<int> DeleteNomineeByApplicantAadharCardNumber(long aadharCardNumber);
        Task<int> DeleteNomineeByApplicantPanNumber(string panNumber);
    }
}
