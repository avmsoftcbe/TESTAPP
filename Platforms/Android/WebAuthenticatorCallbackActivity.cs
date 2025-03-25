using Android.App;
using Android.Content;
using Microsoft.Maui.ApplicationModel;
using Android.Content.PM;
using Android.OS;

namespace TestApp.Platforms.Android
{
 
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
                  Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
                  DataScheme = CALLBACK_SCHEME, DataHost = CALLBACK_HOST)]
    public class WebAuthenticationCallbackActivity : Microsoft.Maui.Authentication.WebAuthenticatorCallbackActivity
    {
        const string CALLBACK_SCHEME = "TestApp";
        const string CALLBACK_HOST = "Oauth2redirect";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
    }
}