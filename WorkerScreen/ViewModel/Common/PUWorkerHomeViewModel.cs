using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.InteropServices;
using WorkerScreen.Views.PickUpWorker;

namespace WorkerScreen.ViewModel.Common
{
    /// <summary>
    /// 메인 메뉴 화면입니다.
    /// </summary>
    public class PUWorkerHomeViewModel : INotifyPropertyChanged
    {

        #region Properties
        /// <summary>
        /// 현재 로그인한 사용자 정보입니다.
        /// </summary>
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EMPNO { get; set; }
        public string DEPTID { get; set; }
        /// <summary>
        /// 로그인한 사용자 정보입니다.
        /// </summary>
        public LoginInfo LoginInfo
        {
            get => loginInfo;
            set => loginInfo = value;
        }
        private LoginInfo loginInfo;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        /// <summary>
        /// 홈페이지 이동 Command 입니다.
        /// </summary>
        public INavigation Navigation { get; set; }
        public ICommand QRCodeCommand => new Command(OnQRCode);
        public ICommand GetOnCommand => new Command(GetOnSecurity);
        public ICommand GetOffCommand => new Command(GetOffSecurity);


        #endregion

        #region PUWorkerHomeViewModel

        public PUWorkerHomeViewModel()
        {
        }
        #endregion

        #region 화면 이동
        /// <summary>
        /// 보안 문서 봉인 화면 이동입니다.
        /// </summary>
        private async void OnQRCode()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new QRCodePage(Name, PhoneNumber, EMPNO, DEPTID));
        }
        /// <summary>
        /// 상차 화면 이동입니다.
        /// </summary>
        private async void GetOnSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GetOnDocument());
        }
        /// <summary>
        /// 하차 화면 이동입니다.
        /// </summary>
        private async void GetOffSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GetOffDocument());
        }


        #endregion

    }
}