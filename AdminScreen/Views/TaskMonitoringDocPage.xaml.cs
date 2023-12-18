namespace AdminScreen.Views
{
    public partial class TaskMonitoringDocPage : ContentPage
    {

        public TaskMonitoringDocPage()
        {
            InitializeComponent();
        }


        public async void LockPictureClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocLockPicturePage());
        }

        public async void CrushingReportClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocCrushingReportPage());
        }
        public async void CrushingProofClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocCrushingProofPage());
        }
    }
}