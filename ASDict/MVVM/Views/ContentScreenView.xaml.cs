using ASDict.MVVM.ViewModels;
using CommunityToolkit.Maui.Core.Views;
using Microsoft.Maui.Controls;

namespace ASDict.MVVM.Views;

public partial class ContentScreenView : ContentPage
{
    private const uint AnimationDuration = 100u;

    public ContentScreenView()
    {
        InitializeComponent();
        BindingContext = new DictionaryViewModel();
    }

    async void menu_Clicked(object sender, EventArgs e)
    {
        _ = ContentGrid.TranslateTo(-this.Width * 0.5, this.Height * 0, AnimationDuration, Easing.CubicIn);
        //await ContentGrid.ScaleTo(0.8, AnimationDuration);
        //_ = ContentGrid.FadeTo(0.8, AnimationDuration);
    }

    private void search_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new HomeScreenView());
    }

    private void bookmark1_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new BookmarkScreenView());
    }

    private void house_icon_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new HomeScreenView());
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

        if (BindingContext is DictionaryViewModel viewModel)
        {
            viewModel.ConvertToSyn();
        }
    }

    private void antonym_Clicked(object sender, EventArgs e)
    {
        antonym.BackgroundColor = Color.FromRgb(248, 76, 84);
        antonym.TextColor = Color.FromRgb(255, 255, 255);

        synonym.BackgroundColor = Color.FromRgb(51, 58, 87);
        synonym.TextColor = Color.FromRgb(128, 128, 128);

        if (BindingContext is DictionaryViewModel viewModel)
        {
            viewModel.ConvertToAnt();
        }
    }

    async void GridArea_Tapped(System.Object sender, System.EventArgs e)
    {
        await CloseMenu();
    }

    private async Task CloseMenu()
    {
        //Close the menu and bring back back the main content
        _ = ContentGrid.FadeTo(1, AnimationDuration);
        _ = ContentGrid.ScaleTo(1, AnimationDuration);
        await ContentGrid.TranslateTo(0, 0, AnimationDuration, Easing.CubicIn);
    }
}