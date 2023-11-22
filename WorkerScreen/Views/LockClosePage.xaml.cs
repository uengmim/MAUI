using Camera.MAUI;
using Camera.MAUI.Test;
using WorkerScreen.Models;
using WorkerScreen.ViewModel;
using XNSC.DD.EX;

namespace WorkerScreen.Views;

public partial class LockClosePage : ContentPage
{


    public LockClosePage(string BoxNum, string Location, string Data, string Name, string ConfNo)
    {

        InitializeComponent();
        photoView.BarCodeDetectionEnabled = false;
        photoView.StartCameraAsync();
        LockCloseViewModel lockCloseViewModel = new LockCloseViewModel();
        lockCloseViewModel.BoxNum = BoxNum;
        lockCloseViewModel.Location = Location;
        lockCloseViewModel.LockData = Data;
        lockCloseViewModel.LockName = Name;
        lockCloseViewModel.CONFNO = ConfNo;
        this.BindingContext = lockCloseViewModel;
        photoView.StartCameraAsync();
    }
    private async void taskPhotoButton_Clicked(object sender, EventArgs e)
    {

        LockCloseViewModel lockCloseViewModel = this.BindingContext as LockCloseViewModel;
        if (lockCloseViewModel != null)
        {
            lockCloseViewModel.PhotoData = photoView.GetSnapShot(Camera.MAUI.ImageFormat.JPEG);
            await lockCloseViewModel.PhotoCommand();

        }

    }

}