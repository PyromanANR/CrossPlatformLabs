namespace LAB6.Models
{
    public class RefTransactionType
    {
        public string TransactionTypeCode { get; set; } // CHAR(15)
        public string TransactionTypeDescription { get; set; }

        // Navigation property
        public ICollection<Transaction> Transactions { get; set; }
    }
}
