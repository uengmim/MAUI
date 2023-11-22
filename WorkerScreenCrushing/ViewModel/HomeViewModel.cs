using WorkerScreenCrushing.Common;
using WorkerScreenCrushing.Models;
using WorkerScreenCrushing.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace WorkerScreenCrushing.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        LoginInfo loginInfo;
        #region Commands
        /// <summary>
        /// 로그인 Command 입니다.
        /// </summary>
        public INavigation Navigation { get; set; }
        public ICommand CrushingDocCommand => new Command(OnCrushingDoc);


        #endregion

        #region HomePageModel

        public event PropertyChangedEventHandler PropertyChanged;
        public HomeViewModel()
        {
        }
        #endregion

        // Methods
        #region onQRCode
        /// <summary>
        /// 로그인 메뉴를 나타냅니다.
        /// </summary>
        private async void OnCrushingDoc()
        {
            //await Shell.Current.GoToAsync(nameof(QRCodePage));
            //await Navigation.PushAsync(new QRCodePage());
            await Application.Current.MainPage.Navigation.PushAsync(new CrushingDocument());
        }


        #endregion

        public LoginInfo LoginInfo
        {
            get => loginInfo;
            set => loginInfo = value;
        }
    }
}