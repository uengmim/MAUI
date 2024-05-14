using ShreDoc.ProxyModel;
using System.ComponentModel;
using System.Windows.Input;
using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.Views;
using WorkerScreen.Views.Common;
using XNSC.DD.EX;
using XNSC.Net;
using ShreDoc.Utils;

namespace WorkerScreen.ViewModel.Common
{
    /// <summary>
    /// 로그인 화면입니다.
    /// </summary>
    public class LoginPageModel : INotifyPropertyChanged
    {

        #region Properties

        /// <summary>
        /// 로딩 패널
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
        /// 현재 로그인한 사용자 정보입니다.
        /// </summary>
        public LoginInfo LoginInfo
        {
            get { return loginInfo; }
            set
            {
                loginInfo = value;
                OnPropertyChanged(nameof(LoginInfo));
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
                    Preferences.Remove(Constants.SaveLoginRoleKey);
                }

                OnPropertyChanged(nameof(IsSaveNumber));
            }
        }

        private bool isSaveNumber = false;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        /// <summary>
        /// 로그인 Command 입니다.
        /// </summary>
        public ICommand LoginCommand => new Command<string>(p => OnLogin(p));
        #endregion

        #region LoginPageModel
        public LoginPageModel()
        {
            LoginInfo = new LoginInfo();

            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                IsSaveNumber = true;
                LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");
                LoginInfo.EMPROLE = Preferences.Get(Constants.SaveLoginRoleKey, "");
            }

        }
        #endregion

        #region OnLogin
        /// <summary>
        /// 로그인 메뉴를 나타냅니다.
        /// </summary>
        private async void OnLogin(string menu)
        {
            try
            {
                IsLoading = true;

                if (LoginInfo.PhoneNumber == null || LoginInfo.PhoneNumber == "")
                {
                    await Application.Current.MainPage.DisplayAlert("전화번호 확인", "전화번호를 입력하세요", "OK");
                    IsLoading = false;
                    return;
                }
                var deviceId = Preferences.Get("shreDoc_deviceId", string.Empty);
                if (string.IsNullOrWhiteSpace(deviceId))
                {
                    deviceId = Guid.NewGuid().ToString();
                    Preferences.Set("shreDoc_deviceId", deviceId);
                }
                var whereCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                    new DIMWhereFieldCondition{ fieldName = "PIN" , value = LoginInfo.PhoneNumber, condition = DIMWhereCondition.Equal}
                    }
                };

                var empInfoList = await ImateHelper.SelectModelData<EmpmstModelList>(App.ServerID, whereCondition, new Dictionary<string, XNSC.DIMSortOrder>());

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

                var deptInfoList = await ImateHelper.SelectModelData<DeptmstModelList>(App.ServerID, whereDeptCondition, new Dictionary<string, XNSC.DIMSortOrder>());
                var whereEmpRoleCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
    {
                                  new DIMWhereFieldCondition{ fieldName = "EMPNO" , value = empInfoList[0].EMPNO, condition = DIMWhereCondition.Equal}
    }
                };

                var empLoleList = await ImateHelper.SelectModelData<EmproleModelList>(App.ServerID, whereEmpRoleCondition, new Dictionary<string, XNSC.DIMSortOrder>());
                if (empLoleList.Count == 1)
                {
                    //로그인한 작업자 부서가 수거팀일 때
                    if (empLoleList[0].ILSID == "C05000C")
                    {
                        LoginInfo.EMPROLE = "PickUp";

                    }
                    //로그인한 작업자 부서가 파쇄팀일 때
                    else if (empLoleList[0].ILSID == "C05000S")
                    {
                        LoginInfo.EMPROLE = "Crush";
                    }
                }
                else
                {
                    LoginInfo.EMPROLE = "All";
                }
                var dataService = ImateHelper.GetSingleTone();
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
                LoginInfo.EMPNO = empInfoList[0].EMPNO;
                LoginInfo.DEPTID = empInfoList[0].DEPTID;
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
                                if (isSaveNumber)
                                {
                                    Preferences.Remove(Constants.SavePhoneNumberKey);
                                    Preferences.Set(Constants.SavePhoneNumberKey, LoginInfo.PhoneNumber);
                                    Preferences.Remove(Constants.SaveLoginNameKey);
                                    Preferences.Set(Constants.SaveLoginNameKey, LoginInfo.Name);
                                    Preferences.Remove(Constants.SaveLoginRoleKey);
                                    Preferences.Set(Constants.SaveLoginRoleKey, LoginInfo.EMPROLE);
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
                                IsLoading = false;
                            }

                            switch (LoginInfo.EMPROLE)
                            {
                                case "PickUp":
                                    await App.Current.MainPage.Navigation.PushAsync(new PUWorkerHomePage(LoginInfo));
                                    break;
                                case "Crush":
                                    await App.Current.MainPage.Navigation.PushAsync(new CRWorkerHomePage(LoginInfo));
                                    break;
                                case "All":
                                    await App.Current.MainPage.Navigation.PushAsync(new AllWorkerHomePage(LoginInfo));
                                    break;
                                default:
                                    // Handle other cases or throw an exception
                                    throw new InvalidOperationException($"Unknown EMPROLE: {LoginInfo.EMPROLE}");
                            }
                            IsLoading = false;
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
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("오류", ex.Message, "OK");
                IsLoading = false;
            }
        }

        #endregion
    }
}