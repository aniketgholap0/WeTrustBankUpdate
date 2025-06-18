using Microsoft.AspNetCore.Mvc;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountStatusController : Controller
    {
        public readonly IAccountStatusService _accountTypeService;
        public AccountStatusController(IAccountStatusService accountStatusService)
        {
            _accountTypeService = accountStatusService;
        }

        [HttpPost("CreateAccountStatus")]
        public async Task<IActionResult> CreateAccountStatus(string accountStatusName, string createdBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accountStatusName) || string.IsNullOrWhiteSpace(createdBy))
                {
                    return BadRequest("AccountstatusName and CreatedBy Must Pass the Both Values");
                }

                var result = await _accountTypeService.CreateAccountStatus(accountStatusName, createdBy);

                return result == -1 ? BadRequest("AccountStatusName Already Exist ") : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPut("UpdateAccountStatus")]
        public async Task<IActionResult> UpdateAccountStatus(int accountStatusId, string accountStatusName, string updatedBy)
        {

            try
            {
                if (accountStatusId <= 0 || string.IsNullOrWhiteSpace(accountStatusName) || string.IsNullOrWhiteSpace(updatedBy))
                {
                    return BadRequest("AccountStatusId, AccountStatusName and UpdatedBy cannot be null or empty");
                }

                var result = await _accountTypeService.UpdateAccountStatus(accountStatusId, accountStatusName, updatedBy);
                return result == -1 ? BadRequest("AccountStatusName Already Exist ") : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetAllAccountStatus")]

        public async Task<IActionResult> GetAllAccountStatus()
        {
            try
            {
                var result = await _accountTypeService.GetAllAccountStatus();
                return result.Count == 0 ? NoContent() : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetAccountStatusById")]
        public async Task<IActionResult> GetAccountStatusById(int accountStatusId)
        {
            try
            {
                if (accountStatusId <= 0)
                {
                    return BadRequest("Parameter accountStatusId Must Pass");
                }
                var result = await _accountTypeService.GetAllAccountStatus();
                var accountStatus = result.FirstOrDefault(a => a.AccountStatusId == accountStatusId);
                
                return result.Count == 0 ? NoContent() : Ok(accountStatus);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("DeleteAccountStatusById")]
        public async Task<IActionResult> DeleteAccountStatusById(int accountStatusId)
        {
            try
            {
                if (accountStatusId <= 0)
                {
                    return BadRequest("Parameter accountStatusId Must Pass");
                }
                var result = await _accountTypeService.DeleteAccountStatusId(accountStatusId);
                return result == -1 ? BadRequest("AccountStatusId Not Found") : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
