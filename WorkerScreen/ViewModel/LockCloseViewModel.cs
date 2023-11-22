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
using CommunityToolkit.Maui.Views;
using System.Windows.Input;
using WorkerScreen.Views;
using WorkerScreen.Models;
using XNSC.DD.EX;
using ShreDoc.Utils;
using ShreDoc.ProxyModel;
using WorkerScreen;
using Org.BouncyCastle.Asn1.Ocsp;
using WorkerScreen.Common;
using XNSC.Net.NOKE;
using Camera.MAUI;
using ZXing.QrCode.Internal;
using static Microsoft.Maui.ApplicationModel.Permissions;
using CommunityToolkit.Maui.Converters;
using Acr.UserDialogs;

namespace WorkerScreen.ViewModel
{
    public class LockCloseViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LockInfom> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        public ObservableCollection<LockInfom> lockDataModel = new ObservableCollection<LockInfom>();
        private string _boxNum = "";
        private string _location = "";
        private string _lockData = "";
        private string _lockName = "";
        private string _confno = "";
        private bool isLoading;

        #region Properties
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
        //사진 촬영 및 자물쇠 봉인
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

                bool confirm = await Application.Current.MainPage.DisplayAlert("알림", "자물쇠를 봉인하시겠습니까?", "확인", "취소");
                if (confirm)
                {
                    IsLoading = true;
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

                        //Siehis 조회
                        var dataService = ImateHelper.GetSingleTone();
                        var whereSiehisCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = CONFNO, condition = DIMWhereCondition.Equal}}
                        };
                        var siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                                    whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                        if (siehisData.Count > 0)
                        {
                            //SIREQ 사진 데이터 Insert
                            var sierepModelList = new SierepModelList();
                            sierepModelList.Add(new SierepModel()
                            {
                                CONFNO = siehisData[0].CONFNO,
                                REPTYP = "C08000A",
                                REPDAT = currentDate,
                                REFDA1 = result,
                                ModelStatus = DIMModelStatus.Add
                            });
                            var sierepResult = await dataService.Adapter.ModifyModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList", sierepModelList);

                            //SIREQ 시간 업데이트
                            var whereSireqCondition = new DIMGroupFieldCondtion()
                            {
                                condition = DIMGroupCondtion.AND,
                                joinCondtion = DIMGroupCondtion.AND,
                                whereFieldConditions = new DIMWhereFieldCondition[]
                                {new DIMWhereFieldCondition{ fieldName = "EREQID" , value = siehisData[0].EREQID, condition = DIMWhereCondition.Equal}}
                            };
                            var sireqData = await dataService.Adapter.SelectModelDataAsync<SireqModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SireqModelList",
                                        whereSireqCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                            if (sireqData.Count > 0)
                            {
                                var reqSearchData = sireqData.FirstOrDefault(h => h.EREQID == sireqData[0].EREQID);
                                var hisSearchData = siehisData.FirstOrDefault(h => h.CONFNO == siehisData[0].CONFNO);

                                reqSearchData.STRDT = currentDate;
                                reqSearchData.ModelStatus = DIMModelStatus.Modify;
                                sireqmodelList.Add(reqSearchData);

                                hisSearchData.CSTATUS = "L";
                                hisSearchData.ModelStatus = DIMModelStatus.Modify;
                                siehismodelList.Add(hisSearchData);
                                var sireqResult = await dataService.Adapter.ModifyModelDataAsync<SireqModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SireqModelList", sireqmodelList);
                                var siehisResult = await dataService.Adapter.ModifyModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList", siehismodelList);

                            }
                            await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리번호({LockData})의 봉인을 확인하였습니다.", "확인");
                            await Application.Current.MainPage.Navigation.PushAsync(new HomePage(LoginInfo));
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
        #region 
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
    #endregion
}