#if __ANDROID__
using Android.Content;
#elif __IOS__
using BackgroundTasks;
using Foundation;
#endif
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.ViewModel.PickUpWorker;
using WorkerScreen.Views.Common;
using XNSC.DD.EX;

namespace WorkerScreen.Views.PickUpWorker
{

    /// <summary>
    /// 상차 처리 화면입니다.
    /// </summary>
    public partial class GetOnDocDetail : ContentPage
    {
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

        public static string CONFNO
        {
            get; set;
        }
        public static string EMPNO
        {
            get; set;
        }

        public GetOnDocDetail()
        {
            LoginInfo = new LoginInfo();
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");
                LoginInfo.EMPROLE = Preferences.Get(Constants.SaveLoginRoleKey, "");

            }
            InitializeComponent();
            GetOnDetailViewModel getOnDetailViewModel = new GetOnDetailViewModel();

            this.BindingContext = getOnDetailViewModel;
        }

        // 중복 실행 방지를 위한 플래그
        private static bool isEventHandling = false;
        private object loadingObj = new object();

        private async void Renew_Clicked(object sender, EventArgs e)
        {
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            // 중복 실행 방지 플래그 체크
            if (isEventHandling)
            {
                // 이미 실행 중이라면 더 이상 실행하지 않음
                return;
            }
            try
            {
                isEventHandling = true;
                base.OnAppearing();
                if (BindingContext is GetOnDetailViewModel viewModel)
                {
                    await viewModel.ExecuteMyCommand();
                }
            }
            finally
            {
                // 중복 실행 방지 플래그 해제
                isEventHandling = false;
            }
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        }

        protected override async void OnAppearing()
        {
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            // 중복 실행 방지 플래그 체크
            if (isEventHandling)
            {
                // 이미 실행 중이라면 더 이상 실행하지 않음
                return;
            }
            try
            {
                isEventHandling = true;
                base.OnAppearing();

                var statusNotifications = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
                if (statusNotifications != PermissionStatus.Granted)
                {
                    statusNotifications = await Permissions.RequestAsync<Permissions.PostNotifications>();
                    if (statusNotifications != PermissionStatus.Granted)
                    {
                        await Application.Current.MainPage.DisplayAlert("알림", "알림 권한 설정이 필요합니다..", "OK");
                    }
                }

                if (BindingContext is GetOnDetailViewModel viewModel)
                {
                    await viewModel.ExecuteMyCommand();
                }
            }
            finally
            {
                // 중복 실행 방지 플래그 해제
                isEventHandling = false;
            }
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        }

        // 클릭 이벤트 핸들러
        public async void HandleSelectionButton_Clicked(object sender, EventArgs e)
        {
            //Lock
            lock (loadingObj)
            {
                if (loadingIndicator.IsRunning == true)
                    return;
                loadingIndicator.IsRunning = true;
                loadingIndicator.IsVisible = true;
            }

            var selectedItems = notesCollection.SelectedItems;

            var siehismodelList = new SiehisModelList();
            var sierepmodelList = new SierepModelList();

            var dataService = ImateHelper.GetSingleTone();

            DateTime currentDate = DateTime.Now;

            var selectData = "";
            if (selectedItems.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("통보", "상차하실 자물쇠를 선택해주세요.", "OK");
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
                return;
            }

            foreach (GetOnInfo item in selectedItems)
            {
                try
                {
                    //SIEHIS 조회
                    var siehisData = ((GetOnDetailViewModel)BindingContext).siehisData.FirstOrDefault(h => h.CONFNO == item.ConfNo);
                    selectData = siehisData.LSN;

                    if (siehisData == null)
                    {
                        loadingIndicator.IsRunning = false;
                        loadingIndicator.IsVisible = false;
                        return;
                    }

                    siehisData.CSTATUS = "P";
                    siehisData.ModelStatus = DIMModelStatus.Modify;
                    siehismodelList.Add(siehisData);

                    //Data 바인딩
                    CONFNO = siehisData.CONFNO;
                    EMPNO = LoginInfo.PhoneNumber;

                    //SIEREP 조회
                    var sierepData = ((GetOnDetailViewModel)BindingContext).sierepData.FirstOrDefault(h => h.CONFNO == item.ConfNo);
                    if (sierepData == null)
                    {
                        loadingIndicator.IsRunning = false;
                        loadingIndicator.IsVisible = false;
                        return;
                    }

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

                    sierepData.REPTYP = "C08000B";
                    sierepData.REPDAT = currentDate;
                    sierepData.CRTUSR = empInfoList[0].EMPNO;
                    sierepData.CRTDT = currentDate;
                    sierepData.ModelStatus = DIMModelStatus.Add;
                    sierepmodelList.Add(sierepData);

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
                        lockSN = siehisData.LSN,
                        ilsId = siehisData.ILSID,
                        eventTime = currentDate,
                        userId = LoginInfo.PhoneNumber,
                        ConfirmNo = siehisData.CONFNO,
                        ReportType = "C08000B",
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
                    return;
                }
            }
            var siehisResult = await ImateHelper.ModifyModelData<SiehisModelList>(App.ServerID, siehismodelList);
            var sierepResult = await ImateHelper.ModifyModelData<SierepModelList>(App.ServerID, sierepmodelList);
            if (selectedItems.Count > 1)
            {
                await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리번호 [{selectData}] 외 {selectedItems.Count - 1}개를 상차하였습니다.", "확인");
                StartBackgroundService();

            }
            else if (selectedItems.Count == 1)
            {
                await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리번호[{selectData}]를 상차하였습니다.", "확인");
                StartBackgroundService();

            }
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;

            //사용자 역할에 따라 홈페이지 이동
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
        }

        /// <summary>
        /// 백그라운드 서비스 실행
        /// </summary>
        private void StartBackgroundService()
        {
#if __ANDROID__
            var intent = new Intent(Android.App.Application.Context, typeof(MyBackgroundService));
            intent.PutExtra("CONFNO", CONFNO);
            intent.PutExtra("EMPNO", EMPNO);
            Android.App.Application.Context.StartService(intent);
#elif __IOS__
            var request = new BGAppRefreshTaskRequest("co.kr.istn.Worker.refresh")
            {
                EarliestBeginDate = (NSDate)DateTime.Now.AddMinutes(1)
            };

            BGTaskScheduler.Shared.Submit(request, out NSError error);
#endif
        }
        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}