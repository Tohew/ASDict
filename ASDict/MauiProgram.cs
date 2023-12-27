using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;
using ASDict.MVVM.ViewModels;
using ASDict.MVVM.Views;
using Android.Content.Res;

namespace ASDict
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("PatrickHand-Regular.ttf", "PatrickHand");

                    fonts.AddFont("FASolid.otf", "FAS");
                    fonts.AddFont("FABrands.otf", "FAB");
                    fonts.AddFont("FARegular5.otf", "FAR");

                });
            builder.Services.AddSingleton<HomeScreenView>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}