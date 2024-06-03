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
        /// �ļ� �۾� ī�޶� �Կ� ȭ���Դϴ�.
        /// </summary>
        /// <param name="Name">�۾��� �̸�</param>
        /// <param name="BoxName">���� ������ �̸�</param>
        /// <param name="Location">���� ������ ��ġ</param>
        /// <param name="LockData">�ڹ��� ����</param>
        /// <param name="PickupDate">���� �Ͻ�</param>
        /// <param name="LockDate">���� �Ͻ�</param>
        /// <param name="CONFNO">�۾� ����</param>
        public CrushingCameraPage(string Name, string BoxName, string Location, string LockData, DateTime PickupDate, DateTime? LockDate, string CONFNO)
        {
            InitializeComponent();
            //ī�޶� ���ڵ� ��� off
            photoView.BarCodeDetectionEnabled = false;
            //ī�޶� on
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
        /// ī�޶� ���� ����
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
                    await Application.Current.MainPage.DisplayAlert("�˸�", "ī�޶� ���� ������ �ʿ��մϴ�..", "OK");
                }
            }
        }


        /// <summary>
        /// ���� �� �̹��� ������Ʈ
        /// </summary>
        /// <param name="images"></param>
        public void UpdateImages(List<ObservableImage> images)
        {
            //pics �ʱ�ȭ
            pics.Clear();
            //�ٽ� �̹��� �߰�
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

        // �ߺ� ���� ������ ���� �÷���
        private static bool isEventHandling = false;

        /// <summary>
        /// ���� �Կ� �̺�Ʈ
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
            var toast = Toast.Make("������ ���������� �Կ��Ǿ����ϴ�.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
            await toast.Show(cancellationTokenSource.Token);
            isEventHandling = true;
        }

        /// <summary>
        /// �ļ� �̹��� ������ �̵��Դϴ�.
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
                        // MainPage�� ParentView �Ӽ��� �����մϴ�.
                        ParentView = this
                    };
                    await Application.Current.MainPage.Navigation.PushAsync(imagePage);
                });

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("�뺸", ex.Message, "OK");
                return;
            }
        }

        /// <summary>
        /// �ļ� ���� �Է��Դϴ�.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void infoInputButton_Clicked(object sender, EventArgs e)
        {
            // �ߺ� ���� ���� �÷��� üũ
            if (isEventHandling == false)
            {
                await Application.Current.MainPage.DisplayAlert("�˸�", "���� �Կ��� �ؾ� �ļ� ���� �Է��� �����մϴ�.", "Ȯ��");
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

                    //viewmodel�� ���� ����
                    await crushingCameraViewModel.InfoInputCommand();
                    await photoView.StartCameraAsync();

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("�뺸", ex.Message, "OK");
                return;
            }
        }
        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //�ڷΰ���
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}