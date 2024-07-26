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
using NetTopologySuite.Index.HPRtree;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// 작업 모니터링 화면
    /// </summary>
    public class MonitoringDetailViewModel : INotifyPropertyChanged
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
        /// LockInfo
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
        /// 지도 Command입니다.
        /// </summary>
        public ICommand MapCommand => new Command(MapShow);
        /// <summary>
        /// 문서 Command입니다.
        /// </summary>
        public ICommand DocCommand => new Command(DocShow);

        private bool isNavigating = false; // 실행 중 여부를 나타내는 변수

        /// <summary>
        /// 지도 페이지 이동
        /// </summary>
        private async void MapShow()
        {
            try
            {
                if (isNavigating) return; // 실행 중이면 메소드 종료

                isNavigating = true; // 실행 중으로 설정

                IsLoading = true;
                if (string.IsNullOrEmpty(CONFNO))
                {
                    await ShowCustomAlert("알림", "진행사항이 없습니다.", "확인", "");
                    return;
                }

                await Application.Current.MainPage.Navigation.PushAsync(new TaskMonitoringMapPage(CONFNO));
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
            finally
            {
                isNavigating = false; // 실행 종료
                IsLoading = false;
            }
        }

        /// <summary>
        /// 문서 페이지 이동.
        /// </summary>
        private async void DocShow()
        {
            try
            {
                if (isNavigating) return; // 실행 중이면 메소드 종료

                isNavigating = true; // 실행 중으로 설정

                IsLoading = true;
                if (string.IsNullOrEmpty(CONFNO))
                {
                    await ShowCustomAlert("알림", "진행사항이 없습니다.", "확인", "");
                    return;
                }

                await Application.Current.MainPage.Navigation.PushAsync(new TaskMonitoringDocPage(lockClose ?? DateTime.MinValue, crushing ?? DateTime.MinValue, CONFNO));
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
            finally
            {
                isNavigating = false; // 실행 종료
                IsLoading = false;
            }
        }
        //자물쇠 로딩
        public async Task ExecuteMyCommand()
        {
            try
            {
                IsLoading = true;
                Items = new ObservableCollection<MonitoringData>();
                var dataService = ImateHelper.GetSingleTone();

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

                if (lkMstData[0].REFDA1 != null)
                {
                    var whereCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {
                            new DIMWhereFieldCondition{ fieldName = "DEPTID" , value = lkMstData[0].REFDA1, condition = DIMWhereCondition.Equal}
                        }
                    };

                    var empInfoList = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList",
                                        whereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                    if (lkMstData[0].ILSID != null)
                    {
                        if (lkMstData[0].LKSTA == "L")
                        {
                            MyImage = "lock_red.png";
                        }
                        else if (lkMstData[0].LKSTA == "U")
                        {
                            MyImage = "lock_open_green.png";
                        }

                        //문서함 번호
                        var ilsWhereCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {
                                new DIMWhereFieldCondition{ fieldName = "ILSID" , value = lkMstData[0].ILSID, condition = DIMWhereCondition.Equal}
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

                        //Siehis 조회
                        var whereSiehisCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {
                                new DIMWhereFieldCondition{ fieldName = "LSN" , value = lkMstData[0].LSN, condition = DIMWhereCondition.Equal},
                                new DIMWhereFieldCondition{ fieldName = "REFDA3" , value = empInfoList[0].DEPTID, condition = DIMWhereCondition.Equal}
                            }
                        };

                        Dictionary<string, DIMSortOrder> sorts = new Dictionary<string, DIMSortOrder>();
                        sorts.Add("EVTDT", DIMSortOrder.Descending);

                        var siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                                    whereSiehisCondition, sorts, QueryCacheType.None);
                        if (siehisData.Count > 0)
                        {
                            //SIEREP 조회
                            var whereSierepCondition = new DIMGroupFieldCondtion()
                            {
                                condition = DIMGroupCondtion.AND,
                                joinCondtion = DIMGroupCondtion.AND,
                                whereFieldConditions = new DIMWhereFieldCondition[]
                                {
                                    new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = siehisData[0].CONFNO, condition = DIMWhereCondition.Equal}
                                }
                            };
                            var sierepData = await dataService.Adapter.SelectModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList",
                                        whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                            LockWorker = empInfoList[0].EMPNM;
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

                            Items.Add(new MonitoringData(lkMstData[0].REFDT1, LKTYP, LSN, LKNM, empInfoList[0].EMPNM, lkMstData[0].ILSID, areaData[0].AREANM,
                            siehisData[0].EVTDT, lockClose, getOn, getOff, lockoff, crushing, LockWorker, CrushNum, CrushWay, LockPicture, CrushPicture, CONFNO, MyImage));

                        }
                        else
                        {
                            Items.Add(new MonitoringData(lkMstData[0].REFDT1, LKTYP, LSN, LKNM, empInfoList[0].EMPNM, null, null,
                                       null, null, null, null, null, null, null, null, null, null, null, CONFNO, MyImage));
                        }
                    }
                    else
                    {
                        Items.Add(new MonitoringData(lkMstData[0].REFDT1, LKTYP, LSN, LKNM, empInfoList[0].EMPNM, null, null,
                                   null, null, null, null, null, null, null, null, null, null, null, CONFNO, MyImage));
                    }
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

        #region MonitoringDetailViewModel
        public MonitoringDetailViewModel()
        {
            SelectedStartDate = DateTime.Now.AddDays(-7);
            SelectedEndDate = DateTime.Now;
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
    #endregion
}