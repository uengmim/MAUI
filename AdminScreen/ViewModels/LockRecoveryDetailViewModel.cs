using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.Views;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using XNSC.DD.EX;
using XNSC.Net.NOKE;
using Command = Microsoft.Maui.Controls.Command;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// 자물쇠 회수 화면
    /// </summary>
    public class LockRecoveryDetailViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 특성 변경 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Lock Search 모델
        /// </summary>
        public ObservableCollection<LockSearchData> SearchDataModel { get { return searchDataModel; } set { searchDataModel = value; OnPropertyChanged(nameof(SearchDataModel)); } }

        /// <summary>
        /// Lock Search 모델
        /// </summary>
        private ObservableCollection<LockSearchData> searchDataModel = new ObservableCollection<LockSearchData>();

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// selectedItem값
        /// </summary>
        private LockSearchData selectedItem;

        public LockSearchData SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
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
        /// LockSearchData
        /// </summary>
        public LockSearchData LockSearchData
        {
            get { return locksearchdata; }
            set
            {
                locksearchdata = value;
                OnPropertyChanged(nameof(locksearchdata));
            }
        }
        private LockSearchData locksearchdata;

        public LkmstModelList lkmstData { get; set; } = new LkmstModelList();
        public SiehisModelList siehisData { get; set; } = new SiehisModelList();


        //자물쇠 조회
        public async Task ExecuteMyCommand()
        {
            try
            {
                IsLoading = true;

                SearchDataModel = new ObservableCollection<LockSearchData>();
                var dataService = ImateHelper.GetSingleTone();

                //자물쇠 상태가 UnLock인 것을 가져옴
                var whereLKCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                        new DIMWhereFieldCondition{ fieldName = "LKSTA" , value = "U", condition = DIMWhereCondition.Equal}
                    }
                };

                var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                            whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                if (lkMstData.Count == 0)
                {
                    await ShowCustomAlert("알림", "검색된 자물쇠가 없습니다.", "확인", "");
                    IsLoading = false;
                    return;
                }

                foreach (var item in lkMstData)
                {
                    //UNLOCK인 자물쇠의 정보로 history에서 ConfNO를 들고옴
                    var siewhereLKCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {
                            new DIMWhereFieldCondition{ fieldName = "LSN" , value = item.LSN, condition = DIMWhereCondition.Equal},
                            new DIMWhereFieldCondition{ fieldName = "ILSID" , value = item.ILSID, condition = DIMWhereCondition.Equal},
                            new DIMWhereFieldCondition{ fieldName = "CSTATUS" , value = "S", condition = DIMWhereCondition.Equal},
                        }
                    };

                    var siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                                siewhereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                    //history에 데이터가 있으면
                    if (siehisData.Count > 0)
                    {
                        //ConfNo와 파쇄 코드가 있을시
                        var sierepwhereLKCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {
                                new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = siehisData[0].CONFNO, condition = DIMWhereCondition.Equal},
                                new DIMWhereFieldCondition{ fieldName = "REPTYP" , value = "C08000E", condition = DIMWhereCondition.Equal},
                            }
                        };

                        var sierepData = await dataService.Adapter.SelectModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList",
                                    sierepwhereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                        if (sierepData.Count != 0)
                        {
                            //수거 모델에 데이터를 넣어줌
                            SearchDataModel.Add(new LockSearchData(item.LSN, item.LKNM, item.MAC, siehisData[0].CONFNO, siehisData[0].REFDA1));
                        }
                        else
                        {
                            await ShowCustomAlert("알림", "검색된 자물쇠가 없습니다.", "확인", "");
                            IsLoading = false;
                            return;
                        }
                    }
                }
                IsLoading = false;
            }
            catch (System.Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
                IsLoading = false;
                return;
            }

        }
        public LockRecoveryDetailViewModel()
        {
            CollectionView collectionView = new CollectionView();
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, "LockSearchData");
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
}