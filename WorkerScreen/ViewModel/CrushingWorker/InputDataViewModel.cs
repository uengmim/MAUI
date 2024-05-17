using System.ComponentModel;
using XNSC.DD.EX;
using System.Windows.Input;
using WorkerScreen.Views;
using WorkerScreen.Models;
using ShreDoc.Utils;
using WorkerScreen.Common;
using ShreDoc.ProxyModel;
using WorkerScreen.Views.Common;

namespace WorkerScreen.ViewModel.CrushingWorker
{
    /// <summary>
    /// 파쇄 정보 입력 화면입니다.
    /// </summary>
    public class InputDataViewModel : INotifyPropertyChanged
    {
        #region Properties

        private string _workerName = "";
        private string _boxName = "";
        private string _location = "";
        private string _lockData = "";
        private DateTime _pickupDate;
        private DateTime _lockDate;
        private List<string> _photoResult = new List<string>();
        private string _confno = "";

        /// <summary>
        /// 로그인한 사용자 정보입니다.
        /// </summary>
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
        /// <summary>
        /// 클릭 아이템입니다.
        /// </summary>
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
        private CrushWayItem selectedItem;

        /// <summary>
        /// 로딩 패널입니다.
        /// </summary>
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
        private bool isLoading;
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
        public List<string> PhotoResult
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
        /// <summary>
        /// 파쇄 방법 선택 모델입니다.
        /// </summary>
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
        private List<CrushWayItem> items;


        public ImageSource SnapShot { get; set; }
        public ImageSource PhotoData { get; set; }

        public string CrushNum { get; set; } = "";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        /// <summary>
        /// 파쇄 정보 등록 Command입니다.
        /// </summary>
        public ICommand InfoRegisterCommand => new Command(InfomRegister);
        #endregion

        #region 파쇄 정보 입력

        // 중복 실행 방지를 위한 플래그
        private object loadingObj = new object();

        /// <summary>
        /// 파쇄 정보 입력 이벤트입니다.
        /// </summary>
        public async void InfomRegister()
        {
            try
            {
                lock (loadingObj)
                {
                    if (isLoading == true)
                        return;
                    isLoading = true;
                }
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
                    var empInfoList = await ImateHelper.SelectModelData<EmpmstModelList>(App.ServerID, whereCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                    DateTime SelectDateTime = SelectedDate;

                    //Siehis 조회
                    var whereSiehisCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = ConfNo, condition = DIMWhereCondition.Equal}}
                    };
                    var siehisData = await ImateHelper.SelectModelData<SiehisModelList>(App.ServerID, whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                    var hisSearchData = siehisData.FirstOrDefault(h => h.CONFNO == ConfNo);


                    hisSearchData.CSTATUS = "S";
                    hisSearchData.ModelStatus = DIMModelStatus.Modify;
                    siehismodelList.Add(hisSearchData);

                    int repSeq = 0; // REPSEQ를 위한 카운터 변수 초기화
                    foreach (var picImage in PhotoResult)
                    {
                        //SIEREP
                        sierepmodelList.Add(new SierepModel()
                        {
                            CONFNO = ConfNo,
                            REPTYP = "C08000E",
                            REPSEQ = repSeq++,
                            REPDAT = SelectedDate,
                            FTYPE = SelectedItem.ID,
                            REFDA1 = picImage,
                            REFDA2 = CrushNum,
                            REFDT1 = hisSearchData.EVTDT,
                            CRTUSR = empInfoList[0].EMPNO,
                            CRTDT = currentDate,
                            ModelStatus = DIMModelStatus.Add
                        });
                    }

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
                    });
                    var sireqResult = await ImateHelper.ModifyModelData<SierepModelList>(App.ServerID, sierepmodelList);
                    var siehisResult = await ImateHelper.ModifyModelData<SiehisModelList>(App.ServerID, siehismodelList);

                }
                await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리번호({LockData})의 파쇄 정보를 등록하였습니다.", "확인");
                switch (LoginInfo.EMPROLE)
                {
                    case "PickUp":
                        await App.Current.MainPage.Navigation.PushAsync(new PUWorkerHomePage(LoginInfo));
                        break;
                    case "Crush":
                        await App.Current.MainPage.Navigation.PushAsync(new CRWorkerHomePage(LoginInfo));
                        break;
                    case "All":
                        await App.Current.MainPage.Navigation.PushAsync(new AllWorkerHomePage(LoginInfo));
                        break;
                    default:
                        // Handle other cases or throw an exception
                        throw new InvalidOperationException($"Unknown EMPROLE: {LoginInfo.EMPROLE}");
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

        #region InputDataViewModel
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
                this.LoginInfo.EMPROLE = Preferences.Get(Constants.SaveLoginRoleKey, "");
            }
        }
        #endregion
    }
}