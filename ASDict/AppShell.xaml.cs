namespace ASDict
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            statusBar.StatusBarColor = Color.FromRgba(250, 196, 199, 255);
            statusBar.StatusBarStyle = CommunityToolkit.Maui.Core.StatusBarStyle.DarkContent;
        }
    }
}