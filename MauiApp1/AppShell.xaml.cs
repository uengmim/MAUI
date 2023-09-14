namespace MauiApp1;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(Views.NotePage), typeof(Views.NotePage));

        Routing.RegisterRoute(nameof(Views.AboutPage), typeof(Views.AboutPage));

        Routing.RegisterRoute(nameof(Views.AllNotesPage), typeof(Views.AllNotesPage));

        Routing.RegisterRoute(nameof(Views.CameraPage), typeof(Views.CameraPage));

        Routing.RegisterRoute(nameof(Views.QRCodePage), typeof(Views.QRCodePage));

    }
}
