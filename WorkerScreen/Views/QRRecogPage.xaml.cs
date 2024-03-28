using Camera.MAUI;
using Camera.MAUI.Test;
using System.Collections.ObjectModel;
using WorkerScreen.Models;
using WorkerScreen.ViewModel;

namespace WorkerScreen.Views;

public partial class QRRecogPage : ContentPage
{


    public QRRecogPage(string boxnum, string location, string area, string deptID, string EMPNO, string EMPName)
    {

        InitializeComponent();
        QRRecogViewModel qRRecogViewModel = new QRRecogViewModel();
        qRRecogViewModel.BoxNum = boxnum;
        qRRecogViewModel.Location = location;
        qRRecogViewModel.AREA = area;
        qRRecogViewModel.DEPTID = deptID;
        qRRecogViewModel.EMPNO = EMPNO;
        qRRecogViewModel.EMPName = EMPName;

        this.BindingContext = qRRecogViewModel;

    }

    public ObservableCollection<LockInfomation> LockInfoModel { get { return lockInfoModel; } set { lockInfoModel = value; OnPropertyChanged(nameof(LockInfoModel)); } }

    public ObservableCollection<LockInfomation> lockInfoModel = new ObservableCollection<LockInfomation>();

    public LockInfomation LockInfo
    {
        get { return lockinfo; }
        set
        {
            lockinfo = value;
            OnPropertyChanged(nameof(LockInfo));
        }
    }

    private LockInfomation lockinfo;
    protected override async void OnAppearing()
    {

        base.OnAppearing();

        if (BindingContext is QRRecogViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }

    }

    /// <summary>
    /// 페이지 로드후 이벤트 처리
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        try
        {
            await CheckAndRequestBluetoothPermission();


            TTlockHelper.InitBLE();
            LockScan();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("오류", ex.Message, "확인");
        }
    }

    /// <summary>
    /// 블루투스 권한 체크 및 요청
    /// </summary>
    /// <returns></returns>
    public async Task CheckAndRequestBluetoothPermission()
    {
        if (!await CheckBluetoothAccess())
        {
            await RequestBluetoothAccess();
        }
    }

    /// <summary>
    /// 블루투스 접근 권한 체크
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CheckBluetoothAccess()
    {
        try
        {
            var requestStatus = await Permissions.CheckStatusAsync<BluetoothPermissions>();
            return requestStatus == PermissionStatus.Granted;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oops  {ex}");
            return false;
        }
    }

    /// <summary>
    /// 블루투스 접근 권한 요청
    /// </summary>
    /// <returns></returns>
    public async Task<bool> RequestBluetoothAccess()
    {
        try
        {
            var requestStatus = await Permissions.RequestAsync<BluetoothPermissions>();
            return requestStatus == PermissionStatus.Granted;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oops  {ex}");
            return false;
        }
    }

    /// <summary>
    /// Lock Sacn
    /// </summary>
    private void LockScan()
    {
        try
        {
            RegistSearchScanCallback scanCallbak = new RegistSearchScanCallback(BindingContext as QRRecogViewModel);
            TTlockHelper.ScanLock(scanCallbak);
            Console.WriteLine("Lock Scan Start");
        }
        catch (Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("오류", ex.Message, "확인");
        }
    }
}