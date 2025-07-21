namespace WeTrustBank.Model
{
    public class UpdateBalanceByAccountNumberDto
    {
        public long AccountNumber { get; set; }
        public long Amount { get; set; }
        public char DebitOrCreit { get; set; }
        public string TransactionSource { get; set; }
    }
}
