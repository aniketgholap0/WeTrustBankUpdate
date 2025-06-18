using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlTypes;
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

        public async  Task<int> UpdateApplicantAccountMappingByPanNumber(string panNumber, string accountStatusName, string updatedBy)
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
    }
}
