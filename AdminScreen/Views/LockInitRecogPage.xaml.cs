
using System.Collections.ObjectModel;
using AdminScreen.Models;
using AdminScreen.ViewModel;
using AdminScreen.ViewModels;
using SmartLock.TT;

namespace AdminScreen.Views;

public partial class LockInitRecogPage : ContentPage
{


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


    /// <summary>
    /// BLE Event  대기
    /// </summary>
    private ManualResetEventSlim holdInitBLE ;
    /// <summary>
    /// TTLOCK Helper
    /// </summary>
    private TTlockHelper ttlockHelper;

    /// <summary>
    /// 생성자
    /// </summary>
    public LockInitRecogPage()
    {

        InitializeComponent();
        LockInitViewModel lockInitViewModel = new LockInitViewModel();

        this.BindingContext = lockInitViewModel;

        ttlockHelper = new TTlockHelper();
        holdInitBLE = new ManualResetEventSlim(false);

        //블루투스 초기화 이벤트
        ttlockHelper.LockBluetoothInitEvent += TtlockHelper_LockBluetoothInitEvent;
        //블루투스 스캔 이벤트
        ttlockHelper.LockScanResultEvent += TtlockHelper_LockScanResultEvent;
    }

    /// <summary>
    /// BLE 초기화 이벤트
    /// </summary>
    /// <param name="e"></param>
    private void TtlockHelper_LockBluetoothInitEvent(SmartLock.Event.LockBluetoothInitEventArgs e)
    {
        holdInitBLE.Set();
    }

    /// <summary>
    /// LOCK SCAN 이벤트
    /// </summary>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void TtlockHelper_LockScanResultEvent(SmartLock.Event.LockScanResultEventArgs e)
    {
        if (!e.IsSuccess)
        {
            Console.WriteLine($"{e.Device.Address}, {e.Error.ErrorMessage}");
            return;
        }

        if (!LockInfoModel.Any(l => l.LockMac == e.Device.Address))
        {
            //스캔한 Device를 표시한다.
            var lockInfo = new LockInfomation(e.Device);
            lockInfo.SetLockInfo();

            LockInfoModel.Add(lockInfo);
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
            //블루투스 권한 체크
            await ttlockHelper.CheckAndRequestBluetoothPermission();

            //블루투스 초기과 후 완료 될때 까지 대기 함
            ttlockHelper.InitBluetooth();
            holdInitBLE.Wait();

            //Lock Scan 시작
            LockScan();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("오류", ex.Message, "확인");
        }
    }

    /// <summary>
    /// Lock Sacn
    /// </summary>
    private void LockScan()
    {
        try
        {
            ttlockHelper.StartLockDeviceScan();
        }
        catch (Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("오류", ex.Message, "확인");
        }
    }
}