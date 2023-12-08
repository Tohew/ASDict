using ASDict.MVVM.ViewModels;
using CommunityToolkit.Maui.Core.Platform;
using Microsoft.Maui.Platform;

namespace ASDict.MVVM.Views;

public partial class HomeScreenView : ContentPage
{
    private const uint AnimationDuration = 100u;
    public HomeScreenView()
	{
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF1cWWhIYVdpR2Nbe05xdl9DZ1ZRRmYuP1ZhSXxQd0djXH9fcXJWRWNYVUE=");
        InitializeComponent();
        BindingContext = new SuggestionViewModel();
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
            await DisplayAlert("help", "cc, cl", "OK");
    }
    private async void infor_Clicked(object sender, EventArgs e)
    {
        if (isMenuOpen)
            await DisplayAlert("info", "cc, cl", "OK");
    }

    private void bookmark1_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new BookmarkScreenView());
    }

    private void house_icon_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("ASDict", "You are in Home", "OK");
    }

    private async void search_Clicked(object sender, EventArgs e)
    {
        if (InputWord != null)
        {
            if (Platform.CurrentActivity.CurrentFocus != null)
                Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);

            var result = new ContentScreenView();
            var resultViewModel = new DictionaryViewModel(InputWord.Text);
            result.BindingContext = resultViewModel;
            await Navigation.PushModalAsync(result);
        }            
    }
}