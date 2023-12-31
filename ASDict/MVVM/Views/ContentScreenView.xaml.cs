using ASDict.MVVM.Models;
using ASDict.MVVM.ViewModels;
using CommunityToolkit.Maui.Core.Views;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace ASDict.MVVM.Views;

public partial class ContentScreenView : ContentPage
{
    private const uint AnimationDuration = 100u;

    public ContentScreenView()
    {
        InitializeComponent();
        BindingContext = new DictionaryViewModel();
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
    private void synonymWin_Clicked(object sender, EventArgs e)
    {
        synonymWin.BackgroundColor = Color.FromRgb(248, 76, 84);
        synonymWin.TextColor = Color.FromRgb(255, 255, 255);

        antonymWin.BackgroundColor = Color.FromRgb(51, 58, 87);
        antonymWin.TextColor = Color.FromRgb(128, 128, 128);

        if (BindingContext is DictionaryViewModel viewModel)
        {
            viewModel.ConvertToSyn();
        }
    }

    private void antonymWin_Clicked(object sender, EventArgs e)
    {
        antonymWin.BackgroundColor = Color.FromRgb(248, 76, 84);
        antonymWin.TextColor = Color.FromRgb(255, 255, 255);

        synonymWin.BackgroundColor = Color.FromRgb(51, 58, 87);
        synonymWin.TextColor = Color.FromRgb(128, 128, 128);

        if (BindingContext is DictionaryViewModel viewModel)
        {
            viewModel.ConvertToAnt();
        }
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
            await DisplayAlert("About us", "Welcome to our ASDict app! At GHQ, we're dedicated to crafting this tool to help you expand your vocabulary and use language more precisely. We offer an intuitive search for synonyms and antonyms, allowing you to gain a deeper understanding of word meanings and their contextual usage. With cross-platform support, our app is ready to accompany you across various devices. Thank you for choosing our app to enhance your language skills!", "OK");
    }
}