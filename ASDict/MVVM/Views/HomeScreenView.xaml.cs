using CommunityToolkit.Maui.Core.Platform;

namespace ASDict.MVVM.Views;

public partial class HomeScreenView : ContentPage
{
	public HomeScreenView()
	{
        InitializeComponent();
    }

    private void bookmark1_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new BookmarkScreenView());
    }

    private void homeScreen_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("ASDict", "You are in Home", "OK");
    }
}