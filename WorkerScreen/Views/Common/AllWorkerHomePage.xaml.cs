using WorkerScreen.Models;
using WorkerScreen.ViewModel.Common;

namespace WorkerScreen.Views.Common;

public partial class AllWorkerHomePage : ContentPage
{
    /// <summary>
    /// All �۾��� ���� ������
    /// </summary>
    /// <param name="log">�α��� ����</param>
    public AllWorkerHomePage(LoginInfo log)
    {
        InitializeComponent();

        AllWorkerHomeViewModel allhomeViewModel = new AllWorkerHomeViewModel();

        allhomeViewModel.Name = log.Name;
        allhomeViewModel.PhoneNumber = log.PhoneNumber;
        allhomeViewModel.EMPNO = log.EMPNO;
        allhomeViewModel.DEPTID = log.DEPTID;

        this.BindingContext = allhomeViewModel;
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
    /// �α׾ƿ� ��ư Ŭ�� �̺�Ʈ�Դϴ�.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Logout_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

    }
}