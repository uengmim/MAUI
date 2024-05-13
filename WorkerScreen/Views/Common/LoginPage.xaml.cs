namespace WorkerScreen.Views.Common
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void Login_Clicked(object sender, EventArgs e)
        {
            KeyboardHelper.HideKeyboard();
        }

    }
}