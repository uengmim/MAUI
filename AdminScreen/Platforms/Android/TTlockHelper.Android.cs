using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;

namespace AdminScreen
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

        /// <summary>
        /// TTLock을 초기화 한다.
        /// </summary>
        /// <param name="device">Device 정보</param>
        /// <param name="initLockCallback">Lock초기화</param>
        public static void InitLock(ExtendedBluetoothDevice device, IInitLockCallback initLockCallback)
        {
            TTLockClient.Default.InitLock(device, initLockCallback);
        }

        /// <summary>
        /// Reset Lock
        /// </summary>
        /// <param name="macAddress">맥주소</param>
        /// <param name="lockData">lock Data</param>
        /// <param name="resetCallBack">Lcok Reset</param>
        public static void ResetLock(string lockData, string macAddress, IResetLockCallback resetCallBack)
        {
            TTLockClient.Default.ResetLock(lockData, macAddress, resetCallBack);
        }
    }
}
