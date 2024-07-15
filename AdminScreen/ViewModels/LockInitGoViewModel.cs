using AdminScreen.Model;
using AdminScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.InteropServices;
using Mapsui.UI.Maui;

namespace AdminScreen.ViewModel
{
    /// <summary>
    /// 자물쇠 스캔 화면
    /// </summary>
    public class LockInitGoViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// 로그인한 사용자의 정보
        /// </summary>
        private string _lockData = "";
        private string _lockName = "";
        private string _lockmacdata = "";

        /// <summary>
        /// Lock Data
        /// </summary>
        public string LockData
        {
            get => _lockData;
            set
            {
                _lockData = value;
                OnPropertyChanged(nameof(LockData));
            }
        }

        /// <summary>
        /// Lock 이름
        /// </summary>
        public string LockName
        {
            get => _lockName;
            set
            {
                _lockName = value;
                OnPropertyChanged(nameof(LockName));
            }
        }

        /// <summary>
        /// LockMac
        /// </summary>
        public string LockMacData
        {
            get => _lockmacdata;
            set
            {
                _lockmacdata = value;
                OnPropertyChanged(nameof(LockMacData));
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

        public LockInitGoViewModel()
        {

        }
    }
}