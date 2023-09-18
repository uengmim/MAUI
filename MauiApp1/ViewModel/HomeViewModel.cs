using MauiApp1.Common;
using MauiApp1.Models;
using MauiApp1.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiApp1.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {

        #region Commands
        /// <summary>
        /// 로그인 Command 입니다.
        /// </summary>
        public ICommand QRCodeCommand => new Command(OnQRCode);


        #endregion

        #region LoginPageModel
        public HomeViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        // Methods
        #region onQRCode
        /// <summary>
        /// 로그인 메뉴를 나타냅니다.
        /// </summary>
        private async void OnQRCode()
        {

            await Shell.Current.GoToAsync(nameof(QRCodePage));
        }
        #endregion

        #region OnMemberJoin
        private async void OnMemberJoin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new QRRecogPage());

        }
        #endregion


    }
}