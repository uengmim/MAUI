namespace ClientScreen.Views
{
    public partial class CliTaskMonitoringDocPage : ContentPage
    {

        public CliTaskMonitoringDocPage()
        {
            InitializeComponent();
        }
        public async void CliLockPictureClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CliDocLockPicturePage());
        }

        public async void CliCrushingReportClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CliDocCrushingReportPage());
        }
        public async void CliCrushingProofClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CliDocCrushingProofPage());
        }
    }
}