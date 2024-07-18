
using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.ViewModels;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using XNSC.DD.EX;

namespace AdminScreen.Views;

public partial class TaskMonitoringPage : ContentPage
{
    /// <summary>
    /// LockInfoData 모델
    /// </summary>
    public ObservableCollection<LockInfoData> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

    /// <summary>
    ///  LockInfoData 모델
    /// </summary>
    private ObservableCollection<LockInfoData> lockDataModel = new ObservableCollection<LockInfoData>();

    /// <summary>
    ///  LockProvision 모델
    /// </summary>
    public ObservableCollection<LockProvision> LockData { get { return lockData; } set { lockData = value; OnPropertyChanged(nameof(LockData)); } }

    /// <summary>
    ///  LockProvision 모델
    /// </summary>
    private ObservableCollection<LockProvision> lockData = new ObservableCollection<LockProvision>();


    public TaskMonitoringPage()
    {
        LockDataModel.Clear();

        InitializeComponent();
        MonitoringViewModel monitoringViewModel = new MonitoringViewModel();

        this.BindingContext = monitoringViewModel;

    }

    // 중복 실행 방지를 위한 플래그
    private static bool isEventHandling = false;
    private async void Renew_Clicked(object sender, EventArgs e)
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
            if (BindingContext is MonitoringViewModel viewModel)
            {
                await viewModel.ExecuteMyCommand();
            }
        }
        finally
        {
            // 중복 실행 방지 플래그 해제
            isEventHandling = false;
        }
    }

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
            if (notesCollection.SelectedItems.Count > 0)
                notesCollection.SelectedItems.Clear();
            if (BindingContext is MonitoringViewModel viewModel)
            {
                await viewModel.ExecuteMyCommand();
            }
        }
        finally
        {
            // 중복 실행 방지 플래그 해제
            isEventHandling = false;
        }
    }

    private void BackBtn_Clicked(object sender, EventArgs e)
    {
        //뒤로가기
        Application.Current.MainPage.Navigation.PopAsync();
    }
}