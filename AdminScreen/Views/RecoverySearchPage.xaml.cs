using AdminScreen.Models;
using AdminScreen.ViewModels;
using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Entity;
using Javax.Crypto;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Collections.ObjectModel;
using XNSC.DD.EX;
using static Android.Content.ClipData;

namespace AdminScreen.Views
{
    public partial class RecoverySearchPage : ContentPage
    {

        public RecoverySearchPage()
        {
            InitializeComponent();

            RecoverSearchModel recoversearchModel = new RecoverSearchModel();


            this.BindingContext = recoversearchModel;
            //notesCollection.ItemsSource = (System.Collections.IEnumerable)LockData;

        }

        protected override async void OnAppearing()
        {
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            base.OnAppearing();

            if (BindingContext is RecoverSearchModel viewModel)
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
                        lockStatus = "L",
                        Longitude = location.Longitude,
                        Latitude = location.Latitude,
                        opeationMode = "ON",
                        connectTime = currentDate,
                        batteryVolt = 100,
                    });
                    var lkmstResult = await dataService.Adapter.ModifyModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList", lkmstModelList);
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
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
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
        

    }
}   