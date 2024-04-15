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
    public class HomeViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        LoginInfo loginInfo;

        #region Commands
        /// <summary>
        /// 로그인 Command 입니다.
        /// </summary>
        public ICommand CrushingDocCommand => new Command(OnCrushingDoc);
        #endregion

        #region HomePageModel

        public event PropertyChangedEventHandler PropertyChanged;
        public HomeViewModel()
        {
        }
        #endregion

        // Methods
        #region Navigation
        private async void OnCrushingDoc()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CrushingDocument());
        }
        #endregion

        public LoginInfo LoginInfo
        {
            get => loginInfo;
            set => loginInfo = value;
        }
    }
}