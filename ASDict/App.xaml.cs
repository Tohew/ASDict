using ASDict.MVVM.Views;
using CommunityToolkit.Maui.Behaviors;

namespace ASDict
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (DeviceInfo.Platform == DevicePlatform.Android)
                MainPage = new AppShell();
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
                MainPage = new AppShell();
        }
    }
}
