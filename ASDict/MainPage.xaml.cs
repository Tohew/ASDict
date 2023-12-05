using Microsoft.Maui.Controls;

namespace ASDict
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF1cWWhIYVdpR2Nbe05xdl9DZ1ZRRmYuP1ZhSXxQd0djXH9fcXJWRWNYVUE=");
            InitializeComponent();
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