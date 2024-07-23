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
    // �ߺ� ���� ������ ���� �÷���
    private bool isEventHandling = false;
    protected override async void OnAppearing()
    {
        // �ߺ� ���� ���� �÷��� üũ
        if (isEventHandling)
        {
            // �̹� ���� ���̶�� �� �̻� �������� ����
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
            await ShowCustomAlert("�˸�", ex.Message, "Ȯ��", "");
        }
    }

    // �˾� ǥ�� ���¸� ��Ÿ���� �÷���
    private bool isAlertShowing = false;

    //�˾�
    private async Task ShowCustomAlert(string title, string message, string accept, string cancle)
    {
        // �̹� ��� �˾��� ǥ�� ���� ��� �߰����� ó���� ���� ����
        if (isAlertShowing)
        {
            return;
        }

        isAlertShowing = true; // ��� �˾� ǥ�� ������ ǥ��

        // �˾� �ִϸ��̼� ��Ȱ��ȭ
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
        //�ڷΰ���
        Application.Current.MainPage.Navigation.PopAsync();
    }
}