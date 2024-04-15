using WorkerScreenCrushing.Common;
using WorkerScreenCrushing.Models;
using WorkerScreenCrushing.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.InteropServices;
using XNSC.DD.EX;
using ShreDoc.ProxyModel;
using Microsoft.Maui.ApplicationModel.Communication;
using ShreDoc.Utils;

namespace WorkerScreenCrushing.ViewModel
{
    public class CrushingDetailViewModel : INotifyPropertyChanged
    {

        #region Properties
        public ObservableCollection<CrushingInfo> CrushingInfoModel { get { return crushinginfoModel; } set { crushinginfoModel = value; OnPropertyChanged(nameof(CrushingInfoModel)); } }

        public ObservableCollection<CrushingInfo> crushinginfoModel = new ObservableCollection<CrushingInfo>();

        private string _name = "";
        private string _boxName = "";
        private string _location = "";
        private string _lockData = "";
        private string _textSearch = "";
        private DateTime _pickupDate;
        private DateTime _lockDate;

        private CrushingInfo selectedItem;

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

        private Color backgroundColorSet;
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
        public string TextSearch
        {
            get => _textSearch;
            set
            {
                _textSearch = value;
                OnPropertyChanged(nameof(TextSearch));
                if(_textSearch.Length > 0)
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
        public ICommand SearchEmptyLoadContactCommand { get; private set; }
        /// <summary>
        /// 데이터모델
        /// </summary>
        public SiehisModelList siehisData { get; set; } = new SiehisModelList();
        public SierepModelList sierepData { get; set; } = new SierepModelList();

        //클릭 이벤트
        public async void SieRepInsert(string Name, string BoxName, string Location, string LockData, DateTime PickupDate, DateTime? LockDate , string ConfNo)
        {
            var siehismodelList = new SiehisModelList();
            var sierepmodelList = new SierepModelList();
            var lkMstmodelList = new LkmstModelList();

            DateTime currentDate = DateTime.Now;
            var dataService = ImateHelper.GetSingleTone();

            if (SelectedItem != null)
            {
                try
                {
                    IsLoading = true;
                    //SIEREP 조회
                    var whereSierepCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                    {new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = ConfNo, condition = DIMWhereCondition.Equal}}
                    };
                    sierepData = await dataService.Adapter.SelectModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList",
                                whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                    //SIEREP상세 조회
                    var repSearchData = sierepData.FirstOrDefault(h => h.REPTYP == "C08000D");
                    var repData = sierepData.FirstOrDefault(h => h.REPTYP == "C08000C");

                    //SIEHIS상세 조회
                    var hisData = siehisData.FirstOrDefault(h => h.CONFNO == ConfNo);

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
                        var empInfoList = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList",
                                                whereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                        var whereLKCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "LSN" , value = LockData, condition = DIMWhereCondition.Equal}}
                        };
                        var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                                    whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

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

                        hisData.CSTATUS = "U";
                        hisData.ModelStatus = DIMModelStatus.Modify;
                        siehismodelList.Add(hisData);

                        LockDate = currentDate;

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
                            lockSN = hisData.LSN,
                            ilsId = hisData.ILSID,
                            eventTime = currentDate,
                            userId = LoginInfo.PhoneNumber,
                            ConfirmNo = hisData.CONFNO,
                            ReportType = "C08000D",
                            lockStatus = "U",
                            Longitude = location.Longitude,
                            Latitude = location.Latitude,
                            opeationMode = "ON",
                            connectTime = currentDate,
                            batteryVolt = 100,
                        });

                        var lkmstResult = await dataService.Adapter.ModifyModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList", lkMstmodelList);
                        var sierepResult = await dataService.Adapter.ModifyModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList", sierepmodelList);
                        var siehisResult = await dataService.Adapter.ModifyModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList", siehismodelList);

                        await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리 번호 [{LockData}] 의 봉인을 해제 하였습니다.", "OK");
                        await Application.Current.MainPage.Navigation.PushAsync(new CrushingCameraPage(Name, BoxName, Location, LockData, PickupDate, LockDate, ConfNo));
                        IsLoading = false;

                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new CrushingCameraPage(Name, BoxName, Location, LockData, PickupDate, LockDate, ConfNo));
                        IsLoading = false;
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
        }

        //자물쇠 로딩
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
                siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                            whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

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

                        //SIEREP 조회
                        var whereSierepCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                        {new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = item.CONFNO, condition = DIMWhereCondition.Equal}}
                        };

                        sierepData = await dataService.Adapter.SelectModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList",
                                    whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                        var sierepREPTYPA = sierepData.FirstOrDefault(h => h.REPTYP == "C08000A");
                        var sierepREPTYPD = sierepData.FirstOrDefault(h => h.REPTYP == "C08000D");
                        DateTime? datData;
                        if (sierepREPTYPD == null)
                        {
                            datData = null;
                            BackgroundColorSet = Colors.Green;
                        }
                        else
                        {
                            datData = sierepREPTYPD.REPDAT;
                            BackgroundColorSet = Colors.Red;

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
        //searchBar
        private void OnSearchContactCommand()
        {
            var foundContacts = CrushingInfoModel.Where(found =>
            found.LockData.StartsWith(TextSearch)).ToList();

            if(foundContacts.Count > 0)
            {
            IsLoading = true;
                CrushingInfoModel.Clear();
                foreach(var contact in foundContacts)
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

        //자물쇠 봉인 해제
        #endregion

        #region CrushingDetailViewModel
        public CrushingDetailViewModel()
        {
            CrushingInfoModel.Clear();
            SearchEmptyLoadContactCommand = new Command(async () => await ExecuteMyCommand());
            this.LoginInfo = new LoginInfo();
            CollectionView collectionView = new CollectionView();
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, "CrushingInfo");

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
