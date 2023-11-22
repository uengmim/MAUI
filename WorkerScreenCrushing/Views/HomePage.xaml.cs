using WorkerScreenCrushing.Models;
using WorkerScreenCrushing.ViewModel;

namespace WorkerScreenCrushing.Views;

public partial class HomePage : ContentPage
{
    public HomePage(LoginInfo log)
	{
		//HomeViewModel. singe
		InitializeComponent();
		HomeViewModel homeViewModel = new HomeViewModel();
		homeViewModel.Name = log.Name;
		homeViewModel.PhoneNumber = log.PhoneNumber;

		this.BindingContext = homeViewModel;


    }

    private async void Logout_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

    }
}