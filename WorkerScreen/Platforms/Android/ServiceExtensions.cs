using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Timers;
using XNSC.DD.EX;

namespace WorkerScreen
{
    public static class ServiceExtensions
    {
        public static void StartForegroundServiceCompat<T>(this Context context, Bundle args = null) where T : Service
        {
            var intent = new Intent(context, typeof(T));
            if (args != null)
            {
                intent.PutExtras(args);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                context.StartForegroundService(intent);
            }
            else
            {
                context.StartService(intent);
            }
        }
    }
}
