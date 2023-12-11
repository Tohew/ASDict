using ASDict.MVVM.Views;
using CommunityToolkit.Maui.Behaviors;
using Microsoft.Maui.ApplicationModel;

namespace ASDict
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            Application.Current.UserAppTheme = AppTheme.Light;
        }
    }
}
