using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;
using Com.Ttlock.BL.Sdk.Entity;
using XNSC.Net.NOKE;

namespace WorkerScreenCrushing
{
    /// <summary>
    /// UnLock Callback
    /// </summary>
    public partial class RegistControlLockCallback : Java.Lang.Object, IControlLockCallback
    {
        public void OnFail(LockError p0)
        {
            ParentVewModel.UnlockFail($"{p0.Lockmac}, {p0.Description}");
        }

        public void OnControlLockSuccess(ControlLockResult p0)
        {
            ParentVewModel.UnlockSuccess(p0.Battery, sierepData, siehisData, lkMstData);
        }
    }
}
