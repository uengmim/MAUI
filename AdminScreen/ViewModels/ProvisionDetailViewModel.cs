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

namespace AdminScreen.ViewModels
{
    public class ProvisionDetailViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LockInfoData> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        public ObservableCollection<LockInfoData> lockDataModel = new ObservableCollection<LockInfoData>();

        public ObservableCollection<EmpMstItem> Items { get { return Item; } set { Item = value; OnPropertyChanged(nameof(Items)); } }

        public ObservableCollection<EmpMstItem> Item = new ObservableCollection<EmpMstItem>();

        public ObservableCollection<LockProvision> LockDatas { get { return lockDatas; } set { lockDatas = value; OnPropertyChanged(nameof(LockDatas)); } }

        public ObservableCollection<LockProvision> lockDatas = new ObservableCollection<LockProvision>();


        private string _boxNum = "";
        private string _location = "";
        private string _lockData = "";
        private string _lockName = "";
        private string _confno = "";

        private EmpMstItem selectedItem;

        public EmpMstItem SelectedItem
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
        #region Properties
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public ICommand RockProvisionCommand => new Command(RockProvision);


        //자물쇠 로딩
        public async Task ExecuteMyCommand()
        {
            try
            {
                IsLoading = true;
                var dataService = ImateHelper.GetSingleTone();
                List<EmpMstItem> itemModels = new List<EmpMstItem>();
                //EMPMST 조회
                var whereCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
    {
                    new DIMWhereFieldCondition{ fieldName = "REFDA5" , value = "data", condition = DIMWhereCondition.Equal}
    }
                };
                var empInfoList = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList",
                                        whereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                if(empInfoList.Count > 0)
                {
                    foreach (var emp in empInfoList)
                    {
                        Items.Add(new EmpMstItem(emp.EMPNO, emp.EMPNM));
                    }
                    IsLoading = false;

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
        //자물쇠지급
        public async void RockProvision()
        {
            try
            {

                IsLoading = true;
                DateTime currentDate = DateTime.Now;
                var dataService = ImateHelper.GetSingleTone();
                var lkMstmodelList = new LkmstModelList();
                if (SelectedItem == null) {
                    await Application.Current.MainPage.DisplayAlert("통보", "지급하실 작업자를 선택해주세요.", "OK");
                    IsLoading = false;
                    return;
                }
                foreach (var selectData in LockDatas)
                {
                    var whereLKCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {new DIMWhereFieldCondition{ fieldName = "LSN" , value = selectData.LSN, condition = DIMWhereCondition.Equal}}
                    };
                    var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                                whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);


                    var lkMstSearchData = lkMstData.FirstOrDefault(h => h.LSN == selectData.LSN);

                    lkMstSearchData.REFDA1 = SelectedItem.ID;
                    lkMstSearchData.REFDT1 = currentDate;
                    lkMstSearchData.ModelStatus = DIMModelStatus.Modify;
                    lkMstmodelList.Add(lkMstSearchData);

                    //EMPMST 조회
                    var whereCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
        {
                    new DIMWhereFieldCondition{ fieldName = "EMPNO" , value = SelectedItem.ID, condition = DIMWhereCondition.Equal}
        }
                    };
                    var empInfoList = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList",
                                            whereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

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
                            lockSN = selectData.LSN,
                            eventTime = currentDate,
                            userId = empInfoList[0].PIN,
                            ReportType = "Provision",
                            lockStatus = "L",
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
                        IsLoading = false;
                        return;
                    }
                    var lkmstResult = await dataService.Adapter.ModifyModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList", lkMstmodelList);
                }
                    await Application.Current.MainPage.DisplayAlert("알림", $"작업자({SelectedItem.Name})에게 자물쇠를 지급하였습니다.", "확인");
                    await Application.Current.MainPage.Navigation.PushAsync(new LockProvisionPage());
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

        #region ProvisionDetailViewModel
        public ProvisionDetailViewModel()
        {
            List<EmpMstItem> Items = new List<EmpMstItem>();

            LockDataModel.Clear();
        }

    }
    #endregion


}