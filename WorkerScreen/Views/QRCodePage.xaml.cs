using Acr.UserDialogs.Infrastructure;
using Camera.MAUI;
using Camera.MAUI.Test;

namespace WorkerScreen.Views;

public partial class QRCodePage : ContentPage
{
    public QRCodePage(string Name, string PhoneNumber)
    {
        InitializeComponent();
        cameraView.BarCodeDetectionEnabled = true;
        cameraView.StartCameraAsync();
        QRCodeViewModel qRCodeViewModel = new QRCodeViewModel();

        qRCodeViewModel.Name = Name;
        qRCodeViewModel.PhoneNumber = PhoneNumber;

        this.BindingContext = qRCodeViewModel;

    }

}