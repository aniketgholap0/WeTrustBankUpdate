using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;
using WeTrustBank.Service.Concrete;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicantsController : Controller
    {
        public readonly IApplicantsService _applicantsService;

        public ApplicantsController(IApplicantsService applicantsService)
        {
            _applicantsService = applicantsService;
        }

        [HttpPost("CreateApplicant")]
        public async Task<IActionResult> CreateApplicant([FromBody] ApplicantCreateDto applicantRequestDto)
        {
            try
            {
                if (applicantRequestDto == null)
                {
                    return BadRequest("Applicant parameters Must Pass");
                }

                var result = await _applicantsService.CreateApplicant(applicantRequestDto);
                return result == -1 ? Conflict("Applicant is already exists")
                                    : Ok("Applicant created successfully.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("UpdateApplicant")]
        public async Task<IActionResult> UpdateApplicant([FromBody] ApplicantUpdateDto applicantUpdateDto)
        {
            try
            {
                if(applicantUpdateDto == null)
                {
                    return BadRequest("Applicant Parameters Must Pass");
                }
                var result = await _applicantsService.UpdateApplicant(applicantUpdateDto);
                return result == -1 ? Conflict("ApplicantID Doesnot Exist")
                                      : Ok("Applicant Created Sucessfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetApplicantById")]
        public async Task<IActionResult> GetApplicantById( int applicantId)
        {
            try
            {
                if(applicantId <= 0)
                {
                    return BadRequest("ApplicantId Parameter");
                }
                var result = await _applicantsService.GetApplicantById(applicantId);
                return result == null ? NotFound() : Ok(result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetAllApplicants")]
        public async Task<IActionResult> GetAllApplicants()
        {
            try
            {
                var result = await _applicantsService.GetAllApplicants();
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        [HttpGet("GetApplicantsBetween")]
        public async Task<IActionResult> GetApplicantsBetween(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate >= endDate)
                {
                    return BadRequest("Start Date should be less than End Date");
                }

                if (startDate > DateTime.Now)
                {
                    return BadRequest("Start date cannot be in the future Date");
                }
                var result = await _applicantsService.GetApplicantsBetween(startDate, endDate);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("UpdateApplicantByAadharCardNumber")]
        public async Task<IActionResult> UpdateApplicantByAadharCardNumber([FromBody] ApplicantUpdateByAadharCardNumberDto applicantsUpdateByAadharCardNumberDto)
        {
            try
            {
                if(applicantsUpdateByAadharCardNumberDto == null)
                {
                    return BadRequest("Please pass all the parameters in the applicantsUpdateByAadharCardNumberDto.");
                }

                var result = await _applicantsService.UpdateApplicantByAadharCardNumber(applicantsUpdateByAadharCardNumberDto);
                return result == -1 ? Conflict("The provided Aadhar card number does not exist.")
                     : Ok("Applicant Updated Successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("UpdateApplicantByPanNumber")]
        public async Task<IActionResult> UpdateApplicantByPanNumber([FromBody] ApplicantUpdatebyPanNumberDto applicantUpdatebyPanNumberDto)
        {
            try
            {
                if(applicantUpdatebyPanNumberDto == null)
                {
                    return BadRequest("Please pass all the parameters in the applicantUpdatebyPanNumberDto.");
                }

                var result = await _applicantsService.UpdateApplicantByPanNumber(applicantUpdatebyPanNumberDto);
                return result == -1 ? Conflict("The PAN number does not exist.")
                    : Ok("Applicant Updated Successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("UpdateApplicantByPhoneNumber")]
        public async Task<IActionResult> UpdateApplicantByPhoneNumber([FromBody] ApplicantUpdateByPhoneNumberDto applicantUpdateByPhoneNumberDto)
        {
            try
            {
                if(applicantUpdateByPhoneNumberDto == null)
                {
                    return BadRequest("Please pass all the parameters in the applicantUpdatebyPhoneNumberDto.");
                }

                var result = await _applicantsService.UpdateApplicantByPhoneNumber(applicantUpdateByPhoneNumberDto);
                return result == -1 ? Conflict("The Phone number does not exist.")
                    : Ok("Applicant Updated Successfully");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
