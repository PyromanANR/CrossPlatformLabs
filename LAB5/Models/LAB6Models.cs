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



}
