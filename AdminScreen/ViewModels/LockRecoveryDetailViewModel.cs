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
    public class LockRecoveryDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<LockSearchData> SearchDataModel { get { return searchDataModel; } set { searchDataModel = value; OnPropertyChanged(nameof(SearchDataModel)); } }

        public ObservableCollection<LockSearchData> searchDataModel = new ObservableCollection<LockSearchData>();

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

        //자물쇠 조회
        public async Task ExecuteMyCommand()
        {
            try
            {
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
                            foreach (var lkmstItem in lkMstData)
                            {
                                //수거 모델에 데이터를 넣어줌
                                SearchDataModel.Add(new LockSearchData(item.LSN, item.LKNM, item.MAC));
                            }
                            return;
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("오류", "검색된 자물쇠가 없습니다.", "OK");
                        return;
                    }
                }
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                return;
            }

        }
        public LockRecoveryDetailViewModel()
        {
            CollectionView collectionView = new CollectionView();
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, "LockSearchData");
        }

    }

}