
using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.ViewModels;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using XNSC.DD.EX;
using static Bumptech.Glide.Load.Engine.Engine;

namespace AdminScreen.Views;

public partial class TaskMonitoringPage : ContentPage
{
    public ObservableCollection<LockInfoData> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

    public ObservableCollection<LockInfoData> lockDataModel = new ObservableCollection<LockInfoData>();

    public ObservableCollection<LockProvision> LockData { get { return lockData; } set { lockData = value; OnPropertyChanged(nameof(LockData)); } }

    public ObservableCollection<LockProvision> lockData = new ObservableCollection<LockProvision>();


    public TaskMonitoringPage()
    {
        LockDataModel.Clear();

        InitializeComponent();
        MonitoringViewModel monitoringViewModel = new MonitoringViewModel();

        this.BindingContext = monitoringViewModel;

    }

    private async void Renew_Clicked(object sender, EventArgs e)
    {

        base.OnAppearing();
        if (BindingContext is MonitoringViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }

    }

    protected override async void OnAppearing()
    {

        base.OnAppearing();
        if (notesCollection.SelectedItems.Count > 0)
            notesCollection.SelectedItems.Clear();
        if (BindingContext is MonitoringViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }

    }
}