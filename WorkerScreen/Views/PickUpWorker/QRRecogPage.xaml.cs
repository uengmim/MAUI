using System.Collections.ObjectModel;
using WorkerScreen.Models;
using SmartLock.TT;
using WorkerScreen.ViewModel.PickUpWorker;

namespace WorkerScreen.Views.PickUpWorker
{
    /// <summary>
    /// �ڹ��� ��ĵ
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
        /// BLE Event  ���
        /// </summary>
        private ManualResetEventSlim holdInitBLE;
        /// <summary>
        /// TTLock API Helper
        /// </summary>
        private TTlockHelper ttlockHelper;
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="boxnum">������ ��ȣ</param>
        /// <param name="location">������ ��ġ</param>
        /// <param name="area">������ ����</param>
        /// <param name="deptID">�μ� ID</param>
        /// <param name="EMPNO">���� ��ȣ</param>
        /// <param name="EMPName">���� �̸�</param>
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

            //������� �ʱ�ȭ �̺�Ʈ
            ttlockHelper.LockBluetoothInitEvent += TtlockHelper_LockBluetoothInitEvent;
            //������� ��ĵ �̺�Ʈ
            ttlockHelper.LockScanResultEvent += TtlockHelper_LockScanResultEvent;
            LockInfoModel = new ObservableCollection<LockInfomation>();

        }
        /// <summary>
        /// ������� �� ��ġ ���� ����
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
                    await Application.Current.MainPage.DisplayAlert("�˸�", "��ġ ���� ������ �ʿ��մϴ�.", "OK");
                }
            }
            if (BindingContext is QRRecogViewModel viewModel)
            {
                await viewModel.ExecuteMyCommand();
            }
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
                    //��ĵ�� Device�� ǥ���Ѵ�.
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


        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //�ڷΰ���
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}