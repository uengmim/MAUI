
using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.ViewModels;
using Kotlin.Reflect;
using Microsoft.Maui.Devices.Sensors;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using XNSC.DD.EX;

namespace AdminScreen.Views;

public partial class TaskMonitoringDetailPage : ContentPage
{
    IGeolocation geolocation;

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

    //지도
    public async void MapClicked(object sender, EventArgs e)
    {
        MonitoringDetailViewModel monitoringDetailViewModel = new MonitoringDetailViewModel();

        await Navigation.PushAsync(new TaskMonitoringMapPage(monitoringDetailViewModel.CONFNO));
    }
    //파쇄 문서
    public async void DocClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TaskMonitoringDocPage());
    }
}