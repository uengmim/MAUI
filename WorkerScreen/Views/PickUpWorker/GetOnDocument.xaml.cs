using WorkerScreen.ViewModel.PickUpWorker;

namespace WorkerScreen.Views.PickUpWorker
{

    public partial class GetOnDocument : ContentPage
    {
        /// <summary>
        /// ���� ȭ���Դϴ�.
        /// </summary>
        public GetOnDocument()
        {
            InitializeComponent();
            GetOnViewModel getOnViewModel = new GetOnViewModel();

            this.BindingContext = getOnViewModel;
        }

        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //�ڷΰ���
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}