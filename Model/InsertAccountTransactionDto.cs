using System.Data.SqlTypes;

namespace WeTrustBank.Model
{
    public class InsertAccountTransactionDto
    {
        public long DebitCardNumber { get; set; }
        public string TransactionSource { get; set; }
        public int SwipeAmount { get; set; }
        public char DebitOrCredit {  get; set; }
    
    }
}
