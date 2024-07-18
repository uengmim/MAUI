using AdminScreen.ViewModels;

namespace AdminScreen.Views;

public partial class TaskMonitoringDetailPage : ContentPage
{

    public TaskMonitoringDetailPage(string LSN, string LKNM, string MAC, string LKTYP, string CONFNO)
    {

        InitializeComponent();
        MonitoringDetailViewModel monitoringDetailViewModel = new MonitoringDetailViewModel();
        monitoringDetailViewModel.LSN = LSN;
        monitoringDetailViewModel.LKNM = LKNM;
        monitoringDetailViewModel.MAC = MAC;
        monitoringDetailViewModel.LKTYP = LKTYP;
        monitoringDetailViewModel.CONFNO = CONFNO;
        this.BindingContext = monitoringDetailViewModel;

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is MonitoringDetailViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }
    }

    private void BackBtn_Clicked(object sender, EventArgs e)
    {
        //뒤로가기
        Application.Current.MainPage.Navigation.PopAsync();
    }
}