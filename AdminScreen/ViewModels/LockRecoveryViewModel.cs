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
    public class LockRecoveryViewModel : INotifyPropertyChanged
    {
        #region Properties
        private string _pin = "";

        public string PIN
        {
            get => _pin;
            set
            {
                _pin = value;
                OnPropertyChanged(nameof(PIN));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand RecoveryDetailCommand => new Command(RecoveryDetailSecurity);
        #endregion
        // Methods
        #region Navigation
        private async void RecoveryDetailSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LockRecoveryDetailPage(PIN));
        }
        #endregion

    }
}