using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.Views;
using Android.Runtime;
using Java.Interop;
using Java.Lang;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AdminScreen.ViewModels
{
    public class RegisterSearchModel : INotifyPropertyChanged
    {

        public ObservableCollection<LockInfo> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        public ObservableCollection<LockInfo> lockDataModel = new ObservableCollection<LockInfo>();

        public LockInfo LockInfo
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfo));
            }
        }       

        private LockInfo lockinfo;

        private LockInfo selectedItem;

        public LockInfo SelectedItem
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


        private async void PerformNavigation(LockInfo lockInfo)
        {
            if (SelectedItem != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new LockRegisterPage(lockInfo));
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

        public RegisterSearchModel()
        {
            SelectedItem = null;
        }
        }
}