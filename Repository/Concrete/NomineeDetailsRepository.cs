using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using WeTrustBank.Common;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;

namespace WeTrustBank.Repository.Concrete
{
    public class NomineeDetailsRepository : INomineeDetailsRepository
    {
        private readonly IOptions<AppSettings> _appSettings;

        public NomineeDetailsRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }
        public async Task<int> MapNomineeDetailsByApplicantsID(NomineeDetailsByApplicantsID nomineeDetailsByApplicantsID)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Map_NomineeDetailsByApplicantsID]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@ApplicantsID", nomineeDetailsByApplicantsID.ApplicantsID);
                sqlCommand.Parameters.AddWithValue("@FirstName", nomineeDetailsByApplicantsID.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", nomineeDetailsByApplicantsID.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", nomineeDetailsByApplicantsID.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", nomineeDetailsByApplicantsID.AadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@PanNumber", nomineeDetailsByApplicantsID.PanNumber);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", nomineeDetailsByApplicantsID.CreatedBy);

                await sqlConnection.OpenAsync();

                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> MapNomineeDetailsByApplicantAadharCardNumber(MapNomineeDetailsByApplicantAadharCardNumberRequestDto mapNomineeDetailsByApplicantAadharCardNumberRequestDto)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Map_NomineeDetailsByApplicantAadharCardNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", mapNomineeDetailsByApplicantAadharCardNumberRequestDto.AadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@FirstName", mapNomineeDetailsByApplicantAadharCardNumberRequestDto.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", mapNomineeDetailsByApplicantAadharCardNumberRequestDto.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", mapNomineeDetailsByApplicantAadharCardNumberRequestDto.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@NomineeAadharCardNumber", mapNomineeDetailsByApplicantAadharCardNumberRequestDto.NomineeAadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@PanNumber", mapNomineeDetailsByApplicantAadharCardNumberRequestDto.PanNumber);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", mapNomineeDetailsByApplicantAadharCardNumberRequestDto.CreatedBy);

                await sqlConnection.OpenAsync();

                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> MapNomineeDetailsByApplicantPanNumber(MapNomineeDetailsByApplicantPanNumberDto mapNomineeDetailsByApplicantPanNumber)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Map_NomineeDetailsByApplicantPanNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@PanNumber", mapNomineeDetailsByApplicantPanNumber.PanNumber);
                sqlCommand.Parameters.AddWithValue("@FirstName", mapNomineeDetailsByApplicantPanNumber.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", mapNomineeDetailsByApplicantPanNumber.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", mapNomineeDetailsByApplicantPanNumber.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", mapNomineeDetailsByApplicantPanNumber.AadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@NomineePanNumber", mapNomineeDetailsByApplicantPanNumber.NomineePanNumber);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", mapNomineeDetailsByApplicantPanNumber.CreatedBy);

                await sqlConnection.OpenAsync();

                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateNomineeDetailsByNomineeAadharNumber(NomineeDetailsUpdateByAadharCardNumberDto nomineeDetailsUpdateByAadharCardNumberDto)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Update_NomineeDetailsByNomineeAadharNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", nomineeDetailsUpdateByAadharCardNumberDto.AadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@FirstName", nomineeDetailsUpdateByAadharCardNumberDto.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", nomineeDetailsUpdateByAadharCardNumberDto.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", nomineeDetailsUpdateByAadharCardNumberDto.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@PanNumber", nomineeDetailsUpdateByAadharCardNumberDto.PanNumber);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", nomineeDetailsUpdateByAadharCardNumberDto.UpdatedBy);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<int> UpdateNomineeDetailsByNomineePanNumber(NomineeDetailsUpdateByPanNumberDto nomineeDetailsUpdateByPanNumberDto)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Update_NomineeDetailsByNomineePanNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PanNumber", nomineeDetailsUpdateByPanNumberDto.PanNumber);
                sqlCommand.Parameters.AddWithValue("@FirstName", nomineeDetailsUpdateByPanNumberDto.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", nomineeDetailsUpdateByPanNumberDto.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", nomineeDetailsUpdateByPanNumberDto.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", nomineeDetailsUpdateByPanNumberDto.AadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", nomineeDetailsUpdateByPanNumberDto.UpdatedBy);

                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateNomineeDetailsByNoineeDeatilsID(NomineeDetailsUpdateByNomineeDetailsIDDto nomineeDetailsUpdateByNomineeDetailsIDDto)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Update_NomineeDetailsByNomineeId]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@NomineeDetailsID", nomineeDetailsUpdateByNomineeDetailsIDDto.NomineeDeatilsID);
                sqlCommand.Parameters.AddWithValue("@FirstName", nomineeDetailsUpdateByNomineeDetailsIDDto.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", nomineeDetailsUpdateByNomineeDetailsIDDto.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", nomineeDetailsUpdateByNomineeDetailsIDDto.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", nomineeDetailsUpdateByNomineeDetailsIDDto.AadharCardNumber);
                sqlCommand.Parameters.AddWithValue("@PanNumber", nomineeDetailsUpdateByNomineeDetailsIDDto.PanNumber);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", nomineeDetailsUpdateByNomineeDetailsIDDto.UpdatedBy);

                await sqlConnection.OpenAsync();

                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != null && rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public  async Task<int> DeleteNomineeByNomineeeDetailsID(int nomineeDeatilsID)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Delete_NomineeByNomineeeDetailsID]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@NomineeDetailsID", nomineeDeatilsID);
                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();

                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteNomineeByNomineeAadharCardNumber(long aadharCardNumber)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Delete_NomineeByNomineeAadharNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AadhaCardNumber", aadharCardNumber);
                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();

                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async  Task<int> DeleteNomineeByNomineePanNumber(string panNumber)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Delete_NomineeByNomineePanNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PanNumber", panNumber);
                await sqlConnection.OpenAsync();
                var rowsAffected = await sqlCommand.ExecuteScalarAsync();

                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;

            }
           catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteNomineeByApplicantsID(int applicantsID)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Delete_NomineeByApplicantsID]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@ApplicantsID", applicantsID);
                await sqlConnection.OpenAsync();

                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async  Task<int> DeleteNomineeByApplicantAadharCardNumber(long aadharCardNumber)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Delete_NomineeByApplicantAadharCardNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@AadharCardNumber", aadharCardNumber);
                await sqlConnection.OpenAsync();

                var rowsAffected = await sqlCommand.ExecuteScalarAsync();
                return rowsAffected != DBNull.Value ? Convert.ToInt32(rowsAffected) : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteNomineeByApplicantPanNumber(string panNumber)
        {
            try
            {
                using SqlConnection sqlConnection = new(_appSettings.Value.WeTrustBankDBConnection);
                using SqlCommand sqlCommand = new("[dbo].[Usp_Delete_NomineeByApplicantPanNumber]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                sqlCommand.Parameters.AddWithValue("@PanNumber", panNumber);
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
