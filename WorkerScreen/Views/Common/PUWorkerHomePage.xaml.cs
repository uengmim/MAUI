using WorkerScreen.Models;
using WorkerScreen.ViewModel.Common;

namespace WorkerScreen.Views.Common;

public partial class PUWorkerHomePage : ContentPage
{
    /// <summary>
    /// �۾��� ���� ������
    /// </summary>
    /// <param name="log">�α��� ������� ����</param>
    public PUWorkerHomePage(LoginInfo log)
    {
        InitializeComponent();

        PUWorkerHomeViewModel puhomeViewModel = new PUWorkerHomeViewModel();

        puhomeViewModel.Name = log.Name;
        puhomeViewModel.PhoneNumber = log.PhoneNumber;
        puhomeViewModel.EMPNO = log.EMPNO;
        puhomeViewModel.DEPTID = log.DEPTID;

        this.BindingContext = puhomeViewModel;
    }
    /// <summary>
    /// ī�޶� ���� ����
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (cameraStatus != PermissionStatus.Granted)
        {
            cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
            if (cameraStatus != PermissionStatus.Granted)
            {
                await Application.Current.MainPage.DisplayAlert("�˸�", "ī�޶� ���� ������ �ʿ��մϴ�..", "OK");
            }
        }
    }
    /// <summary>
    /// �α׾ƿ� ��ư�Դϴ�.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Logout_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

    }
}