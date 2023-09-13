namespace MauiApp1.Views;

public partial class CameraPage : ContentPage
{
 public CameraPage()
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.NumCamerasDetected > 0)
        {
            if (cameraView.NumMicrophonesDetected > 0)
            {
                cameraView.Microphone = cameraView.Microphones.First();
            }
            cameraView.Camera = cameraView.Cameras.First();

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            });
        }
    }

    private void taskPhotoButton_Clicked(object sender, EventArgs e)
    {
        this.photoImage.Source = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG);
    }

    private void mirrorWsitch_Toggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            this.cameraView.MirroredImage = true;
        }
        else
        {
            this.cameraView.MirroredImage = false;
        }
    }
}