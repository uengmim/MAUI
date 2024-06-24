using WorkerScreen.ViewModel.PickUpWorker;

namespace WorkerScreen.Views.PickUpWorker
{

    public partial class QRCodePage : ContentPage
    {
        /// <summary>
        /// 보안 문서 봉인 화면
        /// </summary>
        /// <param name="Name">작업자의 이름</param>
        /// <param name="PhoneNumber">작업자의 핸드폰 번호</param>
        /// <param name="EMPNO">작업자의 작업자번호</param>
        /// <param name="DEPTID">작업자의 부서ID</param>
        public QRCodePage(string Name, string PhoneNumber, string EMPNO, string DEPTID)
        {
            InitializeComponent();
            QRCodeViewModel qRCodeViewModel = new QRCodeViewModel();

            qRCodeViewModel.Name = Name;
            qRCodeViewModel.PhoneNumber = PhoneNumber;
            qRCodeViewModel.EMPNO = EMPNO;
            qRCodeViewModel.DEPTID = DEPTID;

            this.BindingContext = qRCodeViewModel;

            //카메라 QR코드 인식
            cameraView.BarCodeDetectionEnabled = true;
            //카메라 시작
            cameraView.StartCameraAsync();
        }
        /// <summary>
        /// 블루투스 권한 설정
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var nearbyDeviceStatus = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();
            if (nearbyDeviceStatus != PermissionStatus.Granted)
            {
                nearbyDeviceStatus = await Permissions.RequestAsync<Permissions.Bluetooth>();
                if (nearbyDeviceStatus != PermissionStatus.Granted)
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "블루투스 권한 설정이 필요합니다.", "OK");
                }
            }
        }
        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}