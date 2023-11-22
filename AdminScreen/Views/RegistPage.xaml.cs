namespace AdminScreen.Views
{
    public partial class RegistPage : ContentPage
    {
        public RegistPage()
        {
            InitializeComponent();

        }

        [Obsolete]
        public async void SearchClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistSearchPage());
        }
    } 
}