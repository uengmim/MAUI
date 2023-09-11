using MauiApp1.ViewModel;

namespace MauiApp1.Views;

public partial class MainPage : ContentPage
{

	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}

