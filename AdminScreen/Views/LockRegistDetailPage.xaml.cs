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
        /// <summary>
        /// lockinfo
        /// </summary>
        private LockInfomation lockinfo;

        /// <summary>
        /// LockInfomation 모델
        /// </summary>
        private ObservableCollection<LockInfomation> lockDataModel = new ObservableCollection<LockInfomation>();

        /// <summary>
        /// LockInfomation 모델
        /// </summary>
        public ObservableCollection<LockInfomation> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        /// <summary>
        /// LockInfo
        /// </summary>
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
#if __ANDROID__
            TTlockHelper.AndroidDeviceDic.Clear();

#endif
            InitializeComponent();
            LockRegistDetailViewModel registerSearchModel = new LockRegistDetailViewModel();
            this.BindingContext = registerSearchModel;

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
            try
            {
                var viewmodel = (LockRegistDetailViewModel)this.BindingContext;

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

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        LockDataModel.Add(lockInfo);
                        viewmodel.LockDataModel = LockDataModel;
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
               await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
        }
        
        /// <summary>
        /// Lock Sacn
        /// </summary>
        private async void  LockScan()
        {
            try
            {
                ttlockHelper.StartLockDeviceScan();
            }
            catch (Exception ex)
            {
               await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
        }

        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }

        // 팝업 표시 상태를 나타내는 플래그
        private bool isAlertShowing = false;

        //팝업
        private async Task ShowCustomAlert(string title, string message, string accept, string cancle)
        {
            // 이미 경고 팝업이 표시 중인 경우 추가적인 처리를 하지 않음
            if (isAlertShowing)
            {
                return;
            }

            isAlertShowing = true; // 경고 팝업 표시 중임을 표시

            // 팝업 애니메이션 비활성화
            try
            {
                var alertPage = new CustomAlertPage(title, message, accept, cancle);
                alertPage.Disappearing += (sender, e) => isAlertShowing = false;
                await App.Current.MainPage.Navigation.PushModalAsync(alertPage, animated: false);
            }
            finally
            {
                isAlertShowing = true;
            }
        }
    }
}   