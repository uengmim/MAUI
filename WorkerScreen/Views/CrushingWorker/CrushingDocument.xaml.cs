using WorkerScreen.ViewModel.CrushingWorker;

namespace WorkerScreen.Views.CrushingWorker
{
    /// <summary>
    /// ���� �ļ� ��� ȭ���Դϴ�.
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
            //�ڷΰ���
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}