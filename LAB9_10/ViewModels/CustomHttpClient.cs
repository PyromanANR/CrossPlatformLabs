using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace LAB9.ViewModels
{
    public class CustomHttpClient
    {
        public static HttpClient GetHttpClient()
        {
            // Create a custom HttpClientHandler that bypasses certificate validation
            var handler = new HttpClientHandler
            {
                // Disable SSL validation
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            // Create HttpClient using the custom handler
            var client = new HttpClient(handler);

            return client;
        }
    }
}
