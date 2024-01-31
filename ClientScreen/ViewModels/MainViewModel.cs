using ClientScreen.Model;
using ClientScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace ClientScreen.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EMPNO { get; set; }
        public string DEPTID { get; set; }
        public string REFDA2 { get; set; }

        public string WKPL {  get; set; }

        #region Commands
        /// <summary>
        /// 로그인 Command 입니다.
        /// </summary>
        public INavigation Navigation { get; set; }
        public ICommand RecallCommand => new Command(OnRecall);
        public ICommand MonitoringCommand => new Command(OnMonitoring);

        #endregion

        #region HomePageModel

        public event PropertyChangedEventHandler PropertyChanged;
        public MainViewModel()
        {
        }
        #endregion

        // Methods
        #region HomePage
        /// <summary>
        /// 메인 메뉴를 나타냅니다.
        /// </summary>
        private async void OnRecall()
        {
            //await Shell.Current.GoToAsync(nameof(QRCodePage));
            //await Navigation.PushAsync(new QRCodePage());
            await Application.Current.MainPage.Navigation.PushAsync(new LockRecallPage(DEPTID, WKPL, EMPNO, REFDA2));
        }
        private async void OnMonitoring()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CliTaskMonitoringPage(DEPTID, WKPL, EMPNO, REFDA2));
        }



        #endregion


    }
}