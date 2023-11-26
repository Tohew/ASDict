using CommunityToolkit.Maui.Core.Platform;

namespace ASDict.MVVM.Views;

public partial class HomeScreenView : ContentPage
{
	public HomeScreenView()
	{
        InitializeComponent();
        statusBar.StatusBarColor = Color.FromRgba(250, 196, 199, 255);
        statusBar.StatusBarStyle = CommunityToolkit.Maui.Core.StatusBarStyle.DarkContent;
    }
}