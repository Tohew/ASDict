using ASDict.MVVM.Views;

namespace ASDict
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ContentScreenView();
        }
    }
}