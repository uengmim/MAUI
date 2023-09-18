using Camera.MAUI;

namespace MauiApp1.Views;

public partial class QRCodePage : ContentPage
{
    public QRCodePage()
    {
        InitializeComponent();
        cameraView.BarCodeDetectionEnabled = true;
        cameraView.StartCameraAsync();
    }

}