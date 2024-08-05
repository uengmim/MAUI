using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.Views;
using SmartLock.TT;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// 자물쇠 스캔 화면
    /// </summary>
    public class LockRegistDetailViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// lockinfo
        /// </summary>
        private LockInfomation lockinfo;

        /// <summary>
        /// selectedItem
        /// </summary>
        private LockInfomation selectedItem;

        /// <summary>
        /// Lock Data 모델
        /// </summary>
        private ObservableCollection<LockInfomation> lockDataModel = new ObservableCollection<LockInfomation>();

        /// <summary>
        /// Lock Data 모델
        /// </summary>
        public ObservableCollection<LockInfomation> LockDataModel
        { 
            get { 
                return lockDataModel; 
            } 
            set { 
                lockDataModel = value; 
                OnPropertyChanged(nameof(LockDataModel)); 
            } 
        }

        /// <summary>
        /// LockInfo
        /// </summary>
        public LockInfomation LockInfo
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfo));
            }
        }

        /// <summary>
        /// SelectedItem
        /// </summary>
        public LockInfomation SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    if (SelectedItem != null)
                    {
                        PerformNavigation(SelectedItem);
                    }
                }
            }
        }

        /// <summary>
        /// TTLock API Helper
        /// </summary>
        private TTlockHelper ttlockHelper;

        /// <summary>
        /// 생성자
        /// </summary>
        public LockRegistDetailViewModel()
        {
            SelectedItem = null;
            ttlockHelper = new TTlockHelper();
        }

        /// <summary>
        /// LockRegistInputPage 이동
        /// </summary>
        /// <param name="lockInfo"></param>
        private async void PerformNavigation(LockInfomation lockInfo)
        {
            if (SelectedItem != null)
            {

                ttlockHelper.StopLockDeviceScan();

                await Application.Current.MainPage.Navigation.PushAsync(new LockRegistInputPage(lockInfo));

                SelectedItem = null;
            }
            else
            {
                await ShowCustomAlert("알림", "선택된 자물쇠가 없습니다.", "확인", "");
                SelectedItem = null;
                return;
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

        // 팝업 표시 상태를 나타내는 플래그
        private bool isAlertShowing = false;

        //팝업
        private async Task ShowCustomAlert(string title, string message, string accept, string cancle)
        {
            // 이미 경고 팝업이 표시 중인 경우 추가적인 처리를 하지 않음
            if (isAlertShowing)
            {
                return;
            }

            isAlertShowing = true; // 경고 팝업 표시 중임을 표시

            // 팝업 애니메이션 비활성화
            try
            {
                var alertPage = new CustomAlertPage(title, message, accept, cancle);
                alertPage.Disappearing += (sender, e) => isAlertShowing = false;
                await App.Current.MainPage.Navigation.PushModalAsync(alertPage, animated: false);
            }
            finally
            {
                isAlertShowing = true;
            }
        }
    }
}