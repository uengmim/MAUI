using Camera.MAUI;
using Camera.MAUI.Test;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Xml.Linq;
using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.ViewModel;
using XNSC.DD.EX;

namespace WorkerScreen.Views;

public partial class GetOffDocDetail : ContentPage
{
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

    public GetOffDocDetail()
    {
        this.LoginInfo = new LoginInfo();
        if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
        {
            this.LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
            this.LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");

        }
        InitializeComponent();
        GetOffDetailViewModel getOffDetailViewModel = new GetOffDetailViewModel();

        this.BindingContext = getOffDetailViewModel;

    }
    private async void Renew_Clicked(object sender, EventArgs e)
    {

        loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;
        base.OnAppearing();
        if (BindingContext is GetOffDetailViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }
        loadingIndicator.IsRunning = false;
        loadingIndicator.IsVisible = false;
    }

    protected override async void OnAppearing()
    {
        loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;
        base.OnAppearing();

        if (BindingContext is GetOffDetailViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }
        loadingIndicator.IsRunning = false;
        loadingIndicator.IsVisible = false;
    }

    private object loadingObj = new object();

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
            await Application.Current.MainPage.DisplayAlert("통보", "하차하실 자물쇠를 선택해주세요.", "OK");
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            return;
        }

        foreach (GetOnInfo item in selectedItems)
        {
            try
            {
                //SIEHIS 조회
                var siehisData = ((GetOffDetailViewModel)BindingContext).siehisData.FirstOrDefault(h => h.CONFNO == item.ConfNo);
                selectData = siehisData.LSN;

                if (siehisData == null)
                {
                    loadingIndicator.IsRunning = false;
                    loadingIndicator.IsVisible = false;
                    return;
                }

                siehisData.CSTATUS = "Q";
                siehisData.ModelStatus = DIMModelStatus.Modify;
                siehismodelList.Add(siehisData);

                //SIEREP 조회
                var sierepData = ((GetOffDetailViewModel)BindingContext).sierepData.FirstOrDefault(h => h.CONFNO == item.ConfNo);
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
                    new DIMWhereFieldCondition{ fieldName = "PIN" , value = this.LoginInfo.PhoneNumber, condition = DIMWhereCondition.Equal}
}
                };

                var empInfoList = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList",
                                        whereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                sierepData.REPTYP = "C08000C";
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
                    ReportType = "C08000C",
                    lockStatus = "L",
                    Longitude = location.Longitude,
                    Latitude = location.Latitude,
                    opeationMode = "ON",
                    connectTime = currentDate,
                    batteryVolt = 100,
                });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                return;
            }
        }

        var siehisResult = await dataService.Adapter.ModifyModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList", siehismodelList);
        var sierepResult = await dataService.Adapter.ModifyModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList", sierepmodelList);

        if (selectedItems.Count > 1)
        {
            await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리번호 [{selectData}] 외 {selectedItems.Count - 1}개를 하차하였습니다.", "확인");
        }
        else if (selectedItems.Count == 1)
        {
            await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리번호[{selectData}]를 하차하였습니다.", "확인");
        }
        loadingIndicator.IsRunning = false;
        loadingIndicator.IsVisible = false;
        await Application.Current.MainPage.Navigation.PushAsync(new HomePage(LoginInfo));
    }
}