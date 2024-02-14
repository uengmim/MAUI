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
    public class LockRegistDetailViewModel : INotifyPropertyChanged
    {
        private LockInfomation lockinfo;

        private LockInfomation selectedItem;

        private ObservableCollection<LockInfomation> lockDataModel = new ObservableCollection<LockInfomation>();
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

        public LockInfomation LockInfo
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfo));
            }
        }       

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

        private async void PerformNavigation(LockInfomation lockInfo)
        {
            if (SelectedItem != null)
            {

                ttlockHelper.StopLockDeviceScan();

                await Application.Current.MainPage.Navigation.PushAsync(new LockRegistInputPage(lockInfo));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("오류", "선택된 자물쇠가 없습니다.", "OK");
                SelectedItem = null;
                return;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}