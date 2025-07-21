using Microsoft.AspNetCore.Mvc;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountTypeController : Controller
    {
        public readonly IAccountTypeService _accountTypeService;

        public AccountTypeController(IAccountTypeService accountTypeService)
        {
            _accountTypeService = accountTypeService;
        }

        [HttpPost("CreateAccountType")]
        public async Task<IActionResult> CreateAccountType(string accountTypeName, string createdBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accountTypeName) || string.IsNullOrWhiteSpace(createdBy))
                {
                    return BadRequest("Account type name and created by cannot be null or empty");
                }

                var result = await _accountTypeService.CreateAccountType(accountTypeName, createdBy);

                return result == -1 ? BadRequest("Account type name already exists") : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("UpdateAccountType")]
        public async Task<IActionResult> UpdateAccountType(int accountTypeId, string accountTypeName, string updatedBy)
        {
            try
            {
                if (accountTypeId <= 0 || string.IsNullOrWhiteSpace(accountTypeName) || string.IsNullOrWhiteSpace(updatedBy))
                {
                    return BadRequest("Account type ID, name and updated by cannot be null or empty");
                }
                
                var result = await _accountTypeService.UpdateAccountType(accountTypeId, accountTypeName, updatedBy);

                return result == -1 ? Conflict("Account type name doesn't exists") : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetAllAccountTypes")]
        public async Task<IActionResult> GetAllAccountType()
        {
            try
            {
                var result = await _accountTypeService.GetAllAccountTypes();
                return result.Count == 0 ? NoContent() : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetAccountTypeById")]
        public async Task<IActionResult> GetAccountTypeById(int accountTypeId)
        {
            try
            {
                if (accountTypeId <= 0)
                {
                    return BadRequest("Account type ID cannot be null or empty");
                }
                
                var result = await _accountTypeService.GetAccountTypeById(accountTypeId);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("DeleteAccountTypeById")]
        public async Task<IActionResult> DeleteAccountTypeById(int accountTypeId)
        {
            try
            {
                if (accountTypeId <= 0)
                {
                    return BadRequest("Account type ID cannot be null or empty");
                }

                var result = await _accountTypeService.DeleteAccountTypeById(accountTypeId);
                
                return result == -1 ? NotFound("Account Type ID doesn't exists") : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}