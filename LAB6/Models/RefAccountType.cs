namespace LAB6.Models
{
    public class RefAccountType
    {
        public string AccountTypeCode { get; set; } // CHAR(15)
        public string AccountTypeDescription { get; set; }

        // Navigation property
        public ICollection<Account> Accounts { get; set; }
    }
}
