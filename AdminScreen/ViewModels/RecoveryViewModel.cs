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
    public class RegistViewModel : INotifyPropertyChanged
    {
        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand RegistDetailCommand => new Command(RegistDetailSecurity);
        #endregion
        // Methods
        #region Navigation
        private async void RegistDetailSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegistSearchPage());
        }
        #endregion

    }
}