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

    private void search_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Bạn đã nhấn search!", "OK");
    }

    private void bookmark1_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Bạn đã nhấn bookmark!", "OK");
    }

    private void house_icon_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Bạn đã nhấn house!", "OK");
    }

    private bool isStarWhite = true;
    private void star_white_Clicked(object sender, EventArgs e)
    {
        if (isStarWhite)
            star_white.Source = "Resources/Images/star_icon_blue.svg";
        else
            star_white.Source = "Resources/Images/star_icon_white.svg";
        isStarWhite = !isStarWhite;
    }

    private void synonym_Clicked(object sender, EventArgs e)
    {
        synonym.BackgroundColor = Color.FromRgb(248, 76, 84);
        synonym.TextColor = Color.FromRgb(255, 255, 255);

        antonym.BackgroundColor = Color.FromRgb(51, 58, 87);
        antonym.TextColor = Color.FromRgb(128, 128, 128);
    }

    private void antonym_Clicked(object sender, EventArgs e)
    {
        antonym.BackgroundColor = Color.FromRgb(248, 76, 84);
        antonym.TextColor = Color.FromRgb(255, 255, 255);

        synonym.BackgroundColor = Color.FromRgb(51, 58, 87);
        synonym.TextColor = Color.FromRgb(128, 128, 128);
    }
}