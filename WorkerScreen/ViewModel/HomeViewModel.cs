using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace WorkerScreen.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EMPNO { get; set; }
        public string DEPTID { get; set; }

        LoginInfo loginInfo;
        #region Commands
        /// <summary>
        /// 로그인 Command 입니다.
        /// </summary>
        public INavigation Navigation { get; set; }
        public ICommand QRCodeCommand => new Command(OnQRCode);
        public ICommand GetOnCommand => new Command(GetOnSecurity);
        public ICommand GetOffCommand => new Command(GetOffSecurity);


        #endregion

        #region HomePageModel

        public event PropertyChangedEventHandler PropertyChanged;
        public HomeViewModel()
        {
        }
        #endregion

        // Methods
        #region HomePage
        /// <summary>
        /// 메인 메뉴를 나타냅니다.
        /// </summary>
        private async void OnQRCode()
        {
            //await Shell.Current.GoToAsync(nameof(QRCodePage));
            //await Navigation.PushAsync(new QRCodePage());
            await Application.Current.MainPage.Navigation.PushAsync(new QRCodePage(Name, PhoneNumber,EMPNO, DEPTID));
        }
        private async void GetOnSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GetOnDocument());
        }
        private async void GetOffSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GetOffDocument());
        }


        #endregion

        public LoginInfo LoginInfo
        {
            get => loginInfo;
            set => loginInfo = value;
        }
    }
}