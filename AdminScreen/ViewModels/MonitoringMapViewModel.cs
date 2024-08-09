using AdminScreen.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// 운송 경로 조회 화면
    /// </summary>
    public class MonitoringMapViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 특성 변경 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        //public LoginInfo LoginInfo
        //{
        //    get { return loginInfo; }
        //    set
        //    {
        //        loginInfo = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private LoginInfo loginInfo;

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand MenuCommand => new Command<string>(p => OnMenuCommand(p));

        private void OnMenuCommand(string menu)
        {
            Shell.Current.GoToAsync("//App/" + menu);
        }

        //public ICommand LoginCommand => new Command(OnLogin);

        //private async void OnLogin()
        //{
        //    if (this.LoginInfo.ID == null )
        //    {
        //        await Application.Current.MainPage.DisplayAlert("ID 확인", "ID를 입력하세요", "OK");
        //        return;
        //    }

        //    if (this.LoginInfo.PW == null )
        //    {
        //        await Application.Current.MainPage.DisplayAlert("비밀번호확인", "비밀번호를 입력하세요", "OK");

        //        return;
        //    }
        //    await Shell.Current.GoToAsync("//App/");

        //}
    }
}