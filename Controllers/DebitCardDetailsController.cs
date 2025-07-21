using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WeTrustBank.Model;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebitCardDetailsController : Controller
    {
        private readonly IDebitCardDetailsService _debitCardDetailsService;
        public DebitCardDetailsController(IDebitCardDetailsService debitCardDetailsService)
        {
            _debitCardDetailsService = debitCardDetailsService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateDebitCardDetails([FromBody] GenerateDebitCardDetails generateDebitCardDetails)
        {
            try
            {
                if (generateDebitCardDetails == null)
                {
                    return BadRequest("Pass All The Parameters");
                }

                var result = await _debitCardDetailsService.GenerateDebitCardDetails(generateDebitCardDetails);
                return result == -1 ? Conflict("Debit Card Number Already Exist")
                    : Ok("DebitCardNumber Sucessfully Created");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> GenerateBulkDebitCardNumber(long debitCardNumberSerires, int maxRows)
        {
            try
            {
                if (debitCardNumberSerires == 0 || maxRows == 0)
                {
                    return BadRequest("Pass the Parameters for both debitCardNumberSerires & maxRows ");
                }

                var result = await _debitCardDetailsService.GenerateBulkDebitCardNumber(debitCardNumberSerires, maxRows);

                return Ok("GeneratedBulkDebitCardNumberSucessfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("MapApplicantDebitCardNumberByPanNumber")]
        public async Task<IActionResult> MapApplicantDebitCardNumberByPanNumber(string panNumber, string createdBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(panNumber) || string.IsNullOrWhiteSpace(createdBy))
                {
                    return BadRequest("pass parameter for both pannumber and createdby");
                }

                var result = await _debitCardDetailsService.MapApplicantDebitCardNumberByPanNumber(panNumber, createdBy);
                return result == -1 ? Conflict("Pan Number Does Not Exist")
                    : result == -2 ? Conflict("This checks if a mapping already exists for the given PAN number")
                    : result == -3 ? Conflict("DebitCardNumber NULL")
                    : result == -4 ? Conflict("CardStatusID NUll")
                    : Ok("Mapped DebitCardNumber Sucessfully  To Applicant");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet(" MapApplicantAccountMappingByAadharCardNumber")]
        public async Task<IActionResult> MapApplicantAccountMappingByAadharCardNumber(string aadharCardNumber,string createdBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(aadharCardNumber) || string.IsNullOrWhiteSpace(createdBy))
                {
                    BadRequest("Pass All the paramters for Both");
                }

                var result = await _debitCardDetailsService.MapApplicantsDebitCardNumberByAadharCardNumber(aadharCardNumber, createdBy);
                return result == -1 ? Conflict("Aadharcardnumber does not exists")
                    : result == -2 ? Conflict("This Checks if a mapping already exists for the given number")
                    : result == -3 ? Conflict("DebitCardNumber Null")
                    : result == -4 ? Conflict("CardStatusID null")
                    : Ok("Mapped DebitCardNumber Sucessfully TO Applicant");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("UpdateBalanceByAccountNumber")]
        public async Task<IActionResult> UpdateBalanceByAccountNumber([FromBody] UpdateBalanceByAccountNumberDto updateBalanceByAccountNumberDto)
        {
            try
            {
                if (updateBalanceByAccountNumberDto == null)
                {
                    return BadRequest("Pass all the parameters");
                }
                var result = await _debitCardDetailsService.UpdateBalanceByAccountNuber(updateBalanceByAccountNumberDto);
                return result == -1 ? Conflict("Account number or amount is missing")
                    : result == -2 ? Conflict("Account Number does not found")
                    : result == -3 ? Conflict(" Insufficient balance for the debit transaction")
                    : result == -4 ? Conflict("must be 'C' (Credit) or 'D' (Debit).")
                    : Ok("Balance updated Sucessfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
