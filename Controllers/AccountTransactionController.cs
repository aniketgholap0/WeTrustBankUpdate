
using Microsoft.AspNetCore.Mvc;
using WeTrustBank.Model;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountTransactionController : ControllerBase
    {
        private readonly IAccountTransactionService _accountTransactionService;

        public AccountTransactionController(IAccountTransactionService accountTransactionService)
        {
            _accountTransactionService = accountTransactionService;
        }

        [HttpPost("InsertAccountTransaction")]
        public async Task<IActionResult> InsertAccountTransaction([FromBody] InsertAccountTransactionDto insertAccountTransactionDto)
        {
            try
            {
                if (insertAccountTransactionDto == null)
                {
                    return BadRequest("Pass all the parameters");
                }
                var result = await _accountTransactionService.InsertAccountTransaction(insertAccountTransactionDto);

                return result == -1 ? Conflict("DebitCardNumber Does not Exist")
                    : result == -2 ? Conflict("DebitCardNumber Input will be C OR D")
                    : result == -3 ? Conflict("Available Balance is Null")
                    : result == -4 ? Conflict("InSufficent Funds")
                    : Ok("Transaction Sucessfully Completed");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetAccountTransactionByAccountTransActionID")]
        public async Task<IActionResult> GetAccountTransactionByAccountTransActionID( int accountTransactionId)
        {
            try
            {
                if(accountTransactionId == 0)
                {
                    return BadRequest("Pass the Paramter");
                }
                var result = await _accountTransactionService.GetAccountTransactionByAccountTransactionID(accountTransactionId);
                return result == null ? NotFound("No TransactionId Found") 
                    : Ok(result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetAccountTransactionByAccountNumber")]
        public async Task<IActionResult> GetAccountTransactionByAccountNumber(long accountNumber)
        {
            try
            {
                if(accountNumber == 0)
                {
                    return BadRequest("Pass the parameter");
                }
                var result = await _accountTransactionService.GetAccountTransactionByAccountNumber(accountNumber);
                if (result == null || result.Count == 0)
                {
                    return NotFound("No transactions found for the given account number.");
                }

                return Ok(result);
            }
            catch  (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetAccountTransactionByDebitCardNumber")]
        public async Task<IActionResult> GetAccountTransactionByDebitCardNumber(long debitCardNumber)
        {
            try
            {
                if (debitCardNumber == 0)
                {
                    return BadRequest("Pass the Paramter");
                }
                var result = await _accountTransactionService.GetAccountTransactionByDebitCardNumber(debitCardNumber);

                if (result == null || result.Count == 0)
                {
                    return NotFound("No transactions found for the given DebitCardNumber");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetAccountTransactionByRowNumber")]
        public async Task<IActionResult> GetAccountTransactionByRowNumber(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Both pageNumber and pageSize must be greater than 0 ");
                }
                var result = await _accountTransactionService.GetAccountTransactionsAsync(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
