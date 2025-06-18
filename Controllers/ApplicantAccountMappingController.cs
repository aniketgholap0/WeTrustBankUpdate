using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using WeTrustBank.Model;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicantAccountMappingController : Controller
    {
        private readonly IApplicantAccountMappingService _applicantAccountMappingService;

        public ApplicantAccountMappingController(IApplicantAccountMappingService applicantAccountMappingService)
        {
            _applicantAccountMappingService = applicantAccountMappingService;
        }

        [HttpPost("CreateApplicantAccountMapping")]
        public async Task<IActionResult> CreateApplicantAccountMapping([FromBody] ApplicantAccountMappingDto applicantAccountMappingDto)
        {
            try
            {
                if (applicantAccountMappingDto == null)
                {
                    return BadRequest("Parameters both need to pass");
                }

                var result = await _applicantAccountMappingService.CreateApplicantAccountMapping(applicantAccountMappingDto);

                return result == -1 ? Conflict("There is no applicant with the combination of aandharnumber and pan number")
                     : result == -2 ? Conflict("Aadhaar number and PAN number already exist. Action cannot be performed")
                     : result == -3 ? Conflict("No account number was found")
                     : result == -4 ? Conflict("Account status is not Active")
                     : Ok("Account number is mapped to applicant");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("UpdateApplicantAccountMappingByAadharNumber")]
        public async Task<IActionResult> UpdateApplicantAccountMappingByAadharNumber(long aadharCardNumber, string accountStatusName, string updatedBy)
        {
            try
            {
                if (aadharCardNumber <= 0 || string.IsNullOrWhiteSpace(accountStatusName) || string.IsNullOrWhiteSpace(updatedBy))
                {
                    return BadRequest("All three parameters should pass");
                }

                var result = await _applicantAccountMappingService.UpdateApplicantAccountMappingByAadharCardNumber(aadharCardNumber, accountStatusName, updatedBy);

                return result == -1 ? Conflict("There is no applicant with the aadhaar number")
                     : result == -2 ? Conflict("Applicant not mapped to ApplicantAccountMapping")
                     : result == -3 ? Conflict("Account status is not Active")
                     : Ok("Applicant account mapping updated successfully");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPut("GetApplicantAccountMappingByPanNumber")]
        public async Task<IActionResult> GetApplicantAccountMappingByPanNumber(string panNumber, string accountStatusName, string createdBy)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(panNumber) || String.IsNullOrWhiteSpace(accountStatusName) || String.IsNullOrWhiteSpace(createdBy))
                {
                    return BadRequest("All three parameters should pass");
                }

                var result = await _applicantAccountMappingService.UpdateApplicantAccountMappingByPanNumber(panNumber, accountStatusName, createdBy);
                return result == -1 ? Conflict("There is no applicant with the pan number")
                     : result == -2 ? Conflict("Applicant not mapped to ApplicantAccountMapping")
                     : result == -3 ? Conflict("Account status is not Active")
                     : Ok("Applicant account mapping updated successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("UpdateApplicantAccountMappingByApplicantId")]
        public async Task<IActionResult> UpdateApplicantAccountMappingByApplicantId(int applicantsId, string accountStatusName, string updatedBy)
        {
            try
            {
               if(applicantsId<=0 || string.IsNullOrWhiteSpace(accountStatusName) || string.IsNullOrWhiteSpace(updatedBy))
               {
                    return BadRequest("All three parameters should pass");
               }

                var result = await _applicantAccountMappingService.UpdateApplicantAccountMappingByApplicantId(applicantsId, accountStatusName, updatedBy);
                return result == -1 ? Conflict("There is no ApplicantId in the ApplicantAccountMapping")
                     : result == -2 ? Conflict("Applicant not mapped to ApplicantAccountMapping")
                     : result == -3 ? Conflict("Account status is not Active")
                     : Ok("Applicant account mapping updated successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
