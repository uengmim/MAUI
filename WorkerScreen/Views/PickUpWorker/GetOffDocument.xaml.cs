using WorkerScreen.ViewModel.PickUpWorker;

namespace WorkerScreen.Views.PickUpWorker
{
    public partial class GetOffDocument : ContentPage
    {
        /// <summary>
        /// ���� ȭ���Դϴ�.
        /// </summary>
        public GetOffDocument()
        {
            InitializeComponent();
            GetOffViewModel getOffViewModel = new GetOffViewModel();
            this.BindingContext = getOffViewModel;
        }
        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //�ڷΰ���
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}