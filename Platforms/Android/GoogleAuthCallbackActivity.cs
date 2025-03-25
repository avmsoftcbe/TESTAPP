using Android.App;
using Android.Content;
using Android.Content.PM;
using Microsoft.Maui.Authentication;

namespace TestApp.Platforms.Android
{
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)] // Set Exported = true
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "org.shabari.sansthan.testapp", DataHost = "Oauth2redirect") ] // Match your custom URI scheme
    public class Oauth2redirect : WebAuthenticatorCallbackActivity
    {
    }
}