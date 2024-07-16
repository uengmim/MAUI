using AdminScreen.Model;
using AdminScreen.Views;
using AdminScreen.Common;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using XNSC.DD.EX;
using Command = Microsoft.Maui.Controls.Command;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// 로그인 화면
    /// </summary>
    public class AdminLoginModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 특성 변경 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Loading
        /// </summary>
        private bool isLoading;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }

        /// <summary>
        /// 현재 로그인한 사용자 정보
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

        public bool IsSaveNumber
        {
            get { return isSaveNumber; }
            set
            {
                isSaveNumber = value;

                if (!value)
                {
                    Preferences.Remove(Constants.SaveDeptIDKey);
                    Preferences.Remove(Constants.SaveEmpNoKey);
                    Preferences.Remove(Constants.SavePinKey);
                    Preferences.Remove(Constants.SaveEmpNmKey);
                }

                OnPropertyChanged(nameof(IsSaveNumber));
            }
        }

        private bool isSaveNumber = false;
        public AdminLoginModel()
        {
            this.LoginInfo = new LoginInfo();

            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SaveDeptIDKey))
            {
                IsSaveNumber = true;
                LoginInfo.EMPNO = Preferences.Get(Constants.SaveEmpNoKey, "");
                LoginInfo.PIN = Preferences.Get(Constants.SavePinKey, "");
                LoginInfo.EMPNM = Preferences.Get(Constants.SaveEmpNmKey, "");
            }
        }

        /// <summary>
        /// 로그인 Command
        /// </summary>
        public ICommand AdmLoginCommand => new Command(OnAdmLoginClicked);

        public async void OnAdmLoginClicked()
        {
            // 이미 로딩 중인 경우에는 더 이상의 처리를 하지 않음
            if (IsLoading)
            {
                return;
            }

            IsLoading = true;

            try
            {
                if (LoginInfo.AdminID == null || LoginInfo.AdminID == "")
                {
                    await ShowCustomAlert("알림", "ID를 입력해주세요.", "확인", "");
                    IsLoading = false;
                    return;
                }

                if (LoginInfo.AdminPW == null || LoginInfo.AdminPW == "")
                {
                    await ShowCustomAlert("알림", "암호를 입력해주세요.", "확인", "");
                    IsLoading = false;
                    return;
                }

                var dataService = ImateHelper.GetSingleTone();

                var whereCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                        new DIMWhereFieldCondition{ fieldName = "EMPNO" , value = this.loginInfo.AdminID, condition = DIMWhereCondition.Equal},
                        new DIMWhereFieldCondition{ fieldName = "PIN" , value = this.loginInfo.AdminPW, condition = DIMWhereCondition.Equal}
                    }
                };

                var admInfoList = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList",
                        whereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                if (admInfoList.Count == 0)
                {
                    await ShowCustomAlert("알림", "등록되지않은 아이디 또는 비밀번호 입니다.", "확인", "");
                    IsLoading = false;
                    return;
                }
                LoginInfo.DEPTID = admInfoList[0].DEPTID;
                LoginInfo.EMPNO = admInfoList[0].EMPNO;
                LoginInfo.PIN = admInfoList[0].PIN;
                LoginInfo.EMPNM = admInfoList[0].EMPNM;

                if (isSaveNumber)
                {
                    Preferences.Remove(Constants.SaveDeptIDKey);
                    Preferences.Set(Constants.SaveDeptIDKey, LoginInfo.DEPTID);
                    Preferences.Remove(Constants.SaveEmpNoKey);
                    Preferences.Set(Constants.SaveEmpNoKey, LoginInfo.EMPNO);
                    Preferences.Remove(Constants.SavePinKey);
                    Preferences.Set(Constants.SavePinKey, LoginInfo.PIN);
                    Preferences.Remove(Constants.SaveEmpNmKey);
                    Preferences.Set(Constants.SaveEmpNmKey, LoginInfo.EMPNM);
                }
                await App.Current.MainPage.Navigation.PushAsync(new MainPage(LoginInfo));
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
            finally
            {
                IsLoading = false;
            }
        }

        // 팝업 표시 상태를 나타내는 플래그
        private bool isAlertShowing = false;

        //팝업
        private async Task ShowCustomAlert(string title, string message, string accept, string cancle)
        {
            // 이미 경고 팝업이 표시 중인 경우 추가적인 처리를 하지 않음
            if (isAlertShowing)
            {
                return;
            }

            isAlertShowing = true; // 경고 팝업 표시 중임을 표시

            // 팝업 애니메이션 비활성화
            try
            {
                var alertPage = new CustomAlertPage(title, message, accept, cancle);
                alertPage.Disappearing += (sender, e) => isAlertShowing = false;
                await App.Current.MainPage.Navigation.PushModalAsync(alertPage, animated: false);
            }
            finally
            {
                isAlertShowing = true;
            }
        }
    }
}