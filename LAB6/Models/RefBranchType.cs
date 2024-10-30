namespace LAB6.Models
{
    public class RefBranchType
    {
        public string BranchTypeCode { get; set; } // CHAR(15)
        public string BranchTypeDescription { get; set; }

        // Navigation property
        public ICollection<Branch> Branches { get; set; }
    }
}
