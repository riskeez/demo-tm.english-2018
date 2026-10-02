using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tm.English.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeparatorLabel : ContentView
	{
		public SeparatorLabel ()
		{
			InitializeComponent ();
		}


        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(SeparatorLabel), null);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}