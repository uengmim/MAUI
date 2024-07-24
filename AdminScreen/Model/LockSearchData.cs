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
        /// <summary>
        /// Lock SN
        /// </summary>
        private string lsn;

        /// <summary>
        /// Lock 이름
        /// </summary>
        private string lknm;

        /// <summary>
        /// Mac
        /// </summary>
        private string mac;

        private string confno;

        private string name;

        /// <summary>
        /// Lock SN
        /// </summary>
        public string LSN { get { return lsn; } set { lsn = value; OnPropertyChanged(nameof(LSN)); } }

        /// <summary>
        /// Lock 이름
        /// </summary>
        public string LKNM { get { return lknm; } set { lknm = value; OnPropertyChanged(nameof(LKNM)); } }

        /// <summary>
        /// Mac
        /// </summary>
        public string MAC { get { return mac; } set { mac = value; OnPropertyChanged(nameof(MAC)); } }

        public string CONFNO { get { return confno; } set { confno = value; OnPropertyChanged(nameof(CONFNO)); } }

        public string Name { get { return name; } set { name = value; OnPropertyChanged(nameof(Name)); } }

        /// <summary>
        /// 생성자
        /// </summary>
        public LockSearchData(string lsn, string lknm, string mac, string confno, string name)
        {
            LSN = lsn;
            LKNM = lknm;
            MAC = mac;
            CONFNO = confno;
            Name = name;
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