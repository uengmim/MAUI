using WorkerScreen.ViewModel.PickUpWorker;

namespace WorkerScreen.Views.PickUpWorker
{

    public partial class QRCodePage : ContentPage
    {
        /// <summary>
        /// ���� ���� ���� ȭ��
        /// </summary>
        /// <param name="Name">�۾����� �̸�</param>
        /// <param name="PhoneNumber">�۾����� �ڵ��� ��ȣ</param>
        /// <param name="EMPNO">�۾����� �۾��ڹ�ȣ</param>
        /// <param name="DEPTID">�۾����� �μ�ID</param>
        public QRCodePage(string Name, string PhoneNumber, string EMPNO, string DEPTID)
        {
            InitializeComponent();
            QRCodeViewModel qRCodeViewModel = new QRCodeViewModel();

            qRCodeViewModel.Name = Name;
            qRCodeViewModel.PhoneNumber = PhoneNumber;
            qRCodeViewModel.EMPNO = EMPNO;
            qRCodeViewModel.DEPTID = DEPTID;

            this.BindingContext = qRCodeViewModel;

            //ī�޶� QR�ڵ� �ν�
            cameraView.BarCodeDetectionEnabled = true;
            //ī�޶� ����
            cameraView.StartCameraAsync();
        }
        /// <summary>
        /// ������� ���� ����
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
                    await Application.Current.MainPage.DisplayAlert("�˸�", "������� ���� ������ �ʿ��մϴ�.", "OK");
                }
            }
        }
        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //�ڷΰ���
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}