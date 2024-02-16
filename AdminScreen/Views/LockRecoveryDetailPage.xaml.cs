using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.ViewModels;
using Mapsui.UI.Maui;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Collections.ObjectModel;
using XNSC.DD.EX;

namespace AdminScreen.Views
{
    public partial class LockRecoveryDetailPage : ContentPage
    {

        public LockRecoveryDetailPage(string Pin)
        {
            InitializeComponent();

            LockRecoveryDetailViewModel lockRecoveryDetailViewModel = new LockRecoveryDetailViewModel();
            PIN = Pin;

            this.BindingContext = lockRecoveryDetailViewModel;
            //notesCollection.ItemsSource = (System.Collections.IEnumerable)LockData;

        }

        private string _deptId = "";
        private string _empNo = "";
        private string _pin = "";


        public string DEPTID
        {
            get => _deptId;
            set
            {
                _deptId = value;
                OnPropertyChanged(nameof(DEPTID));
            }
        }

        public string EMPNO
        {
            get => _empNo;
            set
            {
                _empNo = value;
                OnPropertyChanged(nameof(EMPNO));
            }
        }

        public string PIN
        {
            get => _pin;
            set
            {
                _pin = value;
                OnPropertyChanged(nameof(PIN));
            }
        }

        protected override async void OnAppearing()
        {
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            base.OnAppearing();

            if (BindingContext is LockRecoveryDetailViewModel viewModel)
            {
                await viewModel.ExecuteMyCommand();
            }
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        }

        //클릭 이벤트 처리
        public async void HandleSelectionButton_Clicked(object sender, EventArgs e)
        {
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;

            var selectedItems = notesCollection.SelectedItems;
            var dataService = ImateHelper.GetSingleTone();
            var selectData = "";
            var lkmstModelList = new LkmstModelList();
            DateTime currentDate = DateTime.Now;

            if (selectedItems.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("통보", "회수하실 자물쇠를 선택해주세요.", "OK");
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
                return;
            }

            //모델 업데이트
            foreach (LockSearchData item in selectedItems)
            {
                try
                {
                    selectData = item.LSN;
                    lkmstModelList.Add(new LkmstModel()
                    {
                        LKNM = item.LKNM,
                        LSN = item.LSN,
                        MAC = item.MAC,
                        LKSTA= "L",
                        QCTOT = 0,
                        QCCNT = 0,
                        REFDA1 = null,
                        ILSID = null,
                        REFDT1 = null,
                        ModelStatus = DIMModelStatus.Modify,
                    });

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
                        lockSN = item.LSN,
                        eventTime = currentDate,
                        ReportType = "Recovery",
                        userId = PIN,
                        lockStatus = "L",
                        opeationMode = "ON",
                        connectTime = currentDate,
                    });
                    var lkmstResult = await dataService.Adapter.ModifyModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList", lkmstModelList);
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                    loadingIndicator.IsRunning = false;
                    loadingIndicator.IsVisible = false;
                    return;
                }
            }
                if (selectedItems.Count > 1)
                {
                    await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리번호 [{selectData}] 외 {selectedItems.Count - 1}개를 회수하였습니다.", "확인");
                }
                else if (selectedItems.Count == 1)
                {
                    await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리번호[{selectData}]를 회수하였습니다.", "확인");
                }
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage(DEPTID, EMPNO, PIN, "관리자"));
        }
        

    }
}   