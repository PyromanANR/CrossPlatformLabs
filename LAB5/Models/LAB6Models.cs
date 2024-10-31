using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LAB5.Models
{
    public class Account
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("accountNumber")]
        public int AccountNumber { get; set; }

        [JsonPropertyName("accountStatusCode")]
        public string AccountStatusCode { get; set; }

        [JsonPropertyName("accountTypeCode")]
        public string AccountTypeCode { get; set; }

        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }

        [JsonPropertyName("currentBalance")]
        public decimal CurrentBalance { get; set; }

        [JsonPropertyName("otherDetails")]
        public string OtherDetails { get; set; }

        [JsonPropertyName("customer")]
        public object Customer { get; set; }

        [JsonPropertyName("accountType")]
        public object AccountType { get; set; }

        [JsonPropertyName("transactions")]
        public object Transactions { get; set; }
    }


    public class Customer
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }

        [JsonPropertyName("addressId")]
        public int AddressId { get; set; }

        [JsonPropertyName("branchId")]
        public string BranchId { get; set; }

        [JsonPropertyName("personalDetails")]
        public string PersonalDetails { get; set; }

        [JsonPropertyName("contactDetails")]
        public string ContactDetails { get; set; }

        [JsonPropertyName("address")]
        public object Address { get; set; } 

        [JsonPropertyName("branch")]
        public object Branch { get; set; } 

        [JsonPropertyName("accounts")]
        public object Accounts { get; set; } 
    }



    public class Transaction
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }

        [JsonPropertyName("accountNumber")]
        public int AccountNumber { get; set; }

        [JsonPropertyName("merchantId")]
        public int MerchantId { get; set; }

        [JsonPropertyName("transactionTypeCode")]
        public string TransactionTypeCode { get; set; }

        [JsonPropertyName("transactionDateTime")]
        public DateTime TransactionDateTime { get; set; }

        [JsonPropertyName("transactionAmount")]
        public decimal TransactionAmount { get; set; }

        [JsonPropertyName("otherDetails")]
        public string OtherDetails { get; set; }

        [JsonPropertyName("account")]
        public object Account { get; set; } 

        [JsonPropertyName("transactionType")]
        public object TransactionType { get; set; } 
    }

    public class Address
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("addressId")]
        public int AddressId { get; set; }

        [JsonPropertyName("line1")]
        public string Line1 { get; set; }

        [JsonPropertyName("line2")]
        public string Line2 { get; set; }

        [JsonPropertyName("townCity")]
        public string TownCity { get; set; }

        [JsonPropertyName("zipPostcode")]
        public string ZipPostcode { get; set; }

        [JsonPropertyName("stateProvinceCounty")]
        public string StateProvinceCounty { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("otherDetails")]
        public string OtherDetails { get; set; }

        [JsonPropertyName("branches")]
        public BranchList Branches { get; set; }

        [JsonPropertyName("customers")]
        public CustomerList Customers { get; set; }
    }

    public class Branch
    {
        public Guid BranchId { get; set; } // UNIQUEIDENTIFIER
        public int AddressId { get; set; }
        public Guid BankId { get; set; }
        public string BranchTypeCode { get; set; }
        public string BranchDetails { get; set; }
    }

    public class BranchList
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("$values")]
        public List<Branch> Values { get; set; }
    }

    public class CustomerList
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("$values")]
        public List<Customer> Values { get; set; }
    }



}
