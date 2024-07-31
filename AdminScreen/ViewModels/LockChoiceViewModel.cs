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
    /// <summary>
    /// 등록 및 초기화 화면
    /// </summary>
    public class LockChoiceViewModel : INotifyPropertyChanged
    {

        #region Commands
        /// <summary>
        /// 로그인 Command 입니다.
        /// </summary>
        public INavigation Navigation { get; set; }

        /// <summary>
        /// 자물쇠 등록 Command
        /// </summary>
        public ICommand RegistCommand => new Command(OnRegist);

        /// <summary>
        /// 자물쇠 초기화 Command
        /// </summary>
        public ICommand InitCommand => new Command(OnInit);

        /// <summary>
        /// 특성 변경 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        // Methods
        #region HomePage
        /// <summary>
        /// LockRegistPage 이동
        /// </summary>
        public async void OnRegist()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LockRegistPage());
        }

        /// <summary>
        /// LockInitRecogPage 이동
        /// </summary>
        public async void OnInit()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LockInitRecogPage());
        }




        #endregion


    }
}