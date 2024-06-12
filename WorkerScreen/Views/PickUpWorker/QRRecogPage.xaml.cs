using System.Collections.ObjectModel;
using WorkerScreen.Models;
using SmartLock.TT;
using WorkerScreen.ViewModel.PickUpWorker;

namespace WorkerScreen.Views.PickUpWorker
{
    /// <summary>
    /// 자물쇠 스캔
    /// </summary>
    public partial class QRRecogPage : ContentPage
    {
        private LockInfomation lockinfo;

        private ObservableCollection<LockInfomation> lockInfoModel = new ObservableCollection<LockInfomation>();
        public ObservableCollection<LockInfomation> LockInfoModel { get { return lockInfoModel; } set { lockInfoModel = value; OnPropertyChanged(nameof(LockInfoModel)); } }


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
        /// TTLock API Helper
        /// </summary>
        private TTlockHelper ttlockHelper;
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="boxnum">문서함 번호</param>
        /// <param name="location">문서함 위치</param>
        /// <param name="area">문서함 지역</param>
        /// <param name="deptID">부서 ID</param>
        /// <param name="EMPNO">직원 번호</param>
        /// <param name="EMPName">직원 이름</param>
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

            //TTLock Helper
            ttlockHelper = new TTlockHelper();
            holdInitBLE = new ManualResetEventSlim(false);

            //블루투스 초기화 이벤트
            ttlockHelper.LockBluetoothInitEvent += TtlockHelper_LockBluetoothInitEvent;
            //블루투스 스캔 이벤트
            ttlockHelper.LockScanResultEvent += TtlockHelper_LockScanResultEvent;
            LockInfoModel = new ObservableCollection<LockInfomation>();

        }
        /// <summary>
        /// 블루투스 및 위치 권한 설정
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locationStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (locationStatus != PermissionStatus.Granted)
            {
                locationStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (locationStatus != PermissionStatus.Granted)
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "위치 권한 설정이 필요합니다.", "OK");
                }
            }
            if (BindingContext is QRRecogViewModel viewModel)
            {
                await viewModel.ExecuteMyCommand();
            }
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
                var viewmodel = (QRRecogViewModel)this.BindingContext;
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

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        LockInfoModel.Add(lockInfo);
                        viewmodel.LockInfoModel = LockInfoModel;
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


        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}