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

namespace WorkerScreenCrushing.ViewModel
{

    public class CrushingCameraViewModel : INotifyPropertyChanged
    {
        #region Properties

        private string _name = "";
        private string _workerName = "";
        private string _boxName = "";
        private string _location = "";
        private string _lockData = "";
        private DateTime _pickupDate;
        private DateTime _lockDate;
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
        public Command PhotoButtonCommand { get; set; }
        public ICommand PhotoCommand => new Command(PhotoShoot);
        #endregion

        // Methods
        #region InfoInput

        public void PhotoShoot()
        {
            OnPropertyChanged(nameof(StopCamera));
            TakeSnapshot = false;
            TakeSnapshot = true;
            //카메라 정지
            AutoStartPreview = false;
            OnPropertyChanged(nameof(AutoStartPreview));
        }
        /// 촬영
        public async Task InfoInputCommand()
        {
            try
            {
                IsLoading = true;
                //카메라 정지
                AutoStartPreview = true;
                AutoStartPreview = false;
                OnPropertyChanged(nameof(AutoStartPreview));
                if (PhotoData != null)
                {
                    //이미지 스트림으로 변환
                    var imageStream = await ((StreamImageSource)PhotoData).Stream(System.Threading.CancellationToken.None);
                    //이미지 소스를 바이트로 변환
                    var data = new byte[imageStream.Length];
                    int imageData = imageStream.Read(data, 0, (int)imageStream.Length);
                    string imgString = imageData.ToString();
                    //파일 업로드
                    var fu = ImateHelper.GetSingleTone().FileUpload;
                    var customHeadDic = new Dictionary<string, string>
                                {
                                    { "X-Imatefts-plant", "" },  //<== 공백
                                    { "X-Imatefts-userId", this.loginInfo.PhoneNumber}, //<==로그ID: 여기에 로그인ID (PIN 번호)를 넣는다/
                                    { "X-Imatefts-docNo", imgString } //<== 문서번호: 여기에 문서 번호를 넣는다.(사진의 고유ID)
                                };

                    var result = await fu.FileUpload("", data, customHeadDic); //사진이름, 사진 데이터, 커스텀헤드 사전
                    await Application.Current.MainPage.Navigation.PushAsync(new CrushingInputData(WorkerName, BoxName, Location, LockData, PickupDate, LockDate, result));

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

        public CrushingCameraViewModel()
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

            this.LoginInfo = new LoginInfo();
            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                this.IsSaveNumber = true;
                this.LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                this.LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");

            }
            OnPropertyChanged(nameof(BarCodeOptions));
            OnPropertyChanged(nameof(StartCamera));
            OnPropertyChanged(nameof(StopCamera));
            OnPropertyChanged(nameof(PhotoButtonCommand));

        }
    }
}