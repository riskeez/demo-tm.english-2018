using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tm.English.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();

            BindingContext = ViewModelLocator.GetInstance<HomePageModel>();

            i_history_list.ItemSelected += history_list_ItemSelected;
		}

        private void history_list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}
