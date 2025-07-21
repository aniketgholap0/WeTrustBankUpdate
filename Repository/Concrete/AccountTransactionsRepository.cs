
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Transactions;
using WeTrustBank.Common;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;

namespace WeTrustBank.Repository.Concrete
{
    public class AccountTransactionsRepository : IAccountTransactionRepository
    {
        private readonly IOptions<AppSettings> _appSettings;

        public AccountTransactionsRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<int> InsertAccountTransaction(InsertAccountTransactionDto insertAccountTransactionDto)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Insert_AccountTransaction]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@DebitCardNumber", insertAccountTransactionDto.DebitCardNumber);
                sqlCommand.Parameters.AddWithValue("@TransactionSource", insertAccountTransactionDto.TransactionSource);
                sqlCommand.Parameters.AddWithValue("@SwipeAmount", insertAccountTransactionDto.SwipeAmount);
                sqlCommand.Parameters.AddWithValue("@DebitOrCredit", insertAccountTransactionDto.DebitOrCredit);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AccountTransaction> GetAccountTransactionByAccountTransactionID(int accountTransactionId)
        {
            try
            {
                AccountTransaction accountTransaction = null;

                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Get_AccountTransactionbyAccountTransactionID]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@AccountTransactionID", accountTransactionId);

                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    accountTransaction = new AccountTransaction
                    {
                        AccountTransactionId = reader["AccountTransactionId"] != DBNull.Value
                                            ? Convert.ToInt64(reader["AccountTransactionId"]) : 0L,

                        AccountNumber = reader["AccountNumber"] != DBNull.Value
                                           ? Convert.ToInt64(reader["AccountNumber"]) : 0L,

                        TransactionSource = reader["TransactionSource"] != DBNull.Value
                                           ? Convert.ToString(reader["TransactionSource"]) : string.Empty,
                        SwipeAmount = reader["SwipeAmount"] != DBNull.Value
                                          ? Convert.ToInt64(reader["SwipeAmount"]) : 0L,

                        DebitOrCredit = reader["DebitOrCredit"] != DBNull.Value
                                         ? Convert.ToChar(reader["DebitOrCredit"]) : ' ',

                        TransactionDateTime = reader["TransactionDateTime"] != DBNull.Value
                                      ? Convert.ToDateTime(reader["TransactionDateTime"]) : DateTime.MinValue,

                        DebitCardNumber = reader["DebitCardNumber"] != DBNull.Value
                                       ? Convert.ToInt64(reader["DebitCardNumber"]) : 0L,
                    };
                }

                return accountTransaction;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<AccountTransaction>> GetAccountTransactionByAccountNumber(long accountNumber)
        {
            try
            {
                List<AccountTransaction> transactions = new();
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Get_AccountTransactionbyAccountNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
                await sqlConnection.OpenAsync();

                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    transactions.Add(new AccountTransaction()
                    {
                        AccountTransactionId = reader["AccountTransactionId"] != DBNull.Value
                            ? Convert.ToInt64(reader["AccountTransactionId"]) : 0L,

                        AccountNumber = reader["AccountNumber"] != DBNull.Value
                            ? Convert.ToInt64(reader["AccountNumber"]) : 0L,

                        TransactionSource = reader["TransactionSource"] != DBNull.Value
                            ? Convert.ToString(reader["TransactionSource"]) : string.Empty,

                        SwipeAmount = reader["SwipeAmount"] != DBNull.Value
                            ? Convert.ToInt64(reader["SwipeAmount"]) : 0L,

                        DebitOrCredit = reader["DebitOrCredit"] != DBNull.Value
                            ? Convert.ToChar(reader["DebitOrCredit"]) : ' ',

                        TransactionDateTime = reader["TransactionDateTime"] != DBNull.Value
                            ? Convert.ToDateTime(reader["TransactionDateTime"]) : DateTime.MinValue,

                        DebitCardNumber = reader["DebitCardNumber"] != DBNull.Value
                            ? Convert.ToInt64(reader["DebitCardNumber"]) : 0L,
                    });
                }

                return transactions;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<AccountTransaction>> GetAccountTransactionsByDebitCardNumber(long debitCardNumber)
        {
            try
            {
                List<AccountTransaction> transactions = new();
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Get_AccountTransactionbyDebitCardNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@DebitCardNumber", debitCardNumber);
                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {
                    transactions.Add(new AccountTransaction()
                    {
                        AccountTransactionId = reader["AccountTransactionId"] != DBNull.Value
                            ? Convert.ToInt64(reader["AccountTransactionId"]) : 0L,

                        AccountNumber = reader["AccountNumber"] != DBNull.Value
                            ? Convert.ToInt64(reader["AccountNumber"]) : 0L,

                        TransactionSource = reader["TransactionSource"] != DBNull.Value
                            ? Convert.ToString(reader["TransactionSource"]) : string.Empty,

                        SwipeAmount = reader["SwipeAmount"] != DBNull.Value
                            ? Convert.ToInt64(reader["SwipeAmount"]) : 0L,

                        DebitOrCredit = reader["DebitOrCredit"] != DBNull.Value
                            ? Convert.ToChar(reader["DebitOrCredit"]) : ' ',

                        TransactionDateTime = reader["TransactionDateTime"] != DBNull.Value
                            ? Convert.ToDateTime(reader["TransactionDateTime"]) : DateTime.MinValue,

                        DebitCardNumber = reader["DebitCardNumber"] != DBNull.Value
                            ? Convert.ToInt64(reader["DebitCardNumber"]) : 0L,
                    });
                }
                 return transactions;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AccountTransaction>> GetAccountTransactionsAsync(int pageNumber, int pageSize)
        {
            try
            {
                List<AccountTransaction> transactions = new();
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_GetAccountTransactions_RowNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PageNumber", pageNumber);
                sqlCommand.Parameters.AddWithValue("@PageSize", pageSize);

                await sqlConnection.OpenAsync();

                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                while(await reader.ReadAsync())
                {
                    transactions.Add(new AccountTransaction()
                    {
                        AccountTransactionId = reader["AccountTransactionId"] != DBNull.Value
                                               ? Convert.ToInt64(reader["AccountTransactionId"]) : 0L,

                        AccountNumber = reader["AccountNumber"] != DBNull.Value
                                          ? Convert.ToInt64(reader["AccountNumber"]) : 0L,

                        TransactionSource = reader["TransactionSource"] != DBNull.Value
                                            ? Convert.ToString(reader["TransactionSource"]) : string.Empty,

                        SwipeAmount = reader["SwipeAmount"] != DBNull.Value
                                        ? Convert.ToInt64(reader["swipeAmount"]) : 0L,

                        DebitOrCredit = reader["DebitOrCredit"] != DBNull.Value
                                        ? Convert.ToChar(reader["DebitOrCredit"]) : ' ',

                        TransactionDateTime = reader["TransactionDateTime"] != DBNull.Value
                                           ? Convert.ToDateTime(reader["TransactionDateTime"]) : DateTime.MinValue,

                        DebitCardNumber = reader["DebitCardNumber"] != DBNull.Value
                                            ? Convert.ToInt64(reader["DebitCardNumber"]) : 0L,

                    }); ;
                }
                return transactions;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
