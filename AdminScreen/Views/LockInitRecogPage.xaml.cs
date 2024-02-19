
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
    /// BLE Event  ���
    /// </summary>
    private ManualResetEventSlim holdInitBLE ;
    /// <summary>
    /// TTLOCK Helper
    /// </summary>
    private TTlockHelper ttlockHelper;

    /// <summary>
    /// ������
    /// </summary>
    public LockInitRecogPage()
    {

        InitializeComponent();
        LockInitViewModel lockInitViewModel = new LockInitViewModel();

        this.BindingContext = lockInitViewModel;

        ttlockHelper = new TTlockHelper();
        holdInitBLE = new ManualResetEventSlim(false);

        //������� �ʱ�ȭ �̺�Ʈ
        ttlockHelper.LockBluetoothInitEvent += TtlockHelper_LockBluetoothInitEvent;
        //������� ��ĵ �̺�Ʈ
        ttlockHelper.LockScanResultEvent += TtlockHelper_LockScanResultEvent;
    }

    /// <summary>
    /// BLE �ʱ�ȭ �̺�Ʈ
    /// </summary>
    /// <param name="e"></param>
    private void TtlockHelper_LockBluetoothInitEvent(SmartLock.Event.LockBluetoothInitEventArgs e)
    {
        holdInitBLE.Set();
    }

    /// <summary>
    /// LOCK SCAN �̺�Ʈ
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
            //��ĵ�� Device�� ǥ���Ѵ�.
            var lockInfo = new LockInfomation(e.Device);
            lockInfo.SetLockInfo();

            LockInfoModel.Add(lockInfo);
        }
    }

    /// <summary>
    /// ������ �ε��� �̺�Ʈ ó��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        try
        {
            //������� ���� üũ
            await ttlockHelper.CheckAndRequestBluetoothPermission();

            //������� �ʱ�� �� �Ϸ� �ɶ� ���� ��� ��
            ttlockHelper.InitBluetooth();
            holdInitBLE.Wait();

            //Lock Scan ����
            LockScan();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("����", ex.Message, "Ȯ��");
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
            Application.Current.MainPage.DisplayAlert("����", ex.Message, "Ȯ��");
        }
    }
}