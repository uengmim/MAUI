using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using System.Windows.Markup;
using System.Collections.Specialized;
using Camera.MAUI.ZXingHelper;
using XNSC.DD.EX;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;
using WorkerScreenCrushing.Views;
using WorkerScreenCrushing.Models;
using Camera.MAUI;
using ShreDoc.Utils;
using WorkerScreenCrushing.Common;
using ShreDoc.ProxyModel;
using SkiaSharp;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Acr.UserDialogs.Infrastructure;

namespace WorkerScreenCrushing.ViewModel
{

    public class InputDataViewModel : INotifyPropertyChanged
    {
        #region Properties

        private string _name = "";
        private string _workerName = "";
        private string _boxName = "";
        private string _location = "";
        private string _lockData = "";
        private DateTime _pickupDate;
        private DateTime _lockDate;
        private string _photoResult = "";
        private string _confno = "";

        private bool isLoading;

        public LoginInfo LoginInfo
        {
            get { return loginInfo; }
            set
            {
                loginInfo = value;
                OnPropertyChanged(nameof(LoginInfo));
            }
        }
        private CrushWayItem selectedItem;

        public CrushWayItem SelectedItem
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
        private LoginInfo loginInfo;
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
        public string Name => LoginInfo.Name;
        public string WorkerName
        {
            get => _workerName;
            set
            {
                _workerName = value;
                OnPropertyChanged(nameof(WorkerName));
            }
        }
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
        public DateTime newPickupDate
        {
            get => _lockDate;
            set
            {
                _lockDate = value;
                OnPropertyChanged(nameof(newPickupDate));
            }
        }
        public string ConfNo
        {
            get => _confno;
            set
            {
                _confno = value;
                OnPropertyChanged(nameof(ConfNo));
            }
        }
        public string PhotoResult
        {
            get => _photoResult;
            set
            {
                _photoResult = value;
                OnPropertyChanged(nameof(PhotoResult));
            }
        }

        private DateTime selectedDate;

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }
        private TimeSpan selectedTime;
        public TimeSpan SelectedTime
        {
            get => selectedTime;
            set
            {
                if (selectedTime != value)
                {
                    selectedTime = value;
                    OnPropertyChanged(nameof(SelectedTime));
                }
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
        private List<CrushWayItem> items;
        public List<CrushWayItem> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged(nameof(items));
            }
        }

        private bool isSaveNumber = false;

        public ImageSource SnapShot { get; set; }
        public ImageSource PhotoData { get; set; }

        public string CrushDate { get; set; } = "";
        public string CrushWay { get; set; } = "";
        public string CrushNum { get; set; } = "";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public ICommand InfoRegisterCommand => new Command(InfomRegister);
        #endregion

        // Methods
        #region InfoInput
        //파쇄 등록
        public async void InfomRegister()
        {
            try
            {
                IsLoading = true;
                var dataService = ImateHelper.GetSingleTone();
                DateTime currentDate = DateTime.Now;

                var sierepmodelList = new SierepModelList();
                var siehismodelList = new SiehisModelList();

                bool confirm = await Application.Current.MainPage.DisplayAlert("알림", "파쇄 정보를 등록하시겠습니까?", "확인", "취소");
                if (confirm)
                {
                    //EMPMST 조회
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

                    DateTime SelectDateTime = SelectedDate;
                    //SIEREP
                    sierepmodelList.Add(new SierepModel()
                    {
                        CONFNO = ConfNo,
                        REPTYP = "C08000E",
                        REPDAT = SelectedDate,
                        FTYPE= SelectedItem.ID,
                        REFDA1 = PhotoResult,
                        REFDA2 = CrushNum,
                        CRTUSR = empInfoList[0].EMPNO,
                        CRTDT = currentDate,
                        ModelStatus = DIMModelStatus.Add
                    });

                    //Siehis 조회
                    var whereSiehisCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = ConfNo, condition = DIMWhereCondition.Equal}}
                    };
                    var siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                                whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                    var hisSearchData = siehisData.FirstOrDefault(h => h.CONFNO == ConfNo);


                    hisSearchData.CSTATUS = "S";
                    hisSearchData.ModelStatus = DIMModelStatus.Modify;
                    siehismodelList.Add(hisSearchData);


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
                        lockSN = hisSearchData.LSN,
                        ilsId = hisSearchData.ILSID,
                        eventTime = currentDate,
                        userId = LoginInfo.PhoneNumber,
                        ConfirmNo = hisSearchData.CONFNO,
                        ReportType = "C08000E",
                        lockStatus = "U",
                        Longitude = location.Longitude,
                        Latitude = location.Latitude,
                        opeationMode = "ON",
                        connectTime = currentDate,
                        batteryVolt = 100,
                    });
                    var sireqResult = await dataService.Adapter.ModifyModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList", sierepmodelList);
                    var siehisResult = await dataService.Adapter.ModifyModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList", siehismodelList);

                }
                await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리번호({LockData})의 파쇄 정보를 등록하였습니다.", "확인");
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage(LoginInfo));
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

        public InputDataViewModel()
        {
            List<CrushWayItem> itemModels = new List<CrushWayItem>();
            itemModels.Add(new CrushWayItem() { Name = "파쇄기", ID = "C12000A" });
            Items = itemModels;
            SelectedDate = DateTime.Now;
            this.LoginInfo = new LoginInfo();
            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                this.IsSaveNumber = true;
                this.LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                this.LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");

            }

        }
    }
}