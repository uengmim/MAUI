using System.Collections.ObjectModel;
using System.ComponentModel;
using Camera.MAUI.ZXingHelper;
using System.Windows.Input;
using WorkerScreen.Views;
using WorkerScreen.Models;
using Camera.MAUI;
using WorkerScreen.Common;
using ShreDoc.Utils;
using WorkerScreen.Views.CrushingWorker;
using Microsoft.Maui.Controls;

namespace WorkerScreen.ViewModel.CrushingWorker
{

    public class CrushingCameraViewModel : INotifyPropertyChanged
    {
        #region Properties
        private string _workerName = "";
        private string _boxName = "";
        private string _location = "";
        private string _lockData = "";
        private string _confno = "";
        private DateTime _pickupDate;
        private DateTime _lockDate;
        private bool isLoading;

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
        public string ConfNo
        {
            get => _confno;
            set
            {
                _confno = value;
                OnPropertyChanged(nameof(ConfNo));
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
        /// 카메라 정보입니다.
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
        public ImageSource SnapShot { get; set; }
        public ImageSource PhotoData { get; set; }

        public List<byte[]> pics = new();

        /// <summary>
        /// 사진 촬영
        /// </summary>
        public bool TakeSnapshot
        {
            get => takeSnapshot;
            set
            {
                takeSnapshot = value;
                OnPropertyChanged(nameof(TakeSnapshot));
            }
        }
        private bool takeSnapshot = false;
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

        #region 사진 촬영Commands
        /// <summary>
        /// 사진 촬영 Command입니다.
        /// </summary>
        public Command PhotoButtonCommand { get; set; }
        #endregion

        #region 파쇄 정보 입력
        /// <summary>
        /// 사진 데이터 넘기기
        /// </summary>
        /// <returns></returns>
        public async Task InfoInputCommand()
        {
            try
            {
                IsLoading = true;

                if (pics.Count > 0)
                {
                    ////이미지 스트림으로 변환
                    //var imageStream = await ((StreamImageSource)PhotoData).Stream(CancellationToken.None);
                    ////이미지 소스를 바이트로 변환
                    //var data = new byte[imageStream.Length];
                    //int imageData = imageStream.Read(data, 0, (int)imageStream.Length);

                    var imgNums = new List<string>();

                    foreach (var picImage in pics)
                    {
                        //파일 업로드
                        var fu = ImateHelper.GetSingleTone().FileUpload;
                        var customHeadDic = new Dictionary<string, string>
                        {
                            { "x-imatefts-docno", ConfNo } //요청 승인번호 기준으로 등록 힌디.
                        };

                        var result = await fu.FileUpload("evidence.jpg", picImage, customHeadDic); //사진이름, 사진 데이터, 커스텀헤드 사전

                        imgNums.Add(result);
                    }
                    

                    await Application.Current.MainPage.Navigation.PushAsync(new CrushingInputData(WorkerName, BoxName, Location, LockData, PickupDate, LockDate, imgNums, ConfNo));

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
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                IsLoading = false;
                return;
            }
        }
        #endregion

        #region CrushingCameraViewModel
        public CrushingCameraViewModel()
        {
            IsLoading = false;
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

            this.LoginInfo = new LoginInfo();
            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                this.IsSaveNumber = true;
                this.LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                this.LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");
                this.LoginInfo.EMPROLE = Preferences.Get(Constants.SaveLoginRoleKey, "");

            }
            OnPropertyChanged(nameof(BarCodeOptions));
            OnPropertyChanged(nameof(StartCamera));
            OnPropertyChanged(nameof(StopCamera));
            OnPropertyChanged(nameof(PhotoButtonCommand));

        }
        #endregion
    }
}