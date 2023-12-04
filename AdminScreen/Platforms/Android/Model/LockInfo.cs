using Com.Ttlock.BL.Sdk.Api;

namespace AdminScreen.Models
{
    /// <summary>
    /// LOCK ITEM
    /// </summary>
    public partial class LockInfo
    {
        /// <summary>
        /// Device 정보
        /// </summary>
        public ExtendedBluetoothDevice Device { get; private set; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="device">TTlock Device 정보</param>
        public LockInfo(ExtendedBluetoothDevice device)
        {
            Device = device;
            
            LockName = Device.Name;
            LockMac = Device.Address;
        }
    }
}