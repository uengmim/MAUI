using WorkerScreen.ViewModel.CrushingWorker;

namespace WorkerScreen.Views.CrushingWorker
{
    /// <summary>
    /// 문서 파쇄 등록 화면입니다.
    /// </summary>
    public partial class CrushingDocument : ContentPage
    {
        public CrushingDocument()
        {
            InitializeComponent();
            CrushingViewModel crushingViewModel = new CrushingViewModel();
            this.BindingContext = crushingViewModel;
        }
        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}