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
}