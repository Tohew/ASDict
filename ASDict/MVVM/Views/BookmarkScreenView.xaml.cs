using ASDict.MVVM.ViewModels;
using Microsoft.Maui.Platform;

namespace ASDict.MVVM.Views;

public partial class BookmarkScreenView : ContentPage
{
	public BookmarkScreenView()
	{
		InitializeComponent();
	}

    private void AppIcon_Clicked(object sender, EventArgs e)
    {

    }

    private void MenuIcon_Clicked(object sender, EventArgs e)
    {

    }

    private void home_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new HomeScreenView());
    }

    private void bookmark_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("ASDict", "You are in Bookmark", "OK");
    }

    private async void search_Clicked(object sender, EventArgs e)
    {
        if (Platform.CurrentActivity.CurrentFocus != null)
            Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);

        var result = new ContentScreenView();
        var resultViewModel = new DictionaryViewModel(InputWord.Text);
        result.BindingContext = resultViewModel;
        await Navigation.PushModalAsync(result);
    }
}