using AdminScreen.ViewModels;
using Android.Media.Metrics;
using Android.Net;

namespace AdminScreen.Views
{
    public partial class RegistSearchPage : ContentPage
    {
        public RegistSearchPage()
        {
            InitializeComponent();
            RegisterSearchModel registerSearchModel = new RegisterSearchModel();
            this.BindingContext = registerSearchModel;
            
        }
        private void Renew_Clicked(object sender, EventArgs e)
        {

            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            base.OnAppearing();
            if (BindingContext is RegisterSearchModel viewModel)
            {
                //await viewModel.onScanLockSuccess();
            }
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            base.OnAppearing();

            if (BindingContext is RegisterSearchModel viewModel)
            {
                viewModel.startScanLock();
            }
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        }
    }
}   