using AdminScreen.Model;
using AdminScreen.Views;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using XNSC.DD.EX;
using Command = Microsoft.Maui.Controls.Command;


namespace AdminScreen.ViewModels
{
    public class AdminLoginModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //PropertyChanged 변경
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

        public AdminLoginModel()
        {
            this.LoginInfo = new LoginInfo();
        }

        /// <summary>
        /// 로그인 Command
        /// </summary>
        public ICommand AdmLoginCommand => new Command(OnAdmLoginClicked);

        //private object loadingObj = new object();

        public async void OnAdmLoginClicked()
        {
            IsLoading = true;
            //Lock
            //lock (loadingObj)
            //{
            //    if (isLoading)
            //        return;
            //    IsLoading = true;
            //}

            try
            {
                if (LoginInfo.AdminID == null || LoginInfo.AdminID == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "ID를 입력해주세요.", "확인");
                    IsLoading = false;
                    return;
                }

                if (LoginInfo.AdminPW == null || LoginInfo.AdminPW == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "암호를 입력해주세요.", "확인");
                    IsLoading = false;
                    return;
                }

                //var deviceId = Preferences.Get("shreDoc_deviceId", string.Empty);
                //if (string.IsNullOrWhiteSpace(deviceId))
                //{
                //    deviceId = System.Guid.NewGuid().ToString();
                //    Preferences.Set("shreDoc_deviceId", deviceId);
                //}
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
                    await Application.Current.MainPage.DisplayAlert("알림", "등록되지않은 아이디 또는 비밀번호 입니다.", "확인");
                    IsLoading = false;
                    return;
                }

                await App.Current.MainPage.Navigation.PushAsync(new MainPage(admInfoList[0].DEPTID, admInfoList[0].EMPNO, admInfoList[0].PIN, admInfoList[0].EMPNM));
                IsLoading = false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                IsLoading = false;
                return;
            }
        }
    }
}