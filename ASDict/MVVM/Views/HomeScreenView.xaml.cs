using ASDict.MVVM.ViewModels;
using CommunityToolkit.Maui.Core.Platform;
using Microsoft.Maui.Platform;

namespace ASDict.MVVM.Views;

public partial class HomeScreenView : ContentPage
{
    public ImageButton SearchButton => search;
    private const uint AnimationDuration = 100u;

    public HomeScreenView()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF1cWWhIYVdpR2Nbe05xdl9DZ1ZRRmYuP1ZhSXxQd0djXH9fcXJWRWNYVUE=");
        InitializeComponent();
        BindingContext = new HomeScreenViewModel();
    }

    private bool isMenuOpen = false;
    async void menu_Clicked(object sender, EventArgs e)
    {

        if (!isMenuOpen)
            _ = ContentGrid.TranslateTo(-this.Width * 0.5, this.Height * 0, AnimationDuration, Easing.CubicIn);
        else
            await CloseMenu();
        isMenuOpen = !isMenuOpen;
    }

    private async Task CloseMenu()
    {
        //Close the menu and bring back back the main content
        _ = ContentGrid.FadeTo(1, AnimationDuration);
        _ = ContentGrid.ScaleTo(1, AnimationDuration);
        await ContentGrid.TranslateTo(0, 0, AnimationDuration, Easing.CubicIn);
    }

    private void bookmark_Clicked(object sender, EventArgs e)
    {
        if (isMenuOpen)
            Navigation.PushModalAsync(new BookmarkScreenView());
    }

    private async void help_Clicked(object sender, EventArgs e)
    {
        if (isMenuOpen)
        {
            string url = "https://nghgi.github.io/ASDict-Download/";
            _ = Launcher.OpenAsync(new Uri(url));
        }
    }
    private async void infor_Clicked(object sender, EventArgs e)
    {
        if (isMenuOpen)
        {//await DisplayAlert("About us", "Welcome to our ASDict app! At GHQ, we're dedicated to crafting this tool to help you expand your vocabulary and use language more precisely. We offer an intuitive search for synonyms and antonyms, allowing you to gain a deeper understanding of word meanings and their contextual usage. With cross-platform support, our app is ready to accompany you across various devices. Thank you for choosing our app to enhance your language skills!", "OK");
            bool isUpdateChecked = await App.Current.MainPage.DisplayAlert("About us", "Welcome to our ASDict app! At GHQ, we're dedicated to crafting this tool to help you expand your vocabulary and use language more precisely. We offer an intuitive search for synonyms and antonyms, allowing you to gain a deeper understanding of word meanings and their contextual usage. With cross-platform support, our app is ready to accompany you across various devices.\n\nIf you have any questions, please feel free to contact us via email:\n\n22520357@gm.uit.edu.com\n22521205@gm.uit.edu.com\n22520577@gm.uit.edu.com\n\nTo check for update, visit our website", "Update", "OK");

            if (isUpdateChecked)
            {
                string url = "https://nghgi.github.io/ASDict-Download/";
                _ = Launcher.OpenAsync(new Uri(url));
            }
        }
    }
}