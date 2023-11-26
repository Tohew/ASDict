using CommunityToolkit.Maui.Core.Views;
using Microsoft.Maui.Controls;

namespace ASDict.MVVM.Views;

public partial class ContentScreenView : ContentPage
{
	public ContentScreenView()
	{

        InitializeComponent();
	}

    private void menu_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Bạn đã nhấn menu!", "OK");
    }

    private void bookmark_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Bạn đã nhấn bookmark!", "OK");
    }
}