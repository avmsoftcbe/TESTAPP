using Microsoft.Maui.Controls;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using System.Linq;
namespace TestApp
{
    public partial class ScannerPage : ContentPage
        {
            public ScannerPage()
            {
                InitializeComponent();


            BarcodeReader.Options = new BarcodeReaderOptions
            {
                Formats = BarcodeFormat.QrCode | BarcodeFormat.Code39 | BarcodeFormat.Code128 | BarcodeFormat.Code93,
                Multiple = false,
                AutoRotate = true,
                TryHarder = false,
                TryInverted = false
            };
        }

        private void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
            {
                var barcode = e.Results.FirstOrDefault();
                if (barcode != null)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        // Stop scanning once a barcode is found
                        BarcodeReader.IsDetecting = false;
                        await DisplayAlert("Scanned Result", $"Value: {barcode.Value}\nType: {barcode.Format}", "OK");
                        // Close the scanner page
                        await Navigation.PopAsync();
                    });
                }
            }
        }
}