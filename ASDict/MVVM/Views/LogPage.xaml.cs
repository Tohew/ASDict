using MetroLog;
using MetroLog.Maui;
using Microsoft.Extensions.Logging;

namespace ASDict.MVVM.Views;

public partial class LogPage : ContentPage
{
	ILogger<HomeScreenView> _logger;
	public LogPage(ILogger<HomeScreenView> logger)
	{
		InitializeComponent();
		BindingContext = new LogController();

		_logger = logger;
	}

    private void home_Clicked(object sender, EventArgs e)
    {
		App.Current.MainPage.Navigation.PushModalAsync(new HomeScreenView());
    }
}