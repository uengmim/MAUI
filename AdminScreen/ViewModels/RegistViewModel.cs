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
    public class RecoveryViewModel : INotifyPropertyChanged
    {
        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand RecoveryDetailCommand => new Command(RecoveryDetailSecurity);
        #endregion
        // Methods
        #region Navigation
        private async void RecoveryDetailSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RecoverySearchPage());
        }
        #endregion

    }
}