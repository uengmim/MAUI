using AdminScreen.Common;
using AdminScreen.Model;
using AdminScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ShreDoc.ProxyModel;
using XNSC.DD.EX;
using XNSC.Net;
using XNSC.Net.NOKE;
using System.Collections.ObjectModel;
using AdminScreen.Models;
using ShreDoc.Utils;
using System.Reflection.Emit;
using XNSC;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// 작업 이력 조회 화면
    /// </summary>
    public class HistoryDetailViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// MonitoringData 모델
        /// </summary>
        public ObservableCollection<MonitoringData> Items { get { return Item; } set { Item = value; OnPropertyChanged(nameof(Items)); } }

        /// <summary>
        /// MonitoringData 모델
        /// </summary>
        private ObservableCollection<MonitoringData> Item = new ObservableCollection<MonitoringData>();

        #region Properties
        /// <summary>
        /// 로그인한 사용자의 정보
        /// </summary>
        private string _lsn = "";
        private string _lknm = "";
        private string _mac = "";
        private string _lktyp = "";
        private string _confno = "";


        /// <summary>
        /// isLoading
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
        /// selectedStartDate
        /// </summary>
        private DateTime selectedStartDate;

        public DateTime SelectedStartDate
        {
            get { return selectedStartDate; }
            set
            {
                if (selectedStartDate != value)
                {
                    selectedStartDate = value;
                    OnPropertyChanged(nameof(SelectedStartDate));
                }
            }
        }

        /// <summary>
        /// selectedEndDate
        /// </summary>
        private DateTime selectedEndDate;

        public DateTime SelectedEndDate
        {
            get { return selectedEndDate; }
            set
            {
                if (selectedEndDate != value)
                {
                    selectedEndDate = value;
                    OnPropertyChanged(nameof(SelectedEndDate));
                }
            }
        }

        /// <summary>
        /// Lock Info
        /// </summary>
        public LockInfoData LockInfo
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfo));
            }
        }
        private LockInfoData lockinfo;

        /// <summary>
        /// Lock SN
        /// </summary>
        public string LSN
        {
            get => _lsn;
            set
            {
                _lsn = value;
                OnPropertyChanged(nameof(LSN));
            }
        }

        /// <summary>
        /// Lock 이름
        /// </summary>
        public string LKNM
        {
            get => _lknm;
            set
            {
                _lknm = value;
                OnPropertyChanged(nameof(LKNM));
            }
        }

        /// <summary>
        /// Mac
        /// </summary>
        public string MAC
        {
            get => _mac;
            set
            {
                _mac = value;
                OnPropertyChanged(nameof(MAC));
            }
        }

        /// <summary>
        /// Lock 유형
        /// </summary>
        public string LKTYP
        {
            get => _lktyp;
            set
            {
                _lktyp = value;
                OnPropertyChanged(nameof(LKTYP));
            }
        }

        /// <summary>
        /// Confno
        /// </summary>
        public string CONFNO
        {
            get => _confno;
            set
            {
                _confno = value;
                OnPropertyChanged(nameof(CONFNO));
            }
        }

        private string myimage;
        public string MyImage
        {
            get => myimage;
            set
            {
                myimage = value;
                OnPropertyChanged(nameof(MyImage));
            }
        }

        /// <summary>
        /// 봉인 시간
        /// </summary>
        private DateTime? lockClose = null;
        /// <summary>
        /// 상차 시간
        /// </summary>
        private DateTime? getOn = null;
        /// <summary>
        /// 하차 시간
        /// </summary>
        private DateTime? getOff = null;
        /// <summary>
        /// 봉인 해제 시간
        /// </summary>
        private DateTime? lockoff = null;
        /// <summary>
        /// 파쇄 시간
        /// </summary>
        private DateTime? crushing = null;

        /// <summary>
        /// 작업자 이름
        /// </summary>
        private string LockWorker = "";
        /// <summary>
        /// 파소 수량
        /// </summary>
        private string CrushNum = "";
        /// <summary>
        /// 파쇄 방법
        /// </summary>
        private string CrushWay = "";
        /// <summary>
        /// 봉인 사진
        /// </summary>
        private string LockPicture = "";
        /// <summary>
        /// 파쇄 사진
        /// </summary>
        private string CrushPicture = ""; 
        /// <summary>
        /// 특성 변경 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion

        #region Commands
        /// <summary>
        /// 작업 이력 조회 Commands
        /// </summary>
        public ICommand DateSearchCommand => new Command(DateSearch);
        /// <summary>
        /// 지도 Command입니다.
        /// </summary>
        public ICommand MapCommand => new Command<MonitoringData>(MapShow);
        /// <summary>
        /// 문서 Command입니다.
        /// </summary>
        public ICommand DocCommand => new Command<MonitoringData>(DocShow);

        private bool isNavigating; // 중복 탐색 방지 플래그

        /// <summary>
        /// 지도 페이지 이동
        /// </summary>
        private async void MapShow(MonitoringData item)
        {
            try
            {
                if (isNavigating) return; // 중복 클릭 방지
                isNavigating = true;

                IsLoading = true;
                if (item == null)
                {
                    await ShowCustomAlert("알림", "오류 발생.", "확인", "");
                    return;
                }
                if (item.CONFNO == null || item.CONFNO == "")
                {
                    await ShowCustomAlert("알림", "진행사항이 없습니다.", "확인", "");
                    return;
                }
                await Application.Current.MainPage.Navigation.PushAsync(new HistoryMapPage(item.CONFNO));
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
            finally
            {
                IsLoading = false;
                isNavigating = false;
            }
        }

        /// <summary>
        /// 문서 페이지 이동.
        /// </summary>
        private async void DocShow(MonitoringData TaskData)
        {
            try
            {
                if (isNavigating) return; // 중복 클릭 방지
                isNavigating = true;

                IsLoading = true;
                if (TaskData == null)
                {
                    await ShowCustomAlert("알림", "오류 발생.", "확인", "");
                    return;
                }

                if (TaskData.CONFNO == null || TaskData.CONFNO == "")
                {
                    await ShowCustomAlert("알림", "진행사항이 없습니다.", "확인", "");
                    return;
                }

                await Application.Current.MainPage.Navigation.PushAsync(new TaskMonitoringDocPage(TaskData.LockClose ?? DateTime.MinValue, TaskData.Crushing ?? DateTime.MinValue, TaskData.CONFNO));
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
            finally
            {
                IsLoading = false;
                isNavigating = false;
            }
        }

        /// <summary>
        /// 날짜 조회 이벤트
        /// </summary>
        public async void DateSearch()
        {
            try
            {
                IsLoading = true;

                lockClose = null;
                getOn = null;
                getOff = null;
                lockoff = null;
                crushing = null;
                Items = new ObservableCollection<MonitoringData>();
                var dataService = ImateHelper.GetSingleTone();
                DateTime startDate = new DateTime(SelectedStartDate.Year, SelectedStartDate.Month, SelectedStartDate.Day, 0, 0, 0);
                DateTime endDate = new DateTime(SelectedEndDate.Year, SelectedEndDate.Month, SelectedEndDate.Day, 23, 59, 59);
                var lockStatus = "";
                //자물쇠 마스터
                var whereLKCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                        new DIMWhereFieldCondition{ fieldName = "MAC" , value = MAC, condition = DIMWhereCondition.Equal}
                    }
                };

                var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                            whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                //Siehis 조회
                var SiehisCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                            new DIMWhereFieldCondition{ fieldName = "LSN" , value = lkMstData[0].LSN, condition = DIMWhereCondition.Equal},
                            new DIMWhereFieldCondition{ fieldName = "EVTDT" , value = startDate, condition = DIMWhereCondition.GreaterEqual},
                            new DIMWhereFieldCondition{ fieldName = "EVTDT" , value = endDate, condition = DIMWhereCondition.LessEqual},
                    }
                };
                Dictionary<string, DIMSortOrder> sorts = new Dictionary<string, DIMSortOrder>();
                sorts.Add("EVTDT", DIMSortOrder.Descending);
                var siehisDatas = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                                SiehisCondition, sorts, QueryCacheType.None);
                if (siehisDatas.Count == 0)
                {
                    await ShowCustomAlert("알림", "조회하신 날짜에 작업이 없습니다.", "확인", "");
                    IsLoading = false;
                    return;
                }
                foreach (var item in siehisDatas)
                {
                    //문서함 번호
                    var ilsWhereCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {
                            new DIMWhereFieldCondition{ fieldName = "ILSID" , value = item.ILSID, condition = DIMWhereCondition.Equal}
                        }
                    };
                    var IlsMasterData = await dataService.Adapter.SelectModelDataAsync<IlsmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.IlsmstModelList",
                                ilsWhereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                    //자물쇠 위치
                    var whereIlsCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {
                            new DIMWhereFieldCondition{ fieldName = "AREA" , value = IlsMasterData[0].AREA, condition = DIMWhereCondition.Equal}
                        }
                    };

                    var areaData = await dataService.Adapter.SelectModelDataAsync<AreamstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.AreamstModelList",
                                whereIlsCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                    //SIEREP 조회
                    var whereSierepCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {
                            new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = item.CONFNO, condition = DIMWhereCondition.Equal}
                        }
                    };
                    var sierepData = await dataService.Adapter.SelectModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList",
                                whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                    LockWorker = item.REFDA1;

                    var sierepREPTYPA = sierepData.FirstOrDefault(h => h.REPTYP == "C08000A");
                    var sierepREPTYPB = sierepData.FirstOrDefault(h => h.REPTYP == "C08000B");
                    var sierepREPTYPC = sierepData.FirstOrDefault(h => h.REPTYP == "C08000C");
                    var sierepREPTYPD = sierepData.FirstOrDefault(h => h.REPTYP == "C08000D");
                    var sierepREPTYPE = sierepData.FirstOrDefault(h => h.REPTYP == "C08000E");
                    if (sierepREPTYPA != null)
                    {
                        lockClose = sierepREPTYPA.CRTDT;
                        LockPicture = sierepREPTYPA.REFDA1;
                    }
                    if (sierepREPTYPB != null)
                        getOn = sierepREPTYPB.CRTDT;

                    if (sierepREPTYPC != null)
                        getOff = sierepREPTYPC.CRTDT;

                    if (sierepREPTYPD != null)
                        lockoff = sierepREPTYPD.CRTDT;

                    if (sierepREPTYPE != null)
                    {
                        crushing = sierepREPTYPE.CRTDT;
                        CrushNum = sierepREPTYPE.REFDA2;
                        CrushWay = sierepREPTYPE.FTYPE;
                        CrushPicture = sierepREPTYPE.REFDA1;
                    }
                    switch (item.CSTATUS)
                    {
                        case "LP":
                            lockStatus = "봉인준비";
                            break;
                        case "L":
                            lockStatus = "봉인";
                            break;
                        case "P":
                            lockStatus = "상차";
                            break;
                        case "Q":
                            lockStatus = "하차";
                            break;
                        case "U":
                            lockStatus = "봉인해제";
                            break;
                        case "S":
                            lockStatus = "파쇄";
                            break;
                        case "C":
                            lockStatus = "회수";
                            break;
                        default:
                            await ShowCustomAlert("알림", "오류", "확인", "");
                            IsLoading = false;
                            break;
                    }
                    Items.Add(new MonitoringData(item.EVTDT, lockStatus, LSN, LKNM, item.REFDA1, item.ILSID, areaData[0].AREANM,
                    item.EVTDT, lockClose, getOn, getOff, lockoff, crushing, LockWorker, CrushNum, CrushWay, LockPicture, CrushPicture, item.CONFNO, MyImage));
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
                IsLoading = false;
                return;

            }
        }

        //자물쇠 로딩
        public async Task ExecuteMyCommand()
        {
            try
            {
                IsLoading = true;
                lockClose = null;
                getOn = null;
                getOff = null;
                lockoff = null;
                crushing = null;
                Items = new ObservableCollection<MonitoringData>();
                DateTime startDate = new DateTime(SelectedStartDate.Year, SelectedStartDate.Month, SelectedStartDate.Day, 0, 0, 0);
                DateTime endDate = new DateTime(SelectedEndDate.Year, SelectedEndDate.Month, SelectedEndDate.Day, 23, 59, 59);
                var dataService = ImateHelper.GetSingleTone();
                var lockStatus = "";

                //자물쇠 마스터
                var whereLKCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                        new DIMWhereFieldCondition{ fieldName = "MAC" , value = MAC, condition = DIMWhereCondition.Equal}
                    }
                };

                var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                            whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                //Siehis 조회
                var SiehisCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                            new DIMWhereFieldCondition{ fieldName = "LSN" , value = lkMstData[0].LSN, condition = DIMWhereCondition.Equal},
                            new DIMWhereFieldCondition{ fieldName = "EVTDT" , value = startDate, condition = DIMWhereCondition.GreaterEqual},
                            new DIMWhereFieldCondition{ fieldName = "EVTDT" , value = endDate, condition = DIMWhereCondition.LessEqual},
                    }
                };

                Dictionary<string, DIMSortOrder> sorts = new Dictionary<string, DIMSortOrder>();
                sorts.Add("EVTDT", DIMSortOrder.Descending);
                var siehisDatas = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                                SiehisCondition, sorts, QueryCacheType.None);
                if (siehisDatas.Count == 0)
                {
                    await ShowCustomAlert("알림", "조회하신 날짜에 작업이 없습니다.", "확인", "");
                    IsLoading = false;
                    return;
                }
                foreach (var item in siehisDatas)
                {
                    //문서함 번호
                    var ilsWhereCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {
                            new DIMWhereFieldCondition{ fieldName = "ILSID" , value = item.ILSID, condition = DIMWhereCondition.Equal}
                        }
                    };
                    var IlsMasterData = await dataService.Adapter.SelectModelDataAsync<IlsmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.IlsmstModelList",
                                ilsWhereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                    //자물쇠 위치
                    var whereIlsCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {
                            new DIMWhereFieldCondition{ fieldName = "AREA" , value = IlsMasterData[0].AREA, condition = DIMWhereCondition.Equal}
                        }
                    };

                    var areaData = await dataService.Adapter.SelectModelDataAsync<AreamstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.AreamstModelList",
                                whereIlsCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                    //SIEREP 조회
                    var whereSierepCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {
                            new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = item.CONFNO, condition = DIMWhereCondition.Equal}
                        }
                    };
                    var sierepData = await dataService.Adapter.SelectModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList",
                                whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                    LockWorker = item.REFDA1;

                    var sierepREPTYPA = sierepData.FirstOrDefault(h => h.REPTYP == "C08000A");
                    var sierepREPTYPB = sierepData.FirstOrDefault(h => h.REPTYP == "C08000B");
                    var sierepREPTYPC = sierepData.FirstOrDefault(h => h.REPTYP == "C08000C");
                    var sierepREPTYPD = sierepData.FirstOrDefault(h => h.REPTYP == "C08000D");
                    var sierepREPTYPE = sierepData.FirstOrDefault(h => h.REPTYP == "C08000E");
                    if (sierepREPTYPA != null)
                    {
                        lockClose = sierepREPTYPA.CRTDT;
                        LockPicture = sierepREPTYPA.REFDA1;
                    }
                    if (sierepREPTYPB != null)
                        getOn = sierepREPTYPB.CRTDT;

                    if (sierepREPTYPC != null)
                        getOff = sierepREPTYPC.CRTDT;

                    if (sierepREPTYPD != null)
                        lockoff = sierepREPTYPD.CRTDT;

                    if (sierepREPTYPE != null)
                    {
                        crushing = sierepREPTYPE.CRTDT;
                        CrushNum = sierepREPTYPE.REFDA2;
                        CrushWay = sierepREPTYPE.FTYPE;
                        CrushPicture = sierepREPTYPE.REFDA1;
                    }
                    switch (item.CSTATUS)
                    {
                        case "LP":
                            lockStatus = "봉인준비";
                            break;
                        case "L":
                            lockStatus = "봉인";
                            break;
                        case "P":
                            lockStatus = "상차";
                            break;
                        case "Q":
                            lockStatus = "하차";
                            break;
                        case "U":
                            lockStatus = "봉인해제";
                            break;
                        case "S":
                            lockStatus = "파쇄";
                            break;
                        case "C":
                            lockStatus = "회수";
                            break;
                        default:
                            await ShowCustomAlert("알림", "오류", "확인", "");
                            IsLoading = false;
                            break;
                    }

                    Items.Add(new MonitoringData(item.EVTDT, lockStatus, LSN, LKNM, item.REFDA1, item.ILSID, areaData[0].AREANM,
                    item.EVTDT, lockClose, getOn, getOff, lockoff, crushing, LockWorker, CrushNum, CrushWay, LockPicture, CrushPicture, item.CONFNO, MyImage));
                }
                IsLoading = false;

            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
                IsLoading = false;
                return;
            }
        }

        #endregion

        #region HistoryDetailViewModel
        public HistoryDetailViewModel()
        {
            DateTime currentDate = DateTime.Now;
            DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0);
            DateTime endDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 23, 59, 59);
            SelectedStartDate = startDate.AddDays(-7);
            SelectedEndDate = endDate;
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
                isAlertShowing = false;
            }
        }

    }
    #endregion
}