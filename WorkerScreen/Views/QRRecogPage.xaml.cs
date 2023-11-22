using Camera.MAUI;
using Camera.MAUI.Test;
using WorkerScreen.Models;
using WorkerScreen.ViewModel;

namespace WorkerScreen.Views;

public partial class QRRecogPage : ContentPage
{


    public QRRecogPage(string boxnum, string location, string lockData, string lockName, string confno)
    {

        InitializeComponent();
        QRRecogViewModel qRRecogViewModel = new QRRecogViewModel();
        qRRecogViewModel.BoxNum = boxnum;
        qRRecogViewModel.Location = location;
        qRRecogViewModel.LockData = lockData;
        qRRecogViewModel.LockName = lockName;
        qRRecogViewModel.CONFNO = confno;

        this.BindingContext = qRRecogViewModel;

    }
    private async void Renew_Clicked(object sender, EventArgs e)
    {

        loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;
        base.OnAppearing();
        if (BindingContext is QRRecogViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }
        loadingIndicator.IsRunning = false;
        loadingIndicator.IsVisible = false;
    }

    protected override async void OnAppearing()
    {
        loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;
        base.OnAppearing();

        if (BindingContext is QRRecogViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }
        loadingIndicator.IsRunning = false;
        loadingIndicator.IsVisible = false;
    }

}