using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;
using Com.Ttlock.BL.Sdk.Constant;

namespace WorkerScreenCrushing
{
    /// <summary>
    /// Ttlock Android Helper Class
    /// </summary>
    internal static partial class TTlockHelper
    {
        /// <summary>
        /// 블루투스 연결을 초기화 한다.
        /// </summary>
        public static void InitBLE()
        {
            TTLockClient.Default.PrepareBTService(Platform.AppContext);
            var isBtEnable = TTLockClient.Default.IsBLEEnabled(Platform.AppContext);
            if (!isBtEnable)
            {
                TTLockClient.Default.RequestBleEnable(Platform.CurrentActivity);
            }
        }

        /// <summary>
        /// 블루투스를 통해 TTLOCK을 스캔 한다.
        /// </summary>
        /// <param name="scanLockCallback"></param>
        public static void ScanLock(IScanLockCallback scanLockCallback)
        {
            TTLockClient.Default.StartScanLock(scanLockCallback);
        }
        public static void ScanStopLock()
        {
            TTLockClient.Default.StopScanLock();
        }
        /// <summary>
        /// TTLOCK을 UnLock 한다.
        /// </summary>
        /// <param name="controlLockCallback"></param>
        public static void DoUnlock(string LockData, string LockMac, IControlLockCallback controlLockCallback)
        {
            TTLockClient.Default.ControlLock(ControlAction.Unlock, LockData, LockMac, controlLockCallback);
        }

        /// <summary>
        /// TTLOCK을 Lock 한다.
        /// </summary>
        /// <param name="controlLockCallback"></param>
        public static void DoLock(string LockData, string LockMac, IControlLockCallback controlLockCallback)
        {
            TTLockClient.Default.ControlLock(ControlAction.Lock, LockData, LockMac, controlLockCallback);
        }
    }
}
