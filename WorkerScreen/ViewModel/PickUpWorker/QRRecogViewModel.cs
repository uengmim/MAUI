using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.Views;
using System.Windows.Input;
using ShreDoc.ProxyModel;
using XNSC.DD.EX;
using System.Collections.ObjectModel;
using System.Data;
using XNSC;
using XNSC.Net.Ttlock;
using ShreDoc.Utils;
using SmartLock.TT;
using SmartLock.TT.Common;
using System.ComponentModel;
using WorkerScreen.Views.PickUpWorker;

namespace WorkerScreen.ViewModel.PickUpWorker
{
    /// <summary>
    /// 자물쇠 인식 화면입니다.
    /// </summary>
    public class QRRecogViewModel : INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// TTLock API Helper
        /// </summary>
        private TTlockHelper ttlockHelper;

        /// <summary>
        /// 자물쇠 정보의 모델입니다.
        /// </summary>
        private ObservableCollection<LockInfom> lockDataModel = new ObservableCollection<LockInfom>();
        public ObservableCollection<LockInfom> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        private ObservableCollection<LockInfomation> lockInfoModel = new ObservableCollection<LockInfomation>();
        public ObservableCollection<LockInfomation> LockInfoModel { get { return lockInfoModel; } set { lockInfoModel = value; OnPropertyChanged(nameof(LockInfoModel)); } }

        /// <summary>
        /// 모델리스트 지역 변수 선언
        /// </summary>
        private SireqModelList sireqmodelList;

        private SilockModelList silockmodelList;

        private SiehisModelList siehismodelList;

        private LkmstModelList lkMstmodelList;

        /// <summary>
        /// 로그인한 사용자 정보입니다.
        /// </summary>
        private string _boxNum = "";
        private string _location = "";
        private string _lockData = "";
        private string _lockName = "";
        private string _confno = "";
        private string _deptID = "";
        private string _area = "";
        private string _empName = "";
        private string _hisConfno = "";

