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
    public class LockChoiceViewModel : INotifyPropertyChanged
    {

        #region Commands
        /// <summary>
        /// 로그인 Command 입니다.
        /// </summary>
        public INavigation Navigation { get; set; }
        public ICommand RegistCommand => new Command(OnRegist);
        public ICommand InitCommand => new Command(OnInit);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        // Methods
        #region HomePage
        /// <summary>
        /// 메인 메뉴를 나타냅니다.
        /// </summary>

        public async void OnRegist()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LockRegistPage());
        }

        public async void OnInit()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LockInitRecogPage());
        }




        #endregion


    }
}