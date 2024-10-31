namespace LAB5.Models
{
    public class SearchViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PersonalDetail { get; set; }
        public string AccountIds { get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Account> Accounts { get; set; } = new List<Account>();
    }

}
