using System.Collections.ObjectModel;
using WorkerScreen.ViewModel.CrushingWorker;
using SmartLock.TT;

namespace WorkerScreen.Views.CrushingWorker
{
    /// <summary>
    /// 자물쇠 파쇄 화면
    /// </summary>
    public partial class CrushingDocDetail : ContentPage
    {
        /// <summary>
        /// BLE Event  대기
        /// </summary>
        private ManualResetEventSlim holdInitBLE;
        /// <summary>
        /// TTLock API Helper
        /// </summary>
        private TTlockHelper ttlockHelper;

        public CrushingDocDetail()
        {
            InitializeComponent();
            CrushingDetailViewModel crushingDetailViewModel = new CrushingDetailViewModel();
            crushingDetailViewModel.BackgroundColorSet = Colors.Blue; // 원하는 배경색으로 설정
            crushingDetailViewModel.SelectedItem = null;
            this.BindingContext = crushingDetailViewModel;

            //TTLock Helper
            ttlockHelper = new TTlockHelper();
            holdInitBLE = new ManualResetEventSlim(false);

            //블루투스 초기화 이벤트
            ttlockHelper.LockBluetoothInitEvent += TtlockHelper_LockBluetoothInitEvent;
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
        /// BLE 초기화 이벤트
        /// </summary>
        /// <param name="e"></param>
        private void TtlockHelper_LockBluetoothInitEvent(SmartLock.Event.LockBluetoothInitEventArgs e)
        {
            holdInitBLE.Set();
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
        // 중복 실행 방지를 위한 플래그
        private static bool isEventHandling = false;
        /// <summary>
        /// 새로고침 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (BindingContext is CrushingDetailViewModel crushingDetailViewModel)
                {
                    await crushingDetailViewModel.ExecuteMyCommand();
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
}