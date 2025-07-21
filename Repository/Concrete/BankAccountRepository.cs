using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Data;
using WeTrustBank.Common;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;

namespace WeTrustBank.Repository.Concrete
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly IOptions<AppSettings> _appSettings;

        public BankAccountRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<int> GenerateBankAccount(long accountNumber, string createdBy)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Generate_BankAccount]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
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

        public async Task<List<BankAccount>> GetAllBankAccounts()
        {
            List<BankAccount> bankAccounts = new List<BankAccount>();
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_GetAll_BankAccounts]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var bankAccount = new BankAccount
                    {
                        BankAccountId = reader["BankAccountId"] != DBNull.Value
                                              ? Convert.ToInt32(reader["BankAccountId"]) : 0,
                        AccountNumber = reader["AccountNumber"] != DBNull.Value
                                               ? Convert.ToInt64(reader["AccountNumber"]) : 0,
                        IsAccountForUse = reader["IsAccountForUse"] != DBNull.Value
                                               ? Convert.ToBoolean(reader["IsAccountForUse"]) : false,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value
                                               ? reader["CreatedBy"].ToString() : string.Empty,
                        CreatedOn = reader["Createdon"] != DBNull.Value
                                               ? Convert.ToDateTime(reader["Createdon"]) : DateTime.MinValue,
                    };
                    bankAccounts.Add(bankAccount);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return bankAccounts;
        }

        public async Task<BankAccount> GetBankAccountById(int bankAccountId)
        {
            try
            {
                BankAccount bankAccount = null;
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Get_BankAccountById]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure 
                };
                sqlCommand.Parameters.AddWithValue("@BankAccountId", bankAccountId);
                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    bankAccount = new BankAccount
                    {
                        BankAccountId = reader["BankAccountId"] != DBNull.Value
                                           ? Convert.ToInt32(reader["BankAccountId"]) : 0,
                        AccountNumber = reader["AccountNumber"] != DBNull.Value
                                           ? Convert.ToInt64(reader["AccountNumber"]) : 0,
                        IsAccountForUse = reader["IsAccountForUse"] != DBNull.Value
                                          ? Convert.ToBoolean(reader["IsAccountForUse"]) : false,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value
                                           ? reader["CreatedBy"].ToString() : string.Empty,
                    };
                }
                return bankAccount;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> BankAccountNumberByAccountNumber(long accountNumber)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Delete_BankAccountNumberByAccountNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure 
                };
                sqlCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();

                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> GenerateBulkBankAccounts(long bankAccountSeries,int maxRows)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Generate_BulkBankAccounts]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@BankAccountSeries",bankAccountSeries);
                sqlCommand.Parameters.AddWithValue("@MaxRows",maxRows);
                await sqlConnection.OpenAsync();

                var result = await sqlCommand.ExecuteScalarAsync();
                return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
