using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util.Store;
using Microsoft.Maui.ApplicationModel;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Text.Json;

namespace TestApp
{

    public class GoogleAuthService
    {
        private const string ClientId = "1071284067417-qu97f9avhf5v4ccsh7rakiimo3ibp537.apps.googleusercontent.com";
        private const string ClientSecret = "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";
        private const string RedirectUri = "TestApp:/oauth2redirect";

        public static async Task<string> SignInWithGoogleAsync()
        {
            try
            {
                // Generate Google OAuth URL
                string authUrl = $"https://accounts.google.com/o/oauth2/auth" +
                    $"?client_id={ClientId}" +
                    $"&redirect_uri={RedirectUri}" +
                    $"&response_type=code" +
                    $"&scope=openid%20email%20profile" +
                    $"&access_type=offline";

                // Open external browser for login
                var browserTask = Launcher.OpenAsync(new Uri(authUrl));

                // Start local server to listen for redirect
                var listener = new HttpListener();
                listener.Prefixes.Add($"{RedirectUri}/");
                listener.Start();

                var context = await listener.GetContextAsync();
                var code = HttpUtility.ParseQueryString(context.Request.Url.Query).Get("code");

                // Send response to browser
                using var response = context.Response;
                string responseString = "<html><body><h1>Login Successful! You can close this window.</h1></body></html>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                listener.Stop();

                if (string.IsNullOrEmpty(code))
                    throw new Exception("Authorization failed.");

                return await GetAccessTokenAsync(code);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Google Sign-In Error: {ex.Message}");
                return null;
            }
        }

        private static async Task<string> GetAccessTokenAsync(string code)
        {
            using var client = new HttpClient();

            var values = new Dictionary<string, string>
        {
            { "code", code },
            { "client_id", ClientId },
            { "redirect_uri", RedirectUri },
            { "grant_type", "authorization_code" }
        };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var json = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);
            return json["access_token"].ToString();
        }
    }
}


namespace TestApp.Services
{
    public class MyGoogleAuthService
    {
        public async Task<string> AuthenticateAsync()
        {
            try
            {
                var authResult = await AuthenticateAsyncInternal();

                if (authResult?.AccessToken != null)
                {
                    return authResult.AccessToken;
                }

                return null; // Authentication failed or was canceled.
            }
            catch (Exception ex)
            {
                // Handle authentication exceptions (e.g., network issues, user cancellation)
                Console.WriteLine($"Google Authentication Error: {ex.Message}");
                return null;
            }
        }

        private async Task<WebAuthenticatorResult> AuthenticateAsyncInternal()
        {

            var authUrl = "https://accounts.google.com/o/oauth2/v2/auth"; // Google's OAuth 2.0 endpoint
            var clientId = "1071284067417-qu97f9avhf5v4ccsh7rakiimo3ibp537.apps.googleusercontent.com"; // Replace with your Google Client ID
            var redirectUrl = "testapp:/auth"; // Replace with your Google Redirect URI (e.g., your app's custom scheme)
            var scope = "openid profile email"; // Scopes requested (e.g., basic profile info, email)

            var authUrlBuilder = new UriBuilder(authUrl);
            authUrlBuilder.Query = $"client_id={clientId}&redirect_uri={Uri.EscapeDataString(redirectUrl)}&response_type=code&scope={Uri.EscapeDataString(scope)}";

            var authUri = authUrlBuilder.Uri;
            var redirectUri = new Uri(redirectUrl);

            return await WebAuthenticator.AuthenticateAsync(authUri, redirectUri);
        }
    }
}
