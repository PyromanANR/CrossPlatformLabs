using IdentityModel.OidcClient;
using LAB9.ViewModels;
namespace LAB9
{
    public partial class MainPage : ContentPage
    {
        private readonly string ClientId = "RUfFXGMGF7ODFbt6DBca412n6TpfFBXr";
        private readonly string Domain = "dev-r4xfunji2agoyxku.us.auth0.com";
        private readonly string RedirectUri = "com.auth0.myapp://callback";


        public MainPage()
        {
            InitializeComponent();

            InitializeAuth0();

            BindingContext = new MainPageViewModel();
        }

        private async void InitializeAuth0()
        {
            try
            {
                var options = new OidcClientOptions
                {
                    Authority = $"https://{Domain}",
                    ClientId = ClientId,
                    RedirectUri = RedirectUri,
                    Scope = "openid profile email"
                };

                var client = new OidcClient(options);
                var loginRequest = new LoginRequest();

                // Build the auth URL with necessary parameters
                var authorizeUrl = new Uri($"https://{Domain}/authorize" +
                    $"?response_type=code" +
                    $"&client_id={ClientId}" +
                    $"&redirect_uri={Uri.EscapeDataString(RedirectUri)}" +
                    $"&scope=openid%20profile%20email");

                var result = await WebAuthenticator.AuthenticateAsync(authorizeUrl, new Uri(RedirectUri));

                if (result?.AccessToken != null)
                {
                    Console.WriteLine($"Successfully logged in. Token: {result.AccessToken}");
                    // Store the token or update UI as needed
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Authentication was canceled by the user.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Authentication failed: {ex.Message}");
            }
        }
    }

}
