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
    public class LockRegistViewModel : INotifyPropertyChanged
    {
        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

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

        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand RegistDetailCommand => new Command(RegistDetailSecurity);
        #endregion
        // Methods
        #region Navigation
        private async void RegistDetailSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LockRegistDetailPage());
        }
        #endregion

    }
}