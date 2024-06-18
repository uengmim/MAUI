using WorkerScreen.ViewModel.PickUpWorker;

namespace WorkerScreen.Views.PickUpWorker
{

    public partial class GetOnDocument : ContentPage
    {
        /// <summary>
        /// 상차 화면입니다.
        /// </summary>
        public GetOnDocument()
        {
            InitializeComponent();
            GetOnViewModel getOnViewModel = new GetOnViewModel();

            this.BindingContext = getOnViewModel;
        }

        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}