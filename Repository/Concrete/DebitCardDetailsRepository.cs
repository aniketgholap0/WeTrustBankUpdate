using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using WeTrustBank.Common;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;

namespace WeTrustBank.Repository.Concrete
{
    public class DebitCardDetailsRepository : IDebitCardDetailsRepository
    {
        private readonly IOptions<AppSettings> _appSettings;
        public DebitCardDetailsRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }


        public async Task<int> GenerateDebitCardDetails(GenerateDebitCardDetails generateDebitCardDetails)
        {
            using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
            using SqlCommand sqlCommand = new("[dbo].[Usp_Generate_DebitCardDetails]", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlCommand.Parameters.AddWithValue("@DebitCardNumber", generateDebitCardDetails.DebitCardNumber);
            sqlCommand.Parameters.AddWithValue("@Cvv", generateDebitCardDetails.Cvv);
            sqlCommand.Parameters.AddWithValue("@Expiration", generateDebitCardDetails.Expiration);
            sqlCommand.Parameters.AddWithValue("@CreatedBy", generateDebitCardDetails.CreatedBy);

            await sqlConnection.OpenAsync();
            var rowsAffected = await sqlCommand.ExecuteScalarAsync();
            return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
        }

        public async Task<int> GenerateBulkDebitCardNumbers(long debitCardSeriesNumber, int maxRows)
        {
            try
            {
                try
                {
                    using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                    using SqlCommand sqlCommand = new("[dbo].[Usp_Generate_BulkDebitCardNumbers]", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@DebitCardNumberSeries", debitCardSeriesNumber);
                    sqlCommand.Parameters.AddWithValue("@MaxRows", maxRows);
                    await sqlConnection.OpenAsync();
                    var result = await sqlCommand.ExecuteScalarAsync();
                    return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> MapApplicantDebitCardMappingByPanNumber(string panNumber, string createdBy)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Map_ApplicantDebitCardMappingByPanNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PanNumber", panNumber);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                await sqlConnection.OpenAsync();
                var result = await sqlCommand.ExecuteScalarAsync();
                return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> MapApplicantDebitCardMappingByAadharCardNumber(string aadharCardNumber, string createdBy)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Map_ApplicantDebitCardMappingByAadharCardNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", aadharCardNumber);
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

        public async Task<int> UpdateBalanceByAccountNumber(UpdateBalanceByAccountNumberDto updateBalanceByAccountNumberDto)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_UpdateBalance_ByAccountNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AccountNumber", updateBalanceByAccountNumberDto.AccountNumber);
                sqlCommand.Parameters.AddWithValue("@Amount", updateBalanceByAccountNumberDto.Amount);
                sqlCommand.Parameters.AddWithValue("@DebitOrCredit", updateBalanceByAccountNumberDto.DebitOrCreit);
                sqlCommand.Parameters.AddWithValue("@TransactionSource", updateBalanceByAccountNumberDto.TransactionSource);
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
