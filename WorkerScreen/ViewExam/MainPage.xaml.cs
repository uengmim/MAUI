using WorkerScreen.ViewModel;

namespace WorkerScreen.Views;

public partial class MainPage : ContentPage
{

	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}

