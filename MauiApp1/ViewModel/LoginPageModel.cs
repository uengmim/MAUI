using MauiApp1.Common;
using MauiApp1.Models;
using MauiApp1.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiApp1.ViewModel
{
    public class LoginPageModel : BaseViewModel
    {
        #region Properties
        /// <summary>
        /// 현재 로그인한 사용자 정보입니다.
        /// </summary>
        public LoginInfo LoginInfo
        {
            get { return loginInfo; }
            set
            {
                loginInfo = value;
                OnPropertyChanged();
            }
        }
        private LoginInfo loginInfo;

        public bool IsSaveID
        {
            get { return isSaveID; }
            set
            {
                isSaveID = value;

                if (!value)
                {
                    Preferences.Remove(Constants.SaveIDKey);
                    IsSavePW = false;
                }

                OnPropertyChanged();
            }
        }

        private bool isSaveID = false;

        public bool IsSavePW
        {
            get { return isSavePW; }
            set
            {
                isSavePW = value;

                if (!value)
                {
                    Preferences.Remove(Constants.SavePWKey);
                }

                OnPropertyChanged();
            }
        }
        private bool isSavePW = false;
        #endregion

        #region Commands
        /// <summary>
        /// 로그인 Command 입니다.
        /// </summary>
        public ICommand LoginCommand => new Command(OnLogin);


        /// <summary>
        /// 회원가입 Command 입니다.
        /// </summary>
        public ICommand MemberJoinCommand => new Command(OnMemberJoin);

        #endregion

        #region LoginPageModel
        public LoginPageModel()
        {
            this.LoginInfo = new LoginInfo();

            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SaveIDKey))
            {
                this.IsSaveID = true;
                this.LoginInfo.LoginId = Preferences.Get(Constants.SaveIDKey, "");
            }

            if (Preferences.ContainsKey(Constants.SavePWKey))
            {
                this.IsSavePW = true;
                this.LoginInfo.LoginPw = Preferences.Get(Constants.SavePWKey, "");
            }
        }
        #endregion

        // Methods
        #region OnLogin
        /// <summary>
        /// 로그인 메뉴를 나타냅니다.
        /// </summary>
        private async void OnLogin()
        {
            if (this.LoginInfo.LoginId == null)
            {
                await Application.Current.MainPage.DisplayAlert("ID 확인", "ID를 입력하세요", "OK");
                return;
            }

            if (this.LoginInfo.LoginPw == null)
            {
                await Application.Current.MainPage.DisplayAlert("비밀번호확인", "비밀번호를 입력하세요", "OK");

                return;
            }

            this.IsBusy = true;

            try
            {
                // 로그인 처리

                // 로그인 완료 시
                if (this.IsSaveID)
                {
                    Preferences.Remove(Constants.SaveIDKey);
                    Preferences.Set(Constants.SaveIDKey, LoginInfo.LoginId);
                }

                if (this.IsSavePW)
                {
                    Preferences.Remove(Constants.SavePWKey);
                    Preferences.Set(Constants.SavePWKey, LoginInfo.LoginPw);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("에러", ex.Message, "OK");
                return;
            }
            finally
            {
                this.IsBusy = false;
            }

            await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
        #endregion

        #region OnMemberJoin
        private async void OnMemberJoin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new QRRecogPage());

        }
        #endregion


    }
}