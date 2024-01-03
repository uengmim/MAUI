using ClientScreen.Model;
using ClientScreen.Views;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using XNSC.DD.EX;
using Command = Microsoft.Maui.Controls.Command;


namespace ClientScreen.ViewModels
{
    public class ClientLoginModel : INotifyPropertyChanged
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
        public ClientLoginInfo ClientLoginInfo
        {
            get { return clientloginInfo; }
            set
            {
                clientloginInfo = value;
                OnPropertyChanged();
            }
        }
        private ClientLoginInfo clientloginInfo;

        public ClientLoginModel()
        {
            this.ClientLoginInfo = new ClientLoginInfo();
        }

        /// <summary>
        /// 로그인 Command
        /// </summary>
        public ICommand CliLoginCommand => new Command(OnCliLoginClicked);

        //private object loadingObj = new object();

        public async void OnCliLoginClicked()
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
                if (ClientLoginInfo.ClientID == null || ClientLoginInfo.ClientID == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "ID를 입력해주세요.", "확인");
                    IsLoading = false;
                    return;
                }

                if (ClientLoginInfo.ClientPW == null || ClientLoginInfo.ClientPW == "")
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
                    new DIMWhereFieldCondition{ fieldName = "EMPNO" , value = this.clientloginInfo.ClientID, condition = DIMWhereCondition.Equal},
                    new DIMWhereFieldCondition{ fieldName = "PIN" , value = this.clientloginInfo.ClientPW, condition = DIMWhereCondition.Equal}
                    }
                };

                var cliInfoList = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList",
                        whereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                if (cliInfoList.Count == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "등록되지않은 아이디 또는 비밀번호 입니다.", "확인");
                    IsLoading = false;
                    return;
                }

                await App.Current.MainPage.Navigation.PushAsync(new ClientMainPage(cliInfoList[0].DEPTID, cliInfoList[0].EMPNO));
                IsLoading = false;
            }

            //    var whereDeptCondition = new DIMGroupFieldCondtion()
            //    {
            //        condition = DIMGroupCondtion.AND,
            //        joinCondtion = DIMGroupCondtion.AND,
            //        whereFieldConditions = new DIMWhereFieldCondition[]
            //        {
            //        new DIMWhereFieldCondition{ fieldName = "DEPTID" , value = cliInfoList[0].DEPTID, condition = DIMWhereCondition.Equal}
            //        }
            //    };

            //    var clideptInfoList = await dataService.Adapter.SelectModelDataAsync<DeptmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.DeptmstModelList",
            //                whereDeptCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

            //    var DeviceTokenInfo = new TokenIdInfo()
            //    {
            //        id = deviceId, //Device 고유 ID, Mac Address, IMIE등 고유 삭별 번호
            //        userid = cliInfoList[0].PIN, //사용자 ID
            //        name = cliInfoList[0].EMPNM, //사용자 또는 기기의 이름
            //        role = "Client",  //기본값(USR)
            //        email = "",  // Email 없을 경우 "id@iaccount.com"으로 설정
            //        deptname = clideptInfoList[0].DEPTNM ?? "", //부서명
            //        empid = cliInfoList[0].EMPNO, //사원ID
            //        deptcode = cliInfoList[0].DEPTID, //부서코드
            //        appname = "SHREDOC", //앱 이름
            //        culture = string.Empty //문화권(기본이면 빈 문자열) 메일 본문 Template 구분하는데 사용
            //    };

            //    ClientLoginInfo.ClientName = cliInfoList[0].EMPNM;
            //    ClientLoginInfo.EMPNO = cliInfoList[0].EMPNO;
            //    ClientLoginInfo.ClientID = cliInfoList[0].DEPTID;
            //    ClientLoginInfo.ClientPW = cliInfoList[0].PIN;

            //}
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                IsLoading = false;
                return;
            }
        }
    }
}