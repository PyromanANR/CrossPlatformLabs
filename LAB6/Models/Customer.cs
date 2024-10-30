using System.Net;

namespace LAB6.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; } // UNIQUEIDENTIFIER
        public int AddressId { get; set; }
        public Guid BranchId { get; set; }
        public string PersonalDetails { get; set; }
        public string ContactDetails { get; set; }

        // Navigation properties
        public Address Address { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
