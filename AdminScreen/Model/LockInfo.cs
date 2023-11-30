using System.ComponentModel;

namespace AdminScreen.Models
{
    /// <summary>
    /// LOCK ITEM
    /// </summary>
    public partial class LockInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// Lock 이름
        /// </summary>
        private string lockName;

        /// <summary>
        /// Lock Mac 어드레스
        /// </summary>
        private string lockMac;

        /// <summary>
        /// lock 이름
        /// </summary>
        public string LockName { get { return lockName; } set { lockName = value; OnPropertyChanged(nameof(LockName)); } }

        /// <summary>
        /// lock MacAddress
        /// </summary>
        public string LockMac { get { return lockMac; } set { lockMac = value; OnPropertyChanged(nameof(lockMac)); } }

        /// <summary>
        /// 생성자
        /// </summary>
        public LockInfo()
        {
            LockName = string.Empty;
            LockMac =string.Empty;
        }

        /// <summary>
        /// 특성 변경 이벤드
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName">프로퍼티 이름</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="args">특성변경 인수</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }

    }
}