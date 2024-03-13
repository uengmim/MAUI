using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;
using Com.Ttlock.BL.Sdk.Entity;
using XNSC.Net.NOKE;

namespace WorkerScreen
{
    /// <summary>
    /// UnLock Callback
    /// </summary>
    public partial class RegistControlLockCallback : Java.Lang.Object
    {
        public void OnFail(LockError p0)
        {
            Console.WriteLine($"{p0.Lockmac}, {p0.Description}");
        }

        public void onControlLockSuccess(ControlLockResult p0)
        {
            Console.WriteLine(p0);


        }
    }
}
