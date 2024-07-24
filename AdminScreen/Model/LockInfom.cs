using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace AdminScreen.Models
{
    public class LockInfom : INotifyPropertyChanged
    {
        /// <summary>
        /// LockData
        /// </summary>
        private string lockdata;

        /// <summary>
        /// Lock 이름
        /// </summary>
        private string lockname;

        /// <summary>
        /// LockData
        /// </summary>
        public string LockData { get { return lockdata; } set { lockdata = value; OnPropertyChanged(nameof(LockData)); } }

        /// <summary>
        /// Lock 이름
        /// </summary>
        public string LockName { get { return lockname; } set { lockname = value; OnPropertyChanged(nameof(LockName)); } }

        /// <summary>
        /// 생성자
        /// </summary>
        public LockInfom(string lockdata, string lockname)
        {
            LockData = lockdata;
            LockName = lockname;
        }

        /// <summary>
        /// 특성 변경 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }

    }
}