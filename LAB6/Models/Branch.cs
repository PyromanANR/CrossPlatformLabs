using System.Net;

namespace LAB6.Models
{
    public class Branch
    {
        public Guid BranchId { get; set; } // UNIQUEIDENTIFIER
        public int AddressId { get; set; }
        public Guid BankId { get; set; }
        public string BranchTypeCode { get; set; }
        public string BranchDetails { get; set; }

        // Navigation properties
        public Bank? Bank { get; set; }
        public Address? Address { get; set; }
        public RefBranchType? BranchType { get; set; }
        public ICollection<Customer>? Customers { get; set; }
    }
}
