using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Windows.Input;
using WorkerScreen.Views.CrushingWorker;

namespace WorkerScreen.ViewModel.Common
{
    /// <summary>
    /// 파쇄 작업자 홈페이지 화면입니다.
    /// </summary>
    public class CRWorkerHomeViewModel : INotifyPropertyChanged
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
        public string PhoneNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        /// <summary>
        /// 파쇄등록 이동 Command 입니다.
        /// </summary>
        public ICommand CrushingDocCommand => new Command(OnCrushingDoc);
        #endregion

        #region 화면 이동
        /// <summary>
        /// 파쇄등록 화면 이동 버튼입니다.
        /// </summary>
        private async void OnCrushingDoc()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CrushingDocument());
        }
        #endregion

        #region CRWorkerHomeViewModel
        public CRWorkerHomeViewModel()
        {
        }
        #endregion
    }
}