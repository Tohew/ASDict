using Microsoft.Maui.Controls;

namespace ASDict
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MVVM.Views.ContentScreenView());
        }

        
    }
}