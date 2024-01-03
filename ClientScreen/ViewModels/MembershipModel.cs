using ClientScreen.Model;
using ClientScreen.Views;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using XNSC.DD.EX;
using XNSC.Net;

namespace ClientScreen.ViewModels
{
    public class MembershipModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //public ObservableCollection<MembershipLogInfo> MembershipDataModel { get { return membershipDataModel; } set { membershipDataModel = value; OnPropertyChanged(nameof(MembershipDataModel)); } }

        //public ObservableCollection<MembershipLogInfo> membershipDataModel = new ObservableCollection<MembershipLogInfo>();


        //PropertyChanged 변경
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool isLoading;
        public string EMPNO { get; set; }
        public string DEPTID { get; set; }

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
        private string clientInfo;
        public string ClientInfo
        {
            get { return clientInfo; }
            set
            {
                if (clientInfo != value)
                {
                    clientInfo = value;
                    OnPropertyChanged(nameof(ClientInfo));
                }
            }
        }     
        
        /// <summary>
        /// 현재 로그인한 사용자 정보입니다.
        /// </summary>
        public MembershipLogInfo MembershipLogInfo
        {
            get { return membershiploginfo; }
            set
            {
                membershiploginfo = value;
                OnPropertyChanged();
            }
        }
        private MembershipLogInfo membershiploginfo;

        /// <summary>
        /// 현재 로그인한 사용자 정보입니다.
        /// </summary>
        public ClientLoginInfo ClientLoginInfo
        {
            get { return clientlogininfo; }
            set
            {
                clientlogininfo = value;
                OnPropertyChanged();
            }
        }
        private ClientLoginInfo clientlogininfo;


        public MembershipModel()
        {
            this.MembershipLogInfo = new MembershipLogInfo();
        }


        //회원등록 Command
        public ICommand MembsershipCommand => new Command(OnMembershipAdd);

        public async void OnMembershipAdd()
        {
            try
            {
                IsLoading = true;

                var dataService = ImateHelper.GetSingleTone();

                //전화번호 입력
                //if (this.MembershipLogInfo.CliNumber == null || this.MembershipLogInfo.CliNumber == "")
                //{
                //    await Application.Current.MainPage.DisplayAlert("알림", "전화번호를 입력하세요.", "확인");
                //    IsLoading = false;
                //    return;
                //}

                //사용할 ID입력
                if (this.MembershipLogInfo.CliID == null || this.MembershipLogInfo.CliID == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "사용하실 ID를 입력해주세요.", "확인");
                    IsLoading = false;
                    return;
                }

                //사용할 비밀번호 입력
                if (this.MembershipLogInfo.CliPW == null || this.MembershipLogInfo.CliPW == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "사용하실 비밀번호를 입력해주세요.", "확인");
                    IsLoading = false;
                    return;
                }

                //비밀번호 확인
                if (this.MembershipLogInfo.CliPWVer == null || this.MembershipLogInfo.CliPWVer == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "비밀번호를 다시 확인해주세요.", "확인");
                    IsLoading = false;
                    return;
                }

                //이름
                if (this.MembershipLogInfo.CliName == null || this.MembershipLogInfo.CliName == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "이름을 입력해주세요.", "확인");
                    IsLoading = false;
                    return;
                }

                //부서
                if (this.MembershipLogInfo.CliDep == null || this.MembershipLogInfo.CliDep == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "부서를 입력해주세요.", "확인");
                    IsLoading = false;
                    return;
                }

                //직책
                if (this.MembershipLogInfo.CliPos == null || this.MembershipLogInfo.CliPos == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "직책을 입력해주세요.", "확인");
                    IsLoading = false;
                    return;
                }

                //사용할 E-mail
                if (this.MembershipLogInfo.CliEmail == null || this.MembershipLogInfo.CliEmail == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "E-Mail을 입력해주세요.", "확인");
                    IsLoading = false;
                    return;
                }

                var deviceId = Preferences.Get("shreDoc_deviceId", string.Empty);
                if (string.IsNullOrWhiteSpace(deviceId))
                {
                    deviceId = System.Guid.NewGuid().ToString();
                    Preferences.Set("shreDoc_deviceId", deviceId);
                }

                var whereCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                    new DIMWhereFieldCondition{ fieldName = "PIN" , value = this.MembershipLogInfo.CliPW, condition = DIMWhereCondition.Equal},
                    new DIMWhereFieldCondition{ fieldName = "EMPNO" , value = this.MembershipLogInfo.CliID, condition = DIMWhereCondition.Equal}
                    }
                };

                var memInfoList = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList",
                        whereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                //if (memInfoList.Count != 0)
                //{
                //    await Application.Current.MainPage.DisplayAlert("통보", "이미 사용중인 아이디 혹은 비밀번호입니다.", "OK");
                //    IsLoading = false;
                //    return;
                //}

                var whereDeptCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                    new DIMWhereFieldCondition{ fieldName = "DEPTID" , value = memInfoList[0].DEPTID, condition = DIMWhereCondition.Equal},
                    }
                };

                var medeptInfoList = await dataService.Adapter.SelectModelDataAsync<DeptmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.DeptmstModelList",
                            whereDeptCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                //UseDeviceCheck가 True일 경우
                var DeviceTokenInfo = new TokenIdInfo()
                {
                    id = deviceId, //Device 고유 ID, Mac Address, IMIE등 고유 삭별 번호
                    userid = memInfoList[0].PIN, //사용자 ID
                    name = memInfoList[0].EMPNM, //사용자 또는 기기의 이름
                    role = "WORK",  //기본값(USR)
                    email = "",  // Email 없을 경우 "id@iaccount.com"으로 설정
                    deptname = medeptInfoList[0].DEPTNM ?? "", //부서명
                    empid = memInfoList[0].EMPNO, //사원ID
                    deptcode = memInfoList[0].DEPTID, //부서코드
                    appname = "SHREDOC", //앱 이름
                    culture = string.Empty //문화권(기본이면 빈 문자열) 메일 본문 Template 구분하는데 사용
                };

                MembershipLogInfo.CliID = memInfoList[0].EMPNO;
                MembershipLogInfo.CliPW = memInfoList[0].PIN;
                MembershipLogInfo.CliName = memInfoList[0].EMPNM;
                MembershipLogInfo.CliDep = memInfoList[0].DEPTID;
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
                        await Application.Current.MainPage.DisplayAlert("알림", $"{imex.Message}{Environment.NewLine}사용 요청을 하였습니다.", "확인");
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
                                await Application.Current.MainPage.DisplayAlert("알림", "회원가입이 완료되었습니다.", "확인");
                            }
                            catch (Exception ex)
                            {
                                await Application.Current.MainPage.DisplayAlert("에러", ex.Message, "OK");
                                IsLoading = false;
                                return;
                            }
                            await Application.Current.MainPage.Navigation.PushAsync(new ClientMainPage(DEPTID, EMPNO));
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