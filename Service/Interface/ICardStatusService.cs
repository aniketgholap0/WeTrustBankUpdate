using WeTrustBank.Model;

namespace WeTrustBank.Service.Interface
{
    public interface ICardStatusService
    {
        Task<int> createCardStatus(string cardStatusName, string createdBy);
        Task<int> UpdateCardStatus(int cardStatusId, string cardStatusName, string updatedBy);
        Task<List<CardStatus>>GetAllCardStatus();
        Task<CardStatus>GetCardStatusById(int cardStatusId);
        Task<int> DeleteCardStatusById(int cardStatusId);
    }
}
