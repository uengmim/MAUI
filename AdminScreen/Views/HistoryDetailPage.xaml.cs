
using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.ViewModels;
using Kotlin.Reflect;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using XNSC.DD.EX;

namespace AdminScreen.Views;

public partial class HistoryDetailPage : ContentPage
{

    IGeolocation geolocation;

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
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is HistoryDetailViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }
    }
    //ÆÄ¼â ¹®¼­
    public async void DocClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TaskMonitoringDocPage());
    }
}