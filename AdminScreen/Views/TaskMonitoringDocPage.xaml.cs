using AdminScreen.ViewModels;

namespace AdminScreen.Views
{
    public partial class TaskMonitoringDocPage : ContentPage
    {
        /// <summary>
        /// 문서 페이지
        /// </summary>
        /// <param name="LockClose">봉인 시간</param>
        /// <param name="Crushing">파쇄 시간</param>
        /// <param name="CONFNO">작업자</param>
        public TaskMonitoringDocPage(DateTime? LockClose, DateTime? Crushing, string CONFNO)
        {
            InitializeComponent();
            MonitoringDocViewModel monitoringDocViewModel = new MonitoringDocViewModel();
            monitoringDocViewModel.LockCloseTime = (DateTime)LockClose;
            monitoringDocViewModel.CrushingTime = (DateTime)Crushing;
            monitoringDocViewModel.CONFNO = CONFNO;

            this.BindingContext = monitoringDocViewModel;

        }
        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}