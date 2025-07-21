using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using WeTrustBank.Model;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : Controller
    {
        public readonly IBankAccountService _bankAccountService;

        public BankAccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpPost("GenerateBankAccount")]
        public async Task<IActionResult> GenerateBankAccount(String createdBy)
        {
            try
            {
                if (string.IsNullOrEmpty(createdBy))
                {
                    return BadRequest("Created By paramter must pass");
                }
                var result = await _bankAccountService.GenerateBankAccount(createdBy);
                return result == -1 ? BadRequest("BankAccountNumber Already Generated") : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetAllBankAccounts")]
        public async Task<IActionResult> GetAllBankAccounts()
        {
            try
            {
                var result = await _bankAccountService.GetAllBankAccounts();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetBankAccountById")]
        public async Task<IActionResult> GetBankAccountById(int bankAccountId)
        {
            try
            {
                if (bankAccountId <= 0)
                {
                    return BadRequest("BankAccountById Must Pass With Integer Value");
                }
                var result = await _bankAccountService.GetBankAccountById(bankAccountId);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("BankAccountNumberByAccountNumber")]
        public async Task<IActionResult> BankAccountNumberByAccountNumber(long accountNumber)
        {
            try
            {
                if (accountNumber <= 0 || accountNumber.ToString().Length != 12)
                {
                    return BadRequest("Account Number must be in 12 digits");
                }

                var result = await _bankAccountService.BankAccountNumberByAccountNumber(accountNumber);
                return result == -1 ? Conflict("Account number can not be deleted as this account is mapped to an Applicant")
                            : result == -2 ? NotFound("Account number not found")
                            : Ok("Account number deleted successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("GenerateBulkBankAccounts")]
        public async Task<IActionResult> GenerateBulkBankAccounts(long bankAccountSeries, int maxRows)
        {
            try
            {
                if (bankAccountSeries <= 0 || maxRows <= 0)
                {
                    return BadRequest("BankAccountSeries and MaxRows must be greater than zero");
                }
                var result = await _bankAccountService.GenerateBulkBankAccounts(bankAccountSeries,maxRows);
                return Ok("Bank accounts generated successfully.");
              
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}