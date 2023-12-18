using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace AdminScreen.Models
{
    public class LockSearchData : INotifyPropertyChanged
    {

        private string lsn;
        private string lknm;
        private string mac;



        public string LSN { get { return lsn; } set { lsn = value; OnPropertyChanged(nameof(LSN)); } }
        public string LKNM { get { return lknm; } set { lknm = value; OnPropertyChanged(nameof(LKNM)); } }
        public string MAC { get { return mac; } set { mac = value; OnPropertyChanged(nameof(MAC)); } }



        public LockSearchData(string lsn, string lknm, string mac)
        {
            LSN = lsn;
            LKNM = lknm;
            MAC = mac;
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