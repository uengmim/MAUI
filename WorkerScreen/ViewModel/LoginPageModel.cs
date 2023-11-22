using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ShreDoc.ProxyModel;
using XNSC.DD.EX;
using XNSC.Net;
using WorkerScreen.Utils;

namespace WorkerScreen.ViewModel
{
    public class LoginPageModel : BaseViewModel
    {
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

        public bool IsSaveNumber
        {
            get { return isSaveNumber; }
            set
            {
                isSaveNumber = value;

                if (!value)
                {
                    Preferences.Remove(Constants.SaveLoginNameKey);
                    Preferences.Remove(Constants.SavePhoneNumberKey);
                }

                OnPropertyChanged();
            }
        }

        private bool isSaveNumber = false;

        #endregion

        #region Commands
        /// <summary>
        /// 로그인 Command 입니다.
        /// </summary>
        public ICommand LoginCommand => new Command<string>(p => OnLogin(p));


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
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                this.IsSaveNumber = true;
                this.LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                this.LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");
            }

        }
        #endregion

        // Methods
        #region OnLogin
        /// <summary>
        /// 로그인 메뉴를 나타냅니다.
        /// </summary>
        private async void OnLogin(string menu)
        {
            try
            {
                IsLoading = true;

                if (this.LoginInfo.PhoneNumber == null || this.LoginInfo.PhoneNumber == "")
                {
                    await Application.Current.MainPage.DisplayAlert("전화번호 확인", "전화번호를 입력하세요", "OK");
                    IsLoading = false;
                    return;
                }
                this.IsBusy = true;
                var deviceId = Preferences.Get("shreDoc_deviceId", string.Empty);
                if (string.IsNullOrWhiteSpace(deviceId))
                {
                    deviceId = System.Guid.NewGuid().ToString();
                    Preferences.Set("shreDoc_deviceId", deviceId);
                }
                var dataService = ImateHelper.GetSingleTone();
                var whereCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                    new DIMWhereFieldCondition{ fieldName = "PIN" , value = this.LoginInfo.PhoneNumber, condition = DIMWhereCondition.Equal}
                    }
                };

                var empInfoList = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList",
                                        whereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                if (empInfoList.Count == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("통보", "해당하는 전화번호가 없습니다.", "OK");
                    IsLoading = false;
                    return;
                }

                var whereDeptCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
{
                    new DIMWhereFieldCondition{ fieldName = "DEPTID" , value = empInfoList[0].DEPTID, condition = DIMWhereCondition.Equal}
}
                };

                var deptInfoList = await dataService.Adapter.SelectModelDataAsync<DeptmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.DeptmstModelList",
                            whereDeptCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                //UseDeviceCheck가 True일 경우
                var DeviceTokenInfo = new TokenIdInfo()
                {
                    id = deviceId, //Device 고유 ID, Mac Address, IMIE등 고유 삭별 번호
                    userid = empInfoList[0].PIN, //사용자 ID
                    name = empInfoList[0].EMPNM, //사용자 또는 기기의 이름
                    role = "WORK",  //기본값(USR)
                    email = "",  // Email 없을 경우 "id@iaccount.com"으로 설정
                    deptname = deptInfoList[0].DEPTNM ?? "", //부서명
                    empid = empInfoList[0].EMPNO, //사원ID
                    deptcode = empInfoList[0].DEPTID, //부서코드
                    appname = "SHREDOC", //앱 이름
                    culture = string.Empty //문화권(기본이면 빈 문자열) 메일 본문 Template 구분하는데 사용
                };
                LoginInfo.Name = empInfoList[0].EMPNM;
                HttpClientHelper.TokenId = DeviceTokenInfo.id;
                ExecuteResult executeResult = null;
                try
                {
                    executeResult = dataService.Adapter.TokenAdapter.RegisterStatus(DeviceTokenInfo.id);
                }
                catch (ImateAdapterException imex)
                {
                    if (imex.InnerException is ExecuteResultException)
                    {
                        //Device 등록
                        executeResult = dataService.Adapter.TokenAdapter.RegisterId(DeviceTokenInfo);
                        await Application.Current.MainPage.DisplayAlert("통보", $"{imex.Message}{Environment.NewLine}사용 요청을 하였습니다.", "OK");
                        IsLoading = false;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("오류", imex.Message, "OK");
                        IsLoading = false;
                    }

                    return;
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                    IsLoading = false;
                    return;
                }

                if (!executeResult.isSuccess)
                {
                    //Device 등록
                    await Application.Current.MainPage.DisplayAlert("경고", executeResult.message, "OK");
                    IsLoading = false;
                    return;
                }
                else
                {
                    //isSuccess가 일경우 message
                    // "REQ" : 요청 상태
                    // "ACC" : 승인 상태
                    // "REJ" : 반려 상태
                    switch (executeResult.message)
                    {
                        case "REQ":
                            await Application.Current.MainPage.DisplayAlert("통보", "사용 승인 대기 중입니다.", "OK");
                            IsLoading = false;
                            break;

                        case "ACC":
                            try
                            {
                                if (this.isSaveNumber)
                                {
                                    Preferences.Remove(Constants.SavePhoneNumberKey);
                                    Preferences.Set(Constants.SavePhoneNumberKey, LoginInfo.PhoneNumber);
                                    Preferences.Remove(Constants.SaveLoginNameKey);
                                    Preferences.Set(Constants.SaveLoginNameKey, LoginInfo.Name);
                                }

                            }
                            catch (Exception ex)
                            {
                                await Application.Current.MainPage.DisplayAlert("에러", ex.Message, "OK");
                                IsLoading = false;
                                return;
                            }
                            finally
                            {
                                this.IsBusy = false;
                            }
                            await App.Current.MainPage.Navigation.PushAsync(new HomePage(LoginInfo));
                            IsLoading = false;
                            //await Shell.Current.GoToAsync("//App/" + menu);
                            break;

                        case "REJ":
                            await Application.Current.MainPage.DisplayAlert("통보", "사용 요청이 거부되었습니다.", "OK");
                            IsLoading = false;
                            break;

                        default:
                            await Application.Current.MainPage.DisplayAlert("통보", $"사용 요청이 거부되었습니다. ({executeResult.message})", "OK");
                            IsLoading = false;
                            break;
                    }
                }


                //App.Current.MainPage = new HomePage();
                //await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("오류", ex.Message, "OK");
                IsLoading = false;
            }
        }

        #endregion

        #region OnMemberJoin
        private async void OnMemberJoin()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new LockClosePage("a", "a", "a", "a"));

        }
        #endregion


    }
}