using WorkerScreenCrushing.Common;
using WorkerScreenCrushing.Models;
using WorkerScreenCrushing.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace WorkerScreenCrushing.ViewModel
{
    public class CrushingViewModel : INotifyPropertyChanged
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
        public ICommand CrushingDetailCommand => new Command(CrushingDetailSecurity);
        #endregion
        // Methods
        #region Navigation
        private async void CrushingDetailSecurity()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CrushingDocDetail());
        }
        #endregion

    }
}