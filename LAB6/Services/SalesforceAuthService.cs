using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LAB6.Services
{
    public interface ISalesforceAuthService
    {
        Task<AuthResult> AuthenticateAsync(string username, string password, string securityToken);
    }

    public class SalesforceAuthService : ISalesforceAuthService
    {
        private readonly string ClientId = "3MVG9si4IYYQbK9eFGnw5btDwJGMXZDjoZoU3xkMVeCUaOma_pXnoe.6kzlfkwHgs8B0EA2Mb9kXJnZs4Nmll";
        private readonly string ClientSecret = "08F1E867B0909F9BCD94CE1FA371D54CB778F60A48DBB8C96E1053DAD9012F70";
        private readonly string loginEndpoint = "https://login.salesforce.com/services/oauth2/token";  // Salesforce OAuth token endpoint

        public async Task<AuthResult> AuthenticateAsync(string username, string password, string securityToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                var requestData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "password" },
                    {"client_id", ClientId },
                    {"client_secret", ClientSecret },
                    {"username", username },
                    {"password", $"{password}{securityToken}" } 
                });
              

        
                var response = await client.PostAsync(loginEndpoint, requestData);

                if (response.IsSuccessStatusCode)
                {
                 
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);

           
                    return new AuthResult
                    {
                        IsSuccess = true,
                        AuthToken = values["access_token"],
                        InstanceUrl = values["instance_url"]
                    };
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();

              
                    return new AuthResult
                    {
                        IsSuccess = false,
                        Result = errorResponse
                    };
                }
            }
        }
    }

    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public string AuthToken { get; set; }
        public string InstanceUrl { get; set; }

        public string Result { get; set; }
    }
}
