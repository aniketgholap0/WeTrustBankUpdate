using System;
using System.Reflection.Metadata.Ecma335;
using WeTrustBank.Model;
using WeTrustBank.Repository.Interface;
using WeTrustBank.Service.Interface;

namespace WeTrustBank.Service.Concrete
{
    public class DebitCardDetailsService : IDebitCardDetailsService
    {
        private readonly IDebitCardDetailsRepository _debitCardDetailsRepository;
        private readonly Random _random;

        public DebitCardDetailsService(IDebitCardDetailsRepository debitCardDetailsRepository)
        {
            _debitCardDetailsRepository = debitCardDetailsRepository;
            _random = new Random();
        }

        public async Task<int> GenerateDebitCardDetails(GenerateDebitCardDetails generateDebitCardDetails)
        {
            int result = 0;
            long generateDebitCard;
            int maxRetries = 3;
            int attempt = 0;

            do
            {
                generateDebitCard = _random.NextInt64(100000000000, 999999999999);
                result = await _debitCardDetailsRepository.GenerateDebitCardDetails(generateDebitCardDetails);
                attempt++;
            }
            while (result == 0 && attempt < maxRetries);

            return result;
        }

        public async Task<int> GenerateBulkDebitCardNumber(long debitCardNumberSeries, int maxRows)
        {
            return await _debitCardDetailsRepository.GenerateBulkDebitCardNumbers(debitCardNumberSeries, maxRows);
        }

        public async  Task<int> MapApplicantDebitCardNumberByPanNumber(string panNumber, string createdBy)
        {
            return await _debitCardDetailsRepository.MapApplicantDebitCardMappingByPanNumber(panNumber, createdBy);
        }

        public async Task<int> MapApplicantsDebitCardNumberByAadharCardNumber(string aadharCardNumber, string createdBy)
        {
            return await _debitCardDetailsRepository.MapApplicantDebitCardMappingByAadharCardNumber(aadharCardNumber, createdBy);
        }

        public  async Task<int> UpdateBalanceByAccountNuber(UpdateBalanceByAccountNumberDto updateBalanceByAccountNumberDto)
        {
            return await _debitCardDetailsRepository.UpdateBalanceByAccountNumber(updateBalanceByAccountNumberDto);
        }
    }
}