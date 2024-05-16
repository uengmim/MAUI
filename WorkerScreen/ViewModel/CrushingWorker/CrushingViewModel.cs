using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Windows.Input;
using WorkerScreen.Views.CrushingWorker;

namespace WorkerScreen.ViewModel.CrushingWorker
{
    /// <summary>
    /// 보안 문서 자물쇠 화면
    /// </summary>
    public class CrushingViewModel : INotifyPropertyChanged
    {
        #region Properties
        /// <summary>
        /// 현재 로그인한 사용자 정보입니다.
        /// </summary>
        public LoginInfo LoginInfo
        {
            get => loginInfo;
            set => loginInfo = value;
        }
        private LoginInfo loginInfo;
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        /// <summary>
        /// 자물쇠 검색 Command입니다.
        /// </summary>
        public ICommand CrushingDetailCommand => new Command(CrushingDetailSecurity);
        #endregion

        #region 화면이동
        /// <summary>
        /// 보안 문서 자물쇠 검색 화면 이동
        /// </summary>
        private async void CrushingDetailSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CrushingDocDetail());
        }
        #endregion
    }
}