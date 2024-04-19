using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Microsoft.Maui;

namespace WorkerScreen
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {

        private const int REQUEST_CODE = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);


                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    // NotificationChannel 클래스의 인스턴스를 생성.
                    // 첫 번째 인수는 채널 ID, 두 번째 인수는 채널 이름, 세 번째 인수는 중요도.
                    var channel = new NotificationChannel("7424", "My Background Service", NotificationImportance.Default)
                    {
                        Description = "Channel for My Background Service"
                    };

                    // 시스템의 NotificationManager 서비스를 가져옴.
                    var notificationManager = (NotificationManager)GetSystemService(NotificationService);

                    // 알림 채널을 시스템에 등록.
                    notificationManager.CreateNotificationChannel(channel);
                }

            }
            catch (Exception ex)
            {
                Log.Error("MyBackgroundService", ex.Message);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == REQUEST_CODE)
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                {
                    // 권한이 허용된 경우, Foreground Service 시작
                    //StartForegroundService(new Intent(this, typeof(MyBackgroundService)));
                }
                else
                {
                    // 권한이 거부된 경우, 사용자에게 필요한 권한에 대해 알려줍니다.
                }
            }
        }

    }
}