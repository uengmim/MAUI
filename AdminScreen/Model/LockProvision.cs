using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace AdminScreen.Models
{
    public class LockProvision : INotifyPropertyChanged
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
        /// Lock SN
        /// </summary>
        public string LSN { get { return lsn; } set { lsn = value; OnPropertyChanged(nameof(LSN)); } }

        /// <summary>
        /// Lock 이름
        /// </summary>
        public string LKNM { get { return lknm; } set { lknm = value; OnPropertyChanged(nameof(LKNM)); } }

        /// <summary>
        /// 생성자
        /// </summary>
        public LockProvision(string lsn, string lknm)
        {
            LSN = lsn;
            LKNM = lknm;
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