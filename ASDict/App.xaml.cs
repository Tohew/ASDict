using ASDict.MVVM.Views;
using CommunityToolkit.Maui.Behaviors;

namespace ASDict
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}