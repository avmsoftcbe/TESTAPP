using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Android.Accounts;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Android;
using Microsoft.Maui.ApplicationModel;
using System.Linq;


namespace TestApp
{
    [Activity(Label = "TestApp", Theme = "@style/Maui.SplashTheme", MainLauncher = true,
     ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
     ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : MauiAppCompatActivity
    {
    }



}
