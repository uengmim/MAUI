namespace AdminScreen.Views
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        [Obsolete]
        public async void RegistClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistPage());
        }
    }
}