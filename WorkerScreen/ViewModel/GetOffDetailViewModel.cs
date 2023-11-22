using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.InteropServices;
using WorkerScreen.Utils;
using XNSC.DD.EX;
using ShreDoc.ProxyModel;

namespace WorkerScreen.ViewModel
{
    public class GetOffDetailViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GetOnInfo> GetOnInfoModel { get { return getoninfoModel; } set { getoninfoModel = value; OnPropertyChanged(nameof(GetOnInfoModel)); } }

        public ObservableCollection<GetOnInfo> getoninfoModel = new ObservableCollection<GetOnInfo>();
        private string _boxNum = "";
        private string _location = "";
        private string _lockData = "";
        private string _lockName = "";

        #region Properties

        private GetOnInfo selectedItem;

        public GetOnInfo SelectedItem
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


        public string Name { get; set; }
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

        public GetOnInfo GetOnInfo
        {
            get { return getoninfo; }
            set
            {
                getoninfo = value;
                OnPropertyChanged(nameof(GetOnInfo));
            }
        }
        private GetOnInfo getoninfo;

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
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public INavigation Navigation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SiehisModelList siehisData { get; set; } = new SiehisModelList();
        public SierepModelList sierepData { get; set; } = new SierepModelList();

        //자물쇠 로딩
        public async Task ExecuteMyCommand()
        {
            IsLoading = true;
            try
            {
                GetOnInfoModel = new ObservableCollection<GetOnInfo>();

                var dataService = ImateHelper.GetSingleTone();
                //SIEHIS 조회
                var whereSiehisCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                {new DIMWhereFieldCondition{ fieldName = "CSTATUS" , value = "P", condition = DIMWhereCondition.Equal}}
                };
                siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                            whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                //SIEREP 조회
                var whereSierepCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                {new DIMWhereFieldCondition{ fieldName = "REPTYP" , value = "C08000A", condition = DIMWhereCondition.Equal}}
                };

                sierepData = await dataService.Adapter.SelectModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList",
                            whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
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

                        var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                                    whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);


                        //문서함 번호
                        var ilsWhereCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "ILSID" , value = item.ILSID, condition = DIMWhereCondition.Equal}}
                        };
                        var IlsMasterData = await dataService.Adapter.SelectModelDataAsync<IlsmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.IlsmstModelList",
                                    ilsWhereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                        //자물쇠 위치
                        var whereIlsCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "AREA" , value = IlsMasterData[0].AREA, condition = DIMWhereCondition.Equal}}
                        };
                        var areaData = await dataService.Adapter.SelectModelDataAsync<AreamstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.AreamstModelList",
                                    whereIlsCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);


                        GetOnInfoModel.Add(new GetOnInfo(LoginInfo.Name, item.ILSID, areaData[0].AREANM, item.LSN, lkMstData[0].LKNM, item.CONFNO));
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
        #endregion

        #region GetOffDetailViewModel
        public GetOffDetailViewModel()
        {
            this.LoginInfo = new LoginInfo();
            CollectionView collectionView = new CollectionView();
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, "LockInfom");

            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                this.IsSaveNumber = true;
                this.LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                this.LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");

            }

        }
        #endregion



    }
}
