using Microsoft.AspNetCore.Mvc;
using System.Security;
using WeTrustBank.Service.Concrete;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardStatusController : Controller
    {
        private readonly ICardStatusService _cardStatusService;

        public CardStatusController(ICardStatusService cardStatusService)
        {
            _cardStatusService = cardStatusService;
        }

        [HttpPost("CreateCardStatus")]
        public async Task<IActionResult> CreateCardStatus(string cardStatusName, string createdBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cardStatusName) || string.IsNullOrWhiteSpace(createdBy))
                {
                    return BadRequest("Must Pass cardStatusName and created By");
                }
                var result = await _cardStatusService.createCardStatus(cardStatusName, createdBy);
                return result == -1 ? BadRequest("Card status name is present") : Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("UpadteCardStatus")]
        public async Task<IActionResult> UpdateCardStatus(int cardStatusId, string cardStatusName, string updatedBy)
        {
            try
            {
                if (cardStatusId <= 0 || string.IsNullOrWhiteSpace(cardStatusName) || string.IsNullOrWhiteSpace(updatedBy))
                {
                    return BadRequest("Must Pass cardStatusId, cardStatusName and updatedBy");
                }

                var result = await _cardStatusService.UpdateCardStatus(cardStatusId, cardStatusName, updatedBy);
                return result == -1 ? BadRequest("Card status name is already present") : Ok(result);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpGet("GetAllCardStatus")]
        public async Task<IActionResult> GetAllCardStatus()
        {
            try
            {
                var result = await _cardStatusService.GetAllCardStatus();
                return result.Count == 0 ? NoContent() : Ok(result);

            }
            catch (Exception ex )
            {
                throw;
            }
        }

        [HttpGet("GetCardStatusById")]
        public async Task<IActionResult> GetCardStatusById(int cardStatusId)
        {
            try
            {
                if (cardStatusId <= 0)
                {
                    return BadRequest("Must Pass cardStatusId");
                }
                var result = await _cardStatusService.GetCardStatusById(cardStatusId);
                return result == null ? NotFound("Card status not found or not available") : Ok(result);
            }
            catch (Exception ex)
            {
                throw;

            }
        }


        [HttpDelete("DeleteCardStatusById")]
        public async Task<IActionResult> DeleteCardStatusById(int CardStatusById)
        {
            try
            {
                if(CardStatusById <= 0)
                {
                    return BadRequest("Must Pass CardStatusById or else it throw an error");
                }
                var result = await _cardStatusService.DeleteCardStatusById(CardStatusById);
                return result == -1 ? NotFound("Card status not found or not available") : Ok(result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

}   }    