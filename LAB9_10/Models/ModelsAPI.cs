using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LAB9.Models
{
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

    public class Branch
    {
        [JsonIgnore]
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("branchId")]
        public Guid BranchId { get; set; }

        [JsonPropertyName("addressId")]
        public int AddressId { get; set; }

        [JsonPropertyName("bankId")]
        public Guid BankId { get; set; }

        [JsonPropertyName("branchTypeCode")]
        public string BranchTypeCode { get; set; }

        [JsonPropertyName("branchDetails")]
        public string BranchDetails { get; set; }

        // A reference to another branch (in case of circular reference with $ref)
        [JsonPropertyName("bank")]
        public Bank Bank { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        // Placeholder for null or empty customer data
        [JsonPropertyName("customers")]
        public object Customers { get; set; }
    }

    public class Bank
    {
        [JsonIgnore]
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("bankId")]
        public Guid BankId { get; set; }

        [JsonPropertyName("bankDetails")]
        public string BankDetails { get; set; }

        // Nested branches in the bank
        [JsonPropertyName("branches")]
        public BranchList Branches { get; set; }
    }

    public class Address
    {
        [JsonIgnore]
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("addressId")]
        [JsonIgnore]
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
        [JsonIgnore]
        public BranchList Branches { get; set; }

        [JsonPropertyName("customers")]
        [JsonIgnore]
        public CustomerList Customers { get; set; }
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



    public class RefBranchType
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("branchTypeCode")]
        public string BranchTypeCode { get; set; }

        [JsonPropertyName("branchTypeDescription")]
        public string BranchDescription { get; set; }
    }

}
