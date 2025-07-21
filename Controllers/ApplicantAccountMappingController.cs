using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
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
                if (applicantsId <= 0 || string.IsNullOrWhiteSpace(accountStatusName) || string.IsNullOrWhiteSpace(updatedBy))
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

        [HttpGet("GetApplicantAccountMappingByAadharCardNumber")]
        public async Task<IActionResult> GetApplicantAccountMappingByAadharCard([FromQuery] long aadharCardNumber)
        {
            try
            {
                if (aadharCardNumber <= 0 || aadharCardNumber.ToString().Length != 12)
                {
                    return BadRequest("Pass the aadharCardNumber as input which consists of exactly 12 digits.");
                }

                var result = await _applicantAccountMappingService.GetApplicantAccountMappingByAadharCardNumberAsync(aadharCardNumber);

                return result == null ? NotFound("No applicant found with the given Aadhaar number")
                      : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetApplicantAccountMappingByPanNumber")]
        public async Task<IActionResult> GetApplicantAccountMappingByPanNumber([FromQuery] string panNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(panNumber))
                {
                    return BadRequest("Pass  parameter value");
                }
                var result = await _applicantAccountMappingService.GetMappingByPanNumberAsync(panNumber);

                return result == null ? NotFound("No applicant found with the given Pannumber")
                      : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpGet("GetApplicantAccountMappingByPhoneNumber")]
        public async Task<IActionResult> GetApplicantAccountMappingByPhoneNumber([FromQuery] string phoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(phoneNumber))
                {
                    return BadRequest("Pass  paramter value");
                }

                var result = await _applicantAccountMappingService.GetApplicantAccountMappingByPhoneNumberAsync(phoneNumber);
                return result == null ? NotFound("No applicant found with the given phone number")
                      : Ok(result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetApplicantAccountMappingByApplicantName")]
        public async Task<IActionResult> GetApplicantAccountMappingByApplicantName([FromQuery] string FirstName, string LastName)
        {
            try
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                {
                    return BadRequest("Pass  parameter value");
                }
                var result = await _applicantAccountMappingService.GetApplicantAccountByApplicantNameAsync(FirstName, LastName);

                return result == null ? NotFound("No applicant found with the given name")
                      : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetAllApplicantAccountMapping")]
        public async Task<IActionResult> GetAllApplicantAccountMapping()
        {
            try
            {
                var result = await _applicantAccountMappingService.GetAllApplicantAccountingMappingsAsync();
                return result.Count == 0 ? NoContent() : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("MapApplicantAccountMapping")]
        public async Task<IActionResult> MapApplicantAccountMapping()
        {
            try
            {
                var result = await _applicantAccountMappingService.MapApplicantAccountMapping();

                if (result == -1)
                    return Conflict("No Applicant Found To Map");

                return Ok("Applicant Account Mapping Successful");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
