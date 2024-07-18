
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
    /// LockInfoData ��
    /// </summary>
    public ObservableCollection<LockInfoData> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

    /// <summary>
    ///  LockInfoData ��
    /// </summary>
    private ObservableCollection<LockInfoData> lockDataModel = new ObservableCollection<LockInfoData>();

    /// <summary>
    ///  LockProvision ��
    /// </summary>
    public ObservableCollection<LockProvision> LockData { get { return lockData; } set { lockData = value; OnPropertyChanged(nameof(LockData)); } }

    /// <summary>
    ///  LockProvision ��
    /// </summary>
    private ObservableCollection<LockProvision> lockData = new ObservableCollection<LockProvision>();


    public TaskMonitoringPage()
    {
        LockDataModel.Clear();

        InitializeComponent();
        MonitoringViewModel monitoringViewModel = new MonitoringViewModel();

        this.BindingContext = monitoringViewModel;

    }

    // �ߺ� ���� ������ ���� �÷���
    private static bool isEventHandling = false;
    private async void Renew_Clicked(object sender, EventArgs e)
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
            if (BindingContext is MonitoringViewModel viewModel)
            {
                await viewModel.ExecuteMyCommand();
            }
        }
        finally
        {
            // �ߺ� ���� ���� �÷��� ����
            isEventHandling = false;
        }
    }

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
            if (notesCollection.SelectedItems.Count > 0)
                notesCollection.SelectedItems.Clear();
            if (BindingContext is MonitoringViewModel viewModel)
            {
                await viewModel.ExecuteMyCommand();
            }
        }
        finally
        {
            // �ߺ� ���� ���� �÷��� ����
            isEventHandling = false;
        }
    }

    private void BackBtn_Clicked(object sender, EventArgs e)
    {
        //�ڷΰ���
        Application.Current.MainPage.Navigation.PopAsync();
    }
}