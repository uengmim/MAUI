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
    public class LockInitViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<LockInfomation> lockInfoModel = new ObservableCollection<LockInfomation>();
        public ObservableCollection<LockInfomation> LockInfoModel { get { return lockInfoModel; } set { lockInfoModel = value; OnPropertyChanged(nameof(LockInfoModel)); } }

        private ObservableCollection<LockInfom> lockDataModel = new ObservableCollection<LockInfom>();
        public ObservableCollection<LockInfom> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

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
                    Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                    return;
                }
            }
        }


        private async void PerformNavigation(string Data, string Name, string LockMac)
        {
            if (SelectedItem != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new LockInitPage(Data, Name, LockMac));

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

        public LockInitViewModel()
        {
            SelectedItem = null;
        }
        }
}