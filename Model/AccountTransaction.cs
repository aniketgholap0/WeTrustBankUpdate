namespace WeTrustBank.Model
{
    public class AccountTransaction
    {
        public long AccountTransactionId { get; set; }
        public long AccountNumber { get; set; }
        public string TransactionSource { get; set; }
        public long SwipeAmount {  get; set; }
        public char DebitOrCredit { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public long DebitCardNumber { get; set; }
    }
}
