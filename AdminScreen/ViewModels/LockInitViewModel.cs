using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Data;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// 자물쇠 초기화 화면
    /// </summary>
    public class LockInitViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Lock Data 모델 
        /// </summary>
        private ObservableCollection<LockInfomation> lockInfoModel = new ObservableCollection<LockInfomation>();

        /// <summary>
        /// Lock Data 모델 
        /// </summary>
        public ObservableCollection<LockInfomation> LockInfoModel { get { return lockInfoModel; } set { lockInfoModel = value; OnPropertyChanged(nameof(LockInfoModel)); } }

        /// <summary>
        /// Lock Infom 모델
        /// </summary>
        private ObservableCollection<LockInfom> lockDataModel = new ObservableCollection<LockInfom>();

        /// <summary>
        /// Lock Infom 모델
        /// </summary>
        public ObservableCollection<LockInfom> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        /// <summary>
        /// selectedItem값
        /// </summary>
        private LockInfomation selectedItem;

        public LockInfomation SelectedItem
        {
            get { return selectedItem; }
            set
            {
                try
                {
                    if (selectedItem != value)
                    {
                        selectedItem = value;
                        OnPropertyChanged(nameof(SelectedItem));


                        if (SelectedItem != null)
                        {
                            PerformNavigation(SelectedItem.Lockid, SelectedItem.LockName, SelectedItem.LockMac);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Task task = ShowCustomAlert("알림", ex.Message, "확인", "");
                    return;
                }
            }
        }

        /// <summary>
        /// LockInitPage 이동
        /// </summary>
        private async void PerformNavigation(string Data, string Name, string LockMac)
        {
            if (SelectedItem != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new LockInitPage(Data, Name, LockMac));

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

        public LockInitViewModel()
        {
            SelectedItem = null;
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