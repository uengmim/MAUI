using WorkerScreen.ViewModel.CrushingWorker;

namespace WorkerScreen.Views.CrushingWorker
{
    public partial class CrushingInputData : ContentPage
    {
        /// <summary>
        /// 파쇄 정보 입력 화면입니다.
        /// </summary>
        /// <param name="Name">작업자 이름</param>
        /// <param name="BoxName">보안 문서함 이름</param>
        /// <param name="Location">보안 문서함 위치</param>
        /// <param name="LockData">자물쇠 정보</param>
        /// <param name="PickupDate">수거 일시</param>
        /// <param name="LockDate">봉인 일시</param>
        /// <param name="PhotoResults">사진 데이터</param>
        /// <param name="ConfNo">작업 정보</param>
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
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}