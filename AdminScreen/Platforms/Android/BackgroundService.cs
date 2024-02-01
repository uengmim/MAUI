using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.InputMethods;
using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;

namespace AdminScreen
{
    [Service]
    internal class BackgroundService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
           return null;
        }
    }
}
