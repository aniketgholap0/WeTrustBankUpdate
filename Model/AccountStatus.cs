namespace WeTrustBank.Model
{
    public class AccountStatus
    {
        public int AccountStatusId { get; set; }
        public string AccountStatusName { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
    }
}
