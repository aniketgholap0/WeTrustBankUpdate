using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection.PortableExecutable;
using WeTrustBank.Common;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;

namespace WeTrustBank.Repository.Concrete
{
    public class ApplicantAccountMappingRepository : IApplicantAccountMappingRepository
    {
        private readonly IOptions<AppSettings> _appSettings;
        public ApplicantAccountMappingRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<int> CreateApplicantAccountMapping(ApplicantAccountMappingDto applicantAccountMappingDto)
        {
            
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Create_ApplicantAccountMapping]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@AadharNumber", applicantAccountMappingDto.AadharNumber);
                sqlCommand.Parameters.AddWithValue("@PanNumber", applicantAccountMappingDto.PanNumber);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", applicantAccountMappingDto.CreatedBy);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateApplicantAccountMappingByAadharCardNumber(long aadharCardNumber, string accountStatusName, string updatedBy)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Update_ApplicantAccountMappingByAadharCardNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", aadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@AccountStatusName", accountStatusName);
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

        public async Task<int> UpdateApplicantAccountMappingByPanNumber(string panNumber, string accountStatusName, string updatedBy)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Update_ApplicantAccountMappingByPanNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PanNumber", panNumber);
                sqlCommand.Parameters.AddWithValue("@AccountStatusName", accountStatusName);
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

        public async Task<int> UpdateApplicantAccountMappingByApplicantId(int applicantsId, string accountStatusName, string updatedBy)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Update_ApplicantAccountMappingByApplicantId]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@ApplicantsId", applicantsId);
                sqlCommand.Parameters.AddWithValue("@AccountStatusName", accountStatusName);
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

