using Microsoft.Maui.Controls;

namespace ASDict
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Light;

        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MVVM.Views.ContentScreenView());

        }
        private void GiangBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MVVM.Views.BookmarkScreenView());
        }
    }
}