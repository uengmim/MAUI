using System.Collections.ObjectModel;
using WorkerScreen.ViewModel.CrushingWorker;
using SmartLock.TT;

namespace WorkerScreen.Views.CrushingWorker
{
    /// <summary>
    /// �ڹ��� �ļ� ȭ��
    /// </summary>
    public partial class CrushingDocDetail : ContentPage
    {
        /// <summary>
        /// BLE Event  ���
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
            crushingDetailViewModel.BackgroundColorSet = Colors.Blue; // ���ϴ� �������� ����
            crushingDetailViewModel.SelectedItem = null;
            this.BindingContext = crushingDetailViewModel;

            //TTLock Helper
            ttlockHelper = new TTlockHelper();
            holdInitBLE = new ManualResetEventSlim(false);

            //������� �ʱ�ȭ �̺�Ʈ
            ttlockHelper.LockBluetoothInitEvent += TtlockHelper_LockBluetoothInitEvent;
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
        /// BLE �ʱ�ȭ �̺�Ʈ
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
                Application.Current.MainPage.DisplayAlert("����", ex.Message, "Ȯ��");
            }
        }
        // �ߺ� ���� ������ ���� �÷���
        private static bool isEventHandling = false;
        /// <summary>
        /// ���ΰ�ħ �̺�Ʈ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Renew_Clicked(object sender, EventArgs e)
        {
            // �ߺ� ���� ���� �÷��� üũ
            if (isEventHandling)
            {
                // �̹� ���� ���̶�� �� �̻� �������� ����
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
                // �ߺ� ���� ���� �÷��� ����
                isEventHandling = false;
            }
        }
        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //�ڷΰ���
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}