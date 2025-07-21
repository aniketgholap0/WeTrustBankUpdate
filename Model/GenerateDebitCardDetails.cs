using System.Globalization;

namespace WeTrustBank.Model
{
    public class GenerateDebitCardDetails
    {
        public long DebitCardNumber { get; set; }
        public string Cvv { get; set; }
        public string Expiration { get; set; }
        public string CreatedBy { get; set; }
    }
}
