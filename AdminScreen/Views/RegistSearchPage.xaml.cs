using AdminScreen.Models;
using AdminScreen.ViewModels;
using System.Collections.ObjectModel;

namespace AdminScreen.Views
{
    /// <summary>
    /// Lock 등록 및 초기화
    /// </summary>
    public partial class RegistSearchPage : ContentPage
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public RegistSearchPage()
        {
            InitializeComponent();
            RegisterSearchModel registerSearchModel = new RegisterSearchModel();
            BindingContext = registerSearchModel;
              
        }

        public ObservableCollection<LockInfo> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        public ObservableCollection<LockInfo> lockDataModel = new ObservableCollection<LockInfo>();

        public LockInfo LockInfo
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfo));
            }
        }

        private LockInfo lockinfo;


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

        private void Renew_Clicked(object sender, EventArgs e)
        {

            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            base.OnAppearing();

            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            base.OnAppearing();

            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        }

        /// <summary>
        /// Lock Sacn
        /// </summary>
        private void LockScan()
        {
            try
            {
                RegistSearchScanCallback scanCallbak = new RegistSearchScanCallback(BindingContext as RegisterSearchModel);
                TTlockHelper.ScanLock(scanCallbak);
                Console.WriteLine("Lock Scan Start");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("오류", ex.Message, "확인");
            }
        }
    }
}   