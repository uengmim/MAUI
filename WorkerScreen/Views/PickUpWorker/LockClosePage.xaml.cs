using Camera.MAUI;
using WorkerScreen.Models;
using WorkerScreen.ViewModel.PickUpWorker;
using XNSC.DD.EX;

namespace WorkerScreen.Views.PickUpWorker;

public partial class LockClosePage : ContentPage
{
    /// <summary>
    /// �ڹ��� ���� ������
    /// </summary>
    /// <param name="BoxNum">���� ������ ��ȣ</param>
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

        //ī�޶� ���ڵ� ��� ����
        photoView.BarCodeDetectionEnabled = false;

        //ī�޶� ����
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
        //�ڷΰ���
        Application.Current.MainPage.Navigation.PopAsync();
    }
}