

using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using XNSC.DD.EX;

namespace ClientScreen.Views
{
    public partial class ClientLoginPage : ContentPage
    {
        public ClientLoginPage()
        {
            InitializeComponent();

        }

        //로그인 버튼
        //[Obsolete]
        //public async void OnClientLoginClicked(object sender, EventArgs e)
        //{
        //    //await Navigation.PushAsync(new ClientMainPage());
        //    try
        //    {
        //        if (CliID.Text == null || CliID.Text == "")
        //        {
        //            await Application.Current.MainPage.DisplayAlert("알림", "ID를 입력해주세요.", "확인");
        //            return;
        //        }
        //        else if (CliPW.Text == null || CliPW.Text == "")
        //        {
        //            await Application.Current.MainPage.DisplayAlert("알림", "암호를 입력해주세요.", "확인");
        //            return;
        //        }

        //        var dataService = ImateHelper.GetSingleTone();

        //        //var dataService = ImateHelper.GetSingleTone();
        //        //var Barcodenum = await dataService.Adapter.SelectModelDataAsync<AreamstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.AreamstModelList", new string[0],
        //        //     "", "", QueryCacheType.None);

        //        var CliLogInfo = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList", new string[0],
        //              "", "", QueryCacheType.None);

        //        int foundIndex = -1;

        //        for (int i = 0; i < CliLogInfo.Count; i++)
        //        {
        //            if (CliID.Text == CliLogInfo[i].DEPTID)
        //            {
        //                if (CliPW.Text == CliLogInfo[i].PIN)
        //                {
        //                    await Navigation.PushAsync(new ClientMainPage());
        //                }
        //                else
        //                {
        //                    await Application.Current.MainPage.DisplayAlert("알림", "암호가 정확하지 않습니다.", "확인");
        //                    return;
        //                }
        //                foundIndex = i;
        //                break;
        //            }
        //        }
        //        if (foundIndex == -1)
        //        {
        //            await Application.Current.MainPage.DisplayAlert("알림", "일치하는 ID가 없습니다.", "확인");
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
        //        return;
        //    }
        //}

        //회원등록 버튼
        public async void OnMembershipClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientMembershipPage());
        }
    }
}