using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Data;
using System.Linq.Expressions;
using WeTrustBank.Common;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;

namespace WeTrustBank.Repository.Concrete
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly IOptions<AppSettings> _appSettings;
        public AccountTypeRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<int> CreateAccountType(string accountTypeName, string createdBy)
        {
            try
            {

                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Create_AccountType]", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AccountTypeName", accountTypeName);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", createdBy);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }

            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<int> UpdateAccountType(int accountTypeId, string accountTypeName, string updatedBy)
        {
            try
            {


                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Update_AccountType]", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AccountTypeId", accountTypeId);
                sqlCommand.Parameters.AddWithValue("@AccountTypeName", accountTypeName);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();

                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<AccountType>> GetAllAccountTypes()
        {
            List<AccountType> accountTypes = [];
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_GetAll_AccountTypes]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var accountType = new AccountType
                    {
                        AccountTypeId = reader["AccountTypeId"] != DBNull.Value
                                            ? Convert.ToInt32(reader["AccountTypeId"]) : 0,

                        AccountTypeName = reader["AccountTypeName"] != DBNull.Value
                                            ? Convert.ToString(reader["AccountTypeName"]) : string.Empty,

                        CreatedBy = reader["CreatedBy"] != DBNull.Value
                                            ? Convert.ToString(reader["CreatedBy"]) : string.Empty,

                        CreatedOn = reader["CreatedOn"] != DBNull.Value
                                            ? Convert.ToDateTime(reader["CreatedOn"]) : DateTime.MinValue,
                    };

                    accountTypes.Add(accountType);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return accountTypes;
        }

        public async Task<AccountType> GetAccountTypeById(int accountTypeId)
        {
            try
            {
                AccountType accountType = null;
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Get_AccountTypeById]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AccountTypeId", accountTypeId);

                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    accountType = new AccountType
                    {
                        AccountTypeId = reader["AccountTypeId"] != DBNull.Value
                                            ? Convert.ToInt32(reader["AccountTypeId"]) : 0,
                        AccountTypeName = reader["AccountTypeName"] != DBNull.Value
                                            ? Convert.ToString(reader["AccountTypeName"]) : string.Empty
                    };
                }
                return accountType;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<int> DeleteAccountTypeById(int accountTypeId)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Delete_AccountTypeById]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AccountTypeId", accountTypeId);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();

                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

