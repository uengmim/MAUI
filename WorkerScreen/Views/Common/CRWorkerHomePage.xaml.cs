using WorkerScreen.Models;
using WorkerScreen.ViewModel.Common;

namespace WorkerScreen.Views.Common;

public partial class CRWorkerHomePage : ContentPage
{
    /// <summary>
    /// �ļ� �۾��� ���� ������
    /// </summary>
    /// <param name="log">�α��� ����</param>
    public CRWorkerHomePage(LoginInfo log)
    {
        InitializeComponent();

        CRWorkerHomeViewModel crhomeViewModel = new CRWorkerHomeViewModel();

        crhomeViewModel.Name = log.Name;
        crhomeViewModel.PhoneNumber = log.PhoneNumber;

        this.BindingContext = crhomeViewModel;
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