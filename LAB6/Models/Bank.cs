namespace LAB6.Models
{
    public class Bank
    {
        public Guid BankId { get; set; } // UNIQUEIDENTIFIER
        public string BankDetails { get; set; }

        // Navigation property
        public ICollection<Branch> Branches { get; set; }
    }
}
