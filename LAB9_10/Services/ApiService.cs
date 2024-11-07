using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using LAB9.Models;
using System.Diagnostics;
using LAB9.ViewModels;
using System.Text.Json.Serialization;

namespace LAB9.Services
{
    public class TransactionList
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("$values")]
        public List<Transaction> Values { get; set; }
    }

    public class BranchListWrapper
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("$values")]
        public List<Branch> Values { get; set; }
    }

    public class BranchTypeList
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("$values")]
        public List<RefBranchType> Values { get; set; }
    }

    public class AddressListWrapper
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("$values")]
        public List<Address> Values { get; set; }
    }

    public class BankListWrapper
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("$values")]
        public List<Bank> Values { get; set; }
    }




    internal class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://192.168.56.10:5074/api";

        public ApiService()
        {
            _httpClient = CustomHttpClient.GetHttpClient();
        }

        public async Task<List<Branch>> GetBranchesAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/branches");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error fetching branches: {response.StatusCode} - {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            // Log the JSON response for debugging
            Console.WriteLine($"Received JSON: {json}");
            // Deserialize into BranchListWrapper
            var branchListWrapper = JsonSerializer.Deserialize<BranchListWrapper>(json);
            // Return the list of branches
            return branchListWrapper?.Values;
        }


        public async Task<bool> AddBranchAsync(Branch branch)
        {
            branch.Id = null;
            branch.BankId = Guid.Parse("10F58903-556A-4E7E-971F-0071B56E785A");
            branch.BranchId = Guid.NewGuid();
            branch.AddressId = 3;
            var json = JsonSerializer.Serialize(branch);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/branches", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<RefBranchType>> GetBranchTypesAsync()
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}/branchtypes");

            // Deserialize into the wrapper class to access the '$values' property
            var branchTypeList = JsonSerializer.Deserialize<BranchTypeList>(response);

            // Return the list of RefBranchType objects from the '$values' property
            return branchTypeList?.Values ?? new List<RefBranchType>();
        }


        // Method to fetch addresses
        public async Task<List<Address>> GetAddressesAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/mobileaddresses");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error fetching addresses: {response.StatusCode} - {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var addressListWrapper = JsonSerializer.Deserialize<AddressListWrapper>(json);
            return addressListWrapper?.Values ?? new List<Address>();
        }

        // Method to add a new address
        public async Task<bool> AddAddressAsync(Address address)
        {
            var json = JsonSerializer.Serialize(address);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/mobileaddresses", content);
            return response.IsSuccessStatusCode;
        }

        // Method to fetch banks
        public async Task<List<Bank>> GetBanksAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/banks");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error fetching banks: {response.StatusCode} - {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var bankListWrapper = JsonSerializer.Deserialize<BankListWrapper>(json);
            return bankListWrapper?.Values ?? new List<Bank>();
        }

        // Method to add a new bank
        public async Task<bool> AddBankAsync(Bank bank)
        {
            bank.BankId = Guid.NewGuid(); 
            var json = JsonSerializer.Serialize(bank);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/banks", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/references/transactions");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error fetching transactions: {response.StatusCode} - {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            // Log the JSON response for debugging
            Console.WriteLine($"Received JSON: {json}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Deserialize the JSON into the TransactionList object
            TransactionList transactionList = JsonSerializer.Deserialize<TransactionList>(json, options);

            // Return the list of transactions from the TransactionList object
            return transactionList.Values; // Return the list of transactions
        }



    }
}
