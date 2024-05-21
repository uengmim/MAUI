using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Windows.Input;
using WorkerScreen.Views.PickUpWorker;

namespace WorkerScreen.ViewModel.PickUpWorker
{
    /// <summary>
    /// 하차 화면입니다.
    /// </summary>
    public class GetOffViewModel : INotifyPropertyChanged
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
        public ICommand GetOffDetailCommand => new Command(GetOffDetailSecurity);
        #endregion

        #region 하차 처리 화면 이동
        private async void GetOffDetailSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GetOffDocDetail());
        }
        #endregion

    }
}