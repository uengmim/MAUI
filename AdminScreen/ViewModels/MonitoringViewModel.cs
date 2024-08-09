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
using XNSC;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// 작업 모니터링 화면
    /// </summary>
    public class MonitoringViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// LockInfoData 모델
        /// </summary>
        public ObservableCollection<LockInfoData> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        /// <summary>
        /// LockInfoData 모델
        /// </summary>
        private ObservableCollection<LockInfoData> lockDataModel = new ObservableCollection<LockInfoData>();

        /// <summary>
        /// selectedItem값
        /// </summary>
        private LockInfoData selectedItem;

        public LockInfoData SelectedItem
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
                        PerformNavigation(SelectedItem.LSN, SelectedItem.LKNM, SelectedItem.MAC, SelectedItem.LKTYP, SelectedItem.CONFNO);
                    }
                }
            }
        }

        /// <summary>
        /// TaskMonitoringDetailPage 이동
        /// </summary>
        private async void PerformNavigation(string LSN, string LKNM, string MAC, string LKTYP, string CONFNO)
        {
            if (SelectedItem != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new TaskMonitoringDetailPage(LSN, LKNM, MAC, LKTYP, CONFNO));
            }
            else
            {
                await ShowCustomAlert("알림", "선택된 자물쇠가 없습니다.", "확인", "");
                SelectedItem = null;
                return;
            }
        }
        /// <summary>
        /// 로그인한 사용자의 정보
        /// </summary>
        private string _boxNum = "";
        private string _location = "";
        private string _lockData = "";
        private string _lockName = "";
        private string _confno = "";

        /// <summary>
        /// 자물쇠 조회
        /// </summary>
        private string _textSearch = "";

        #region Properties

        /// <summary>
        /// 자물쇠 조회
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
        /// 보안 문서함
        /// </summary>
        public string BoxNum
        {
            get => _boxNum;
            set
            {
                _boxNum = value;
                OnPropertyChanged(nameof(BoxNum));
            }
        }

        /// <summary>
        /// 위치
        /// </summary>
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        /// <summary>
        /// Lock Data
        /// </summary>
        public string LockData
        {
            get => _lockData;
            set
            {
                _lockData = value;
                OnPropertyChanged(nameof(LockData));
            }
        }

        /// <summary>
        /// Lock 이름
        /// </summary>
        public string LockName
        {
            get => _lockName;
            set
            {
                _lockName = value;
                OnPropertyChanged(nameof(LockName));
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
        /// 스택그라운드 색상
        /// </summary>
        private Color stackBackgroundColor;
        public Color StackBackgroundColor
        {
            get { return stackBackgroundColor; }
            set
            {
                if (stackBackgroundColor != value)
                {
                    stackBackgroundColor = value;
                    OnPropertyChanged(nameof(StackBackgroundColor));
                }
            }
        }

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
        public ICommand SearchEmptyLoadContactCommand { get; private set; }

        //검색기능
        private void OnSearchContactCommand()
        {
            var foundContacts = LockDataModel.Where(found =>
            found.LSN.StartsWith(TextSearch)).ToList();

            if (foundContacts.Count > 0)
            {
                IsLoading = true;
                LockDataModel.Clear();
                foreach (var contact in foundContacts)
                {
                    LockDataModel.Add(contact);
                }
                IsLoading = false;
            }
            else
            {
                IsLoading = true;
                LockDataModel.Clear();
                IsLoading = false;
            }
        }
        //자물쇠 로딩
        public async Task ExecuteMyCommand()
        {
            try
            {
                IsLoading = true;

                LockDataModel = new ObservableCollection<LockInfoData>();
                var dataService = ImateHelper.GetSingleTone();

                var lockStatus = "";
                var whereLKCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.OR,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                        new DIMWhereFieldCondition{ fieldName = "LKSTA" , value = "L", condition = DIMWhereCondition.Equal},
                        new DIMWhereFieldCondition{ fieldName = "LKSTA" , value = "U", condition = DIMWhereCondition.Equal}
                    }
                };

                var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                            whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                if (lkMstData.Count > 0)
                {
                    foreach (var item in lkMstData)
                    {

                        if (item.LKSTA == "L")
                        {
                            MyImage = "lock_red.png";
                        }
                        else if (item.LKSTA == "U")
                        {
                            MyImage = "lock_open_green.png";
                        }

                        var whereSireqCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {
                                new DIMWhereFieldCondition{ fieldName = "WEMPID" , value = item.REFDA1, condition = DIMWhereCondition.Equal}
                            }
                        };
                        var sireqData = await dataService.Adapter.SelectModelDataAsync<SireqModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SireqModelList",
                                    whereSireqCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);


                        var whereSiehisCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {
                                new DIMWhereFieldCondition{ fieldName = "LSN" , value = item.LSN, condition = DIMWhereCondition.Equal}
                            }
                        };

                        Dictionary<string, DIMSortOrder> sorts = new Dictionary<string, DIMSortOrder>();
                        sorts.Add("EVTDT", DIMSortOrder.Descending);

                        var siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                                    whereSiehisCondition, sorts, QueryCacheType.None);

                        var siehisConfNo = "";

                        if (siehisData.Count == 0)
                            siehisConfNo = null;
                        else if (siehisData.Count > 0)
                            siehisConfNo = siehisData[0].CONFNO;

                        //SIEREP 조회
                        var whereSierepCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {
                                new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = siehisConfNo, condition = DIMWhereCondition.Equal}
                            }
                        };

                        var sierepData = await dataService.Adapter.SelectModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList",
                                    whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                        var sierepREPTYPA = sierepData.FirstOrDefault(h => h.REPTYP == "C08000A");
                        var sierepREPTYPB = sierepData.FirstOrDefault(h => h.REPTYP == "C08000B");
                        var sierepREPTYPC = sierepData.FirstOrDefault(h => h.REPTYP == "C08000C");
                        var sierepREPTYPD = sierepData.FirstOrDefault(h => h.REPTYP == "C08000D");
                        var sierepREPTYPE = sierepData.FirstOrDefault(h => h.REPTYP == "C08000E");

                        StackBackgroundColor = Colors.DodgerBlue;


                        if (item.ILSID == null && item.REFDA1 == null && siehisData.Count > 0)
                            lockStatus = "회수";

                        else if (item.ILSID == null && item.REFDA1 != null)
                        { 
                            lockStatus = "지급";
                            siehisConfNo = "";
                        }

                        else if (item.ILSID != null && sierepData.Count == 0)
                            lockStatus = "봉인준비";

                        else if (item.ILSID != null && sierepData.Count > 0 && sierepREPTYPA != null && sierepREPTYPB == null && sierepREPTYPC == null && sierepREPTYPD == null && sierepREPTYPE == null)
                        {
                            lockStatus = "봉인";
                            //StackBackgroundColor = Colors.Crimson;
                        }

                        else if (item.ILSID != null && sierepData.Count > 0 && sierepREPTYPB != null && sierepREPTYPC == null && sierepREPTYPD == null && sierepREPTYPE == null)
                        {
                            lockStatus = "상차";
                            //StackBackgroundColor = Colors.Crimson;
                        }

                        else if (item.ILSID != null && sierepData.Count > 0 && sierepREPTYPC != null && sierepREPTYPD == null && sierepREPTYPE == null)
                        {
                            lockStatus = "하차";
                            //StackBackgroundColor = Colors.OliveDrab;
                        }

                        else if (item.ILSID != null && sierepData.Count > 0 && sierepREPTYPD != null && sierepREPTYPE == null)
                            lockStatus = "봉인해제";

                        else if (item.ILSID != null && sierepData.Count > 0 && sierepREPTYPE != null)
                        {
                            lockStatus = "파쇄";
                            //StackBackgroundColor = Colors.OliveDrab;
                        }

                        else if (sireqData.Count == 0)
                            lockStatus = "등록";

                        if (lockStatus != "회수" && lockStatus != "등록")
                        {
                            LockDataModel.Add(new LockInfoData(item.LSN, item.MAC, lockStatus, item.LKNM, siehisConfNo, MyImage));
                        }
                    }
                    IsLoading = false;
                    return;

                }
                if (lkMstData.Count == 0)
                {
                    await ShowCustomAlert("알림", "검색된 자물쇠가 없습니다.", "확인", "");
                    IsLoading = false;
                }
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
                IsLoading = false;
                return;
            }

        }
        #endregion

        #region MonitoringViewModel
        public MonitoringViewModel()
        {
            SearchEmptyLoadContactCommand = new Command(async () => await ExecuteMyCommand());
            CollectionView collectionView = new CollectionView();
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, "LockInfom");
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