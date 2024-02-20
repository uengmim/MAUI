using ClientScreen.ViewModel;

namespace ClientScreen.Views
{
    public partial class ClientMainPage : ContentPage
    {

        public ClientMainPage(string DeptId, string Empno)
        {
            InitializeComponent();

            MainViewModel mainViewModel = new MainViewModel();

            mainViewModel.DEPTID = DeptId;
            mainViewModel.EMPNO = Empno;



            this.BindingContext = mainViewModel;

        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ClientLoginPage());

        }
    }
}