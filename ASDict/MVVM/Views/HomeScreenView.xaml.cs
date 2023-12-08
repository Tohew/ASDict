using ASDict.MVVM.ViewModels;
using CommunityToolkit.Maui.Core.Platform;
using Microsoft.Maui.Platform;

namespace ASDict.MVVM.Views;

public partial class HomeScreenView : ContentPage
{
	public HomeScreenView()
	{
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF1cWWhIYVdpR2Nbe05xdl9DZ1ZRRmYuP1ZhSXxQd0djXH9fcXJWRWNYVUE=");
        InitializeComponent();
        BindingContext = new SuggestionViewModel();
    }

    private void bookmark1_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new BookmarkScreenView());
    }

    private void homeScreen_Clicked(object sender, EventArgs e)
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