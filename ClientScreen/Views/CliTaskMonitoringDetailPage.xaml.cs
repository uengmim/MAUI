
using ClientScreen.Model;
using ClientScreen.Models;
using ClientScreen.ViewModels;
using Microsoft.Maui.Devices.Sensors;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using XNSC.DD.EX;

namespace ClientScreen.Views;

public partial class CliTaskMonitoringDetailPage : ContentPage
{
    IGeolocation geolocation;

    public CliTaskMonitoringDetailPage(string LSN, string LKNM, string MAC, string LKTYP, string CONFNO)
    {

        InitializeComponent();
        CliMonitoringDetailViewModel monitoringDetailViewModel = new CliMonitoringDetailViewModel();
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

        if (BindingContext is CliMonitoringDetailViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }
    }

    //ÆÄ¼â ¹®¼­
    public async void DocClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CliTaskMonitoringDocPage());
    }
}