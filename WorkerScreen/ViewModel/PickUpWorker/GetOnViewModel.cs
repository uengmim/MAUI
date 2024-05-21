using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Windows.Input;
using WorkerScreen.Views.PickUpWorker;

namespace WorkerScreen.ViewModel.PickUpWorker
{
    /// <summary>
    /// 상차 화면입니다.
    /// </summary>
    public class GetOnViewModel : INotifyPropertyChanged
    {
        #region Properties
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
        public INavigation Navigation { get; set; }
        /// <summary>
        /// 상차 처리 Command입니다.
        /// </summary>
        public ICommand GetOnDetailCommand => new Command(GetOnDetailSecurity);
        #endregion

        #region 상차 처리 화면 이동
        private async void GetOnDetailSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GetOnDocDetail());
        }
        #endregion

    }
}