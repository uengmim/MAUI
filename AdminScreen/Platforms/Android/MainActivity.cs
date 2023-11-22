using AdminScreen.Interface;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;

namespace AdminScreen
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        IScanLockCallback mScanLockCallback;
        
        protected override void OnStart()
        {
            base.OnStart();
            if (TTLockClient.Default.IsBLEEnabled(this))
            {                
                TTLockClient.Default.PrepareBTService(this);
                mScanLockCallback = new ScanLockCallback();
                TTLockClient.Default.StartScanLock(mScanLockCallback);
            }
            else
                TTLockClient.Default.RequestBleEnable(this);
        }
    }
}