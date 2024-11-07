namespace LAB6.Models
{
    public class Address
    {
        public int AddressId { get; set; } // UNIQUEIDENTIFIER
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string TownCity { get; set; }
        public string ZipPostcode { get; set; }
        public string StateProvinceCounty { get; set; }
        public string Country { get; set; }
        public string OtherDetails { get; set; }

        // Navigation properties
        public ICollection<Branch>? Branches { get; set; }
        public ICollection<Customer>? Customers { get; set; }
    }
}
