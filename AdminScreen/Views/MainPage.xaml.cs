namespace AdminScreen.Views
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        public async void RegistClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistPage());
        }
        public async void ProvisionClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LockProvisionPage());
        }

        public async void RecoveryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LockRecoveryPage());
        }
        public async void MonitoringClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskMonitoringPage());
        }
        public async void HistoryCheckClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryPage());
        }
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

        }
    }
}