using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tm.English.Data;
using Tm.English.UI;
using Xamarin.Forms;

namespace Tm.English
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            StartUp.RegisterServices();

            MainPage = new NavigationPage(new HomePage());
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
            OnResume();
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{

		}
	}
}
