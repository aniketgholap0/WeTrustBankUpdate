using System.Xml;

namespace WeTrustBank.Model
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }
        public long AccountNumber { get; set; }
        public bool IsAccountForUse { get; set; } = true;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.MinValue;
    }
}
