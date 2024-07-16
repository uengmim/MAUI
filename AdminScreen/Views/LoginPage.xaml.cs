using AdminScreen.ViewModels;
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
        }

        /// <summary>
        /// 블루투스 및 위치 권한 설정
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // 권한 체크 및 요청
            await CheckAndRequestPermissionsAsync();
        }
        /// <summary>
        /// 권한 체크 및 권한 요구
        /// </summary>
        /// <returns></returns>
        private async Task CheckAndRequestPermissionsAsync()
        {
            //알림 권한을 담기 위한 배열
            var dataArray = new List<string>();
            //각 권한과 해당 권한이 거부된 경우의 메시지를 매핑.
            var permissions = new List<(Permissions.BasePermission Permission, string Message)>
            {
                (new Permissions.Bluetooth(), "블루투스"),
                (new Permissions.LocationWhenInUse(), "위치")
            };
            //foreach 루프를 통해 각 권한을 확인하고, 권한이 거부된 경우 권한을 요청
            foreach (var (permission, message) in permissions)
            {
                var status = await permission.CheckStatusAsync();
                if (status != PermissionStatus.Granted)
                {
                    status = await permission.RequestAsync();
                    if (status != PermissionStatus.Granted)
                    {
                        //배열에 거부된 권한 추가
                        dataArray.Add($"{message}");
                    }
                }
            }
            //알림 문구
            var alertMessage = "";
            //거부된 권한 리스트가 들어있는 배열을 foreach 돌려
            foreach (var data in dataArray)
            {
                if (alertMessage.Length > 0)
                {
                    alertMessage += ", ";
                }
                //공백 제거
                alertMessage += data.Trim();
            }
            //alertMessage가 빈값이 아니라면
            if (!string.IsNullOrEmpty(alertMessage))
            {
                alertMessage += "의 권한 설정이 필요합니다.";
                await Application.Current.MainPage.DisplayAlert("알림", alertMessage, "설정");
                AppInfo.ShowSettingsUI();
            }
        }
        private void Login_Clicked(object sender, EventArgs e)
        {
            KeyboardHelper.HideKeyboard();
        }
    }
}