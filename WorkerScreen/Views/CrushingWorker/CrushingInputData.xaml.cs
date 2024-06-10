using WorkerScreen.ViewModel.CrushingWorker;

namespace WorkerScreen.Views.CrushingWorker
{
    public partial class CrushingInputData : ContentPage
    {
        /// <summary>
        /// �ļ� ���� �Է� ȭ���Դϴ�.
        /// </summary>
        /// <param name="Name">�۾��� �̸�</param>
        /// <param name="BoxName">���� ������ �̸�</param>
        /// <param name="Location">���� ������ ��ġ</param>
        /// <param name="LockData">�ڹ��� ����</param>
        /// <param name="PickupDate">���� �Ͻ�</param>
        /// <param name="LockDate">���� �Ͻ�</param>
        /// <param name="PhotoResults">���� ������</param>
        /// <param name="ConfNo">�۾� ����</param>
        public CrushingInputData(string Name, string BoxName, string Location, string LockData, DateTime PickupDate, DateTime LockDate, List<string> PhotoResults, string ConfNo)
        {

            InitializeComponent();
            InputDataViewModel inputDataViewModel = new InputDataViewModel();

            inputDataViewModel.WorkerName = Name;
            inputDataViewModel.BoxName = BoxName;
            inputDataViewModel.Location = Location;
            inputDataViewModel.LockData = LockData;
            inputDataViewModel.PickupDate = PickupDate;
            inputDataViewModel.LockDate = LockDate;
            inputDataViewModel.PhotoResult = PhotoResults;
            inputDataViewModel.ConfNo = ConfNo;

            this.BindingContext = inputDataViewModel;
        }

        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //�ڷΰ���
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}