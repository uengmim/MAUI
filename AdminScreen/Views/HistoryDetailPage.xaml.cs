using AdminScreen.ViewModels;

namespace AdminScreen.Views;

public partial class HistoryDetailPage : ContentPage
{

    public HistoryDetailPage(string LSN, string LKNM, string MAC, string LKTYP, string CONFNO)
    {

        InitializeComponent();
        HistoryDetailViewModel historyDetailViewModel = new HistoryDetailViewModel();
        historyDetailViewModel.LSN = LSN;
        historyDetailViewModel.LKNM = LKNM;
        historyDetailViewModel.MAC = MAC;
        historyDetailViewModel.LKTYP = LKTYP;
        historyDetailViewModel.CONFNO = CONFNO;
        this.BindingContext = historyDetailViewModel;

    }
    // 중복 실행 방지를 위한 플래그
    private bool isEventHandling = false;
    protected override async void OnAppearing()
    {
        // 중복 실행 방지 플래그 체크
        if (isEventHandling)
        {
            // 이미 실행 중이라면 더 이상 실행하지 않음
            return;
        }
        try
        {
            isEventHandling = true;
            base.OnAppearing();

            if (BindingContext is HistoryDetailViewModel viewModel)
            {
                await viewModel.ExecuteMyCommand();
            }
        }
        catch (Exception ex) {
            await ShowCustomAlert("알림", ex.Message, "확인", "");
        }
    }

    // 팝업 표시 상태를 나타내는 플래그
    private bool isAlertShowing = false;

    //팝업
    private async Task ShowCustomAlert(string title, string message, string accept, string cancle)
    {
        // 이미 경고 팝업이 표시 중인 경우 추가적인 처리를 하지 않음
        if (isAlertShowing)
        {
            return;
        }

        isAlertShowing = true; // 경고 팝업 표시 중임을 표시

        // 팝업 애니메이션 비활성화
        try
        {
            var alertPage = new CustomAlertPage(title, message, accept, cancle);
            alertPage.Disappearing += (sender, e) => isAlertShowing = false;
            await App.Current.MainPage.Navigation.PushModalAsync(alertPage, animated: false);
        }
        finally
        {
            isAlertShowing = true;
        }

    }
    private void BackBtn_Clicked(object sender, EventArgs e)
    {
        //뒤로가기
        Application.Current.MainPage.Navigation.PopAsync();
    }
}