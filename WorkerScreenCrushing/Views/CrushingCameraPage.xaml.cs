using Acr.UserDialogs.Infrastructure;
using Camera.MAUI;
using WorkerScreenCrushing.ViewModel;
namespace WorkerScreenCrushing.Views;
public partial class CrushingCameraPage : ContentPage
{
    public CrushingCameraPage(string Name, string BoxName, string Location, string LockData, DateTime PickupDate, DateTime? LockDate)
    {
        InitializeComponent();
        photoView.BarCodeDetectionEnabled = false;
        photoView.StartCameraAsync();
        CrushingCameraViewModel crushingCameraViewModel = new CrushingCameraViewModel();
        crushingCameraViewModel.WorkerName = Name;
        crushingCameraViewModel.BoxName = BoxName;
        crushingCameraViewModel.Location = Location;
        crushingCameraViewModel.LockData = LockData;
        crushingCameraViewModel.PickupDate = PickupDate;
        crushingCameraViewModel.LockDate = (DateTime)LockDate;
        this.BindingContext = crushingCameraViewModel;
        photoView.StartCameraAsync();


    }
    private async void infoInputButton_Clicked(object sender, EventArgs e)
    {

        CrushingCameraViewModel crushingCameraViewModel = this.BindingContext as CrushingCameraViewModel;
        if (crushingCameraViewModel != null)
        {
            crushingCameraViewModel.PhotoData = photoView.GetSnapShot(Camera.MAUI.ImageFormat.JPEG);
            await crushingCameraViewModel.InfoInputCommand();

        }

    }
}