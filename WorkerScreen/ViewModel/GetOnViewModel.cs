using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace WorkerScreen.ViewModel
{
    public class GetOnViewModel : INotifyPropertyChanged
    {
        #region Properties
        public LoginInfo LoginInfo
        {
            get => loginInfo;
            set => loginInfo = value;
        }
        public string Name { get; set; }
        LoginInfo loginInfo;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand GetOnDetailCommand => new Command(GetOnDetailSecurity);
        #endregion
        // Methods
        #region Navigation
        private async void GetOnDetailSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GetOnDocDetail());
        }
        #endregion

    }
}