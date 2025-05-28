using System.Threading.Tasks;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Service.Concrete
{
    public class CardStatusService : ICardStatusService
    {
        private readonly ICardStatusRepository _cardStatusRepository;

        public CardStatusService(ICardStatusRepository cardStatusRepository)
        {
            _cardStatusRepository = cardStatusRepository;
        }

        public async  Task<int> createCardStatus(string cardStatusName, string createdBy)
        {
            return await _cardStatusRepository.CreateCardStatus(cardStatusName, createdBy);
        }

        public Task<int> UpdateCardStatus(int cardStatusId, string cardStatusName, string updatedBy)
        {
           return _cardStatusRepository.UpdateCardStatus(cardStatusId, cardStatusName, updatedBy);
        }

        public Task<List<CardStatus>> GetAllCardStatus()
        {
            return _cardStatusRepository.GetAllCardStatus();    
        }

        public Task<CardStatus> GetCardStatusById(int cardStatusId)
        {
            return _cardStatusRepository.GetCardStatusById(cardStatusId);
        }

        public Task<int> DeleteCardStatusById(int cardStatusId)
        {
            return _cardStatusRepository.DeleteCardStatusById(cardStatusId);
        }
    }
}
