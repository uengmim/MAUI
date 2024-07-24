using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace AdminScreen.Models
{
    public class LockInfoData : INotifyPropertyChanged
    {
        /// <summary>
        /// Lock SN
        /// </summary>
        private string lsn;

        /// <summary>
        /// Mac
        /// </summary>
        private string mac;

        /// <summary>
        /// Lock 유형
        /// </summary>
        private string lktyp;

        /// <summary>
        /// Lock 이름
        /// </summary>
        private string lknm;

        /// <summary>
        /// Confno
        /// </summary>
        private string confno;

        /// <summary>
        /// image
        /// </summary>
        private string myimage;

        /// <summary>
        /// Lock SN
        /// </summary>
        public string LSN { get { return lsn; } set { lsn = value; OnPropertyChanged(nameof(LSN)); } }

        /// <summary>
        /// Mac
        /// </summary>
        public string MAC { get { return mac; } set { mac = value; OnPropertyChanged(nameof(MAC)); } }

        /// <summary>
        /// Lock 유형
        /// </summary>
        public string LKTYP { get { return lktyp; } set { lktyp = value; OnPropertyChanged(nameof(LKTYP)); } }

        /// <summary>
        /// Lock 이름
        /// </summary>
        public string LKNM { get { return lknm; } set { lknm = value; OnPropertyChanged(nameof(LKNM)); } }

        /// <summary>
        /// Confno
        /// </summary>
        public string CONFNO { get { return confno; } set { confno = value; OnPropertyChanged(nameof(CONFNO)); } }

        /// <summary>
        /// 이미지 변경
        /// </summary>
        public string MyImage { get { return myimage; } set { myimage = value; OnPropertyChanged(nameof(MyImage)); } }

        /// <summary>
        /// 생성자
        /// </summary>
        public LockInfoData(string lsn, string mac, string lktyp, string lknm, string confno, string myimage)
        {
            LSN = lsn;
            MAC = mac;
            LKTYP = lktyp;
            LKNM = lknm;
            CONFNO = confno;
            MyImage = myimage;
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