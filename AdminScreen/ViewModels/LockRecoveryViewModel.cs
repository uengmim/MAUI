using AdminScreen.Common;
using AdminScreen.Models;
using AdminScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace AdminScreen.ViewModel
{
    /// <summary>
    /// 자물쇠 회수 화면
    /// </summary>
    public class LockRecoveryViewModel : INotifyPropertyChanged
    {

        private bool _isBusy = false; // 실행 중 여부를 나타내는 플래그 추가

        #region Properties
        /// <summary>
        /// Pin
        /// </summary>
        private string _pin = "";

        /// <summary>
        /// PIn
        /// </summary>
        public string PIN
        {
            get => _pin;
            set
            {
                _pin = value;
                OnPropertyChanged(nameof(PIN));
            }
        }

        /// <summary>
        /// 특성 변경 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public INavigation Navigation { get; set; }
        /// <summary>
        /// 자물쇠 검색 Command
        /// </summary>
        public ICommand RecoveryDetailCommand => new Command(RecoveryDetailSecurity);
        #endregion

        // Methods
        #region Navigation
        /// <summary>
        /// LockRecoveryDetailPage 이동
        /// </summary>
        private async void RecoveryDetailSecurity()
        {
            // 실행 중인지 확인하고 중복 실행을 방지
            if (_isBusy)
                return;

            _isBusy = true; // 실행 중 플래그 설정

            await Application.Current.MainPage.Navigation.PushAsync(new LockRecoveryDetailPage(PIN));

            _isBusy = false; // 실행 완료 후 플래그 해제

        }
        #endregion

    }
}