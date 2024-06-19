using Camera.MAUI;
using WorkerScreen.Models;
using WorkerScreen.ViewModel.PickUpWorker;
using XNSC.DD.EX;

namespace WorkerScreen.Views.PickUpWorker;

public partial class LockClosePage : ContentPage
{
    /// <summary>
    /// 자물쇠 봉인 페이지
    /// </summary>
    /// <param name="BoxNum">보안 문서함 번호</param>
    /// <param name="Location"></param>
    /// <param name="Data"></param>
    /// <param name="Name"></param>
    /// <param name="ConfNo"></param>

    public LockClosePage(string BoxNum, string Location, string Data, string Name, string ConfNo)
    {

        InitializeComponent();

        LockCloseViewModel lockCloseViewModel = new LockCloseViewModel();

        lockCloseViewModel.BoxNum = BoxNum;
        lockCloseViewModel.Location = Location;
        lockCloseViewModel.LockData = Data;
        lockCloseViewModel.LockName = Name;
        lockCloseViewModel.CONFNO = ConfNo;

        this.BindingContext = lockCloseViewModel;

        //카메라 바코드 기능 해제
        photoView.BarCodeDetectionEnabled = false;

        //카메라 시작
        photoView.StopRecordingAsync();
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
    private void BackBtn_Clicked(object sender, EventArgs e)
    {
        //뒤로가기
        Application.Current.MainPage.Navigation.PopAsync();
    }
}