using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace AdminScreen.Models
{
    public class LockInfom : INotifyPropertyChanged
    {

        private string lockdata;
        private string lockname;

        /// <summary>
        /// 로그인 아이디
        /// </summary>
        public string LockData { get { return lockdata; } set { lockdata = value; OnPropertyChanged(nameof(LockData)); } }
        public string LockName { get { return lockname; } set { lockname = value; OnPropertyChanged(nameof(LockName)); } }


        public LockInfom(string lockdata, string lockname)
        {
            LockData = lockdata;
            LockName = lockname;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }

    }
}