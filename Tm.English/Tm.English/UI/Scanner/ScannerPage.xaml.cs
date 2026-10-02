using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tm.English.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScannerPage : ContentPage, IModalPage
	{
		public ScannerPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            i_scanner.IsAnalyzing = true;
            i_scanner.IsScanning = true;

            //Cam autofocus
            TimeSpan ts = TimeSpan.FromMilliseconds(2000);
            Device.StartTimer(ts, () =>
            {
                if (i_scanner.IsAnalyzing)
                {
                    i_scanner.AutoFocus();
                    return true;
                }

                return false;
            });
        }

        protected override void OnDisappearing()
        {
            i_scanner.IsAnalyzing = false;

            base.OnDisappearing();
        }
    }
}