        public LockInfom LockInfom
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfom));
            }
        }
        private LockInfom lockinfo;


        /// <summary>
        /// 자물쇠 클릭 이벤트입니다.
        /// </summary>
        public LockInfomation SelectedItem
        {
            get { return selectedItem; }
            set
            {
                try
                {
                    if (selectedItem != value)
                    {
                        selectedItem = value;
                        OnPropertyChanged(nameof(SelectedItem));


                        OnSelectionChanged(value);
                        if (SelectedItem != null)
                        {
                            PerformNavigation(SelectedItem.Lockid, SelectedItem.LockName, SelectedItem.LockMac);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                    return;
                }
            }
        }
        private LockInfomation selectedItem;

        public string Name => LoginInfo.Name;

        /// <summary>
        /// 로딩 패널입니다.
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



        public string BoxNum
        {
            get => _boxNum;
            set
            {
                _boxNum = value;
                OnPropertyChanged(nameof(BoxNum));
            }
        }
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        public string LockData
        {
            get => _lockData;
            set
            {
                _lockData = value;
                OnPropertyChanged(nameof(LockData));
            }
        }
        public string LockName
        {
            get => _lockName;
            set
            {
                _lockName = value;
                OnPropertyChanged(nameof(LockName));
            }
        }
        public string CONFNO
        {
            get => _confno;
            set
            {
                _confno = value;
                OnPropertyChanged(nameof(CONFNO));
            }
        }
        public string DEPTID
        {
            get => _deptID;
            set
            {
                _deptID = value;
                OnPropertyChanged(nameof(DEPTID));
            }
        }
        public string AREA
        {
            get => _area;
            set
            {
                _area = value;
                OnPropertyChanged(nameof(AREA));
            }
        }
        public string EMPName
        {
            get => _empName;
            set
            {
                _empName = value;
                OnPropertyChanged(nameof(EMPName));
            }
        }
        public string HisConfno
        {
            get => _hisConfno;
            set
            {
                _hisConfno = value;
                OnPropertyChanged(nameof(HisConfno));
            }
        }

        public bool IsSaveNumber
        {
            get { return isSaveNumber; }
            set
            {
                isSaveNumber = value;

                if (!value)
                {
                    Preferences.Remove(Constants.SavePhoneNumberKey);
                }
                OnPropertyChanged(nameof(IsSaveNumber));
            }
        }

        private bool isSaveNumber = false;
        public string EMPNO { get; set; }
        public LkmstModelList lkMstData { get; set; } = new LkmstModelList();

        #endregion

        #region Commands
        /// <summary>
        /// 자물쇠 클릭 Command입니다.
        /// </summary>
        public ICommand SelectionChangedCommand { get; set; }

        private void OnSelectionChanged(LockInfomation selectedItem)
        {
            if (selectedItem != null)
            {
                if (selectedItem.Worker != DEPTID)
                {
                    Application.Current.MainPage.DisplayAlert("오류", $"{EMPName} 작업자로 지정되지 않은 자물쇠입니다.", "OK");
                    SelectedItem = null;
                    return;
                }
            }
        }

        /// <summary>
        /// 이미 해제된 자물쇠가 있을경우 이벤트입니다.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteMyCommand()
        {
            try
            {
                IsLoading = true;
                var dataService = ImateHelper.GetSingleTone();
                //자물쇠 마스터
                var whereLKCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                        new DIMWhereFieldCondition{ fieldName = "LKSTA" , value = "U", condition = DIMWhereCondition.Equal}
                    }
                };

                var lkMstData = await ImateHelper.SelectModelData<LkmstModelList>(App.ServerID, whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                for (int i = 0; i < lkMstData.Count; i++)
                {
                    //SIEHIS 조회
                    var whereSiehisCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
    {
                    new DIMWhereFieldCondition{ fieldName = "LSN" , value = lkMstData[i].LSN, condition = DIMWhereCondition.Equal},
                    new DIMWhereFieldCondition{ fieldName = "CSTATUS" , value = "LP", condition = DIMWhereCondition.Equal},
                    new DIMWhereFieldCondition{ fieldName = "REFDA1" , value = LoginInfo.Name, condition = DIMWhereCondition.Equal},
                    new DIMWhereFieldCondition{ fieldName = "ILSID" , value = BoxNum, condition = DIMWhereCondition.Equal}
    }
                    };
                    var siehisData = await ImateHelper.SelectModelData<SiehisModelList>(App.ServerID, whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                    if (siehisData.Count > 0)
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new LockClosePage(BoxNum, Location, siehisData[0].LSN, lkMstData[0].LKNM, siehisData[0].CONFNO));
                        IsLoading = false;
                        break;
                    }
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                IsLoading = false;
                return;
            }
        }


        /// <summary>
        /// 자물쇠 클릭 이벤트입니다.
        /// </summary>
        /// <param name="Data">자물쇠 ID</param>
        /// <param name="Name">자물쇠 이름</param>
        /// <param name="LockMac">자물쇠 맥 주소</param>
        private async void PerformNavigation(string Data, string Name, string LockMac)
        {
            try
            {
                IsLoading = true;
                ttlockHelper.StopLockDeviceScan();
                DateTime currentDate = DateTime.Now;
                string formattedDate = currentDate.ToString("yyyyMMdd");
                sireqmodelList = new SireqModelList();
                silockmodelList = new SilockModelList();
                siehismodelList = new SiehisModelList();
                lkMstmodelList = new LkmstModelList();
                var dataService = ImateHelper.GetSingleTone();

                if (SelectedItem != null)
                {

                    //스토어드 프로시저
                    var queryParams = new QueryParameter[]
                    { new QueryParameter(){name = "prefix", dataType = QueryDataType.String, value= formattedDate } };

                    var queryMsg = new QueryMessage()
                    {
                        queryMethod = QueryRunMethod.Alone,
                        queryName = "getDocSeq",
                        dataSource = App.ServerID, //<--- 프리페어런스의 값으로 변경하여야 함
                        queryTemplate = "SELECT `getDocSeq`('sireq', @prefix) AS seq",
                        parameters = queryParams,
                        cacheType = QueryCacheType.None
                    };
                    var queryData = await ImateHelper.GetSingleTone().Adapter.DbSelectToDataSetAsync(new List<QueryMessage>(new QueryMessage[] { queryMsg }));
                    var seq = queryData.Tables[0].Rows[0].Field<decimal>("seq");
                    var seqData = seq.ToString().PadLeft(5, '0');

                    string guidStr = Guid.NewGuid().ToString();

                    //SIREQ
                    sireqmodelList.Add(new SireqModel()
                    {
                        EREQID = formattedDate + seqData,
                        EREQTYP = "C07000C",
                        WDEPTID = DEPTID,
                        WEMPID = EMPNO,
                        EREQDT = currentDate,
                        ModelStatus = DIMModelStatus.Add
                    });

                    //SILOCK
                    silockmodelList.Add(new SilockModel()
                    {
                        EREQID = sireqmodelList[0].EREQID,
                        LSN = Data,
                        AREA = AREA,
                        ILSID = BoxNum,
                        ASTATUS = "A",
                        CONFNO = guidStr,
                        ModelStatus = DIMModelStatus.Add
                    });

                    //SIEHIS
                    siehismodelList.Add(new SiehisModel()
                    {
                        CONFNO = guidStr,
                        EVTDT = currentDate,
                        EREQID = sireqmodelList[0].EREQID,
                        LSN = Data,
                        ILSID = BoxNum,
                        ASTATUS = "A",
                        CSTATUS = "LP",
                        REFDA1 = EMPName,
                        REFDA2 = Location,
                        REFDA3 = DEPTID,
                        ModelStatus = DIMModelStatus.Add
                    }); ;

                    var whereLKCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {new DIMWhereFieldCondition{ fieldName = "LSN" , value = Data, condition = DIMWhereCondition.Equal}}
                    };
                    var lkMstData = await ImateHelper.SelectModelData<LkmstModelList>(App.ServerID, whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                    var lkMstSearchData = lkMstData.FirstOrDefault(h => h.LSN == Data);

                    lkMstSearchData.LKSTA = "U";
                    lkMstSearchData.ILSID = BoxNum;
                    lkMstSearchData.ModelStatus = DIMModelStatus.Modify;
                    lkMstmodelList.Add(lkMstSearchData);


                    var result = dataService.Ttlock.LockEKey(new TtlockInfo() { lockId = Data, lockMacAddr = LockMac, lockData = "" });

                    var lockData = result.lockData;

                    ttlockHelper.UnlockAction(new LockDevice { Address = LockMac }, lockData);
                    Console.WriteLine("Lock Unlock");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("오류", "선택된 자물쇠가 없습니다.", "OK");
                    SelectedItem = null;
                    IsLoading = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                SelectedItem = null;
                IsLoading = false;
                return;
            }
        }

        /// <summary>
        /// 자물쇠 해제 성공 시 이벤트입니다.
        /// </summary>
        public async void UnlockSuccess(int battery)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                var dataService = ImateHelper.GetSingleTone();
                var sireqResult = await ImateHelper.ModifyModelData<SireqModelList>(App.ServerID, sireqmodelList);
                var silockResult = await ImateHelper.ModifyModelData<SilockModelList>(App.ServerID, silockmodelList);
                var siehisResult = await ImateHelper.ModifyModelData<SiehisModelList>(App.ServerID, siehismodelList);
                var lkmstResult = await ImateHelper.ModifyModelData<LkmstModelList>(App.ServerID, lkMstmodelList);
                //TTLOCK
                string ttGuidStr = Guid.NewGuid().ToString();
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                    {
                        DesiredAccuracy = GeolocationAccuracy.High,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                dataService.Ttlock.ActiveLog(new XNSC.Net.NOKE.LockActivity()
                {
                    trackingKey = ttGuidStr,
                    lockSN = lkMstmodelList[0].LSN,
                    ilsId = lkMstmodelList[0].ILSID,
                    eventTime = currentDate,
                    userId = LoginInfo.PhoneNumber,
                    ConfirmNo = siehismodelList[0].CONFNO,
                    ReportType = "READY",
                    lockStatus = "U",
                    Longitude = location.Longitude,
                    Latitude = location.Latitude,
                    opeationMode = "ON",
                    connectTime = currentDate,
                    batteryVolt = battery,
                });

                await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리 번호 [{lkMstmodelList[0].LSN}] 의 봉인을 해제 하였습니다.", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new LockClosePage(lkMstmodelList[0].ILSID, siehismodelList[0].REFDA2, lkMstmodelList[0].LSN, lkMstmodelList[0].LKNM, siehismodelList[0].CONFNO));


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
            }
            finally
            {
                SelectedItem = null;
                IsLoading = false;
            }

        }
        /// <summary>
        /// 자물쇠 해제 이벤트입니다.
        /// </summary>
        /// <param name="e"></param>
        private async void TlockHelper_LockControlActionEvent(SmartLock.Event.LockControlActionEventArgs e)
        {
            if (!e.IsSuccess)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    SelectedItem = null;
                    IsLoading = false;
                    Application.Current.MainPage.DisplayAlert("오류", e.Error.ErrorMessage, "OK");
                });

                return;
            }

            try
            {
                UnlockSuccess(e.Battery);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                SelectedItem = null;
                IsLoading = false;
            }
            finally
            {
                try
                {
                    SelectedItem = null;
                    IsLoading = false;

                    ttlockHelper.StopLockDeviceScan();
                    ttlockHelper.StopBluetoothService();
                }
                catch (Exception ie)
                {
                    Console.WriteLine(ie.ToString());
                }

            }
        }

        #endregion
        #region QRRecogPage
        public QRRecogViewModel()
        {
            //TTLock
            ttlockHelper = new TTlockHelper();
            //Control Lock Event
            ttlockHelper.LockControlActionEvent += TlockHelper_LockControlActionEvent;

            LoginInfo = new LoginInfo();

            SelectionChangedCommand = new Command<LockInfomation>(OnSelectionChanged);

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

    }
}