using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.VisualBasic;
using System.Security.Cryptography.Pkcs;
using WeTrustBank.Model;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NomineeDetailsController : Controller
    {
        private readonly INomineeDetailsService _nomineeDetailsService;

        public NomineeDetailsController(INomineeDetailsService nomineeDetailsService)
        {
            _nomineeDetailsService = nomineeDetailsService;
        }

        [HttpPost("MapNomineeDetailsByApplicantsID")]
        public async Task<IActionResult> MapNomineeDetailsByApplicantsID([FromBody] NomineeDetailsByApplicantsID nomineeDetailsByApplicantsID)
        {

            try
            {
                if (nomineeDetailsByApplicantsID == null)
                {
                    BadRequest("Pass all the parameters");
                }

                var result = await _nomineeDetailsService.MapNomineeDetailsByApplicantsID(nomineeDetailsByApplicantsID);
                return result == -1 ? Conflict("ApplicantId Does Not Exist")
                    :  result == -2 ? Conflict("Nominee already exists for this Applicant")
                    : Ok("NomineeDetails Updated Sucessfully To Applicant");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("MapNomineeDetailsByApplicantAadharCardNumber")]
        public async Task<IActionResult> MapNomineeDetailsByApplicantAadharCardNumber(MapNomineeDetailsByApplicantAadharCardNumberRequestDto mapNomineeDetailsByApplicantAadharCardNumberRequestDto)
        {
            try
            {
                if (mapNomineeDetailsByApplicantAadharCardNumberRequestDto == null)
                {
                    BadRequest("Pass all the parameters");
                }

                var result = await _nomineeDetailsService.MapNomineeDetailsByApplicantAadharCardNumber(mapNomineeDetailsByApplicantAadharCardNumberRequestDto);
                return result == -1 ? Conflict("Applicant Id Does not Found")
                   : result == -2 ? Conflict("Nominee already exists for this Applicant")
                   : Ok("NomineeDeatils Updated Sucessfully to Applicant By AadharCardNumber");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost("MapNomineeDetailsByPanNumber")]
        public async Task<IActionResult> MapNomineeDetailsByPanNumber(MapNomineeDetailsByApplicantPanNumberDto mapNomineeDetailsByApplicantPanNumber)
        {
            try
            {
                if(mapNomineeDetailsByApplicantPanNumber == null)
                {
                    BadRequest("Pass all the parameters");
                }
                var result = await _nomineeDetailsService.MapNomineeDetailsByApplicantPanNumber(mapNomineeDetailsByApplicantPanNumber);
                return result == -1 ? Conflict("ApplicantID Does not Exist")
                   : result == -2 ?Conflict("Nominee already exists for this Applicant")
                   : Ok("NomineeDeatils Updated Sucessfully to Applicant By PanNube");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("UpdateNomineeDetailsByNomineeAadharNumber")]
        public async Task<IActionResult> UpdateNomineeDetailsByNomineeAadharNumber([FromBody] NomineeDetailsUpdateByAadharCardNumberDto nomineeDetailsUpdateByAadharCardNumber)
        {
            try
            {
                if (nomineeDetailsUpdateByAadharCardNumber == null)
                {
                    BadRequest("Pass all the parameters");
                }

                var result = await _nomineeDetailsService.UpdateNomineeDetailsByNomineeAadharNumber(nomineeDetailsUpdateByAadharCardNumber);
                return result == -1 ? Conflict("No Nominee Details Found With this AadharCardNumber")
                            : Ok("Nominee Details Updated Sucessfully To Applicant");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPut("UpdateNomineeDetailsByNomineePanNumber")]
        public async Task<IActionResult> UpdateNomineeDetailsByNomineePanNumber([FromBody] NomineeDetailsUpdateByPanNumberDto nomineeDetailsUpdateByPanNumberDto)
        {
            try
            {
                if(nomineeDetailsUpdateByPanNumberDto == null)
                {
                    BadRequest("Pass all the parameters");
                }

                var result = await _nomineeDetailsService.UpdateNomineeDetailsByNomineePanNumber(nomineeDetailsUpdateByPanNumberDto);
                return result == -1 ? Conflict("No Nominee Deatils Found With this PanNumber")
                    : Ok("Nominee Details UpdatedSucessfully To Applicant");

            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPut("UpdateNomineeDetailsByNomineeI")]
        
        public async Task<IActionResult> UpdateNomineeDetailsByNomineeId([FromBody] NomineeDetailsUpdateByNomineeDetailsIDDto nomineeDetailsUpdateByNomineeDetailsIDDto)
        {
            try
            {
                if(nomineeDetailsUpdateByNomineeDetailsIDDto == null)
                {
                    BadRequest("Pass all the parameters");
                }
                var result = await _nomineeDetailsService.UpdateNomineeDetailsByNomineeDeatilsID(nomineeDetailsUpdateByNomineeDetailsIDDto);
                return result == -1 ? Conflict("No NomineeDeatilsId Found ")
                    : Ok("NomineeDetails Updated Sucessfully By NomineeDetailsId");

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("DeleteNomineeByNomineeeDetailsID")]
        public async Task<IActionResult> DeleteNomineeByNomineeeDetailsID( int nomineeDetailsID)
        {
            try
            {
                if(nomineeDetailsID <= 0)
                {
                    return BadRequest("Pass the NomineeDetailsId");
                }
                var result = await _nomineeDetailsService.DeleteNomineeByNomineeeDetailsID(nomineeDetailsID);
                return result == -1 ? Conflict("NomineeDeatilsID Does not Exist")
                    : Ok("By Using NomineeDeatilsID NomineeDeleted Sucessfully");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("DeleteNomineeByNomineeAadharNumber")]
       public async Task<IActionResult> DeleteNomineeByNomineeAadharNumber( long aadharCardNumber )
       {
            try
            {
                if (aadharCardNumber <=0)
                {
                    return BadRequest("Pass the AadharCardNumber ");
                }

                var result = await _nomineeDetailsService.DeleteNomineeByNomineeAadharCardNumber(aadharCardNumber);
                return result == -1 ? Conflict("NomineeAadharNumber Doesnot Exist")
                   : Ok("By Using NomineeAadharCard NomineeDeleted Sucessfully");
            }
            catch (Exception ex)
            {
                throw;
            }
       }

        [HttpDelete("DeleteNomineeByNomineePanNumber")]
        public async Task<IActionResult> DeleteNomineeByNomineePanNumber( string panNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(panNumber))
                {
                    return BadRequest("Pass the panNumber");
                }

                var result = await _nomineeDetailsService.DeleteNomineeByNomineePanNumber(panNumber);
                return result == -1 ? Conflict("PanNumber Does not Exist")
                    : Ok("Deleted Sucessfully");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("DeleteNomineeByApplicantsID")]
        public async Task<IActionResult> DeleteNomineeByApplicantsID( int applicantsID)
        {
            try
            {
                if(applicantsID <= 0)
                {
                    return BadRequest("Pass the ApplicantsID");
                }

                var result = await _nomineeDetailsService.DeleteNomineeByApplicantsID(applicantsID);
                return result == -1 ? Conflict("ApplicantsID Does not exists ")
                    : Ok("Deleted Sucessfully");

            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("DeleteNomineeByApplicantAadharCardNumber")]
        public async Task<IActionResult> DeleteNomineeByApplicantAadharCardNumber( long aadharCardNumber)
        {
            try
            {
                if(aadharCardNumber <= 0)
                {
                    return BadRequest("Pass the AadharCardNumber");
                }

                var result = await _nomineeDetailsService.DeleteNomineeByApplicantAadharCardNuber(aadharCardNumber);
                return result == -1 ? Conflict("Applicant not found")
                    : result == -2 ? Conflict("no nominees are linked to the applicant")
                    : Ok("Deleted Sucessfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("DeleteNomineeByApplicantPanNumber")]
        public async Task<IActionResult> DeleteNomineeByApplicantPanNumber( string panNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(panNumber))
                {
                    return BadRequest("Pass the PanNumber");
                }

                var result = await _nomineeDetailsService.DeleteNomineeByApplicantPanNumber(panNumber);
                return result == -1 ? Conflict("Applicant not found")
                    : result == -2 ? Conflict("no nominees are linked to the applicant")
                    : Ok("Deleted Sucessfully");
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
