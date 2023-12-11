using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;
using Com.Ttlock.BL.Sdk.Entity;

namespace AdminScreen
{
    /// <summary>
    /// Lock Scan Callback
    /// </summary>
    public partial class RegistSearchScanCallback : Java.Lang.Object, IScanLockCallback
    {
        public void OnFail(LockError p0)
        {
            Console.WriteLine($"{p0.Lockmac}, {p0.Description}");
        }

        public void OnScanLockSuccess(ExtendedBluetoothDevice p0)
        {
            //p0.Device
            if (!ViewModel.LockDataModel.Any(l => l.LockMac == p0.Address))
                ViewModel.LockDataModel.Add(new Models.LockInfo(p0));

            Console.WriteLine(p0.Address);
        }
    }
}
