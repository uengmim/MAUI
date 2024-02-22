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
    public class AdmMainViewModel : INotifyPropertyChanged
    {

        private string _pin = "";
        private string _name = "";
        public string EMPNO { get; set; }
        public string DEPTID { get; set; }
        public string REFDA2 { get; set; }
        public string PIN
        {
            get => _pin;
            set
            {
                _pin = value;
                OnPropertyChanged(nameof(PIN));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string WKPL {  get; set; }

        #region Commands
        /// <summary>
        /// 로그인 Command 입니다.
        /// </summary>
        public INavigation Navigation { get; set; }
        public ICommand RegistCommand => new Command(OnRegist);
        public ICommand ProvisionCommand => new Command(OnProvision);
        public ICommand RecoveryCommand => new Command(OnRecovery);
        public ICommand MonitoringCommand => new Command(OnMonitoring);
        public ICommand HistoryCheckCommand => new Command(OnHistoryCheck);


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region HomePageModel

        public AdmMainViewModel()
        {
        }
        #endregion

        // Methods
        #region HomePage
        /// <summary>
        /// 메인 메뉴를 나타냅니다.
        /// </summary>

        public async void OnRegist()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LockChoicePage());
        }

        public async void OnProvision()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LockProvisionPage(PIN));
        }

        public async void OnRecovery()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LockRecoveryPage(PIN));
        }

        public async void OnMonitoring()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TaskMonitoringPage());
        }

        public async void OnHistoryCheck()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HistoryPage());
        }



        #endregion


    }
}