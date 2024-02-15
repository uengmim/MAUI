using AdminScreen.Models;
using AdminScreen.ViewModels;
using SmartLock.TT;
using System.Collections.ObjectModel;

namespace AdminScreen.Views
{
    /// <summary>
    /// Lock 등록 및 초기화
    /// </summary>
    public partial class LockRegistDetailPage : ContentPage
    {
        private LockInfomation lockinfo;

        private ObservableCollection<LockInfomation> lockDataModel = new ObservableCollection<LockInfomation>();

        public ObservableCollection<LockInfomation> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }


        public LockInfomation LockInfo
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfo));
            }
        }

        /// <summary>
        /// BLE Event  대기
        /// </summary>
        private ManualResetEventSlim holdInitBLE;
        /// <summary>
        /// TTLOCK Helper
        /// </summary>
        private TTlockHelper ttlockHelper;

        /// <summary>
        /// 생성자
        /// </summary>
        public LockRegistDetailPage()
        {
            InitializeComponent();
            LockRegistDetailViewModel registerSearchModel = new LockRegistDetailViewModel();
            BindingContext = registerSearchModel;

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
        private void TtlockHelper_LockScanResultEvent(SmartLock.Event.LockScanResultEventArgs e)
        {
            if (!e.IsSuccess)
            {
                Console.WriteLine($"{e.Device.Address}, {e.Error.ErrorMessage}");
                return;
            }

            if (!LockDataModel.Any(l => l.LockMac == e.Device.Address))
            {
                //스캔한 Device를 표시한다.
                var lockInfo = new LockInfomation(e.Device);
                lockInfo.SetLockInfo();

                LockDataModel.Add(lockInfo);
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
}   