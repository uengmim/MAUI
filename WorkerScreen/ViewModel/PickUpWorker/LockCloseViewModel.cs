using System.Collections.ObjectModel;
using System.ComponentModel;
using Camera.MAUI.ZXingHelper;
using WorkerScreen.Views;
using WorkerScreen.Models;
using XNSC.DD.EX;
using ShreDoc.Utils;
using ShreDoc.ProxyModel;
using WorkerScreen.Common;
using Camera.MAUI;
using WorkerScreen.Views.Common;

namespace WorkerScreen.ViewModel.PickUpWorker
{
    /// <summary>
    /// 자물쇠 봉인 화면입니다.
    /// </summary>
    public class LockCloseViewModel : INotifyPropertyChanged
    {
        #region Properties
        /// <summary>
        /// 자물쇠 정보의 모델입니다.
        /// </summary>
        private ObservableCollection<LockInfom> lockDataModel = new ObservableCollection<LockInfom>();
        public ObservableCollection<LockInfom> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }


        private string _boxNum = "";
        private string _location = "";
        private string _lockData = "";
        private string _lockName = "";
        private string _confno = "";

        /// <summary>
        /// 현재 로그인한 사용자 정보입니다.
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
        /// <summary>
        /// 로그인 정보 모델입니다.
        /// </summary>
        public LockInfom LockInfom
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfom));
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
        /// <summary>
        /// 번호 저장
        /// </summary>
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
        /// 카메라 조작입니다.
        /// </summary>
        public CameraInfo Camera
        {
            get => camera;
            set
            {
                camera = value;
                OnPropertyChanged(nameof(Camera));
                AutoStartPreview = false;
                OnPropertyChanged(nameof(AutoStartPreview));
                AutoStartPreview = true;
                OnPropertyChanged(nameof(AutoStartPreview));
            }
        }
        private CameraInfo camera = null;
        public ObservableCollection<CameraInfo> Cameras
        {
            get => cameras;
            set
            {
                cameras = value;
                OnPropertyChanged(nameof(Cameras));
            }
        }
        private ObservableCollection<CameraInfo> cameras = new();
        public int NumCameras
        {
            set
            {
                if (value > 0)
                    Camera = Cameras.First();
            }
        }
        public ImageSource SnapShot { get; set; }
        public ImageSource PhotoData { get; set; }


        private bool takeSnapshot = false;
        public bool TakeSnapshot
        {
            get => takeSnapshot;
            set
            {
                takeSnapshot = value;
                OnPropertyChanged(nameof(TakeSnapshot));
            }
        }
        public BarcodeDecodeOptions BarCodeOptions { get; set; }
        public bool AutoStartPreview { get; set; } = false;
        public bool AutoStartRecording { get; set; } = false;

        public Command StartCamera { get; set; }
        public Command StopCamera { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        /// <summary>
        /// 사진 촬영 Command입니다.
        /// </summary>
        public Command PhotoButtonCommand { get; set; }
        public async Task PhotoCommand()
        {
            try
            {
                OnPropertyChanged(nameof(StopCamera));
                TakeSnapshot = false;
                TakeSnapshot = true;
                //카메라 정지
                AutoStartPreview = false;
                OnPropertyChanged(nameof(AutoStartPreview));

                DateTime currentDate = DateTime.Now;
                string formattedDate = currentDate.ToString("yyyyMMdd");
                var sireqmodelList = new SireqModelList();
                var siehismodelList = new SiehisModelList();
                var lkmstmodelList = new LkmstModelList();
                var dataService = ImateHelper.GetSingleTone();

                bool confirm = await Application.Current.MainPage.DisplayAlert("알림", "자물쇠를 봉인하시겠습니까?", "확인", "취소");
                if (confirm)
                {
                    IsLoading = true;
                    if (PhotoData != null)
                    {
                        //이미지 스트림으로 변환
                        var imageStream = await ((StreamImageSource)PhotoData).Stream(CancellationToken.None);
                        //이미지 소스를 바이트로 변환
                        var data = new byte[imageStream.Length];
                        int imageData = imageStream.Read(data, 0, (int)imageStream.Length);
                        //string imgString = imageData.ToString();
                        //파일 업로드
                        var fu = ImateHelper.GetSingleTone().FileUpload;
                        var customHeadDic = new Dictionary<string, string>
                        {
                            { "x-imatefts-docno", CONFNO } //요청 승인번호 기준으로 등록 힌디.
                        };

                        var result = await fu.FileUpload("evidence.jpg", data, customHeadDic); //사진이름, 사진 데이터, 커스텀헤드 사전

                        var whereCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
{
                    new DIMWhereFieldCondition{ fieldName = "PIN" , value = LoginInfo.PhoneNumber, condition = DIMWhereCondition.Equal}
}
                        };

                        var empInfoList = await ImateHelper.SelectModelData<EmpmstModelList>(App.ServerID, whereCondition, new Dictionary<string, XNSC.DIMSortOrder>());
                        //Siehis 조회
                        var whereSiehisCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = CONFNO, condition = DIMWhereCondition.Equal}}
                        };
                        var siehisData = await ImateHelper.SelectModelData<SiehisModelList>(App.ServerID, whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>());
                        if (siehisData.Count > 0)
                        {
                            //SIREQ 사진 데이터 Insert
                            var sierepModelList = new SierepModelList();
                            sierepModelList.Add(new SierepModel()
                            {
                                CONFNO = siehisData[0].CONFNO,
                                REPTYP = "C08000A",
                                REPSEQ = 0,
                                REPDAT = currentDate,
                                REFDA1 = result,
                                CRTUSR = empInfoList[0].EMPNO,
                                CRTDT = currentDate,
                                ModelStatus = DIMModelStatus.Add
                            });
                            var sierepResult = await ImateHelper.ModifyModelData<SierepModelList>(App.ServerID, sierepModelList);

                            //SIREQ 시간 업데이트
                            var whereSireqCondition = new DIMGroupFieldCondtion()
                            {
                                condition = DIMGroupCondtion.AND,
                                joinCondtion = DIMGroupCondtion.AND,
                                whereFieldConditions = new DIMWhereFieldCondition[]
                                {new DIMWhereFieldCondition{ fieldName = "EREQID" , value = siehisData[0].EREQID, condition = DIMWhereCondition.Equal}}
                            };
                            var sireqData = await ImateHelper.SelectModelData<SireqModelList>(App.ServerID, whereSireqCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                            var whereLKCondition = new DIMGroupFieldCondtion()
                            {
                                condition = DIMGroupCondtion.AND,
                                joinCondtion = DIMGroupCondtion.AND,
                                whereFieldConditions = new DIMWhereFieldCondition[]
    {
                    new DIMWhereFieldCondition{ fieldName = "LSN" , value = LockData, condition = DIMWhereCondition.Equal}
    }
                            };

                            var lkMstData = await ImateHelper.SelectModelData<LkmstModelList>(App.ServerID, whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>());
                            if (sireqData.Count > 0)
                            {
                                var reqSearchData = sireqData.FirstOrDefault(h => h.EREQID == sireqData[0].EREQID);
                                var hisSearchData = siehisData.FirstOrDefault(h => h.CONFNO == siehisData[0].CONFNO);
                                var lkSearchData = lkMstData.FirstOrDefault(h => h.LSN == lkMstData[0].LSN);

                                reqSearchData.STRDT = currentDate;
                                reqSearchData.ModelStatus = DIMModelStatus.Modify;
                                sireqmodelList.Add(reqSearchData);

                                hisSearchData.CSTATUS = "L";
                                hisSearchData.REFDT1 = currentDate;
                                hisSearchData.ModelStatus = DIMModelStatus.Modify;
                                siehismodelList.Add(hisSearchData);

                                lkSearchData.LKSTA = "L";
                                lkSearchData.ModelStatus = DIMModelStatus.Modify;
                                lkmstmodelList.Add(lkSearchData);

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
                                        lockSN = LockData,
                                        ilsId = BoxNum,
                                        eventTime = currentDate,
                                        userId = LoginInfo.PhoneNumber,
                                        ConfirmNo = siehisData[0].CONFNO,
                                        ReportType = "C08000A",
                                        lockStatus = "L",
                                        Longitude = location.Longitude,
                                        Latitude = location.Latitude,
                                        opeationMode = "ON",
                                        connectTime = currentDate,
                                    });
                                }
                                catch (Exception ex)
                                {
                                    await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                                    IsLoading = false;
                                    return;
                                }
                                var sireqResult = await ImateHelper.ModifyModelData<SireqModelList>(App.ServerID, sireqmodelList);
                                var siehisResult = await ImateHelper.ModifyModelData<SiehisModelList>(App.ServerID, siehismodelList);
                                var lkmstResult = await ImateHelper.ModifyModelData<LkmstModelList>(App.ServerID, lkmstmodelList);

                            }
                            await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리번호({LockData})의 봉인을 확인하였습니다.", "확인");
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
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("알림", "정상적으로 촬영되지 않았습니다.", "확인");
                        //카메라 재생
                        AutoStartPreview = true;
                        OnPropertyChanged(nameof(AutoStartPreview));
                        IsLoading = false;
                        return;
                    }
                }
                else
                {
                    AutoStartPreview = true;
                    OnPropertyChanged(nameof(AutoStartPreview));
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                IsLoading = false;
                AutoStartPreview = true;
                OnPropertyChanged(nameof(AutoStartPreview));
                return;
            }
        }
        #endregion

        #region LockCloseViewModel
        public LockCloseViewModel()
        {
            StartCamera = new Command(() =>
            {
                AutoStartPreview = true;
                OnPropertyChanged(nameof(AutoStartPreview));
            });
            StopCamera = new Command(() =>
            {
                AutoStartPreview = false;
                OnPropertyChanged(nameof(AutoStartPreview));
            });

            LoginInfo = new LoginInfo();
            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                IsSaveNumber = true;
                LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");
                LoginInfo.EMPROLE = Preferences.Get(Constants.SaveLoginRoleKey, "");

            }
            OnPropertyChanged(nameof(BarCodeOptions));
            OnPropertyChanged(nameof(StartCamera));
            OnPropertyChanged(nameof(StopCamera));
            OnPropertyChanged(nameof(PhotoButtonCommand));

        }

    }
    #endregion
}