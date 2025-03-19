using System.Net.Http;
using System.Web;
using System.Text.Json;
using Google.Apis.Auth;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Auth.OAuth2;
using Microsoft.Maui.Controls.PlatformConfiguration;
using TestApp.Services;
namespace TestApp;

public partial class MainPage : ContentPage
{
    private readonly MyGoogleAuthService _googleAuthService;
    int count = 0;
    public MainPage()
    {
        InitializeComponent();
        _googleAuthService = new MyGoogleAuthService();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

        await Navigation.PushAsync(new ScannerPage());

    }



    private async void OnGoogleLoginClicked(object sender, EventArgs e)
    {
        string accessToken = await GoogleAuthService.SignInWithGoogleAsync();

        if (!string.IsNullOrEmpty(accessToken))
        {
            await DisplayAlert("Login Successful", "Access Token: " + accessToken, "OK");
        }
        else
        {
            await DisplayAlert("Login Failed", "Could not log in with Google", "OK");
        }
    }

    private async void GoogleAccountInfo_Clicked(object sender, EventArgs e)
    {
        string accessToken = await _googleAuthService.AuthenticateAsync();

        if (accessToken != null)
        {
            // Authentication successful, use the access token
            Console.WriteLine($"Access Token: {accessToken}");

            await DisplayAlert("Success", "Google Sign-in successful!", "OK");

            //Navigate to the next page, or perform other actions.
        }
        else
        {
            // Authentication failed
            await DisplayAlert("Error", "Google Sign-in failed.", "OK");
        }
    }
}