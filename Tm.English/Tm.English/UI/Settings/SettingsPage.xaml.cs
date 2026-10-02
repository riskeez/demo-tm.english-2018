using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tm.English.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty ToolbarProperty =
            BindableProperty.Create(nameof(Toolbar), typeof(IEnumerable<ToolbarItem>), typeof(SettingsPage), null, BindingMode.TwoWay, null, OnPropertyChanged);

        public IEnumerable<ToolbarItem> Toolbar
        {
            get => (IEnumerable<ToolbarItem>)GetValue(ToolbarProperty);
            set => SetValue(ToolbarProperty, value);
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Page page)
            {
                page.ToolbarItems.Clear();
                foreach (var tbItem in newValue as IEnumerable<ToolbarItem>)
                {
                    page.ToolbarItems.Add(tbItem);
                }
            }
        }
    }
}