        public async Task<ApplicantAccountMappingRequestDto> GetApplicantAccountMappingByAadharCardNumberDtoAsync(long aadharCardNumber)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Get_ApplicantAccountMappingByAadharCardNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", aadharCardNumber);
                await sqlConnection.OpenAsync();
                using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                if ((await sqlDataReader.ReadAsync()))
                {

                    if (sqlDataReader.FieldCount == 1 && sqlDataReader.GetName(0) == "" || sqlDataReader.GetName(0).StartsWith("("))
                    {
                        var code = sqlDataReader.GetInt32(0);

                        return null;
                    }
                    return new ApplicantAccountMappingRequestDto
                    {
                        FirstName = sqlDataReader["FirstName"] != DBNull.Value ?
                                  Convert.ToString(sqlDataReader["FirstName"]) : string.Empty,
                        LastName = sqlDataReader["LastName"] != DBNull.Value ?
                                    Convert.ToString(sqlDataReader["LastName"]) : string.Empty,
                        Email = sqlDataReader["Email"] != DBNull.Value ?
                                    Convert.ToString(sqlDataReader["Email"]) : string.Empty,
                        PhoneNumber = sqlDataReader["PhoneNumber"] != DBNull.Value ?
                                    Convert.ToString(sqlDataReader["PhoneNumber"]) : string.Empty,
                        AccountNumber = sqlDataReader["AccountNumber"] != DBNull.Value ?
                                    Convert.ToInt64(sqlDataReader["AccountNumber"]) : 0,
                        AccountStatusName = sqlDataReader["AccountStatusName"] != DBNull.Value ?
                                    Convert.ToString(sqlDataReader["AccountStatusName"]) : string.Empty
                    };

                }
                return null;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ApplicantAccountMappingRequestDto> GetMappingByPanNumberDtoAsync(string panNumber)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new SqlCommand("[dbo].[Usp_Get_ApplicantAccountMappingByPanNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@PanNumber", panNumber);
                await sqlConnection.OpenAsync();
                using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                if (await sqlDataReader.ReadAsync())
                {

                    if (sqlDataReader.FieldCount >= 1 && sqlDataReader.GetName(0) == "FirstName")
                        return new ApplicantAccountMappingRequestDto
                        {
                            FirstName = sqlDataReader["FirstName"] != DBNull.Value ?
                                      Convert.ToString(sqlDataReader["FirstName"]) : string.Empty,
                            LastName = sqlDataReader["LastName"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["LastName"]) : string.Empty,
                            Email = sqlDataReader["Email"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["Email"]) : string.Empty,
                            PhoneNumber = sqlDataReader["PhoneNumber"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["PhoneNumber"]) : string.Empty,
                            AccountNumber = sqlDataReader["AccountNumber"] != DBNull.Value ?
                                        Convert.ToInt64(sqlDataReader["AccountNumber"]) : 0,
                            AccountStatusName = sqlDataReader["AccountStatusName"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["AccountStatusName"]) : string.Empty
                        };
                }
                return null;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ApplicantAccountMappingRequestDto> GetApplicantAccountMappingByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Get_ApplicantAccountMappingByPhoneNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                await sqlConnection.OpenAsync();
                using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                if (await sqlDataReader.ReadAsync())
                {
                    if (sqlDataReader.FieldCount >= 1 && sqlDataReader.GetName(0) == "FirstName")
                        return new ApplicantAccountMappingRequestDto
                        {
                            FirstName = sqlDataReader["FirstName"] != DBNull.Value ?
                                      Convert.ToString(sqlDataReader["FirstName"]) : string.Empty,
                            LastName = sqlDataReader["LastName"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["LastName"]) : string.Empty,
                            Email = sqlDataReader["Email"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["Email"]) : string.Empty,
                            PhoneNumber = sqlDataReader["PhoneNumber"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["PhoneNumber"]) : string.Empty,
                            AccountNumber = sqlDataReader["AccountNumber"] != DBNull.Value ?
                                        Convert.ToInt64(sqlDataReader["AccountNumber"]) : 0,
                            AccountStatusName = sqlDataReader["AccountStatusName"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["AccountStatusName"]) : string.Empty
                        };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ApplicantAccountMappingRequestDto> GetApplicantAccountMappingByApplicantNameAsync(string firstName, string lastName)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Get_ApplicantAccountMappingByApplicantName]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                sqlCommand.Parameters.AddWithValue("@LastName", lastName);
                await sqlConnection.OpenAsync();
                using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                if (await sqlDataReader.ReadAsync())
                {
                    if (sqlDataReader.FieldCount >= 1 && sqlDataReader.GetName(0) == "FirstName")
                        return new ApplicantAccountMappingRequestDto
                        {
                            FirstName = sqlDataReader["FirstName"] != DBNull.Value ?
                                      Convert.ToString(sqlDataReader["FirstName"]) : string.Empty,
                            LastName = sqlDataReader["LastName"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["LastName"]) : string.Empty,
                            Email = sqlDataReader["Email"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["Email"]) : string.Empty,
                            PhoneNumber = sqlDataReader["PhoneNumber"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["PhoneNumber"]) : string.Empty,
                            AccountNumber = sqlDataReader["AccountNumber"] != DBNull.Value ?
                                        Convert.ToInt64(sqlDataReader["AccountNumber"]) : 0,
                            AccountStatusName = sqlDataReader["AccountStatusName"] != DBNull.Value ?
                                        Convert.ToString(sqlDataReader["AccountStatusName"]) : string.Empty
                        };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetAllApplicantAccountMappingRequestDto>> GetAllApplicantAccountMappingsAsync()
        {
            List<GetAllApplicantAccountMappingRequestDto> applicantAccountMappings = new List<GetAllApplicantAccountMappingRequestDto>();

            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Get_AllApplicantAccountMapping]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                await sqlConnection.OpenAsync();
                using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                 
                   var applicantAccountMapping = new GetAllApplicantAccountMappingRequestDto
                   {
                        ApplicantAccountMappingId = sqlDataReader["ApplicantAccountMappingId"] != DBNull.Value ?
                             Convert.ToInt32(sqlDataReader["ApplicantAccountMappingId"]) : 0,
                        AccountStatusId = sqlDataReader["AccountStatusId"] != DBNull.Value ?
                             Convert.ToInt32(sqlDataReader["AccountStatusId"]) : 0,
                        ApplicantsId = sqlDataReader["ApplicantsId"] != DBNull.Value ?
                             Convert.ToInt32(sqlDataReader["ApplicantsId"]) : 0,
                        AccountNumber = sqlDataReader["AccountNumber"] != DBNull.Value ?
                             Convert.ToInt64(sqlDataReader["AccountNumber"]) : 0,
                        AvailableBalance = sqlDataReader["AvailableBalance"] != DBNull.Value ?
                             Convert.ToInt64(sqlDataReader["AvailableBalance"]) : 0,
                        CreatedBy = sqlDataReader["CreatedBy"] != DBNull.Value ?
                            Convert.ToString(sqlDataReader["CreatedBy"]) : string.Empty,
                        CreatedOn = sqlDataReader["CreatedOn"] != DBNull.Value ?
                            Convert.ToDateTime(sqlDataReader["CreatedOn"]) : DateTime.MinValue
                   };
                        applicantAccountMappings.Add(applicantAccountMapping);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return applicantAccountMappings;
        }

        public async Task<int> MapApplicantAccountMapping()
        {
            {
                using var sqlConnection = new SqlConnection(_appSettings.Value.WeTrustBankDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[Usp_Map_ApplicantMappingMapping]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await sqlConnection.OpenAsync();

                var result = await sqlCommand.ExecuteScalarAsync();

                return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
            }
        }
    }
}

