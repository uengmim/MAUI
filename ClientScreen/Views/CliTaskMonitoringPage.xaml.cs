
using ClientScreen.Model;
using ClientScreen.Models;
using ClientScreen.ViewModels;
using NetTopologySuite.GeometriesGraph;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using XNSC.DD.EX;

namespace ClientScreen.Views;

public partial class CliTaskMonitoringPage : ContentPage
{
    public ObservableCollection<CliLockInfoData> CliLockDataModel { get { return clilockDataModel; } set { clilockDataModel = value; OnPropertyChanged(nameof(CliLockDataModel)); } }

    public ObservableCollection<CliLockInfoData> clilockDataModel = new ObservableCollection<CliLockInfoData>();

    public ObservableCollection<CliLockProvision> CliLockData { get { return clilockData; } set { clilockData = value; OnPropertyChanged(nameof(CliLockData)); } }

    public ObservableCollection<CliLockProvision> clilockData = new ObservableCollection<CliLockProvision>();


    public CliTaskMonitoringPage(string DeptId, string Wkpl, string Empno, string Refda2)
    {
        CliLockDataModel.Clear();

        InitializeComponent();
        CliMonitoringViewModel climonitoringViewModel = new CliMonitoringViewModel();
        climonitoringViewModel.DEPTID = DeptId;
        climonitoringViewModel.WKPL = Wkpl;
        climonitoringViewModel.EMPNO = Empno;
        climonitoringViewModel.REFDA2 = Refda2;
        this.BindingContext = climonitoringViewModel;

    }

    private async void Renew_Clicked(object sender, EventArgs e)
    {

        base.OnAppearing();
        if (BindingContext is CliMonitoringViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }

    }

    protected override async void OnAppearing()
    {

        base.OnAppearing();
        if (notesCollection.SelectedItems.Count > 0)
            notesCollection.SelectedItems.Clear();
        if (BindingContext is CliMonitoringViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }

    }
}