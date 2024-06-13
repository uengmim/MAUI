using WorkerScreen.ViewModel.PickUpWorker;

namespace WorkerScreen.Views.PickUpWorker
{
    public partial class GetOffDocument : ContentPage
    {
        /// <summary>
        /// 하차 화면입니다.
        /// </summary>
        public GetOffDocument()
        {
            InitializeComponent();
            GetOffViewModel getOffViewModel = new GetOffViewModel();
            this.BindingContext = getOffViewModel;
        }
        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}