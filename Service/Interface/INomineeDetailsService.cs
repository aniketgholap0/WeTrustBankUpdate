using Microsoft.AspNetCore.Mvc;
using WeTrustBank.Model;

namespace WeTrustBank.Service.Interface
{
    public interface INomineeDetailsService
    {

        Task<int> MapNomineeDetailsByApplicantsID(NomineeDetailsByApplicantsID nomineeDetailsByApplicantsID);
        Task<int> MapNomineeDetailsByApplicantAadharCardNumber(MapNomineeDetailsByApplicantAadharCardNumberRequestDto mapNomineeDetailsByApplicantAadharCardNumberRequestDto);
        Task<int> MapNomineeDetailsByApplicantPanNumber(MapNomineeDetailsByApplicantPanNumberDto mapNomineeDetailsByApplicantPanNumber);
        Task<int> UpdateNomineeDetailsByNomineeAadharNumber(NomineeDetailsUpdateByAadharCardNumberDto nomineeDetailsUpdateByAadharCardNumberDto);
        Task<int> UpdateNomineeDetailsByNomineePanNumber(NomineeDetailsUpdateByPanNumberDto nomineeDetailsUpdateByPanNumberDto);
        Task<int> UpdateNomineeDetailsByNomineeDeatilsID(NomineeDetailsUpdateByNomineeDetailsIDDto nomineeDetailsUpdateByNomineeDetailsIDDto);
        Task<int> DeleteNomineeByNomineeeDetailsID(int nomineeDeatilsID);
        Task<int> DeleteNomineeByNomineeAadharCardNumber(long aadharCardNumber);
        Task<int> DeleteNomineeByNomineePanNumber(string panNumber);
        Task<int> DeleteNomineeByApplicantsID(int applicantsID);
        Task<int> DeleteNomineeByApplicantAadharCardNuber(long aadharCardNumber);
        Task<int> DeleteNomineeByApplicantPanNumber(string PanNumber);
    }
}
