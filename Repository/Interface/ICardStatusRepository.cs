using WeTrustBank.Model;

namespace WeTrustBank.Repository.Interface
{
    public interface ICardStatusRepository
    {
        Task<int> CreateCardStatus(string cardStatusName , string createdBy);
        Task<int> UpdateCardStatus(int cardStatusId, string cardStatusName, string updatedBy);
        Task<List<CardStatus>> GetAllCardStatus();
        Task<CardStatus> GetCardStatusById(int cardStatusId);
        Task<int> DeleteCardStatusById(int cardStatusId);
    }
}
