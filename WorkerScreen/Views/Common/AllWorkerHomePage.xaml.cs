using WorkerScreen.Models;
using WorkerScreen.ViewModel.Common;

namespace WorkerScreen.Views.Common;

public partial class AllWorkerHomePage : ContentPage
{
    /// <summary>
    /// All 작업자 메인 페이지
    /// </summary>
    /// <param name="log">로그인 정보</param>
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
    /// 카메라 권한 설정
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
                await Application.Current.MainPage.DisplayAlert("알림", "카메라 권한 설정이 필요합니다..", "OK");
            }
        }
    }
    /// <summary>
    /// 로그아웃 버튼 클릭 이벤트입니다.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Logout_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

    }
}