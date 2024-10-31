using LAB5.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LAB5.Services
{
    public class AccountList
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("$values")]
        public List<Account> Values { get; set; }
    }

    public class CustomerList
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("$values")]
        public List<Customer> Values { get; set; }
    }

    public class TransactionList
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("$values")]
        public List<Transaction> Values { get; set; }
    }



    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            var response = await _httpClient.GetAsync("api/accounts");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error fetching accounts: {response.StatusCode} - {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            // Log the JSON response for debugging
            Console.WriteLine($"Received JSON: {json}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Deserialize the JSON into the AccountList object
            AccountList accountLists = JsonSerializer.Deserialize<AccountList>(json, options);

            // Return the list of accounts from the AccountList object
            return accountLists.Values; // Assuming AccountList has a property called Accounts
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Account>($"https://192.168.56.10:5074/api/accounts/{id}");
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var response = await _httpClient.GetAsync("api/references/customers");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error fetching customers: {response.StatusCode} - {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            // Log the JSON response for debugging
            Console.WriteLine($"Received JSON: {json}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Deserialize the JSON into the CustomerList object
            CustomerList customerList = JsonSerializer.Deserialize<CustomerList>(json, options);

            // Return the list of customers from the CustomerList object
            return customerList.Values; // Return the list of customers
        }


        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            var response = await _httpClient.GetAsync("api/references/transactions");

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

        // Method to search transactions
        public async Task<List<Transaction>> SearchTransactionsAsync(DateTime startDate, DateTime endDate)
        {
            var response = await _httpClient.GetAsync($"api/search/transactions?startDate={startDate}&endDate={endDate}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Deserialize into AccountList instead of directly into List<Account>
                var transactionList = JsonSerializer.Deserialize<TransactionList>(json, options);
                return transactionList?.Values; // Ensure Values is not null
            }

            throw new Exception("Error fetching transactions.");
        }

        // Method to search customers
        public async Task<List<Customer>> SearchCustomersAsync(string personalDetail)
        {
            var response = await _httpClient.GetAsync($"api/search/customers?personalDetail={personalDetail}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Deserialize into AccountList instead of directly into List<Account>
                var custumerList = JsonSerializer.Deserialize<CustomerList>(json, options);
                return custumerList?.Values; // Ensure Values is not null

            }

            throw new Exception("Error fetching customers.");
        }

        // Method to search accounts
        public async Task<List<Account>> SearchAccountsAsync(List<int> accountIds)
        {
            // Build the query string for multiple account IDs
            var query = string.Join("&", accountIds.Select(id => $"accountIds={id}"));
            var response = await _httpClient.GetAsync($"api/search/accounts?{query}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received JSON: {json}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Deserialize into AccountList instead of directly into List<Account>
                var accountList = JsonSerializer.Deserialize<AccountList>(json, options);
                return accountList?.Values; // Ensure Values is not null
            }

            throw new Exception("Error fetching accounts.");
        }

        public async Task<string> ConvertTimeAsync(DateTime inputDate)
        {
            string accessToken = Services.GetToken.GetAccessToken(); // Get the access token

            // Format the date-time as a string
            string dateTimeString = inputDate.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            // URL-encode the date-time string
            string encodedDateTime = Uri.EscapeDataString(dateTimeString);

            // Create the complete API request URL
            string requestUrl = $"https://192.168.56.10:5074/api/time/convert?inputDate={encodedDateTime}";

            // Prepare the request message
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            // Send the GET request
            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                // Read and return the response content
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Error calling API: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }


        public async Task<Address> GetAddressAsync(int addressId, string apiVersion = "v1")
        {
            var response = await _httpClient.GetAsync($"api/{apiVersion}/addresses/{addressId}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error fetching address: {response.StatusCode} - {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Received JSON from API {apiVersion}: {json}");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<Address>(json, options);
        }

    }
}
