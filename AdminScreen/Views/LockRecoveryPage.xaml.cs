namespace AdminScreen.Views
{
    public partial class LockRecoveryPage : ContentPage
    {
        public LockRecoveryPage()
        {
            InitializeComponent();

        }
        public async void RecoveryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecoverySearchPage());
        }
    } 
}