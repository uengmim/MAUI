using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using XNSC.DD.EX;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using XNSC.Net.Ttlock;
using SmartLock.TT;
using SmartLock.TT.Common;
using WorkerScreen.Views.CrushingWorker;
using XNSC;

namespace WorkerScreen.ViewModel.CrushingWorker
{
    /// <summary>
    /// 자물쇠 해제 화면입니다.
    /// </summary>
    public class CrushingDetailViewModel : INotifyPropertyChanged
    {

        #region Properties
        /// <summary>
        /// 모델리스트 지역 변수 선언
        /// </summary>
        private SierepModelList sierepmodelList;

        private SiehisModelList siehismodelList;

        private LkmstModelList lkMstmodelList;
        /// <summary>
        /// 자물쇠 정보의 모델입니다.
        /// </summary>
        private ObservableCollection<CrushingInfo> crushinginfoModel = new ObservableCollection<CrushingInfo>();
        public ObservableCollection<CrushingInfo> CrushingInfoModel { get { return crushinginfoModel; } set { crushinginfoModel = value; OnPropertyChanged(nameof(CrushingInfoModel)); } }

        private CrushingInfo selectedItem;
        /// <summary>
        /// 자물쇠 선택아이템입니다.
        /// </summary>
        public CrushingInfo SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));

                    if (SelectedItem != null)
                    {
                        SieRepInsert(SelectedItem.Name, SelectedItem.BoxName, SelectedItem.Location, SelectedItem.LockData, (DateTime)SelectedItem.PickupDate, (DateTime?)SelectedItem.LockDate, SelectedItem.ConfNo);
                    }
                }
            }
        }

        /// <summary>
        /// TTLock API Helper
        /// </summary>
        private TTlockHelper ttlockHelper;

        /// <summary>
        /// 자물쇠 검색 이벤트입니다.
        /// </summary>
        public string TextSearch
        {
            get => _textSearch;
            set
            {
                _textSearch = value;
                OnPropertyChanged(nameof(TextSearch));
                if (_textSearch.Length > 0)
                {
                    OnSearchContactCommand();
                    SelectedItem = null;
                }
                else
                {
                    SearchEmptyLoadContactCommand.Execute(null);
                    SelectedItem = null;
                    //Task.Run(async () => await ExecuteMyCommand());
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _boxName = "";
        private string _location = "";
        private string _lockData = "";
        private string _textSearch = "";
        private DateTime _pickupDate;
        private DateTime _lockDate;
        private string _hisConfno = "";
        private string _lsn = "";
        private string _ilsId = "";
        public string Name { get; set; }
        //자물쇠 상태 색입니다.
        public Color BackgroundColorSet
        {
            get { return backgroundColorSet; }
            set
            {
                if (backgroundColorSet != value)
                {
                    backgroundColorSet = value;
                    OnPropertyChanged(nameof(BackgroundColorSet));
                }
            }
        }
        private Color backgroundColorSet;
        /// <summary>
        /// 로딩패널입니다.
        /// </summary>
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
        private bool isLoading;
        /// <summary>
        /// 로그인한 사용자의 정보입니다.
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
        /// <summary>
        /// 파쇄 정보 모델입니다.
        /// </summary>
        public CrushingInfo CrushingInfo
        {
            get { return crushinginfo; }
            set
            {
                crushinginfo = value;
                OnPropertyChanged(nameof(CrushingInfo));
            }
        }
        private CrushingInfo crushinginfo;

        public string BoxName
        {
            get => _boxName;
            set
            {
                _boxName = value;
                OnPropertyChanged(nameof(BoxName));
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
        public string HisConfno
        {
            get => _hisConfno;
            set
            {
                _hisConfno = value;
                OnPropertyChanged(nameof(HisConfno));
            }
        }
        public string LSN
        {
            get => _lsn;
            set
            {
                _lsn = value;
                OnPropertyChanged(nameof(LSN));
            }
        }
        public string ILSID
        {
            get => _ilsId;
            set
            {
                _ilsId = value;
                OnPropertyChanged(nameof(ILSID));
            }
        }
        public DateTime PickupDate
        {
            get => _pickupDate;
            set
            {
                _pickupDate = value;
                OnPropertyChanged(nameof(PickupDate));
            }
        }
        public DateTime LockDate
        {
            get => _lockDate;
            set
            {
                _lockDate = value;
                OnPropertyChanged(nameof(LockDate));
            }
        }
        /// <summary>
        /// 번호 저장
        /// </summary>
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
        /// <summary>
        /// 데이터모델
        /// </summary>
        public SiehisModelList siehisData { get; set; } = new SiehisModelList();
        public SierepModelList sierepData { get; set; } = new SierepModelList();
        #endregion

        #region Commands
        /// <summary>
        /// 자물쇠 검색 Commmand입니다.
        /// </summary>
        public ICommand SearchEmptyLoadContactCommand { get; private set; }
        #endregion

        #region 자물쇠 선택 이벤트
        /// <summary>
        /// 자물쇠 선택 이벤트입니다.
        /// </summary>
        /// <param name="Name">작업자 이름</param>
        /// <param name="BoxName">보안 문서함 이름</param>
        /// <param name="Location">보안 문서함 위치</param>
        /// <param name="LockData">자물쇠 정보</param>
        /// <param name="PickupDate">수거 일시</param>
        /// <param name="LockDate">봉인 일시</param>
        /// <param name="ConfNo">작업 번호</param>
        public async void SieRepInsert(string Name, string BoxName, string Location, string LockData, DateTime PickupDate, DateTime? LockDate, string ConfNo)
        {
            try
            {
                IsLoading = true;
                ttlockHelper.StopLockDeviceScan();

                DateTime currentDate = DateTime.Now;

                siehismodelList = new SiehisModelList();
                sierepmodelList = new SierepModelList();
                lkMstmodelList = new LkmstModelList();

                if (SelectedItem != null)
                {
                    //SIEREP 조회
                    var whereSierepCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                    {new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = ConfNo, condition = DIMWhereCondition.Equal}}
                    };

                    sierepData = await ImateHelper.SelectModelData<SierepModelList>(App.ServerID,  whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                    //SIEREP상세 조회
                    var repSearchData = sierepData.FirstOrDefault(h => h.REPTYP == "C08000D");
                    var repData = sierepData.FirstOrDefault(h => h.REPTYP == "C08000C");

                    //C08000D가 없다면
                    if (repSearchData == null)
                    {
                        var whereCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
{
                    new DIMWhereFieldCondition{ fieldName = "PIN" , value = this.LoginInfo.PhoneNumber, condition = DIMWhereCondition.Equal}
}
                        };
                        var empInfoList = await ImateHelper.SelectModelData<EmpmstModelList>(App.ServerID, whereCondition, new Dictionary<string, XNSC.DIMSortOrder>());
                        var whereLKCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "LSN" , value = LockData, condition = DIMWhereCondition.Equal}}
                        };
                        var lkMstData = await ImateHelper.SelectModelData<LkmstModelList>(App.ServerID, whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                        var lkMstSearchData = lkMstData.FirstOrDefault(h => h.LSN == LockData);

                        lkMstSearchData.LKSTA = "U";
                        lkMstSearchData.ModelStatus = DIMModelStatus.Modify;
                        lkMstmodelList.Add(lkMstSearchData);

                        repData.REPTYP = "C08000D";
                        repData.REPDAT = currentDate;
                        repData.CRTUSR = empInfoList[0].EMPNO;
                        repData.CRTDT = currentDate;
                        repData.ModelStatus = DIMModelStatus.Add;
                        sierepmodelList.Add(repData);

                        //SIEHIS상세 조회
                        var hisData = siehisData.FirstOrDefault(h => h.CONFNO == ConfNo);

                        hisData.CSTATUS = "U";
                        hisData.REFDA2 = Location;
                        hisData.ModelStatus = DIMModelStatus.Modify;
                        siehismodelList.Add(hisData);
                        var result = ImateHelper.GetSingleTone().Ttlock.LockEKey(new TtlockInfo() { lockId = LockData, lockMacAddr = lkMstSearchData.MAC, lockData = "" });

                        var lockData = result.lockData;

                        ttlockHelper.UnlockAction(new LockDevice { Address = lkMstSearchData.MAC }, lockData);
                        Console.WriteLine("Lock Unlock");
                    }

                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new CrushingCameraPage(Name, BoxName, Location, LockData, PickupDate, LockDate, ConfNo));
                        IsLoading = false;
                    }
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
        /// 성공 시 실행되는 이벤트입니다.
        /// </summary>
        public async void UnlockSuccess(int battery)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                var dataService = ImateHelper.GetSingleTone();
                var lkmstResult = await ImateHelper.ModifyModelData<LkmstModelList>(App.ServerID, lkMstmodelList);
                var sierepResult = await ImateHelper.ModifyModelData<SierepModelList>(App.ServerID, sierepmodelList);
                var siehisResult = await ImateHelper.ModifyModelData<SiehisModelList>(App.ServerID, siehismodelList);
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
                    lockSN = siehismodelList[0].LSN,
                    ilsId = siehismodelList[0].ILSID,
                    eventTime = currentDate,
                    userId = LoginInfo.PhoneNumber,
                    ConfirmNo = siehismodelList[0].CONFNO,
                    ReportType = "C08000D",
                    lockStatus = "U",
                    Longitude = location.Longitude,
                    Latitude = location.Latitude,
                    opeationMode = "ON",
                    connectTime = currentDate,
                    batteryVolt = battery,
                });
                LockDate = currentDate;

                var sierepREPTYPA = sierepData.FirstOrDefault(h => h.REPTYP == "C08000A");

                await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리 번호 [{siehismodelList[0].LSN}] 의 봉인을 해제 하였습니다.", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new CrushingCameraPage(siehismodelList[0].REFDA1, siehismodelList[0].ILSID, siehismodelList[0].REFDA2, siehismodelList[0].LSN, (DateTime)siehismodelList[0].REFDT1, LockDate, siehismodelList[0].CONFNO));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                SelectedItem = null;
                IsLoading = false;
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

        /// <summary>
        /// 작업 진행 중인 자물쇠가 있을 때 실행되는 이벤트입니다.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteMyCommand()
        {
            CrushingInfoModel.Clear();
            SelectedItem = null;
            IsLoading = true;
            try
            {
                CrushingInfoModel = new ObservableCollection<CrushingInfo>();

                var dataService = ImateHelper.GetSingleTone();
                //SIEHIS 조회
                var whereSiehisCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.OR,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                        new DIMWhereFieldCondition{ fieldName = "CSTATUS" , value = "Q", condition = DIMWhereCondition.Equal},
                        new DIMWhereFieldCondition{ fieldName = "CSTATUS" , value = "U", condition = DIMWhereCondition.Equal}
                    }
                };
                siehisData = await ImateHelper.SelectModelData<SiehisModelList>(App.ServerID, whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                if (siehisData.Count > 0)
                {
                    foreach (var item in siehisData)
                    {
                        //자물쇠 마스터
                        var whereLKCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "LSN" , value = item.LSN, condition = DIMWhereCondition.Equal}}
                        };
                        var lkMstData = await ImateHelper.SelectModelData<LkmstModelList>(App.ServerID, whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                        //문서함 번호
                        var ilsWhereCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "ILSID" , value = item.ILSID, condition = DIMWhereCondition.Equal}}
                        };
                        var IlsMasterData = await ImateHelper.SelectModelData<IlsmstModelList>(App.ServerID, ilsWhereCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                        //자물쇠 위치
                        var whereIlsCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "AREA" , value = IlsMasterData[0].AREA, condition = DIMWhereCondition.Equal}}
                        };
                        var areaData = await ImateHelper.SelectModelData<AreamstModelList>(App.ServerID, whereIlsCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                        //SIEREP 조회
                        var whereSierepCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                        {new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = item.CONFNO, condition = DIMWhereCondition.Equal}}
                        };

                        sierepData = await ImateHelper.SelectModelData<SierepModelList>(App.ServerID, whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                        var sierepREPTYPA = sierepData.FirstOrDefault(h => h.REPTYP == "C08000A");
                        var sierepREPTYPD = sierepData.FirstOrDefault(h => h.REPTYP == "C08000D");
                        DateTime? datData;
                        if (sierepREPTYPD == null)
                        {
                            datData = null;
                            BackgroundColorSet = Colors.Red;
                        }
                        else
                        {
                            datData = sierepREPTYPD.REPDAT;
                            BackgroundColorSet = Colors.Green;

                        }
                        CrushingInfoModel.Add(new CrushingInfo(item.REFDA1, item.ILSID, areaData[0].AREANM, item.LSN, sierepREPTYPA.REPDAT, datData, item.CONFNO, BackgroundColorSet));
                        IsLoading = false;
                    }
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("오류", "검색된 자물쇠가 없습니다.", "OK");
                    IsLoading = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                IsLoading = false;
                return;
            }
        }
        /// <summary>
        /// 자물쇠 검색창 이벤트입니다.
        /// </summary>
        private void OnSearchContactCommand()
        {
            var foundContacts = CrushingInfoModel.Where(found =>
            found.LockData.StartsWith(TextSearch)).ToList();

            if (foundContacts.Count > 0)
            {
                IsLoading = true;
                CrushingInfoModel.Clear();
                foreach (var contact in foundContacts)
                {
                    CrushingInfoModel.Add(contact);
                }
                IsLoading = false;
            }
            else
            {
                IsLoading = true;
                CrushingInfoModel.Clear();
                IsLoading = false;
            }
        }

        #endregion

        #region CrushingDetailViewModel
        public CrushingDetailViewModel()
        {
            CrushingInfoModel.Clear();
            SearchEmptyLoadContactCommand = new Command(async () => await ExecuteMyCommand());
            this.LoginInfo = new LoginInfo();

            //TTLock
            ttlockHelper = new TTlockHelper();
            //Control Lock Event
            ttlockHelper.LockControlActionEvent += TlockHelper_LockControlActionEvent;

            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                this.IsSaveNumber = true;
                this.LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                this.LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");
                this.LoginInfo.EMPROLE = Preferences.Get(Constants.SaveLoginRoleKey, "");

            }
        }
        #endregion
    }
}
