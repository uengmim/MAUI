using WorkerScreen.ViewModel.CrushingWorker;
using Camera.MAUI;
using System.Threading;
using CommunityToolkit.Maui.Alerts;
using static WorkerScreen.Views.CrushingWorker.CrushingImagePage;

namespace WorkerScreen.Views.CrushingWorker
{
    public partial class CrushingCameraPage : ContentPage
    {
        private readonly List<byte[]> pics = new();
        public ImageSource PhotoData { get; set; }

        public List<ObservableImage> Pics => _pics;

        private int index;
        private readonly List<ObservableImage> _pics;

        /// <summary>
        /// 파쇄 작업 카메라 촬영 화면입니다.
        /// </summary>
        /// <param name="Name">작업자 이름</param>
        /// <param name="BoxName">보안 문서함 이름</param>
        /// <param name="Location">보안 문서함 위치</param>
        /// <param name="LockData">자물쇠 정보</param>
        /// <param name="PickupDate">수거 일시</param>
        /// <param name="LockDate">봉인 일시</param>
        /// <param name="CONFNO">작업 정보</param>
        public CrushingCameraPage(string Name, string BoxName, string Location, string LockData, DateTime PickupDate, DateTime? LockDate, string CONFNO)
        {
            InitializeComponent();
            //카메라 바코드 기능 off
            photoView.BarCodeDetectionEnabled = false;
            //카메라 on
            photoView.StartCameraAsync();

            CrushingCameraViewModel crushingCameraViewModel = new CrushingCameraViewModel();
            crushingCameraViewModel.WorkerName = Name;
            crushingCameraViewModel.BoxName = BoxName;
            crushingCameraViewModel.Location = Location;
            crushingCameraViewModel.LockData = LockData;
            crushingCameraViewModel.PickupDate = PickupDate;
            crushingCameraViewModel.ConfNo = CONFNO;
            crushingCameraViewModel.LockDate = (DateTime)LockDate;

            this.BindingContext = crushingCameraViewModel;

            photoView.StartCameraAsync();
        }
        /// <summary>
        /// 카메라 권한 설정
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (cameraStatus != PermissionStatus.Granted)
            {
                cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
                if (cameraStatus != PermissionStatus.Granted)
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "카메라 권한 설정이 필요합니다..", "OK");
                }
            }
        }


        /// <summary>
        /// 삭제 후 이미지 업데이트
        /// </summary>
        /// <param name="images"></param>
        public void UpdateImages(List<ObservableImage> images)
        {
            //pics 초기화
            pics.Clear();
            //다시 이미지 추가
            foreach (var image in images)
            {
                var streamImageSource = (StreamImageSource)image.Source;
                using var imageStream = streamImageSource.Stream(CancellationToken.None).Result;
                var data = new byte[imageStream.Length];
                imageStream.Read(data, 0, (int)imageStream.Length);
                pics.Add(data);
            }
            CounterBtn.Text = pics.Count.ToString();
        }

        // 중복 실행 방지를 위한 플래그
        private static bool isEventHandling = false;

        /// <summary>
        /// 사진 촬영 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void taskPhotoButton_Clicked(object sender, EventArgs e)
        {
            var snap = photoView.GetSnapShot(Camera.MAUI.ImageFormat.JPEG);
            var imageStream = await ((StreamImageSource)snap).Stream(CancellationToken.None);
            var data = new byte[imageStream.Length];
            imageStream.Read(data, 0, (int)imageStream.Length);
            pics.Add(data);
            CounterBtn.Text = pics.Count.ToString();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            var toast = Toast.Make("사진이 정상적으로 촬영되었습니다.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
            await toast.Show(cancellationTokenSource.Token);
            isEventHandling = true;
        }

        /// <summary>
        /// 파쇄 이미지 페이지 이동입니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Image_Clicked(object sender, EventArgs e)
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var imagePage = new CrushingImagePage(index, pics)
                    {
                        // MainPage를 ParentView 속성에 설정합니다.
                        ParentView = this
                    };
                    await Application.Current.MainPage.Navigation.PushAsync(imagePage);
                });

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                return;
            }
        }

        /// <summary>
        /// 파쇄 정보 입력입니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void infoInputButton_Clicked(object sender, EventArgs e)
        {
            // 중복 실행 방지 플래그 체크
            if (isEventHandling == false)
            {
                await Application.Current.MainPage.DisplayAlert("알림", "사진 촬영을 해야 파쇄 정보 입력이 가능합니다.", "확인");
                return;
            }
            try
            {
                CrushingCameraViewModel crushingCameraViewModel = this.BindingContext as CrushingCameraViewModel;
                if (crushingCameraViewModel != null)
                {
                    await photoView.StartCameraAsync();

                    crushingCameraViewModel.pics = pics;
                    //crushingCameraViewModel.PhotoData = photoView.GetSnapShot(Camera.MAUI.ImageFormat.JPEG);

                    //viewmodel로 정보 전달
                    await crushingCameraViewModel.InfoInputCommand();
                    await photoView.StartCameraAsync();

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                return;
            }
        }
        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}