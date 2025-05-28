using Azure.Messaging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlTypes;
using WeTrustBank.Common;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;

namespace WeTrustBank.Repository.Concrete
{
    public class AccountStatusRepository : IAccountStatusRepository
    {
        private readonly IOptions<AppSettings> _appSettings;

        public AccountStatusRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<int> CreateAccountStatus(string accountstatusName, string createdBy)
        {
            try
            {
                using SqlConnection connection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand command = new("[dbo].[Usp_Create_AccountStatus]", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@accountstatusName", accountstatusName);
                command.Parameters.AddWithValue("@createdBy", createdBy);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteScalarAsync();

                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateAccountStatus(int accountStatusId, string accountStatusName, string updatedBy)
        {
            try
            {
                SqlConnection connection = new(_appSettings.Value.WeTrustBankDBConnection);
                SqlCommand command = new("[dbo].[Usp_Update_AccountStatus]", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@AccountStatusId", accountStatusId);
                command.Parameters.AddWithValue("@AccountStatusName", accountStatusName);
                command.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteScalarAsync();
                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<AccountStatus>> GetAllAccountStatus()
        {
            var accountStatusList = new List<AccountStatus>();

            try
            {
                using SqlConnection connection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand command = new("[dbo].[Usp_GetAll_AccountStatus]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var accountStatus = new AccountStatus
                    {
                        AccountStatusId = reader["AccountStatusId"] != DBNull.Value
                            ? Convert.ToInt32(reader["AccountStatusId"]) : 0,
                        AccountStatusName = reader["AccountStatusName"] != DBNull.Value
                            ? Convert.ToString(reader["AccountStatusName"]) : string.Empty,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value
                            ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        CreatedOn = reader["CreatedOn"] != DBNull.Value
                            ? Convert.ToDateTime(reader["CreatedOn"]) : DateTime.MinValue
                    };

                    accountStatusList.Add(accountStatus);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return accountStatusList;
        }

        public async Task<AccountStatus> GetAccountStatusId(int accountStatusById)
        {
            try
            {
                AccountStatus accountStatus = null;
                using SqlConnection connection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand command = new("[dbo].[Usp_GetAccountStatusId]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue(" @AccountStatusId", accountStatusById);
                await connection.OpenAsync();
                using SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    accountStatus = new AccountStatus
                    {
                        AccountStatusId = reader["AccountStatusId"] != DBNull.Value
                            ? Convert.ToInt32(reader["AccountStatusId"]) : 0,
                        AccountStatusName = reader["AccountStatusName"] != DBNull.Value
                            ? Convert.ToString(reader["AccountStatusName"]) : string.Empty,

                    };
                }
                return accountStatus;
            }
            catch (Exception ex)
            {
                throw;
            } 
        }
        public async Task<int> DeleteAccountStatusById(int accountStatusById)
        {
            try
            {
                using SqlConnection connection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand command = new("[dbo].[Usp_Delete_AccountStatusById]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@AccountStatusId", accountStatusById);

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

