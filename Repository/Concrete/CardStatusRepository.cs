using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Data;
using System.Data.SqlTypes;
using WeTrustBank.Common;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;

namespace WeTrustBank.Repository.Concrete
{
    public class CardStatusRepository : ICardStatusRepository
    {
        private readonly IOptions<AppSettings> _appSettings;

        public CardStatusRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<int> CreateCardStatus(string cardStatusName, string createdBy)
        {
            try
            {
                using SqlConnection connection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand command = new("[dbo].[Usp_Create_CardStatus]", connection);
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                command.Parameters.AddWithValue("@CardStatusName", cardStatusName);
                command.Parameters.AddWithValue("@CreatedBy ", createdBy);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteScalarAsync();
                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<int> UpdateCardStatus(int cardStatusId, string cardStatusName, string updatedBy)
        {
            try
            {
                using SqlConnection connection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand command = new("[dbo].[Usp_Update_CardStatus]", connection);
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                command.Parameters.AddWithValue("@CardStatusId", cardStatusId);
                command.Parameters.AddWithValue("@CardStatusName", cardStatusName);
                command.Parameters.AddWithValue("@UpdatedBy",updatedBy);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteScalarAsync();
                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<CardStatus>> GetAllCardStatus()
        {
            List<CardStatus> cardStatuses = [];
            try
            {
                using SqlConnection connection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand command = new("[dbo].[Usp_GetAll_CardStatus]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var cardStatus = new CardStatus
                    {
                        CardStatusId   = reader["CardStatusId"] != DBNull.Value
                                         ? Convert.ToInt32(reader["CardStatusId"]) : 0,
                        CardStatusName = reader["CardStatusName"] != DBNull.Value
                                         ? Convert.ToString(reader["CardStatusName"]) : string.Empty,
                        CreatedBy      = reader["CreatedBy"] != DBNull.Value
                                         ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        Createdon      = reader["CreatedOn"] != DBNull.Value
                                         ? Convert.ToDateTime(reader["CreatedOn"]) : DateTime.MinValue
                    };

                    cardStatuses.Add(cardStatus);
                }
            }
            catch (Exception ex)
            {
                throw; 
            }
            return cardStatuses;
        }

        public async Task<CardStatus> GetCardStatusById(int cardStatusId)
        {
            try
            {
                CardStatus cardStatus = null;
                using SqlConnection connection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand command = new("[dbo].[Usp_Get_CardStatusById]", connection);
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                command.Parameters.AddWithValue("@CardStatusId", cardStatusId);
                await connection.OpenAsync();
                using SqlDataReader reader = await command.ExecuteReaderAsync();

                if(await reader.ReadAsync())
                {
                    cardStatus = new CardStatus
                    {
                        CardStatusId   = reader["CardStatusId"] != DBNull.Value
                                         ? Convert.ToInt32(reader["CardStatusId"]) : 0,
                        CardStatusName = reader["CardStatusName"] != DBNull.Value
                                         ? Convert.ToString(reader["CardStatusName"]) : string.Empty,
                    };
                }

                return cardStatus;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public  async Task<int> DeleteCardStatusById(int cardStatusId)
        {
            try
            {
               using SqlConnection connection = new(_appSettings.Value.WeTrustBankDBConnection);
               using SqlCommand command = new("[dbo].[Usp_Delete_CardStatusById]", connection);
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                command.Parameters.AddWithValue("@CardStatusId", cardStatusId);
                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteScalarAsync();
                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;


            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
