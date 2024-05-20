using System.Collections.ObjectModel;
using System.ComponentModel;
using ZXing;
using Camera.MAUI.ZXingHelper;
using System.Windows.Input;
using WorkerScreen.Views;
using WorkerScreen.Models;
using XNSC.DD.EX;
using ShreDoc.Utils;
using ShreDoc.ProxyModel;
using Camera.MAUI;
using WorkerScreen.Views.PickUpWorker;

namespace WorkerScreen.ViewModel.PickUpWorker
{
    /// <summary>
    /// 보안 문서함 QR코드 인식 화면입니다.
    /// </summary>
    public class QRCodeViewModel : INotifyPropertyChanged
    {
        #region Properties
        private string _boxNum = "";
        private string _location = "";
        private string _lockData = "";
        private string _lockName = "";
        /// <summary>
        /// 카메라 정보
        /// </summary>
        private CameraInfo camera = null;
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
        private ObservableCollection<CameraInfo> cameras = new();
        public ObservableCollection<CameraInfo> Cameras
        {
            get => cameras;
            set
            {
                cameras = value;
                OnPropertyChanged(nameof(Cameras));
            }
        }
        public int NumCameras
        {
            set
            {
                if (value > 0)
                    Camera = Cameras.First();
            }
        }
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
        public string BarcodeText { get; set; } = "";
        public bool AutoStartPreview { get; set; } = false;
        public bool AutoStartRecording { get; set; } = false;

        private Result[] barCodeResults;

        public Result[] BarCodeResults
        {
            get => barCodeResults;
            set
            {
                barCodeResults = value;
                if (barCodeResults != null && barCodeResults.Length > 0)
                {
                    BarcodeText = barCodeResults[0].Text;
                    if (MainThread.IsMainThread)
                    {
                        OnPropertyChanged(nameof(StopCamera));
                        QRRecognition();
                    }
                    else
                    {
                        OnPropertyChanged(nameof(StopCamera));
                        MainThread.BeginInvokeOnMainThread(QRRecognition);
                    }
                    //Shell.Current.GoToAsync(nameof(QRRecogPage));
                }
                OnPropertyChanged(nameof(BarcodeText));
            }
        }

        public Command StartCamera { get; set; }
        public Command StopCamera { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// TTLock Guid 생성
        /// </summary>
        private Guid _guid;
        public Guid Guid
        {
            get
            {
                if (_guid == default)
                {
                    _guid = Guid.NewGuid();
                }

                return _guid;
            }
            set
            {
                _guid = value;
            }
        }
        /// <summary>
        /// 사용자 로그인 정보입니다.
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
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EMPNO { get; set; }
        public string DEPTID { get; set; }

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
        #endregion

        #region Commands
        /// <summary>
        /// QR 인식 Command 입니다.
        /// </summary>
        public ICommand RecognitionCommand => new Command(QRRecognition);
        #endregion

        #region QR코드 인식

        // 중복 실행 방지를 위한 플래그
        private static bool isEventHandling = false;
        /// <summary>S
        /// QR코드 인식을 나타냅니다.
        /// </summary>
        public async void QRRecognition()
        {
            // 중복 실행 방지 플래그 체크
            if (isEventHandling)
            {
                // 이미 실행 중이라면 더 이상 실행하지 않음
                return;
            }
            try
            {
                IsLoading = true;
                isEventHandling = true;

                OnPropertyChanged(nameof(StopCamera));
                TakeSnapshot = false;
                TakeSnapshot = true;
                //카메라 정지
                AutoStartPreview = false;
                OnPropertyChanged(nameof(AutoStartPreview));
                DateTime currentDate = DateTime.Now;
                string formattedDate = currentDate.ToString("yyyyMMdd");
                var sireqmodelList = new SireqModelList();
                var silockmodelList = new SilockModelList();
                var siehismodelList = new SiehisModelList();
                var dataService = ImateHelper.GetSingleTone();


                if (BarcodeText == null || BarcodeText == "")
                {
                    await Application.Current.MainPage.DisplayAlert("오류", "문서함 번호를 입력하세요", "OK");
                    //카메라 재생
                    AutoStartPreview = true;
                    OnPropertyChanged(nameof(AutoStartPreview));
                    IsLoading = false;
                    return;
                }

                var ilsWhereCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {new DIMWhereFieldCondition{ fieldName = "ILSID" , value = BarcodeText, condition = DIMWhereCondition.Equal}}
                };
                //문서함 번호
                var IlsMasterData = await ImateHelper.SelectModelData<IlsmstModelList>(App.ServerID, ilsWhereCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                if (IlsMasterData.Count > 0)
                {
                    var BarcodeData = IlsMasterData[0];
                    BarcodeData.ILSID = BarcodeText;
                    var areaCode = BarcodeData.AREA;
                    //자물쇠 위치
                    var whereIlsCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {new DIMWhereFieldCondition{ fieldName = "AREA" , value = areaCode, condition = DIMWhereCondition.Equal}}
                    };
                    var areaData = await ImateHelper.SelectModelData<AreamstModelList>(App.ServerID, whereIlsCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                    if (areaData.Count > 0)
                    {
                        BoxNum = BarcodeData.ILSID;
                        Location = areaData[0].AREANM;
                    }
                    await Application.Current.MainPage.Navigation.PushAsync(new QRRecogPage(BoxNum, Location, areaCode, DEPTID, EMPNO, Name));
                    AutoStartPreview = true;
                    OnPropertyChanged(nameof(AutoStartPreview));

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("오류", "문서함 번호가 정확하지 않습니다.", "OK");
                    //카메라 재생
                    AutoStartPreview = true;
                    OnPropertyChanged(nameof(AutoStartPreview));
                    IsLoading = false;
                    return;

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                return;
            }
            finally
            {
                isEventHandling = false;
                IsLoading = false;
            }

        }
        #endregion

        #region QRCodeViewModel
        public QRCodeViewModel()
        {
            LoginInfo = new LoginInfo();
            BarCodeOptions = new Camera.MAUI.ZXingHelper.BarcodeDecodeOptions
            {
                AutoRotate = true,
                PossibleFormats = { ZXing.BarcodeFormat.QR_CODE },
                ReadMultipleCodes = false,
                TryHarder = true,
                TryInverted = true
            };
            OnPropertyChanged(nameof(BarCodeOptions));
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
            OnPropertyChanged(nameof(StartCamera));
            OnPropertyChanged(nameof(StopCamera));
        }
    }
    #endregion

}