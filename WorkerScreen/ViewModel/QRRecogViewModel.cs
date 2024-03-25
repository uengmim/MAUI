using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ShreDoc.ProxyModel;
using XNSC.DD.EX;
using System.Collections.ObjectModel;
using Google.Protobuf.WellKnownTypes;
using System.Data;
using XNSC;
using XNSC.Net.Ttlock;
using ShreDoc.Utils;

namespace WorkerScreen.ViewModel
{
    public class QRRecogViewModel : BaseViewModel
    {
        private ObservableCollection<LockInfom> lockDataModel = new ObservableCollection<LockInfom>();
        public ObservableCollection<LockInfom> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        private ObservableCollection<LockInfomation> lockInfoModel = new ObservableCollection<LockInfomation>();
        public ObservableCollection<LockInfomation> LockInfoModel { get { return lockInfoModel; } set { lockInfoModel = value; OnPropertyChanged(nameof(LockInfoModel)); } }

        #region Properties
        private string _boxNum = "";
        private string _location = "";
        private string _lockData = "";
        private string _lockName = "";
        private string _confno = "";
        private string _deptID = "";
        private string _area = "";
        private string _empName = "";

        private LockInfomation selectedItem;

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
        public string Name => LoginInfo.Name;

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
                OnPropertyChanged();
            }
        }
        private LoginInfo loginInfo;

        public LockInfom LockInfom
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged();
            }
        }
        private LockInfom lockinfo;

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
                OnPropertyChanged();
            }
        }

        private bool isSaveNumber = false;
        public string EMPNO { get; set; }
        public LkmstModelList lkMstData { get; set; } = new LkmstModelList();

        #endregion

        #region Commands

        public ICommand SelectionChangedCommand { get; set; }


        private void OnSelectionChanged(LockInfomation selectedItem)
        {
            if (selectedItem != null)
            {
                if (selectedItem.Worker != EMPNO)
                {
                    Application.Current.MainPage.DisplayAlert("오류", $"{EMPName} 작업자로 지정되지 않은 자물쇠입니다.", "OK");

                    SelectedItem = null;
                    return;
                }
            }
        }

        //자물쇠 클릭 이벤트
        private async void PerformNavigation(string Data, string Name, string LockMac)
        {
            try
            {
                IsLoading = true;
                DateTime currentDate = DateTime.Now;
                string formattedDate = currentDate.ToString("yyyyMMdd");
                var sireqmodelList = new SireqModelList();
                var silockmodelList = new SilockModelList();
                var siehismodelList = new SiehisModelList();
                var lkMstmodelList = new LkmstModelList();
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
                        ModelStatus = DIMModelStatus.Add
                    }); ;



                    var whereLKCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {new DIMWhereFieldCondition{ fieldName = "LSN" , value = Data, condition = DIMWhereCondition.Equal}}
                    };
                    var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                                whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                    var lkMstSearchData = lkMstData.FirstOrDefault(h => h.LSN == Data);

                    lkMstSearchData.LKSTA = "U";
                    lkMstSearchData.ILSID = BoxNum;
                    lkMstSearchData.ModelStatus = DIMModelStatus.Modify;
                    lkMstmodelList.Add(lkMstSearchData);


                    try
                    {
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
                            lockSN = Data,
                            ilsId = BoxNum,
                            eventTime = currentDate,
                            userId = LoginInfo.PhoneNumber,
                            ConfirmNo= siehismodelList[0].CONFNO,
                            ReportType= "READY",
                            lockStatus = "U",
                            Longitude = location.Longitude,
                            Latitude = location.Latitude,
                            opeationMode = "ON",
                            connectTime = currentDate,
                            batteryVolt = 100,
                        });
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                        SelectedItem = null;
                        IsLoading = false;
                        return;
                    }

                    //try
                    //{
                    //    QRRecogViewModel qRRecogViewModel = new QRRecogViewModel();
                    //    RegistControlLockCallback unLockCallbak = new RegistControlLockCallback(qRRecogViewModel);

                    //    TTlockHelper.DoUnlock(Data, LockMac, (IControlLockCallback)unLockCallbak);
                    //    Console.WriteLine("Lock Unlock");
                    //}
                    //catch (Exception ex)so
                    //{
                    //    await Application.Current.MainPage.DisplayAlert("오류", ex.Message, "OK");
                    //    SelectedItem = null;
                    //    IsLoading = false;
                    //    return;
                    //}

                    var sireqResult = await dataService.Adapter.ModifyModelDataAsync<SireqModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SireqModelList", sireqmodelList);
                    var silockResult = await dataService.Adapter.ModifyModelDataAsync<SilockModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SilockModelList", silockmodelList);
                    var siehisResult = await dataService.Adapter.ModifyModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList", siehismodelList);
                    var lkmstResult = await dataService.Adapter.ModifyModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList", lkMstmodelList);
                    await Application.Current.MainPage.Navigation.PushAsync(new LockClosePage(BoxNum, Location, Data, Name, siehismodelList[0].CONFNO));
                    IsLoading = false;
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

        [Obsolete]
        public async Task ExecuteMyCommand()
        {
            try
            {
                IsLoading = true;
                var dataService = ImateHelper.GetSingleTone();
                var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                            new string[0], "", "", QueryCacheType.None);

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
                    var siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                                 whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                    var whereLKCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
    {
                    new DIMWhereFieldCondition{ fieldName = "LSN" , value = lkMstData[i].LSN, condition = DIMWhereCondition.Equal},
                    new DIMWhereFieldCondition{ fieldName = "LKSTA" , value = "U", condition = DIMWhereCondition.Equal}
    }
                    };
                    var lkMstSearchData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                                whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                    if (siehisData.Count > 0 && lkMstSearchData.Count > 0)
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new LockClosePage(BoxNum, Location, siehisData[0].LSN, lkMstSearchData[0].LKNM, siehisData[0].CONFNO));
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

        #endregion

        #region QRRecogPage
        public QRRecogViewModel()
        {
            this.LoginInfo = new LoginInfo();
            CollectionView collectionView = new CollectionView();
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, "LockInfom");

            SelectionChangedCommand = new Command<LockInfomation>(OnSelectionChanged);

            foreach (var item in LockInfoModel)
            {
                var hisSearchData = lkMstData.FirstOrDefault(h => h.MAC == item.LockMac);
                LockDataModel.Add(new LockInfom(hisSearchData.LSN, hisSearchData.LKNM));
            }

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