using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace AdminScreen.Views
{
    public partial class CustomAlertPage : ContentPage
    {
        private TaskCompletionSource<bool> userConfirmedTaskSource;

        public ICommand AcceptCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public string CustomTitle { get; private set; }
        public string Message { get; private set; }
        public string Accept { get; private set; }
        public string Cancel { get; private set; }

        public Task<bool> UserConfirmedTask => userConfirmedTaskSource.Task;

        public event EventHandler<bool> AcceptButtonClicked;
        public event EventHandler<bool> CancelButtonClicked;

        public CustomAlertPage(string title, string message, string accept, string cancel)
        {
            InitializeComponent();
            CustomTitle = title;
            Message = message;
            Accept = accept;
            Cancel = cancel;

            userConfirmedTaskSource = new TaskCompletionSource<bool>();

            AcceptCommand = new Command(async () =>
            {
                await CloseModalAsync();
                AcceptButtonClicked?.Invoke(this, true);
                userConfirmedTaskSource.TrySetResult(true);
            });

            CancelCommand = new Command(async () =>
            {
                CancelButtonClicked?.Invoke(this, false);

                await CloseModalAsync();
                userConfirmedTaskSource.TrySetResult(false);
            });

            BindingContext = this;
        }

        protected override bool OnBackButtonPressed()
        {
            CloseModal();
            return true; // 기본 동작을 막음
        }

        private async Task CloseModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(animated: false);
        }

        private void CloseModal()
        {
            CloseModalAsync().Wait(); // 비동기 메서드를 동기적으로 호출하여 처리가 완료될 때까지 기다림
        }
    }
}