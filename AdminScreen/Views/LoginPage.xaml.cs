using AdminScreen.ViewModels;
using Acr.UserDialogs;
using ShreDoc.Utils;
using XNSC.DD.EX;
using ShreDoc.ProxyModel;

namespace AdminScreen.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            this.BindingContext = new LoginPageModel();
        }

        //로그인
        [Obsolete]
        public async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                if (IDID.Text == null || IDID.Text == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "ID를 입력해주세요.", "확인");
                    return;
                }
                else if (PWPW.Text == null || PWPW.Text == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "PW를 입력해주세요.", "확인");
                    return;
                }

                var dataService = ImateHelper.GetSingleTone();

                //var dataService = ImateHelper.GetSingleTone();
                //var Barcodenum = await dataService.Adapter.SelectModelDataAsync<AreamstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.AreamstModelList", new string[0],
                //     "", "", QueryCacheType.None);

                var LogInfo = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList", new string[0],
                      "", "", QueryCacheType.None);
                int foundIndex = -1;

                for (int i = 0; i < LogInfo.Count; i++)
                {
                    if (IDID.Text == LogInfo[i].EMPNO)
                    {
                        if (PWPW.Text == LogInfo[i].PIN)
                        {
                            await Shell.Current.GoToAsync("//App/");
                            this.Unfocus();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("알림", "비밀번호가 정확하지 않습니다.", "확인");
                            return;
                        }
                        foundIndex = i;
                        break;
                    }
                }
                if (foundIndex == -1)
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "일치하는 ID가 없습니다.", "확인");
                    return;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                return;
            }
        }
    }
}