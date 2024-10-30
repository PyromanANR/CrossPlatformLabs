namespace LAB6.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; } // UNIQUEIDENTIFIER
        public int AccountNumber { get; set; }
        public int MerchantId { get; set; }
        public string TransactionTypeCode { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal TransactionAmount { get; set; }
        public string OtherDetails { get; set; }

        // Navigation properties
        public Account Account { get; set; }
        public RefTransactionType TransactionType { get; set; }
    }
}
