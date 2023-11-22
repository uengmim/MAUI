using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using WorkerScreenCrushing.ViewModel;
using WorkerScreenCrushing.Views;
using Camera.MAUI;

namespace WorkerScreenCrushing
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCameraView()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
            builder.Services.AddSingleton<MainPage>();
#endif

            return builder.Build();
        }
    }
}