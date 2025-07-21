using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using WeTrustBank.Common;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;

namespace WeTrustBank.Repository.Concrete
{
    public class ApplicantsRepository : IApplicantsRepository
    {
        private readonly IOptions<AppSettings> _appSettings;

        public ApplicantsRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<int> CreateApplicant(ApplicantCreateDto applicantRequestDto)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Create_Applicants]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@FirstName", applicantRequestDto.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", applicantRequestDto.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", applicantRequestDto.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Email", applicantRequestDto.Email);
                sqlCommand.Parameters.AddWithValue("@Gender", applicantRequestDto.Gender);
                sqlCommand.Parameters.AddWithValue("@DateOfBirth", applicantRequestDto.DateOfBirth);
                sqlCommand.Parameters.AddWithValue("@Address", applicantRequestDto.Address);
                sqlCommand.Parameters.AddWithValue("@Country", applicantRequestDto.Country);
                sqlCommand.Parameters.AddWithValue("@State", applicantRequestDto.State);
                sqlCommand.Parameters.AddWithValue("@District", applicantRequestDto.District);
                sqlCommand.Parameters.AddWithValue("@City", applicantRequestDto.City);
                sqlCommand.Parameters.AddWithValue("@ZipCode", applicantRequestDto.ZipCode);
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", applicantRequestDto.AadharNumber);
                sqlCommand.Parameters.AddWithValue("@PanNumber", applicantRequestDto.PanNumber);
                sqlCommand.Parameters.AddWithValue("@AccountTypeName", applicantRequestDto.AccountTypeName);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", applicantRequestDto.CreatedBy);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateApplicant(ApplicantUpdateDto applicantUpdateDto)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Update_Applicants]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@ApplicantsID", applicantUpdateDto.ApplicantsID);
                sqlCommand.Parameters.AddWithValue("@FirstName", applicantUpdateDto.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", applicantUpdateDto.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", applicantUpdateDto.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Email", applicantUpdateDto.Email);
                sqlCommand.Parameters.AddWithValue("@Address", applicantUpdateDto.Address);
                sqlCommand.Parameters.AddWithValue("@State", applicantUpdateDto.State);
                sqlCommand.Parameters.AddWithValue("@City", applicantUpdateDto.City);
                sqlCommand.Parameters.AddWithValue("@District", applicantUpdateDto.District);
                sqlCommand.Parameters.AddWithValue("@ZipCode", applicantUpdateDto.ZipCode);
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", applicantUpdateDto.AadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@PanNumber", applicantUpdateDto.PanNumber);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", applicantUpdateDto.@UpdatedBy);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ApplicantDto> GetApplicantById(int applicantId)
        {
            try
            {
                ApplicantDto applicantDto = null;
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Get_ApplicantById]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@ApplicantId", applicantId);

                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    applicantDto = new ApplicantDto
                    {
                        FirstName = reader["FirstName"] != DBNull.Value
                                    ? Convert.ToString(reader["FirstName"]) : string.Empty,
                        LastName = reader["LastName"] != DBNull.Value
                                    ? Convert.ToString(reader["LastName"]) : string.Empty,
                        PhoneNumber = reader["PhoneNumber"] != DBNull.Value
                                      ? Convert.ToString(reader["PhoneNumber"]) : string.Empty,
                        Email = reader["Email"] != DBNull.Value
                                ? Convert.ToString(reader["Email"]) : string.Empty,
                        Gender = reader["Gender"] != DBNull.Value
                                ? Convert.ToString(reader["Gender"]) : string.Empty,
                        DateOfBirth = reader["DateOfBirth"] != DBNull.Value
                                ? Convert.ToDateTime(reader["DateOfBirth"]) : default,
                        Address = reader["Address"] != DBNull.Value
                                ? Convert.ToString(reader["Address"]) : string.Empty,
                        Country = reader["Country"] != DBNull.Value
                                ? Convert.ToString(reader["Country"]) : string.Empty,
                        State = reader["State"] != DBNull.Value
                                ? Convert.ToString(reader["State"]) : string.Empty,
                        District = reader["District"] != DBNull.Value
                                ? Convert.ToString(reader["District"]) : string.Empty,
                        City = reader["City"] != DBNull.Value
                                ? Convert.ToString(reader["City"]) : string.Empty,
                        ZipCode = reader["ZipCode"] != DBNull.Value
                                ? Convert.ToInt32(reader["ZipCode"]) : 0,
                        AadharCardNumber = reader["AadharCardNumber"] != DBNull.Value
                                ? Convert.ToInt64(reader["AadharCardNumber"]) : 0,
                        PanNumber = reader["PanNumber"] != DBNull.Value
                                ? Convert.ToString(reader["PanNumber"]) : string.Empty,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value
                                ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                    };
                    return applicantDto;
                }

                return applicantDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetAllApplicantsDto>> GetAllApplicants()
        {
            List<GetAllApplicantsDto> applicants = [];
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Get_AllApplicants]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    GetAllApplicantsDto applicant = new()
                    {
                        ApplicantsId = reader["ApplicantsID"] != DBNull.Value
                                       ? Convert.ToInt32(reader["ApplicantsID"]) : 0,
                        FirstName = reader["FirstName"] != DBNull.Value
                                    ? Convert.ToString(reader["FirstName"]) : string.Empty,
                        LastName = reader["LastName"] != DBNull.Value
                                   ? Convert.ToString(reader["LastName"]) : string.Empty,
                        PhoneNumber = reader["PhoneNumber"] != DBNull.Value
                                      ? Convert.ToString(reader["PhoneNumber"]) : string.Empty,
                        Email = reader["Email"] != DBNull.Value
                                ? Convert.ToString(reader["Email"]) : string.Empty,
                        DateOfBirth = reader["DateOfBirth"] != DBNull.Value
                                ? Convert.ToDateTime(reader["DateOfBirth"]) : default,
                        Address = reader["Address"] != DBNull.Value
                                  ? Convert.ToString(reader["Address"]) : string.Empty,
                        Country = reader["Country"] != DBNull.Value
                                  ? Convert.ToString(reader["Country"]) : string.Empty,
                        State = reader["State"] != DBNull.Value
                                ? Convert.ToString(reader["State"]) : string.Empty,
                        District = reader["District"] != DBNull.Value
                                   ? Convert.ToString(reader["District"]) : string.Empty,
                        City = reader["City"] != DBNull.Value
                               ? Convert.ToString(reader["City"]) : string.Empty,
                        ZipCode = reader["ZipCode"] != DBNull.Value
                                  ? Convert.ToInt32(reader["ZipCode"]) : 0,
                        AadharCardNumber = reader["AadharCardNumber"] != DBNull.Value
                                           ? Convert.ToInt64(reader["AadharCardNumber"]) : 0,
                        PanNumber = reader["PanNumber"] != DBNull.Value
                                    ? Convert.ToString(reader["PanNumber"]) : string.Empty,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value
                                    ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        CreatedOn = reader["CreatedOn"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["DateOfBirth"]) : default,
                    };
                    applicants.Add(applicant);
                }
                return applicants;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ApplicantDto>> GetApplicantsBetween(DateTime startDate, DateTime endDate)
        {
            var applicants = new List<ApplicantDto>();

            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("dbo.Usp_Get_ApplicantsBetween", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", endDate);

                await sqlConnection.OpenAsync();

                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    applicants.Add(new ApplicantDto
                    {
                        FirstName = reader["FirstName"] != DBNull.Value
                                    ? Convert.ToString(reader["FirstName"]) : string.Empty,
                        LastName = reader["LastName"] != DBNull.Value
                                    ? Convert.ToString(reader["LastName"]) : string.Empty,
                        PhoneNumber = reader["PhoneNumber"] != DBNull.Value
                                      ? Convert.ToString(reader["PhoneNumber"]) : string.Empty,
                        Email = reader["Email"] != DBNull.Value
                                ? Convert.ToString(reader["Email"]) : string.Empty,
                        Gender = reader["Gender"] != DBNull.Value
                                ? Convert.ToString(reader["Gender"]) : string.Empty,
                        DateOfBirth = reader["DateOfBirth"] != DBNull.Value
                                ? Convert.ToDateTime(reader["DateOfBirth"]) : default,
                        Address = reader["Address"] != DBNull.Value
                                ? Convert.ToString(reader["Address"]) : string.Empty,
                        Country = reader["Country"] != DBNull.Value
                                ? Convert.ToString(reader["Country"]) : string.Empty,
                        State = reader["State"] != DBNull.Value
                                ? Convert.ToString(reader["State"]) : string.Empty,
                        District = reader["District"] != DBNull.Value
                                ? Convert.ToString(reader["District"]) : string.Empty,
                        City = reader["City"] != DBNull.Value
                                ? Convert.ToString(reader["City"]) : string.Empty,
                        ZipCode = reader["ZipCode"] != DBNull.Value
                                ? Convert.ToInt32(reader["ZipCode"]) : 0,
                        AadharCardNumber = reader["AadharCardNumber"] != DBNull.Value
                                ? Convert.ToInt64(reader["AadharCardNumber"]) : 0,
                        PanNumber = reader["PanNumber"] != DBNull.Value
                                ? Convert.ToString(reader["PanNumber"]) : string.Empty,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value
                                ? Convert.ToString(reader["CreatedBy"]) : string.Empty,

                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return applicants;
        }

        public async Task<int> UpdateApplicantByAadharCardNumber(ApplicantUpdateByAadharCardNumberDto applicantsUpdateByAadharCardNumberDto)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Update_ApplicantByPhoneNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure 
                };
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", applicantsUpdateByAadharCardNumberDto.AadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@FirstName", applicantsUpdateByAadharCardNumberDto.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", applicantsUpdateByAadharCardNumberDto.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", applicantsUpdateByAadharCardNumberDto.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Email", applicantsUpdateByAadharCardNumberDto.Email);
                sqlCommand.Parameters.AddWithValue("@Address", applicantsUpdateByAadharCardNumberDto.Address);
                sqlCommand.Parameters.AddWithValue("@Country", applicantsUpdateByAadharCardNumberDto.Country);
                sqlCommand.Parameters.AddWithValue("@State", applicantsUpdateByAadharCardNumberDto.State);
                sqlCommand.Parameters.AddWithValue("@District", applicantsUpdateByAadharCardNumberDto.District);
                sqlCommand.Parameters.AddWithValue("@City", applicantsUpdateByAadharCardNumberDto.City);
                sqlCommand.Parameters.AddWithValue("@ZipCode", applicantsUpdateByAadharCardNumberDto.ZipCode);
                sqlCommand.Parameters.AddWithValue("@PanNumber", applicantsUpdateByAadharCardNumberDto.PanNumber);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", applicantsUpdateByAadharCardNumberDto.UpdatedBy);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
     
        public async Task<int> UpdateApplicantByPanNumber(ApplicantUpdatebyPanNumberDto applicantsUpdateByPanNumberDto)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Update_ApplicantByPhoneNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure 
                };

                sqlCommand.Parameters.AddWithValue("@PanNumber", applicantsUpdateByPanNumberDto.PanNumber);
                sqlCommand.Parameters.AddWithValue("@FirstName", applicantsUpdateByPanNumberDto.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", applicantsUpdateByPanNumberDto.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", applicantsUpdateByPanNumberDto.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Email", applicantsUpdateByPanNumberDto.Email);
                sqlCommand.Parameters.AddWithValue("@Address", applicantsUpdateByPanNumberDto.Address);
                sqlCommand.Parameters.AddWithValue("@Country", applicantsUpdateByPanNumberDto.Country);
                sqlCommand.Parameters.AddWithValue("@State", applicantsUpdateByPanNumberDto.State);
                sqlCommand.Parameters.AddWithValue("@District", applicantsUpdateByPanNumberDto.District);
                sqlCommand.Parameters.AddWithValue("@City", applicantsUpdateByPanNumberDto.City);
                sqlCommand.Parameters.AddWithValue("@ZipCode", applicantsUpdateByPanNumberDto.ZipCode);
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", applicantsUpdateByPanNumberDto.AadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", applicantsUpdateByPanNumberDto.UpdatedBy);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateApplicantByPhoneNumber(ApplicantUpdateByPhoneNumberDto applicantUpdateByPhoneNumberDto)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Update_ApplicantByPhoneNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", applicantUpdateByPhoneNumberDto.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@FirstName", applicantUpdateByPhoneNumberDto.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", applicantUpdateByPhoneNumberDto.LastName);
                sqlCommand.Parameters.AddWithValue("@Email", applicantUpdateByPhoneNumberDto.Email);
                sqlCommand.Parameters.AddWithValue("@Address", applicantUpdateByPhoneNumberDto.Address);
                sqlCommand.Parameters.AddWithValue("@Country", applicantUpdateByPhoneNumberDto.Country);
                sqlCommand.Parameters.AddWithValue("@State", applicantUpdateByPhoneNumberDto.State);
                sqlCommand.Parameters.AddWithValue("@District", applicantUpdateByPhoneNumberDto.District);
                sqlCommand.Parameters.AddWithValue("@City", applicantUpdateByPhoneNumberDto.City);
                sqlCommand.Parameters.AddWithValue("@ZipCode", applicantUpdateByPhoneNumberDto.ZipCode);
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", applicantUpdateByPhoneNumberDto.AadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@PanNumber", applicantUpdateByPhoneNumberDto.PanNumber);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", applicantUpdateByPhoneNumberDto.UpdatedBy);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
