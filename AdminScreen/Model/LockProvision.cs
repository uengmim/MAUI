using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace AdminScreen.Models
{
    public class LockProvision : INotifyPropertyChanged
    {

        private string lsn;
        private string lknm;

        /// <summary>
        /// 로그인 아이디
        /// </summary>
        public string LSN { get { return lsn; } set { lsn = value; OnPropertyChanged(nameof(LSN)); } }
        public string LKNM { get { return lknm; } set { lknm = value; OnPropertyChanged(nameof(LKNM)); } }


        public LockProvision(string lsn, string lknm)
        {
            LSN = lsn;
            LKNM = lknm;
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