using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;
using Com.Ttlock.BL.Sdk.Entity;

namespace WorkerScreen
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
            if (!ViewModel.LockInfoModel.Any(l => l.Device.Address == p0.Address))
            {
                var lockInfo = new Models.LockInfomation(p0);
                lockInfo.SetLockInfo();

                ViewModel.LockInfoModel.Add(lockInfo);
            }

            Console.WriteLine(p0.Address);
        }
    }
}
