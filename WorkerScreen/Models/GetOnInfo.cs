using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace WorkerScreen.Models
{
    public class GetOnInfo : INotifyPropertyChanged
    {

        private string name;
        private string boxname;
        private string location;
        private string lockdata;
        private string lockname;
        private string confno;

        /// <summary>
        /// 로그인 아이디
        /// </summary>
        public string Name { get { return name; } set { name = value; OnPropertyChanged(nameof(Name)); } }
        public string BoxName { get { return boxname; } set { boxname = value; OnPropertyChanged(nameof(BoxName)); } }
        public string Location { get { return location; } set { location = value; OnPropertyChanged(nameof(Location)); } }
        public string LockData { get { return lockdata; } set { lockdata = value; OnPropertyChanged(nameof(LockData)); } }
        public string LockName { get { return lockname; } set { lockname = value; OnPropertyChanged(nameof(LockName)); } }
        public string ConfNo { get { return confno; } set { confno = value; OnPropertyChanged(nameof(ConfNo)); } }


        public GetOnInfo(string name ,string boxname, string location, string lockdata, string lockname, string confno)
        {
            Name = name;
            BoxName = boxname;
            Location = location;
            LockData = lockdata;
            LockName = lockname;
            ConfNo = confno;
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