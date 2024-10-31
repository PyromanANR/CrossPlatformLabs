using RestSharp;
using Newtonsoft.Json.Linq;

namespace LAB5.Services
{
    public class GetToken()
    {
        public static string GetAccessToken()
        {
            var client = new RestClient("https://dev-r4xfunji2agoyxku.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");

            var body = new JObject
            {
                { "client_id", "RUfFXGMGF7ODFbt6DBca412n6TpfFBXr" },
                { "client_secret", "ZliPXyyIKw7ykLpN102cDEGpFWXiaME7RZfCq0AXjpKeOk9FOd6XMVb9zQOhG7Nj" },
                { "audience", "https://dev-r4xfunji2agoyxku.us.auth0.com/api/v2/" },
                { "grant_type", "client_credentials" }
            }.ToString();

            request.AddParameter("application/json", body, ParameterType.RequestBody);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var jsonResponse = JObject.Parse(response.Content);
                return jsonResponse["access_token"].ToString();
            }
            else
            {
                throw new Exception($"Error retrieving access token: {response.Content}");
            }
        }
    }

}
